using SportCalendar.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportCalendar.RepositoryCommon
{
    public interface ILocationRepository
    {
        Task<List<Location>> GetAllREST();
        Task<Location> GetById(Guid id);
        Task<Location> Create(Location location);
        Task<Location> Put(Location location, Guid id);
        Task<bool> Delete(Guid id);
    }
}
