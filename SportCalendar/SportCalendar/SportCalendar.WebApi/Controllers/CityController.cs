using SportCalendar.Model;
using SportCalendar.Service;
using SportCalendar.ServiceCommon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace SportCalendar.WebApi.Controllers
{
    public class CityController : ApiController
    {
        private ICityService CityService;
        public CityController(ICityService service)
        {
            CityService = service;
            
        }

        public async Task<HttpResponseMessage> GetAll()
        {
            List<City> result = await CityService.GetAll();
            if (result == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, "We could not find County");
            }
            return Request.CreateResponse(HttpStatusCode.OK, result);
        }
    }
}