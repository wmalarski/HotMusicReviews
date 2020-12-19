using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using GreenDonut;
using HotChocolate.DataLoader;
using HotMusicReviews.Models;
using HotMusicReviews.Services;

namespace HotMusicReviews.GraphQL.LastFm
{
    public class PerformerByMBidDataLoader : BatchDataLoader<string, PerformerDetails>
    {
        private readonly LastFmService _lastFmService;

        public PerformerByMBidDataLoader(
            IBatchScheduler batchScheduler,
            LastFmService lastFmService)
            : base(batchScheduler)
        {
            _lastFmService = lastFmService;
        }

        protected override async Task<IReadOnlyDictionary<string, PerformerDetails>> LoadBatchAsync(
            IReadOnlyList<string> keys,
            CancellationToken cancellationToken)
        {
            var tasks = keys.Select(mBid => _lastFmService.GetPerformer(mBid)).ToArray();
            var results = await Task.WhenAll(tasks);
            if (results == null) return new Dictionary<string, PerformerDetails>();
            return results.Where(performer => performer != null).OfType<PerformerDetails>().ToDictionary(performer => performer.mBid);
        }
    }
}