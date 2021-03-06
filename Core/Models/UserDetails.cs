﻿using Core.Domain;
using System.Collections.Generic;

namespace Core.Models
{
    public class UserDetails
    {
        public int UserId { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string Company { get; set; }
        public List<Phone> Phones { get; set; }
    }
}
