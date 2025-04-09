using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Travels.Application.Dtos.Review;
using Travels.Domain.Entities;

namespace Travels.Application.Interfaces
{
    public interface IReviewService
    {
        Task AddReview(ReviewDto reviewDto,int? userId);
        Task<IEnumerable<ReviewDto>> GetReviews();
        Task ChangeReview(ReviewDto reviewDto);
        Task DeleteReview(ReviewDto reviewDto);
    }
}
