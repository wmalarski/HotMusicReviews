using System.Collections.Generic;
using HotMusicReviews.GraphQL.Common;
using HotMusicReviews.Models;
using HotChocolate.Types.Relay;

namespace HotMusicReviews.GraphQL.Reviews
{
    public record DeleteReviewInput(
        [ID(nameof(Review))] string Id
    );
}