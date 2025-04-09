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
        public async Task AddReview(ReviewDto reviewDto, int? userId)
        {
            if (reviewDto == null)
                throw new ArgumentNullException(nameof(reviewDto));

            if (userId.HasValue)
            {
                // Zalogowany użytkownik
                var user = await _userRepository.GetById(userId.Value);
                if (user != null)
                {
                    reviewDto.UserName = user.Name; // Ustawiamy imię zalogowanego użytkownika
                }
                else
                {
                    throw new ArgumentException("User not found");
                }
            }
            else
            {
                // Niezalogowany użytkownik - generujemy losowe imię
                reviewDto.UserName = Guid.NewGuid().ToString(); // Generujemy losowe imię dla gościa
            }

            var travelOffer = await _travelOfferRepository.GetTravel(reviewDto.TravelOfferId);
            if (travelOffer == null)
                throw new ArgumentException("Travel offer not found");

            // Mapowanie DTO na encję Review
            var review = _mapper.Map<Review>(reviewDto);
            await _reviewRepository.AddReview(review); // Zapisujemy recenzję
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

        public async Task<IEnumerable<ReviewDto>> GetReviews()
        {
            var reviews = await _reviewRepository.GetReviews();
            if (reviews == null)
                throw new ArgumentNullException("Review not found");

            var reviewDtos =  _mapper.Map<IEnumerable<ReviewDto>>(reviews);
            return reviewDtos;
        }
    }
}
