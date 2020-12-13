using HotMusicReviews.Models;
using HotChocolate.Types.Relay;

namespace HotMusicReviews.GraphQL.Albums
{
    public record DeleteAlbumInput(
        [ID(nameof(Album))] string Id
    );
}