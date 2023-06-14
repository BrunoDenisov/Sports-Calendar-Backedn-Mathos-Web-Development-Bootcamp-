﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportCalendar.Model
{
    public class Location
    {
        public Guid Id { get; set; }
        public string Venue { get; set; }
        public bool IsActive { get; set; }
        public Guid UpdatedByUserId { get; set; }
        public Guid CreatedByUserId { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateUpdated { get; set; }
        public Guid CountyId { get; set; }
        public Guid CityId { get; set; }
    }
}
