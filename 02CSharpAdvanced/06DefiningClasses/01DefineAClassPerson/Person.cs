﻿namespace DefiningClasses
{
    public class Person
    {
        private string name;
        private int age;

        public string Name { get => name; set => this.name = value; }
        public int Age { get => age; set => this.age = value; }
    }
}
