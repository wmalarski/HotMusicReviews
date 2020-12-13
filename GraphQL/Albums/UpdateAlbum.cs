using System.Collections.Generic;
using HotChocolate.Types.Relay;
using HotMusicReviews.GraphQL.Common;
using HotMusicReviews.Models;

namespace HotMusicReviews.GraphQL.Albums
{

    public record UpdateAlbumInput(
        [ID(nameof(Album))] string Id,
        string MBid,
        string Name,
        [ID(nameof(Performer))] string Performer,
        int Year
    );
    public class UpdateAlbumPayload : AlbumPayloadBase
    {
        public UpdateAlbumPayload(Album album) : base(album)
        {
        }

        public UpdateAlbumPayload(IReadOnlyList<UserError> errors) : base(errors)
        {
        }
    }
}