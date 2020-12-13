using System;
using System.Threading;
using System.Threading.Tasks;
using HotChocolate;
using HotChocolate.Types;
using HotMusicReviews.GraphQL.Common;
using HotMusicReviews.Models;
using HotMusicReviews.Services;

namespace HotMusicReviews.GraphQL.Reviews
{
    [ExtendObjectType(Name = "Mutation")]
    public class ReviewMutations
    {
        public async Task<CreateReviewPayload> CreateReviewAsync(
            CreateReviewInput input,
            [Service] ReviewService reviewService,
            CancellationToken cancellationToken
        )
        {
            var review = new Review
            {
                User = "TODO",
            };

            await reviewService.CreateAsync(review, cancellationToken);

            return new CreateReviewPayload(review);
        }

        public async Task<UpdateReviewPayload> UpdateReviewAsync(
            UpdateReviewInput input,
            [Service] ReviewService reviewService,
            CancellationToken cancellationToken
        )
        {
            var album = new Review
            {
                Id = input.Id,
                Text = input.Text,
                Rating = input.Rating,
                UpdatedAt = DateTime.Now
            };

            await reviewService.UpdateAsync(album, cancellationToken);

            return new UpdateReviewPayload(album);
        }

        public async Task<DeletePayload> DeleteReviewAsync(
            DeleteReviewInput input,
            [Service] ReviewService reviewService,
            CancellationToken cancellationToken
        )
        {
            var result = await reviewService.DeleteAsync(input.Id, cancellationToken);
            return new DeletePayload(result == null ? false : result.DeletedCount > 0);
        }
    }
}