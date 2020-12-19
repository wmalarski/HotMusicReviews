using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using GreenDonut;
using HotChocolate.DataLoader;
using HotMusicReviews.Models;
using HotMusicReviews.Services;

namespace HotMusicReviews.GraphQL.Performers
{
    public class PerformerByIdDataLoader : BatchDataLoader<string, Performer>
    {
        private readonly PerformerService _performerService;

        public PerformerByIdDataLoader(
            IBatchScheduler batchScheduler,
            PerformerService performerService)
            : base(batchScheduler)
        {
            _performerService = performerService;
        }

        protected override async Task<IReadOnlyDictionary<string, Performer>> LoadBatchAsync(
            IReadOnlyList<string> keys,
            CancellationToken cancellationToken)
        {
            var performers = await _performerService.GetAsync(keys, cancellationToken);
            return performers.ToDictionary(performer => performer.Id);
        }
    }
}