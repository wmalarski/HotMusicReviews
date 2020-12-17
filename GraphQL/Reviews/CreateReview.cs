using System.Collections.Generic;
using HotChocolate.Types.Relay;
using HotMusicReviews.GraphQL.Common;
using HotMusicReviews.Models;

namespace HotMusicReviews.GraphQL.Reviews
{
    public record CreateReviewInput(
        [ID(nameof(Album))] string Album,
        decimal Rating,
        string Text
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