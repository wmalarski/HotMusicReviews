using System.Collections.Generic;
using HotChocolate.Types.Relay;
using HotMusicReviews.GraphQL.Common;
using HotMusicReviews.Models;

namespace HotMusicReviews.GraphQL.Performers
{

    public record UpdatePerformerInput(
        [ID(nameof(Performer))] string Id,
        string Name
    );

    public class UpdatePerformerPayload : PerformerPayloadBase
    {
        public UpdatePerformerPayload(Performer performer) : base(performer)
        {
        }

        public UpdatePerformerPayload(IReadOnlyList<UserError> errors) : base(errors)
        {
        }
    }
}