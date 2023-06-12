using SportCalendar.Common;
using SportCalendar.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportCalendar.ServiceCommon
{
    public interface ISportService
    {
        Task<PagedList<Sport>> GetSportsAsync(Sorting sorting, Paging paging, SportFilter filtering);
        Task<Sport> GetSportAsync(Guid id);
        Task<bool> DeleteSportAsync(Guid id);
        Task<bool> PostSportAsync(Sport sport);
        Task<bool> UpdateSportAsync(Guid id, Sport sport);
    }
}
