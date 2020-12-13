using HotMusicReviews.Models;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;
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

        public async Task<List<Performer>> GetAsync() => await (await _performers.FindAsync(_ => true)).ToListAsync();

        public async Task<Performer?> GetAsync(string id) => await (await _performers.FindAsync<Performer>(performer => performer.Id == id)).FirstAsync();

        async public Task<Performer> Create(Performer performer)
        {
            await _performers.InsertOneAsync(performer);
            return performer;
        }
    }
}