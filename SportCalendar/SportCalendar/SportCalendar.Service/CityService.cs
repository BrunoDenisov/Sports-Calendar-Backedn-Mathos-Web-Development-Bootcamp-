using SportCalendar.Model;
using SportCalendar.RepositoryCommon;
using SportCalendar.ServiceCommon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportCalendar.Service
{
    public class CityService : ICityService
    {
        private ICityRepository CityRepository;

        public CityService(ICityRepository repository)
        {
            CityRepository = repository;
        }
        public async Task<List<City>> GetAll()
        {
            try
            {
                var result = await CityRepository.GetAll();
                return result;
            }
            catch (Exception ex)
            {
                return null;
            }
        }


    }
}
