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
                return Request.CreateResponse(HttpStatusCode.NotFound, "We could not find you'r Cities");
            }
            return Request.CreateResponse(HttpStatusCode.OK, result);
        }

        public async Task<HttpResponseMessage> GetById(Guid id)
        {
            List<City> result = await CityService.GetById(id);
            if (result == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, "We could not find you'r specific City");
            }
            return Request.CreateResponse(HttpStatusCode.OK, result);
        }

        public async Task<HttpResponseMessage> Post(City city)
        {
            var createdCounty = await CityService.Post(city);
            if (createdCounty == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, "We could not insert you'r City, check body!");
            }
            return Request.CreateResponse(HttpStatusCode.OK, createdCounty);
        }

        public async Task<HttpResponseMessage> Put(Guid id, City updatedCity)
        {
            var createdCounty = await CityService.Put(id, updatedCity);
            if (createdCounty == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, "We could not insert you'r new City, check body!");
            }
            return Request.CreateResponse(HttpStatusCode.OK, createdCounty);
        }
    }
}