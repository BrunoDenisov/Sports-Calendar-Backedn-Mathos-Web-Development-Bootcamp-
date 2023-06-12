using SportCalendar.Model;
using SportCalendar.ModelCommon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportCalendar.ServiceCommon
{
    public interface ICountyService
    {
        Task<List<County>> GetAll();
        Task<List<County>> GetById(Guid id);
        Task<County> Post(County county);

        Task<County> Put(Guid id, County county);
        Task<bool> Delete(Guid id);

    }
}
