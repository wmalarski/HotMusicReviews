using System.Collections.Generic;
using HotChocolate.Types.Relay;
using HotMusicReviews.GraphQL.Common;
using HotMusicReviews.Models;

namespace HotMusicReviews.GraphQL.Reviews
{
    public record CreateReviewInput(
        string? MBid,
        string? Name,
        [ID(nameof(Performer))] string? Performer,
        int? Year
    );

    public class CreateReviewPayload : ReviewPayloadBase
    {
        public CreateReviewPayload(Review review) : base(review)
        {
        }

        public CreateReviewPayload(IReadOnlyList<UserError> errors) : base(errors)
        {
        }
    }
}