using SportCalendar.ModelCommon;
using SportCalendar.RepositoryCommon;
using SportCalendar.ServiceCommon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportCalendar.Service
{
    public class LocationService : ILocationService
    {
        private ILocationRepository LocationRepository;

        public LocationService(ILocationRepository repository)
        {
            LocationRepository = repository;
        }

        public async Task<List<Location>> GetAllREST()
        {
            try
            {
                var result = await LocationRepository.GetAllREST();
                return result;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

    }
}
