using SportCalendar.RepositoryCommon;
using SportCalendar.ServiceCommon;
using System;

namespace SportCalendar.Service
{
    public class SportService:ISportService
    {
        private ISportRepository _sportRepository;
        public SportService(ISportRepository sportRepository)
        {
            _sportRepository = sportRepository;
        }
        public bool Get()
        {
            return _sportRepository.Get();
        }
    }
}
