using System.Threading;
using System.Threading.Tasks;
using HotChocolate;
using HotChocolate.Types;
using HotMusicReviews.Models;
using HotMusicReviews.Services;
using HotMusicReviews.GraphQL.Common;
using System;
using HotChocolate.AspNetCore.Authorization;
using HotMusicReviews.GraphQL.Users;
using System.Collections.Generic;

namespace HotMusicReviews.GraphQL.Albums
{
    [Authorize]
    [ExtendObjectType(Name = "Mutation")]
    public class AlbumMutations
    {
        public async Task<CreateAlbumPayload> CreateAlbumAsync(
            CreateAlbumInput input,
            [Service] AlbumService albumService,
            [CurrentUserGlobalState] CurrentUser currentUser,
            CancellationToken cancellationToken
        )
        {
            var album = new Album
            {
                Name = input.Name,
                MBid = input.MBid,
                User = currentUser.UserId,
                Performer = input.Performer,
                Year = input.Year,
            };

            await albumService.CreateAsync(album, cancellationToken);

            return new CreateAlbumPayload(album);
        }

        public async Task<UpdateAlbumPayload> UpdateAlbumAsync(
            UpdateAlbumInput input,
            [Service] AlbumService albumService,
            [CurrentUserGlobalState] CurrentUser currentUser,
            CancellationToken cancellationToken
        )
        {
            var currentAlbum = await albumService.GetAsync(input.Id, cancellationToken);
            if (currentAlbum?.User != currentUser.UserId)
            {
                return new UpdateAlbumPayload(new List<UserError> {
                    new NoAccessError()
                });
            }

            var album = new Album
            {
                Id = input.Id,
                Name = input.Name,
                MBid = input.MBid,
                Performer = input.Performer,
                UpdatedAt = DateTime.Now,
                Year = input.Year,
                CreatedAt = currentAlbum.CreatedAt,
                User = currentAlbum.User,
            };

            await albumService.UpdateAsync(album, cancellationToken);

            return new UpdateAlbumPayload(album);
        }

        public async Task<DeletePayload> DeleteAlbumAsync(
            DeleteAlbumInput input,
            [Service] AlbumService albumService,
            [CurrentUserGlobalState] CurrentUser currentUser,
            CancellationToken cancellationToken
        )
        {
            var album = await albumService.GetAsync(input.Id, cancellationToken);

            if (album?.User != currentUser.UserId)
            {
                return new DeletePayload(new List<UserError> {
                    new NoAccessError()
                });
            }

            var result = await albumService.DeleteAsync(input.Id, cancellationToken);
            return new DeletePayload(result == null ? false : result.DeletedCount > 0);
        }
    }
}