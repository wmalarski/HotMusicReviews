using System.Threading;
using System.Threading.Tasks;
using HotChocolate;
using HotChocolate.Types;
using HotMusicReviews.Models;
using HotMusicReviews.Services;
using HotMusicReviews.GraphQL.Common;
using System;

namespace HotMusicReviews.GraphQL.Albums
{
    [ExtendObjectType(Name = "Mutation")]
    public class AlbumMutations
    {
        public async Task<UpdateAlbumPayload> CreateAlbumAsync(
            UpdateAlbumInput input,
            [Service] AlbumService albumService,
            CancellationToken cancellationToken
        )
        {
            var album = new Album
            {
                Name = input.Name,
                MBid = input.MBid,
                User = "TODO",
            };

            await albumService.UpdateAsync(album, cancellationToken);

            return new UpdateAlbumPayload(album);
        }

        public async Task<UpdateAlbumPayload> UpdateAlbumAsync(
            UpdateAlbumInput input,
            [Service] AlbumService albumService,
            CancellationToken cancellationToken
        )
        {
            var album = new Album
            {
                Id = input.Id,
                Name = input.Name,
                MBid = input.MBid,
                Performer = input.Performer,
                UpdatedAt = DateTime.Now
            };

            await albumService.UpdateAsync(album, cancellationToken);

            return new UpdateAlbumPayload(album);
        }

        public async Task<DeletePayload> DeleteAlbumAsync(
            DeleteAlbumInput input,
            [Service] AlbumService albumService,
            CancellationToken cancellationToken
        )
        {
            var result = await albumService.DeleteAsync(input.Id, cancellationToken);
            return new DeletePayload(result == null ? false : result.DeletedCount > 0);
        }
    }
}