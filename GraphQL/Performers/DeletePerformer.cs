using HotMusicReviews.Models;
using HotChocolate.Types.Relay;

namespace HotMusicReviews.GraphQL.Performers
{
    public record DeletePerformerInput(
        [ID(nameof(Performer))] string Id
    );
}