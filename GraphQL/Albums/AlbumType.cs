using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using HotChocolate;
using HotChocolate.Types;
using HotMusicReviews.GraphQL.LastFm;
using HotMusicReviews.GraphQL.Performers;
using HotMusicReviews.GraphQL.Reviews;
using HotMusicReviews.GraphQL.Users;
using HotMusicReviews.Models;
using HotMusicReviews.Services;

namespace HotMusicReviews.GraphQL.Albums
{
    public class AlbumType : ObjectType<Album>
    {
        protected override void Configure(IObjectTypeDescriptor<Album> descriptor)
        {
            descriptor
                .ImplementsNode()
                .IdField(t => t.Id)
                .ResolveNode((ctx, id) => ctx.Service<AlbumService>().GetAsync(id, default!)!);

            descriptor
                .Field(t => t.User)
                .Name("user")
                .ResolveWith<AlbumResolvers>(t => t.GetUser(default!));

            descriptor
                .Field(t => t.Performer)
                .ResolveWith<AlbumResolvers>(t => t.GetPerformerAsync(default!, default!, default!));

            descriptor
                .Field("reviews")
                .UsePaging<NonNullType<ReviewType>>()
                .UseFiltering()
                .UseSorting()
                .ResolveWith<AlbumResolvers>(t => t.GetReviews(default!, default!));

            descriptor
                .Field("reviewsCount")
                .ResolveWith<AlbumResolvers>(t => t.GetReviewsCount(default!, default!));

            descriptor
                .Field("details")
                .ResolveWith<AlbumResolvers>(t => t.GetDetails(default!, default!, default!));
        }

        private class AlbumResolvers
        {
            public User? GetUser(Album album)
            {
                return album.User == null ? null : new User(album.User);
            }
            public async Task<Performer?> GetPerformerAsync(
                Album album,
                CancellationToken cancellationToken,
                PerformerByIdDataLoader dataLoader
            )
            {
                return await dataLoader.LoadAsync(album.Performer, cancellationToken);
            }

            public IEnumerable<Review> GetReviews(
                Album album,
                [Service] ReviewService reviewService
            )
            {
                return reviewService.GetByAlbum(album.Id);
            }

            public int GetReviewsCount(
                Album album,
                [Service] ReviewService reviewService
            )
            {
                return reviewService.GetByAlbum(album.Id).Count();
            }

            public async Task<AlbumDetails> GetDetails(
                Album album,
                AlbumByMBidDataLoader dataLoader,
                CancellationToken cancellationToken
            )
            {
                return await dataLoader.LoadAsync(album.MBid, cancellationToken);
            }
        }
    }
}