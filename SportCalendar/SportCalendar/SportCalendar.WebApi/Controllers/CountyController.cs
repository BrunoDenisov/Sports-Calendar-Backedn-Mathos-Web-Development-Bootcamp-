using SportCalendar.ServiceCommon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using SportCalendar.Model;

namespace SportCalendar.WebApi.Controllers
{
    [RoutePrefix("api/County")]
    public class CountyController : ApiController
    {
        private ICountyService CountyService;
        public CountyController(ICountyService service)
        {
            CountyService = service;
        }
        [HttpGet]
        [Route("")]
        public async Task<HttpResponseMessage> GetAll()
        {
            List<County> result = await CountyService.GetAll();
            if (result == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, "We could not find County");
            }
            return Request.CreateResponse(HttpStatusCode.OK, result);
        }

        public async Task<HttpResponseMessage> GetById(Guid id)
        {
            List<County> result = await CountyService.GetById(id);
            if (result == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, "We could not find County by Id");
            }
            return Request.CreateResponse(HttpStatusCode.OK, result);
        }

        public async Task<HttpResponseMessage> Post(County county)
        {
            var createdCounty = await CountyService.Post(county);
            if (createdCounty == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, "We could not insert you'r County, check body!");
            }
            return Request.CreateResponse(HttpStatusCode.OK, createdCounty);
        }

        public async Task<HttpResponseMessage> Put(Guid id, County county)
        {
            var createdCounty = await CountyService.Put(id, county);
            if (createdCounty == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, "We could not update you'r County, check body!");
            }
            return Request.CreateResponse(HttpStatusCode.OK, createdCounty);
        }

        public async Task<HttpResponseMessage> Delete(Guid id)
        {
            var createdCounty = await CountyService.Delete(id);
            if (createdCounty == false)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, "We could not Delete you'r County");
            }
            return Request.CreateResponse(HttpStatusCode.OK, createdCounty);
        }

    }
}