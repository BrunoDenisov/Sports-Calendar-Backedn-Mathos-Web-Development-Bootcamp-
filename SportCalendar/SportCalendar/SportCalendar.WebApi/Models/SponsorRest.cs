using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SportCalendar.WebApi.Models
{
    public class SponsorRest
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Website { get; set; }

        public bool IsActive { get; set; }

        public Guid CreatedByUserId { get; set; }

        public Guid UpdatedByUserId { get; set; }

        public DateTime DateCreated { get; set; }

        public DateTime DateUpdated { get; set; }
    }
}