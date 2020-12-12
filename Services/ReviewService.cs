using HotMusicReviews.Models;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;

namespace HotMusicReviews.Services
{
    public class ReviewService
    {
        private readonly IMongoCollection<Review> _reviews;

        public ReviewService(IMongoCollection<Review> reviews)
        {
            _reviews = reviews;
        }

        public List<Review> Get() => _reviews.Find(_ => true).ToList();
    }
}
