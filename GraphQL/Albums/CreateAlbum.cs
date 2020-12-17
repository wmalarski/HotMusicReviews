using System.Collections.Generic;
using HotChocolate.Types.Relay;
using HotMusicReviews.GraphQL.Common;
using HotMusicReviews.Models;

namespace HotMusicReviews.GraphQL.Albums
{
    public record CreateAlbumInput(
        string MBid,
        string Name,
        [ID(nameof(Performer))] string Performer,
        int Year
    );

    public class CreateAlbumPayload : AlbumPayloadBase
    {
        public CreateAlbumPayload(Album album) : base(album)
        {
        }

        public CreateAlbumPayload(IReadOnlyList<UserError> errors) : base(errors)
        {
        }
    }
}