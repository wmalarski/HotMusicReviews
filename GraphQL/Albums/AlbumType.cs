using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using HotChocolate;
using HotChocolate.Types;
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
                .UsePaging()
                .UseFiltering()
                .ResolveWith<AlbumResolvers>(t => t.GetReviews(default!, default!));
        }

        private class AlbumResolvers
        {
            public User? GetUser(Album album)
            {
                return album.User == null ? null : new User(album.User);
            }
            public async Task<Performer?> GetPerformerAsync(
                Album album,
                [Service] PerformerService performerService,
                CancellationToken cancellationToken
            )
            {
                return await performerService.GetAsync(album.Performer, cancellationToken);
            }

            public IEnumerable<Review> GetReviews(
                Album album,
                [Service] ReviewService reviewService
            )
            {
                return reviewService.GetByAlbum(album.Id);
            }
        }
    }
}