﻿namespace BirthdayCelebrations.Models
{
    using Contracts;

    public class Robot : IIdentifiable
    {
        public Robot(string model, string id)
        {
            this.Model = model;
            this.Id = id;
        }

        public string Model { get; private set; }

        public string Id { get; private set; }

        public bool CheckId(string fakeIdsLastDigits) => this.Id.EndsWith(fakeIdsLastDigits);
    }
}
