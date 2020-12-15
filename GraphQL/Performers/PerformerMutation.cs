using System;
using System.Threading;
using System.Threading.Tasks;
using HotChocolate;
using HotChocolate.AspNetCore.Authorization;
using HotChocolate.Types;
using HotMusicReviews.GraphQL.Common;
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
            CancellationToken cancellationToken
        )
        {
            var user = "0"; // TODO: add JWT user data 

            var performer = new Performer
            {
                Name = input.Name,
                MBid = input.MBid,
                User = user
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
                    User = user
                };

                await albumService.CreateAsync(album, cancellationToken);
            });

            return new CreatePerformerPayload(performer);
        }

        public async Task<UpdatePerformerPayload> UpdatePerformerAsync(
            UpdatePerformerInput input,
            [Service] PerformerService performerService,
            CancellationToken cancellationToken
        )
        {
            var album = new Performer
            {
                Id = input.Id,
                Name = input.Name,
                UpdatedAt = DateTime.Now
            };

            await performerService.UpdateAsync(album, cancellationToken);

            return new UpdatePerformerPayload(album);
        }

        public async Task<DeletePayload> DeletePerformerAsync(
            DeletePerformerInput input,
            [Service] PerformerService performerService,
            CancellationToken cancellationToken
        )
        {
            var result = await performerService.DeleteAsync(input.Id, cancellationToken);
            return new DeletePayload(result == null ? false : result.DeletedCount > 0);
        }
    }
}