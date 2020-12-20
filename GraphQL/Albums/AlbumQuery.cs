using HotChocolate;
using HotChocolate.Data;
using HotChocolate.Types;
using HotChocolate.Types.Relay;
using HotMusicReviews.GraphQL.Users;
using HotMusicReviews.Models;
using HotMusicReviews.Services;
using System.Collections.Generic;
using System.Linq;
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

        [UseFiltering(typeof(AlbumFilterInputType))]
        [UseSorting]
        public IEnumerable<Album> GetRandomAlbums(
            int count,
            [Service] AlbumService albumService,
            [Service] ReviewService reviewService,
            CancellationToken cancellationToken
        )
        {
            var albumIds = reviewService.GetUniqueAlbums().ToList();
            return albumService.GetRandom(count, albumIds);
        }

        [UseFiltering(typeof(AlbumFilterInputType))]
        [UseSorting]
        public IEnumerable<Album> GetMyRandomAlbums(
            int count,
            [Service] AlbumService albumService,
            [CurrentUserGlobalState] CurrentUser currentUser,
            [Service] ReviewService reviewService,
            CancellationToken cancellationToken
        )
        {
            var albumIds = reviewService.GetUniqueAlbums(currentUser.UserId).ToList();
            return albumService.GetRandom(count, albumIds);
        }

        public Task<Album> GetAlbumAsync(
            [ID(nameof(Album))] string id,
            CancellationToken cancellationToken,
            AlbumByIdDataLoader dataLoader
        ) =>
            dataLoader.LoadAsync(id, cancellationToken);
    }
}