using System.Collections.Generic;
using HotMusicReviews.GraphQL.Common;
using HotMusicReviews.Models;

namespace HotMusicReviews.GraphQL.Performers
{
    public record CreatePerformerInput(
        string MBid,
        string Name
    );

    public class CreatePerformerPayload : PerformerPayloadBase
    {
        public CreatePerformerPayload(Performer performer) : base(performer)
        {
        }

        public CreatePerformerPayload(IReadOnlyList<UserError> errors) : base(errors)
        {
        }
    }
}