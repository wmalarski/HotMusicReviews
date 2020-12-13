using HotMusicReviews.Models;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace HotMusicReviews.Services
{
    public class PerformerService
    {
        private readonly IMongoCollection<Performer> _performers;

        public PerformerService(IMongoCollection<Performer> performers)
        {
            _performers = performers;
        }

        public async Task<List<Performer>> GetAsync(CancellationToken cancellationToken)
        {
            var performers = await _performers.FindAsync(_ => true, null, cancellationToken);
            return await performers.ToListAsync(cancellationToken);
        }

        public async Task<Performer?> GetAsync(string id, CancellationToken cancellationToken)
        {
            var performers = await _performers.FindAsync(performer => performer.Id == id, null, cancellationToken);
            return await performers.FirstAsync(cancellationToken);
        }

        public async Task<List<Performer>> GetByUserAsync(string user, CancellationToken cancellationToken)
        {
            var performers = await _performers.FindAsync(performer => performer.User == user, null, cancellationToken);
            return await performers.ToListAsync(cancellationToken);
        }

        public async Task<Performer> CreateAsync(Performer performer, CancellationToken cancellationToken)
        {
            await _performers.InsertOneAsync(performer, null, cancellationToken);
            return performer;
        }

        public async Task<ReplaceOneResult?> UpdateAsync(Performer performerInput, CancellationToken cancellationToken)
        {
            return await _performers.ReplaceOneAsync(book => book.Id == performerInput.Id, performerInput, new ReplaceOptions(), cancellationToken);
        }

        public async Task<DeleteResult?> DeleteAsync(string id, CancellationToken cancellationToken)
        {
            return await _performers.DeleteOneAsync(book => book.Id == id, cancellationToken);
        }
    }
}