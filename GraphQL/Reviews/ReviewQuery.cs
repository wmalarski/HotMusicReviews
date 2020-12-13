using HotChocolate;
using HotChocolate.Data;
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
        [UsePaging(typeof(NonNullType<ReviewType>))]
        [UseFiltering]
        [UseSorting]
        public IEnumerable<Review> GetReviews(
            [Service] ReviewService reviewService
        ) => reviewService.Get();

        public Task<Review?> GetReviewAsync(
            [ID(nameof(Review))] string id,
            [Service] ReviewService reviewService,
            CancellationToken cancellationToken
        ) =>
            reviewService.GetAsync(id, cancellationToken);
    }
}