using System.Collections.Generic;
using HotMusicReviews.GraphQL.Common;
using HotMusicReviews.Models;

namespace HotMusicReviews.GraphQL.Performers
{
    public class AddPerformerPayload : PerformerPayloadBase
    {
        public AddPerformerPayload(Performer performer) : base(performer)
        {
        }

        public AddPerformerPayload(IReadOnlyList<UserError> errors) : base(errors)
        {
        }
    }
}