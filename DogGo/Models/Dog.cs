﻿namespace DogGo.Models
{
    public class Dog
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Breed { get; set; }
        public string? Notes { get; set; }  = null;
        public string? ImageUrl { get; set; } = null;

    }
}
