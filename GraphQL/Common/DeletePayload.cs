using System.Collections.Generic;
using HotMusicReviews.GraphQL.Common;
using HotMusicReviews.Models;

namespace HotMusicReviews.GraphQL.Common
{
    public class DeletePayload : Payload
    {
        public DeletePayload(bool success)
        {
            Success = success;
        }

        public DeletePayload(IReadOnlyList<UserError> errors) : base(errors)
        {
        }

        public bool Success { get; }
    }
}