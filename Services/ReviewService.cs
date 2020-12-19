using HotMusicReviews.Models;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;
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

        public IEnumerable<Review> Get() =>
            _reviews.Find(_ => true).ToEnumerable();

        public async Task<Review?> GetAsync(string id, CancellationToken cancellationToken)
        {
            var reviews = await _reviews.FindAsync(review => review.Id == id, null, cancellationToken);
            return await reviews.FirstAsync(cancellationToken);
        }

        public async Task<IEnumerable<Review>> GetAsync(IReadOnlyList<string> keys, CancellationToken cancellationToken)
        {
            var reviews = await _reviews.FindAsync(review => keys.Contains(review.Id), null, cancellationToken);
            return reviews.ToEnumerable(cancellationToken);
        }

        public IEnumerable<Review> GetByUser(string user) =>
            _reviews.Find(review => review.User == user).ToEnumerable();

        public IEnumerable<Review> GetByAlbum(string album) =>
            _reviews.Find(review => review.Album == album).ToEnumerable();

        public async Task<Review> CreateAsync(Review review, CancellationToken cancellationToken)
        {
            await _reviews.InsertOneAsync(review, null, cancellationToken);
            return review;
        }

        public async Task<ReplaceOneResult?> UpdateAsync(Review reviewInput, CancellationToken cancellationToken) =>
            await _reviews.ReplaceOneAsync(book => book.Id == reviewInput.Id, reviewInput, new ReplaceOptions(), cancellationToken);

        public async Task<DeleteResult?> DeleteAsync(string id, CancellationToken cancellationToken) =>
            await _reviews.DeleteOneAsync(book => book.Id == id, cancellationToken);
    }
}
