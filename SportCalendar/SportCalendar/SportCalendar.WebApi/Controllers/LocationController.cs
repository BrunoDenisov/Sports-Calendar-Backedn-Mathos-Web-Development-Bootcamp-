using SportCalendar.ServiceCommon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace SportCalendar.WebApi.Controllers
{
    public class LocationController : ApiController
    {
        private ILocationService CityService;
        public LocationController(ILocationService service)
        {
            CityService = service;

        }
    }
}