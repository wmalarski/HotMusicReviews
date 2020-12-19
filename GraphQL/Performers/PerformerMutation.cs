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

namespace HotMusicReviews.GraphQL.Performers
{
    [Authorize]
    [ExtendObjectType(Name = "Mutation")]
    public class PerformerMutations
    {
        public async Task<CreatePerformerPayload> CreatePerformerAsync(
            CreatePerformerInput input,
            [Service] PerformerService performerService,
            [Service] AlbumService albumService,
            [Service] ReviewService reviewService,
            [CurrentUserGlobalState] CurrentUser currentUser,
            CancellationToken cancellationToken
        )
        {
            var currentPerformer = await performerService.GetByMBidAsync(input.MBid, cancellationToken);
            if (currentPerformer != null)
            {
                return new CreatePerformerPayload(new List<UserError> {
                    new UserError("Performer with this MBid exists", "409"),
                });
            }

            var performer = new Performer
            {
                Name = input.Name,
                MBid = input.MBid,
                User = currentUser.UserId,
            };

            await performerService.CreateAsync(performer, cancellationToken);

            input.Albums.ForEach(async albumInput =>
            {
                var album = new Album
                {
                    MBid = albumInput.MBid,
                    Name = albumInput.Name,
                    Performer = performer.Id,
                    Year = albumInput.Year,
                    User = currentUser.UserId
                };

                await albumService.CreateAsync(album, cancellationToken);
            });

            return new CreatePerformerPayload(performer);
        }

        public async Task<UpdatePerformerPayload> UpdatePerformerAsync(
            UpdatePerformerInput input,
            [Service] PerformerService performerService,
            [CurrentUserGlobalState] CurrentUser currentUser,
            CancellationToken cancellationToken
        )
        {
            var currentPerformer = await performerService.GetAsync(input.Id, cancellationToken);
            if (currentPerformer == null)
            {
                return new UpdatePerformerPayload(new List<UserError> {
                    new NoAccessError()
                });
            }

            var performer = new Performer
            {
                Id = input.Id,
                Name = input.Name,
                UpdatedAt = DateTime.Now,
                MBid = currentPerformer.MBid,
                CreatedAt = currentPerformer.CreatedAt,
                User = currentPerformer.User,
            };

            await performerService.UpdateAsync(performer, cancellationToken);

            return new UpdatePerformerPayload(performer);
        }

        public async Task<DeletePayload> DeletePerformerAsync(
            DeletePerformerInput input,
            [Service] PerformerService performerService,
            [CurrentUserGlobalState] CurrentUser currentUser,
            CancellationToken cancellationToken
        )
        {
            var performer = await performerService.GetAsync(input.Id, cancellationToken);

            if (performer?.User != currentUser.UserId)
            {
                return new DeletePayload(new List<UserError> {
                    new NoAccessError()
                });
            }

            var result = await performerService.DeleteAsync(input.Id, cancellationToken);
            return new DeletePayload(result == null ? false : result.DeletedCount > 0);
        }
    }
}