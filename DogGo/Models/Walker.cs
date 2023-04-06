﻿using System.ComponentModel;

namespace DogGo.Models
{
    public class Walker
    {
        public int Id { get; set; }
        public string Name { get; set; }
        [DisplayName("Neighborhood Name")]
        public string NeighborhoodName { get; set; }
        [DisplayName("Profile Pic")]
        public string ImageUrl { get; set; }
        public Neighborhood Neighborhood { get; set; }
    }
}