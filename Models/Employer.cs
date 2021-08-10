﻿using System;
using System.ComponentModel.DataAnnotations;

namespace TechJobsPersistent.Models
{
    public class Employer
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Name is required.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Location is required.")]
        public string Location { get; set; }

        public Employer()
        {
        }

        public Employer(string name, string location)
        {
            Name = name;
            Location = location;
        }
    }
}
