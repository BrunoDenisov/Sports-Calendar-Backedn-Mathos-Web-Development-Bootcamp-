using SportCalendar.Common;
using SportCalendar.Model;
using SportCalendar.ServiceCommon;
using SportCalendar.WebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace SportCalendar.WebApi.Controllers
{
    public class PlacementController : ApiController
    {
        private IPlacementService placementService;

        public PlacementController(IPlacementService service)
        {
            placementService = service;
        }

        [HttpGet]
        [Authorize(Roles = "Super_admin")]
        public async Task<HttpResponseMessage> PlacementGetFilteredAsync(string orderBy = "FinishOrder", string sortOrder = "DESC", int pageSize = 10, int pageNumber = 1, Guid? eventId = null)
        {
            try
            {
                Sorting sorting = new Sorting();
                sorting.SortOrder = sortOrder;
                sorting.OrderBy = orderBy;

                Paging paging = new Paging();
                paging.PageNumber = pageNumber;
                paging.PageSize = pageSize;

                PlacementFilter filter = new PlacementFilter();
                filter.EventId = eventId;

                PagedList<Placement> pagedList = await placementService.PlacementGetFilteredAsync(paging, sorting, filter);
                if (pagedList.Data.Any())
                {
                    List<PlacementRest> placementRests = MapPlacemensToRest(pagedList);
                    return Request.CreateResponse(HttpStatusCode.OK, placementRests);
                }
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        [HttpPost]
        [Authorize(Roles = "Super_admin")]
        public async Task<HttpResponseMessage> PlacementPostAsync([FromBody]Placement placement)
        {
            try
            {
                var resautl = await placementService.PlacementPostAsync(placement);
                if (resautl == true)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, "Placement row inserted");
                }
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Error! Placement row not inserted");
            }
            catch (Exception ex)
            {

                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
      
        }

        [HttpDelete]
        [Authorize(Roles = "Super_admin")]
        public async Task<HttpResponseMessage> PlacementDelteAsync(Guid id)
        {
            try
            {
                var resault = await placementService.PlacementDeleteAsync(id);
                if(resault == true)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, "Placement row deleted.");
                }
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "No row deleted");
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        [HttpPut]
        [Authorize(Roles = "Super_admin")]
        public async Task<HttpResponseMessage> PlacementPutAsync(Guid id, [FromBody]Placement placement)
        {
            try
            {
                var resault = await placementService.PlacementPutAsync(id, placement);
                if(resault == true)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, "Placement row updated");
                }
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "No row updated");
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        private PlacementRest MapToRest(Placement placement)
        {
            PlacementRest placementsRest = new PlacementRest();
            placementsRest.Id = placement.Id;
            placementsRest.Name = placement.Name;
            placementsRest.FinishOrder = placement.FinishOrder;
            placementsRest.EventId = placement.EventId;

            return placementsRest;
        }

        private List<PlacementRest>MapPlacemensToRest(PagedList<Placement> placements)
        {
            List<PlacementRest> placementsRest = new List<PlacementRest>();

            foreach (Placement placement in placements.Data)
            {
                placementsRest.Add(MapToRest(placement));
            }
            return placementsRest;
        }
    }
}
