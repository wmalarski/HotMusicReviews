using HotMusicReviews.Models;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace HotMusicReviews.Services
{
    public class ReviewService
    {
        private readonly IMongoCollection<Review> _reviews;

        public ReviewService(IMongoCollection<Review> reviews)
        {
            _reviews = reviews;
        }

        public IEnumerable<Review> Get()
        {
            return _reviews.Find(_ => true).ToEnumerable();
        }

        public async Task<Review?> GetAsync(string id, CancellationToken cancellationToken)
        {
            var reviews = await _reviews.FindAsync(review => review.Id == id, null, cancellationToken);
            return await reviews.FirstAsync(cancellationToken);
        }

        public IEnumerable<Review> GetByUser(string user)
        {
            return _reviews.Find(review => review.User == user).ToEnumerable();
        }

        public IEnumerable<Review> GetByAlbum(string album)
        {
            return _reviews.Find(review => review.Album == album).ToEnumerable();
        }

        public async Task<Review> CreateAsync(Review review, CancellationToken cancellationToken)
        {
            await _reviews.InsertOneAsync(review, null, cancellationToken);
            return review;
        }

        public async Task<ReplaceOneResult?> UpdateAsync(Review reviewInput, CancellationToken cancellationToken)
        {
            return await _reviews.ReplaceOneAsync(book => book.Id == reviewInput.Id, reviewInput, new ReplaceOptions(), cancellationToken);
        }

        public async Task<DeleteResult?> DeleteAsync(string id, CancellationToken cancellationToken)
        {
            return await _reviews.DeleteOneAsync(book => book.Id == id, cancellationToken);
        }
    }
}
