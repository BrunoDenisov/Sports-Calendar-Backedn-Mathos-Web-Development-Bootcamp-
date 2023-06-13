using SportCalendar.Common;
using SportCalendar.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportCalendar.ServiceCommon
{
    public interface IReviewService
    {
        Task<PagedList<Review>> GetReviewsAsync(Sorting sorting, Paging paging, ReviewFilter filtering);
        Task<Review> GetReviewAsync(Guid id);
        Task<bool> DeleteReviewAsync(Guid id);
        Task<bool> PostReviewAsync(Review review);
        Task<bool> UpdateReviewAsync(Guid id, Review review);
    }
}
