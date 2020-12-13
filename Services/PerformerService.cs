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

        public async Task<List<Performer>> GetAsync() =>
            await (await _performers.FindAsync(_ => true)).ToListAsync();

        public async Task<Performer?> GetAsync(string id) =>
            await (await _performers.FindAsync<Performer>(performer => performer.Id == id)).FirstAsync();

        public async Task<IReadOnlyDictionary<string, Performer>> GetAsync(IReadOnlyList<string> keys)
        {
            var filtered = await _performers.FindAsync(performer => keys.Contains(performer.Id));
            var list = await filtered.ToListAsync();
            return list.ToDictionary(performer => performer.Id);
        }

        public async Task<List<Performer>> GetByUserAsync(string user, CancellationToken cancellationToken) =>
            await (await _performers
                .FindAsync(performer => performer.User == user, null, cancellationToken))
                .ToListAsync(cancellationToken);

        async public Task<Performer> Create(Performer performer)
        {
            await _performers.InsertOneAsync(performer);
            return performer;
        }
    }
}