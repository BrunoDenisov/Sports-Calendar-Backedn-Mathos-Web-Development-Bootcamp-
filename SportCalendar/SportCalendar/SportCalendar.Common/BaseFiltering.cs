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
        public string SearchQuery { get; private set; }
        public DateTime? FromDate { get; private set; }
        public DateTime? ToDate { get; private set; }
        public DateTime? FromTime { get; private set; }
        public DateTime? ToTime { get; private set; }
    }
}
