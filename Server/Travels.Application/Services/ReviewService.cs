using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Travels.Application.Dtos.Review;
using Travels.Application.Interfaces;
using Travels.Domain.Entities;
using Travels.Domain.Interfaces;

namespace Travels.Application.Services
{
    public class ReviewService : IReviewService
    {
        private readonly IUserRepository _userRepository;
        private readonly ITravelOfferRepository _travelOfferRepository;
        private readonly IReviewRepository _reviewRepository;
        private readonly IMapper _mapper;

        public ReviewService(IUserRepository userRepository, ITravelOfferRepository travelOfferRepository, IReviewRepository reviewRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _travelOfferRepository = travelOfferRepository;
            _reviewRepository = reviewRepository;
            _mapper = mapper;
        }
        public async Task AddReview(ReviewDto reviewDto,int? userId)
        {
            if(reviewDto == null)
                throw new ArgumentNullException(nameof(reviewDto));

            var user = await _userRepository.GetById(userId);
            if (user == null)
            {
                var guessUserId = Guid.NewGuid().ToString();
                reviewDto.UserName = guessUserId;
            }
            else
            {
                reviewDto.UserName = user.Name;
            }

            var travelOffer = await _travelOfferRepository.GetTravel(reviewDto.TravelOfferId);
            if (travelOffer == null)
                throw new ArgumentException("Travel offer not found");

            var review = _mapper.Map<Review>(reviewDto);
            await _reviewRepository.AddReview(review);

        }

        public async Task ChangeReview(ReviewDto reviewDto)
        {
            if (reviewDto == null)
                throw new ArgumentNullException(nameof(reviewDto));

            var review = await _reviewRepository.GetReview(reviewDto.Id);
            if (review == null)
                throw new ArgumentException("Review not found");
        }

        public Task DeleteReview(ReviewDto reviewDto)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Review>> GetReviews(ReviewDto reviewDto)
        {
            throw new NotImplementedException();
        }
    }
}
