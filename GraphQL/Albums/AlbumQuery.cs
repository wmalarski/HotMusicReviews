using HotChocolate;
using HotChocolate.Data;
using HotChocolate.Types;
using HotChocolate.Types.Relay;
using HotMusicReviews.GraphQL.Users;
using HotMusicReviews.Models;
using HotMusicReviews.Services;
using System;
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

        [UsePaging(typeof(NonNullType<AlbumType>))]
        [UseFiltering(typeof(AlbumFilterInputType))]
        [UseSorting]
        public async Task<IEnumerable<Album>> GetSearchAsync(
            string query,
            [Service] AlbumService albumService,
            [Service] PerformerService performerService
        ) {
            var performers = performerService.Get(query).Select(performer => performer.Id).ToList().ToHashSet();
            Console.WriteLine(performers);
            return await albumService.GetByPerformerOrQueryAsync(performers, query);
        }


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