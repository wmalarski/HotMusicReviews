using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using HotChocolate;
using HotChocolate.AspNetCore.Authorization;
using HotChocolate.Types;
using HotMusicReviews.GraphQL.Common;
using HotMusicReviews.GraphQL.Users;
using HotMusicReviews.Models;
using HotMusicReviews.Services;

namespace HotMusicReviews.GraphQL.Reviews
{
    [Authorize]
    [ExtendObjectType(Name = "Mutation")]
    public class ReviewMutations
    {
        public async Task<CreateReviewPayload> CreateReviewAsync(
            CreateReviewInput input,
            [Service] ReviewService reviewService,
            [CurrentUserGlobalState] CurrentUser currentUser,
            CancellationToken cancellationToken
        )
        {
            var review = new Review
            {
                User = currentUser.UserId,
                Album = input.Album,
                Rating = input.Rating,
                Text = input.Text,
            };

            await reviewService.CreateAsync(review, cancellationToken);

            return new CreateReviewPayload(review);
        }

        public async Task<UpdateReviewPayload> UpdateReviewAsync(
            UpdateReviewInput input,
            [Service] ReviewService reviewService,
            [CurrentUserGlobalState] CurrentUser currentUser,
            CancellationToken cancellationToken
        )
        {
            var currentReview = await reviewService.GetAsync(input.Id, cancellationToken);
            if (currentReview?.User != currentUser.UserId)
            {
                return new UpdateReviewPayload(new List<UserError> {
                    new NoAccessError()
                });
            }

            var review = new Review
            {
                Id = input.Id,
                Text = input.Text,
                Rating = input.Rating,
                UpdatedAt = DateTime.Now,
                Album = currentReview.Album,
                CreatedAt = currentReview.CreatedAt,
                User = currentReview.User,
            };

            await reviewService.UpdateAsync(review, cancellationToken);

            return new UpdateReviewPayload(review);
        }

        public async Task<DeletePayload> DeleteReviewAsync(
            DeleteReviewInput input,
            [Service] ReviewService reviewService,
            [CurrentUserGlobalState] CurrentUser currentUser,
            CancellationToken cancellationToken
        )
        {
            var review = await reviewService.GetAsync(input.Id, cancellationToken);

            if (review?.User != currentUser.UserId)
            {
                return new DeletePayload(new List<UserError> {
                    new NoAccessError()
                });
            }

            var result = await reviewService.DeleteAsync(input.Id, cancellationToken);
            return new DeletePayload(result == null ? false : result.DeletedCount > 0);
        }
    }
}