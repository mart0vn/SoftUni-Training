﻿namespace RemoveVillain
{
    using System;
    using Microsoft.Data.SqlClient;

    class Engine
    {
        public void Run()
        {
            var villainId = int.Parse(Console.ReadLine());

            using (var connection = new SqlConnection(Configuration.ConnectionString))
            {
                connection.Open();

                var villainName = GetVillainNameById(connection, villainId);

                if (villainName == null)
                {
                    Console.WriteLine("No such villain was found.");
                    Environment.Exit(0);
                }

                var releasedMinions = GetReleasedMinions(connection, villainId);
                DeleteVillainById(connection, villainId);

                Console.WriteLine($"{villainName} was deleted.");
                Console.WriteLine($"{releasedMinions} minions were released.");
            }
        }

        private static void DeleteVillainById(SqlConnection connection, int villainId)
        {
            var command = new SqlCommand(Queries.DeleteFromVillain, connection);
            command.Parameters.AddWithValue("@villainId", villainId);
            command.ExecuteNonQuery();
        }

        private static int GetReleasedMinions(SqlConnection connection, int villainId)
        {
            var command = new SqlCommand(Queries.DeleteFromMappingTable, connection);
            command.Parameters.AddWithValue("@villainId", villainId);

            return command.ExecuteNonQuery();
        }

        private static string GetVillainNameById(SqlConnection connection, int villainId)
        {
            var command = new SqlCommand(Queries.SelectVillainNameById, connection);
            command.Parameters.AddWithValue("@villainId", villainId);

            return command.ExecuteScalar()?.ToString();
        }
    }
}
