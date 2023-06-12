using SportCalendar.ServiceCommon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace SportCalendar.WebApi.Controllers
{
    public class SportController : ApiController
    {
        private ISportService _sportService;

        public SportController(ISportService sportService)
        {
            _sportService = sportService;
        }

        [HttpGet]
        public bool Get()
        {
            return _sportService.Get();
        }
    }
}
