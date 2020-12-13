using HotChocolate;
using HotChocolate.Types;
using HotChocolate.Types.Relay;
using HotMusicReviews.Models;
using HotMusicReviews.Services;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace HotMusicReviews.GraphQL.Albums
{

    [ExtendObjectType(Name = "Query")]
    public class AlbumQuery
    {
        public Task<List<Album>> GetAlbumsAsync(
            [Service] AlbumService albumService,
            CancellationToken cancellationToken
        ) =>
            albumService.GetAsync(cancellationToken);

        public Task<Album?> GetAlbumAsync(
            [ID(nameof(Album))] string id,
            [Service] AlbumService albumService,
            CancellationToken cancellationToken
        ) =>
            albumService.GetAsync(id, cancellationToken);
    }
}