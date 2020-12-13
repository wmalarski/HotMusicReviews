using System.Collections.Generic;
using HotMusicReviews.GraphQL.Common;
using HotMusicReviews.Models;

namespace HotMusicReviews.GraphQL.Performers
{
    public class PerformerPayloadBase : Payload
    {
        protected PerformerPayloadBase(Performer performer)
        {
            Performer = performer;
        }

        protected PerformerPayloadBase(IReadOnlyList<UserError> errors) : base(errors)
        {
        }

        public Performer? Performer { get; }
    }
}