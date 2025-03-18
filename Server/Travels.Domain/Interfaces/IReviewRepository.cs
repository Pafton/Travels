using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Travels.Domain.Entities;

namespace Travels.Domain.Interfaces
{
    public interface IReviewRepository
    {
        Task AddReview(Review review);
        Task<IEnumerable<Review>> GetReviews();
        Task<Review?> GetReview(int? id);
        Task ChangeReview(Review review);
        Task DeleteReview(int id);
    }
}
