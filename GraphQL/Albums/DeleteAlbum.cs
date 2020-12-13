using System.Collections.Generic;
using HotMusicReviews.GraphQL.Common;
using HotMusicReviews.Models;
using HotChocolate.Types.Relay;

namespace HotMusicReviews.GraphQL.Albums
{
    public record DeleteAlbumInput(
        [ID(nameof(Performer))] string Id
    );
}