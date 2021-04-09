﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Capstone.Models
{
    public class Brewery
    {
        public int BreweryId { get; set; }
        public string BreweryName { get; set; }
        public int UserId { get; set; }
        public string History { get; set; } = "";
        public string StreetAddress { get; set; } = "";
        public string Phone { get; set; } = "";
        public string City { get; set; } = "";
        public string ZipCode { get; set; } = "";
        public bool IsActive { get; set; } = true;

    }

}
