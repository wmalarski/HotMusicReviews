using HotChocolate;
using HotChocolate.Data;
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

        [UsePaging(typeof(NonNullType<AlbumType>))]
        [UseFiltering(typeof(AlbumFilterInputType))]
        [UseSorting]
        public IEnumerable<Album> GetAlbums(
            [Service] AlbumService albumService
        ) =>
            albumService.Get();

        public Task<Album> GetAlbumAsync(
            [ID(nameof(Album))] string id,
            CancellationToken cancellationToken,
            AlbumByIdDataLoader dataLoader
        ) =>
            dataLoader.LoadAsync(id, cancellationToken);
    }
}