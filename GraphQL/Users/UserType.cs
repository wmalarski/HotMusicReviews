using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using HotChocolate;
using HotChocolate.Types;
using HotMusicReviews.Models;
using HotMusicReviews.Services;

namespace HotMusicReviews.GraphQL.Users
{

    public record User(string id);

    public class UserType : ObjectType<User>
    {
        protected override void Configure(IObjectTypeDescriptor<User> descriptor)
        {
            descriptor
                .Field("performers")
                .ResolveWith<UserResolvers>(t => t.GetPerformers(default!, default!));

            descriptor
                .Field("albums")
                .ResolveWith<UserResolvers>(t => t.GetAlbums(default!, default!));

            descriptor
                .Field("reviews")
                .ResolveWith<UserResolvers>(t => t.GetReviews(default!, default!));

            descriptor.Field(f => f.id);
        }

        private class UserResolvers
        {
            public IEnumerable<Performer> GetPerformers(
                User record,
                [Service] PerformerService performerService
            ) =>
               performerService.GetByUser(record.id);

            public IEnumerable<Album> GetAlbums(
                User record,
                [Service] AlbumService albumService
            ) =>
               albumService.GetByUser(record.id);

            public IEnumerable<Review> GetReviews(
                User record,
                [Service] ReviewService reviewService
            ) =>
               reviewService.GetByUser(record.id);
        }
    }
}