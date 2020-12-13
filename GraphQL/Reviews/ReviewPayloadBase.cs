using System.Collections.Generic;
using HotMusicReviews.GraphQL.Common;
using HotMusicReviews.Models;

namespace HotMusicReviews.GraphQL.Reviews
{
    public class ReviewPayloadBase : Payload
    {
        protected ReviewPayloadBase(Review review)
        {
            Review = review;
        }

        protected ReviewPayloadBase(IReadOnlyList<UserError> errors) : base(errors)
        {
        }

        public Review? Review { get; }
    }
}