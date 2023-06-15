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
    public class ReviewController : ApiController
    {
        private IReviewService _reviewService;

        public ReviewController(IReviewService reviewService)
        {
            _reviewService = reviewService;
        }

        [HttpGet]
        public async Task<HttpResponseMessage> Get(string orderBy = "Rating", string sortOrder = "DESC", int pageSize = 10, int pageNumber = 1, Guid? userId = null, Guid? eventId = null)
        {
            try
            {
                Sorting sorting = new Sorting();
                sorting.SortOrder = sortOrder;
                sorting.OrderBy = orderBy;

                Paging paging = new Paging();
                paging.PageNumber = pageNumber;
                paging.PageSize = pageSize;

                ReviewFilter filter = new ReviewFilter();
                filter.EventId = eventId;
                filter.UserId = userId;

                PagedList<Review> pagedList = await _reviewService.GetReviewsAsync(sorting, paging, filter);
                if (pagedList.Any())
                {
                    List<ReviewRest> reviewsRest = MapReviewsToRest(pagedList);
                    return Request.CreateResponse(HttpStatusCode.OK, reviewsRest);
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
                Review review = await _reviewService.GetReviewAsync(id);
                if (review == null)
                {
                    return Request.CreateResponse(HttpStatusCode.NotFound, "Requested review not found!");
                }
                return Request.CreateResponse(HttpStatusCode.OK, MapToRest(review));
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }

        [HttpPost]
        public async Task<HttpResponseMessage> PostAsync([FromBody] Review review)
        {
            try
            {
                bool postStatus = await _reviewService.PostReviewAsync(review);
                if (postStatus)
                {
                    return Request.CreateResponse(HttpStatusCode.Created, "Review created!");
                }
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Review creation failed!");
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }
        [HttpPut]
        public async Task<HttpResponseMessage> PutAsync(Guid id, [FromBody] Review review)
        {
            try
            {
                bool postStatus = await _reviewService.UpdateReviewAsync(id, review);
                if (postStatus)
                {
                    return Request.CreateResponse(HttpStatusCode.Created, "review updated!");
                }
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "review update failed!");
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
                bool deleteStatus = await _reviewService.DeleteReviewAsync(id);
                if (deleteStatus)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, "Review deleted");
                }
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }
        private ReviewRest MapToRest(Review review)
        {
            ReviewRest reviewRest = new ReviewRest();

            reviewRest.Id = review.Id;
            reviewRest.Rating = review.Rating;
            reviewRest.Content = review.Content;
            reviewRest.Attended = review.Attended;
            reviewRest.EventName = review.EventName;
            reviewRest.EventId = review.EventId;
            reviewRest.UserName = review.UserName;

            return reviewRest;
        }

        private List<ReviewRest> MapReviewsToRest(PagedList<Review> reviews)
        {
            List<ReviewRest> reviewsRest = new List<ReviewRest>();

            foreach (Review review in reviews)
            {
                reviewsRest.Add(MapToRest(review));
            }
            return reviewsRest;
        }
    }
}

