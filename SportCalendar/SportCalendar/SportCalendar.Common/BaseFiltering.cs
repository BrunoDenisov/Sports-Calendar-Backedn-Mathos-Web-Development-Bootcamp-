using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportCalendar.Common
{
    public class BaseFiltering
    {
        public BaseFiltering(string searchQuery, DateTime? fromDate, DateTime? toDate, DateTime? fromTime, DateTime? toTime)
        {
            SearchQuery = searchQuery;
            FromDate = fromDate;
            ToDate = toDate;
            FromTime = fromTime;
            ToTime = toTime;
        }

        public string SearchQuery { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
        public DateTime? FromTime { get; set; }
        public DateTime? ToTime { get; set; }

    }
    public class SportFilter : BaseFiltering
    {
        public SportFilter(string searchQuery, string type, DateTime? fromDate, DateTime? toDate, DateTime? fromTime, DateTime? toTime) : base(searchQuery, fromDate, toDate, fromTime, toTime)
        {
            this.Type = type;
        }
        public string Type { get; set; }
    }
    public class ReviewFilter
    {
        public Guid? UserId { get; set; }
        public Guid? EventId { get; set; }
    }

    public class PlacementFilter
    {
        public Guid? EventId { get; set; }    
    }
    
    public class UserFiltering : BaseFiltering
    {
        public UserFiltering(string searchQuery, DateTime? fromDate, DateTime? toDate, DateTime? fromTime, DateTime? toTime, DateTime? fromDateUpdate, DateTime? toDateUpdate) : base(searchQuery, fromDate, toDate, fromTime, toTime)
        {

            FromDateUpdate = fromDateUpdate;
            ToDateUpdate = toDateUpdate;
        }

        public DateTime? FromDateUpdate { get; set; }
        public DateTime? ToDateUpdate { get; set; }
    }
}
