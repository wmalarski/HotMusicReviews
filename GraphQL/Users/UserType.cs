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
                .ResolveWith<UserResolvers>(t => t.GetPerformersAsync(default!, default!, default));

            descriptor
                .Field("albums")
                .ResolveWith<UserResolvers>(t => t.GetAlbumsAsync(default!, default!, default));

            descriptor
                .Field("reviews")
                .ResolveWith<UserResolvers>(t => t.GetReviewsAsync(default!, default!, default));

            descriptor.Field(f => f.id).ID(nameof(UserType));
        }

        private class UserResolvers
        {
            public async Task<IEnumerable<Performer>> GetPerformersAsync(
                User record,
                [Service] PerformerService performerService,
                CancellationToken cancellationToken
            ) =>
               await performerService.GetByUserAsync(record.id, cancellationToken);

            public async Task<IEnumerable<Album>> GetAlbumsAsync(
                User record,
                [Service] AlbumService albumService,
                CancellationToken cancellationToken
            ) =>
               await albumService.GetByUserAsync(record.id, cancellationToken);

            public async Task<IEnumerable<Review>> GetReviewsAsync(
                User record,
                [Service] ReviewService reviewService,
                CancellationToken cancellationToken
            ) =>
               await reviewService.GetByUserAsync(record.id, cancellationToken);
        }
    }
}