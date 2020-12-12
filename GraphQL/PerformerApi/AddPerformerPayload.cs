using HotMusicReviews.Models;

namespace HotMusicReviews.GraphQL.PerformerApi
{
    public class AddPerformerPayload
    {
        public AddPerformerPayload(Performer performer)
        {
            Performer = performer;
        }

        public Performer Performer { get; }
    }
}