using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using GreenDonut;
using HotChocolate.DataLoader;
using HotMusicReviews.Models;
using HotMusicReviews.Services;

namespace HotMusicReviews.GraphQL.Reviews
{
    public class ReviewByIdDataLoader : BatchDataLoader<string, Review>
    {
        private readonly ReviewService _reviewService;

        public ReviewByIdDataLoader(
            IBatchScheduler batchScheduler,
            ReviewService reviewService)
            : base(batchScheduler)
        {
            _reviewService = reviewService;
        }

        protected override async Task<IReadOnlyDictionary<string, Review>> LoadBatchAsync(
            IReadOnlyList<string> keys,
            CancellationToken cancellationToken)
        {
            var reviews = await _reviewService.GetAsync(keys, cancellationToken);
            return reviews.ToDictionary(review => review.Id);
        }
    }
}