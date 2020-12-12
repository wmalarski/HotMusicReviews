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

        public List<Performer> Get() => _performers.Find(_ => true).ToList();

        async public Task<Performer> Create(Performer performer)
        {
            await _performers.InsertOneAsync(performer);
            return performer;
        }
    }
}