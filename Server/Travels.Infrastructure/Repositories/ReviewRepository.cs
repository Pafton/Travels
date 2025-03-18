using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Travels.Domain.Entities;
using Travels.Domain.Interfaces;
using Travels.Infrastructure.Presistance;

namespace Travels.Infrastructure.Repositories
{
    public class ReviewRepository : IReviewRepository
    {
        private readonly AppDbContext _appDbContext;
        public ReviewRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task AddReview(Review review)
        {
            if (review == null)
                throw new ArgumentNullException(nameof(review));

            await _appDbContext.Reviews.AddAsync(review);
            await _appDbContext.SaveChangesAsync();
        }

        public async Task ChangeReview(Review review)
        {
            if (review == null)
                throw new ArgumentNullException(nameof(review));

            _appDbContext.Reviews.Update(review);
            await _appDbContext.SaveChangesAsync();
        }

        public async Task DeleteReview(int id)
        {
            var review = await _appDbContext.Reviews.FindAsync(id);
            if (review == null)
                throw new KeyNotFoundException($"Review with id {id} not found.");

            _appDbContext.Reviews.Remove(review);
            await _appDbContext.SaveChangesAsync();
        }

        public async Task<Review?> GetReview(int? id)
        {
            if (id == null)
                return null;

            return await _appDbContext.Reviews
                .FirstOrDefaultAsync(r => r.Id == id);
        }

        public async Task<IEnumerable<Review>> GetReviews()
        {
            return await _appDbContext.Reviews.ToListAsync();
        }
    }
}
