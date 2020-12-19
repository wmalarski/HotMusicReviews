using System.Threading;
using System.Threading.Tasks;
using HotChocolate;
using HotChocolate.Types;
using HotMusicReviews.GraphQL.Albums;
using HotMusicReviews.GraphQL.Users;
using HotMusicReviews.Models;
using HotMusicReviews.Services;

namespace HotMusicReviews.GraphQL.Reviews
{
    public class ReviewType : ObjectType<Review>
    {
        protected override void Configure(IObjectTypeDescriptor<Review> descriptor)
        {
            descriptor
                .ImplementsNode()
                .IdField(t => t.Id)
                .ResolveNode((ctx, id) => ctx.Service<ReviewService>().GetAsync(id, default!)!);

            descriptor
                .Field(t => t.User)
                .ResolveWith<ReviewResolvers>(t => t.GetUser(default!));

            descriptor
                .Field(t => t.Album)
                .ResolveWith<ReviewResolvers>(t => t.GetAlbumAsync(default!, default!, default!));
        }

        private class ReviewResolvers
        {
            public User? GetUser(Review review)
            {
                return review.User == null ? null : new User(review.User);
            }

            public async Task<Album> GetAlbumAsync(
                Review review,
                AlbumByIdDataLoader dataLoader,
                CancellationToken cancellationToken
            )
            {
                return await dataLoader.LoadAsync(review.Album, cancellationToken);
            }
        }
    }
}