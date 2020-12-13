using System.Collections.Generic;
using HotChocolate.Types.Relay;
using HotMusicReviews.GraphQL.Common;
using HotMusicReviews.Models;

namespace HotMusicReviews.GraphQL.Reviews
{

    public record UpdateReviewInput(
        [ID(nameof(Review))] string Id,
        string Text,
        decimal Rating
    );
    public class UpdateReviewPayload : ReviewPayloadBase
    {
        public UpdateReviewPayload(Review review) : base(review)
        {
        }

        public UpdateReviewPayload(IReadOnlyList<UserError> errors) : base(errors)
        {
        }
    }
}