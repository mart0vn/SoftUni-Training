﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace SUS.HTTP
{
    public class HttpServer : IHttpServer
    {
        readonly List<Route> routeTable;

        public HttpServer(List<Route> routeTable)
        {
            this.routeTable = routeTable;
        }

        public async Task StartAsync(int port)
        {
            TcpListener tcpListener = new TcpListener(IPAddress.Loopback, port);
            tcpListener.Start();

            while (true)
            {
                TcpClient tcpClient = await tcpListener.AcceptTcpClientAsync();
                await ProcessClientAsync(tcpClient);
            }
        }

        private async Task ProcessClientAsync(TcpClient tcpClient)
        {
            try
            {
                using NetworkStream stream = tcpClient.GetStream();

                var data = new List<byte>();
                var buffer = new byte[HttpConstants.BufferSize];
                var position = 0;

                while (true)
                {
                    int count = await stream.ReadAsync(buffer, position, buffer.Length);
                    position += count;

                    if (count < buffer.Length)
                    {
                        var partialBuffer = new byte[count];
                        Array.Copy(buffer, partialBuffer, count);
                        data.AddRange(partialBuffer);
                        break;
                    }
                    else
                    {
                        data.AddRange(buffer);
                    }
                }

                var requestAsString = Encoding.UTF8.GetString(data.ToArray());
                var request = new HttpRequest(requestAsString);
                Console.WriteLine($"{request.Method} {request.Path} => {request.Headers.Count}");

                HttpResponse response;

                var route = this.routeTable.FirstOrDefault(
                    x => string.Compare(x.Path, request.Path, true) == 0
                    && x.Method == request.Method);

                if (route != null)
                {
                    response = route.Action(request);
                }
                else
                {
                    response = new HttpResponse("text/html", new byte[0], HttpStatusCode.NotFound);
                }

                response.Headers.Add(new Header("Server", "SUS Server 1.0"));

                var sessionCookie = request.Cookies.FirstOrDefault(x => x.Name == HttpConstants.SessionsCookieName);
                if (sessionCookie != null)
                {
                    var responseSessionCookie = new ResponseCookie(sessionCookie.Name, sessionCookie.Value);
                    responseSessionCookie.Path ="/";
                    response.Cookies.Add(responseSessionCookie);
                }

                var responseHeaderBytes = Encoding.UTF8.GetBytes(response.ToString());                
                await stream.WriteAsync(responseHeaderBytes, 0, responseHeaderBytes.Length);

                if (response.Body != null)
                {
                    await stream.WriteAsync(response.Body, 0, response.Body.Length);
                }

                tcpClient.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
