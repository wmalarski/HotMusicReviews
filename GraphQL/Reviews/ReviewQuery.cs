using HotChocolate;
using HotChocolate.Types;
using HotChocolate.Types.Relay;
using HotMusicReviews.Models;
using HotMusicReviews.Services;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace HotMusicReviews.GraphQL.Reviews
{

    [ExtendObjectType(Name = "Query")]
    public class ReviewQuery
    {
        public Task<List<Review>> GetReviewsAsync(
            [Service] ReviewService reviewService,
            CancellationToken cancellationToken
        ) => reviewService.GetAsync(cancellationToken);

        public Task<Review?> GetReviewAsync(
            [ID(nameof(Review))] string id,
            [Service] ReviewService reviewService,
            CancellationToken cancellationToken
        ) =>
            reviewService.GetAsync(id, cancellationToken);
    }
}