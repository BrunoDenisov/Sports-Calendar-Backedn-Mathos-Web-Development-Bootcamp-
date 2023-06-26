using SportCalendar.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportCalendar.RepositoryCommon
{
    public interface IEventSponsorRepository
    {
        Task<List<EventSponsor>> EventSponsorGetAsync();

        Task<bool> EventSponsorPostAsync(EventSponsor eventSponsor);

        Task<bool> EventSponsorDeleteAsync(Guid id, EventSponsor eventSponsor);

        Task<bool> EventSponsorPutAsync(Guid id, EventSponsor eventSponsor);
    }
}
