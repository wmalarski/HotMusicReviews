using HotMusicReviews.Models;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace HotMusicReviews.Services
{
    public class AlbumService
    {
        private readonly IMongoCollection<Album> _albums;

        public AlbumService(IMongoCollection<Album> albums)
        {
            _albums = albums;
        }
        public IEnumerable<Album> Get() =>
            _albums.Find(_ => true).ToEnumerable();

        public async Task<Album?> GetAsync(string id, CancellationToken cancellationToken)
        {
            var albums = await _albums.FindAsync(album => album.Id == id, null, cancellationToken);
            return await albums.FirstOrDefaultAsync(cancellationToken);
        }

        public async Task<IEnumerable<Album>> GetAsync(IReadOnlyList<string> keys, CancellationToken cancellationToken)
        {
            var albums = await _albums.FindAsync(album => keys.Contains(album.Id), null, cancellationToken);
            return albums.ToEnumerable(cancellationToken);
        }

        public IEnumerable<Album> GetRandom(int sample, IReadOnlyList<string> excludeAlbums)
        {
            var albums = _albums.AsQueryable().Where(album => !excludeAlbums.Contains(album.Id)).Sample(sample);
            return albums.ToEnumerable();
        }

        public IEnumerable<Album> GetByUser(string user) =>
            _albums.Find(album => album.User == user).ToEnumerable();

        public IEnumerable<Album> GetByPerformer(string performer) =>
            _albums.Find(album => album.Performer == performer).ToEnumerable();

        public async Task<Album?> GetByMBidAsync(string mBid, CancellationToken cancellationToken)
        {
            var albums = await _albums.FindAsync(album => album.MBid == mBid, null, cancellationToken);
            return await albums.FirstOrDefaultAsync(cancellationToken);
        }

        public async Task<Album> CreateAsync(Album album, CancellationToken cancellationToken)
        {
            await _albums.InsertOneAsync(album, null, cancellationToken);
            return album;
        }

        public async Task<ReplaceOneResult?> UpdateAsync(Album albumInput, CancellationToken cancellationToken) =>
            await _albums.ReplaceOneAsync(book => book.Id == albumInput.Id, albumInput, new ReplaceOptions(), cancellationToken);

        public async Task<DeleteResult?> DeleteAsync(string id, CancellationToken cancellationToken) =>
            await _albums.DeleteOneAsync(book => book.Id == id, cancellationToken);
    }
}