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

        public async Task<List<Review>> GetAsync(CancellationToken cancellationToken)
        {
            var reviews = await _reviews.FindAsync(_ => true, null, cancellationToken);
            return await reviews.ToListAsync(cancellationToken);
        }

        public async Task<Review?> GetAsync(string id, CancellationToken cancellationToken)
        {
            var reviews = await _reviews.FindAsync(review => review.Id == id, null, cancellationToken);
            return await reviews.FirstAsync(cancellationToken);
        }

        public async Task<List<Review>> GetByUserAsync(string user, CancellationToken cancellationToken)
        {
            var reviews = await _reviews.FindAsync(review => review.User == user, null, cancellationToken);
            return await reviews.ToListAsync(cancellationToken);
        }

        public async Task<List<Review>> GetByAlbumAsync(string album, CancellationToken cancellationToken)
        {
            var reviews = await _reviews.FindAsync(review => review.Album == album, null, cancellationToken);
            return await reviews.ToListAsync(cancellationToken);
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
