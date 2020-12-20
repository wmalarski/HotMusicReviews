using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using GreenDonut;
using HotChocolate.DataLoader;
using HotMusicReviews.Models;
using HotMusicReviews.Services;

namespace HotMusicReviews.GraphQL.Albums
{
    public class AlbumByIdDataLoader : BatchDataLoader<string, Album>
    {
        private readonly AlbumService _albumService;

        public AlbumByIdDataLoader(
            IBatchScheduler batchScheduler,
            AlbumService albumService)
            : base(batchScheduler)
        {
            _albumService = albumService;
        }

        protected override async Task<IReadOnlyDictionary<string, Album>> LoadBatchAsync(
            IReadOnlyList<string> keys,
            CancellationToken cancellationToken)
        {
            var albums = await _albumService.GetAsync(keys, cancellationToken);
            return albums.ToDictionary(album => album.Id);
        }
    }
}