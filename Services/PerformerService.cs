using HotMusicReviews.Models;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;
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

        public IEnumerable<Performer> Get() => 
            _performers.Find(_ => true).ToEnumerable();

        public IEnumerable<Performer> Get(string query) => 
            _performers.Find(performer => performer.Name.Contains(query)).ToEnumerable();

        public async Task<Performer?> GetAsync(string id, CancellationToken cancellationToken)
        {
            var performers = await _performers.FindAsync(performer => performer.Id == id, null, cancellationToken);
            return await performers.FirstOrDefaultAsync(cancellationToken);
        }

        public async Task<IEnumerable<Performer>> GetAsync(IReadOnlyList<string> keys, CancellationToken cancellationToken)
        {
            var performers = await _performers.FindAsync(performer => keys.Contains(performer.Id), null, cancellationToken);
            return performers.ToEnumerable(cancellationToken);
        }

        public IEnumerable<Performer> GetByUser(string user) =>
            _performers.Find(performer => performer.User == user).ToEnumerable();

        public async Task<Performer?> GetByMBidAsync(string mBid, CancellationToken cancellationToken)
        {
            var performers = await _performers.FindAsync(performer => performer.MBid == mBid, null, cancellationToken);
            return await performers.FirstOrDefaultAsync(cancellationToken);
        }

        public async Task<Performer> CreateAsync(Performer performer, CancellationToken cancellationToken)
        {
            await _performers.InsertOneAsync(performer, null, cancellationToken);
            return performer;
        }

        public async Task<ReplaceOneResult?> UpdateAsync(Performer performerInput, CancellationToken cancellationToken) =>
            await _performers.ReplaceOneAsync(book => book.Id == performerInput.Id, performerInput, new ReplaceOptions(), cancellationToken);

        public async Task<DeleteResult?> DeleteAsync(string id, CancellationToken cancellationToken) =>
            await _performers.DeleteOneAsync(book => book.Id == id, cancellationToken);
    }
}