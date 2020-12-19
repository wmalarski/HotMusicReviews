using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using GreenDonut;
using HotChocolate.DataLoader;
using HotMusicReviews.Models;
using HotMusicReviews.Services;

namespace HotMusicReviews.GraphQL.LastFm
{
    public class AlbumByMBidDataLoader : BatchDataLoader<string, AlbumDetails>
    {
        private readonly LastFmService _lastFmService;

        public AlbumByMBidDataLoader(
            IBatchScheduler batchScheduler,
            LastFmService lastFmService)
            : base(batchScheduler)
        {
            _lastFmService = lastFmService;
        }

        protected override async Task<IReadOnlyDictionary<string, AlbumDetails>> LoadBatchAsync(
            IReadOnlyList<string> keys,
            CancellationToken cancellationToken)
        {
            var tasks = keys.Select(mBid => _lastFmService.GetAlbum(mBid)).ToArray();
            var results = await Task.WhenAll(tasks);
            if (results == null) return new Dictionary<string, AlbumDetails>(); 
            return results.Where(album => album != null).OfType<AlbumDetails>().ToDictionary(album => album.mBid);
        }        
    }
}