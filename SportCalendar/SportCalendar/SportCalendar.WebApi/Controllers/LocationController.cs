using SportCalendar.Model;
using SportCalendar.Repository;
using SportCalendar.ServiceCommon;
using SportCalendar.WebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace SportCalendar.WebApi.Controllers
{
    public class LocationController : ApiController
    {
        private ILocationService LocationService;
        public LocationController(ILocationService service)
        {
            LocationService = service;

        }
        //Rest Works
        public async Task<HttpResponseMessage> GetAllREST()
        {
            try
            {
                List<Location> locations = await LocationService.GetAllREST();
                List<RESTLocation> restLocations = locations.Select(location => new RESTLocation
                {
                    Id = location.Id,
                    IsActive = location.IsActive,
                    Venue = location.Venue,
                    CountyName = location.CountyName,
                    CityName = location.CountyName
                }).ToList();

                if (restLocations.Count > 0)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, restLocations);
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.NotFound, "No locations found.");
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

    }
}