﻿using SportCalendar.Common;
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
    public class SportController : ApiController
    {
        private ISportService _sportService;

        public SportController(ISportService sportService)
        {
            _sportService = sportService;
        }

        [HttpGet]
        public async Task<HttpResponseMessage> Get(string orderBy = "Name", string sortOrder = "DESC", int pageSize = 10, int pageNumber = 1, string searchQuery = null, string type = null,DateTime? fromDate=null,DateTime? toDate = null,DateTime? fromTime=null,DateTime? toTime = null)
        {
            try
            {
                Sorting sorting = new Sorting();
                sorting.SortOrder = sortOrder;
                sorting.OrderBy = orderBy;

                Paging paging = new Paging();
                paging.PageNumber = pageNumber;
                paging.PageSize = pageSize;

                SportFilter sportFilter = new SportFilter(searchQuery,type,fromDate,toDate,fromTime,toTime);

                PagedList<Sport> pagedList = await _sportService.GetSportsAsync(sorting, paging, sportFilter);
                if (pagedList.Any())
                {
                    List<SportRest> sportsRest = MapSportsToRest(pagedList);
                    return Request.CreateResponse(HttpStatusCode.OK, sportsRest);
                }
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }
        [HttpGet]
        public async Task<HttpResponseMessage> GetAsync(Guid id)
        {
            try
            {
                Sport sport = await _sportService.GetSportAsync(id);
                if(sport == null)
                {
                    return Request.CreateResponse(HttpStatusCode.NotFound, "Requested sport not found!");
                }
                return Request.CreateResponse(HttpStatusCode.OK, MapToRest(sport));
            }catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }
        [HttpPost]
        public async Task<HttpResponseMessage> PostAsync([FromBody]Sport sport)
        {
            try
            {
                bool postStatus = await _sportService.PostSportAsync(sport);
                if (postStatus)
                {
                    return Request.CreateResponse(HttpStatusCode.Created, "Sport created!");
                }
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Sport creation failed!");
            }catch(Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }
        [HttpPut]
        public async Task<HttpResponseMessage> PutAsync(Guid id, [FromBody]Sport sport)
        {
            try
            {
                bool postStatus = await _sportService.UpdateSportAsync(id,sport);
                if (postStatus)
                {
                    return Request.CreateResponse(HttpStatusCode.Created, "Sport updated!");
                }
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Sport update failed!");
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }
        [HttpDelete]
        public async Task<HttpResponseMessage> DeleteAsync(Guid id)
        {
            try
            {
                bool deleteStatus = await _sportService.DeleteSportAsync(id);
                if (deleteStatus)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, "Sport deleted");
                }
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }

        private SportRest MapToRest(Sport sport)
        {
            SportRest sportRest = new SportRest();

            sportRest.Id = sport.Id;
            sportRest.Name = sport.Name;
            sportRest.Type = sport.Type;

            return sportRest;
        }

        private List<SportRest> MapSportsToRest(PagedList<Sport> sports)
        {
            List<SportRest> sportsRest = new List<SportRest>();

            foreach (Sport sport in sports)
            {
                sportsRest.Add(MapToRest(sport));
            }
            return sportsRest;
        }
    }
}
