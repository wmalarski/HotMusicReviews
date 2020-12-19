using HotMusicReviews.Models;
using MongoDB.Driver;
using System.Collections.Generic;
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
        public IEnumerable<Album> Get()
        {
            return _albums.Find(_ => true).ToEnumerable();
        }

        public async Task<Album?> GetAsync(string id, CancellationToken cancellationToken)
        {
            var albums = await _albums.FindAsync(album => album.Id == id, null, cancellationToken);
            return await albums.FirstAsync(cancellationToken);
        }

        public IEnumerable<Album> GetByUser(string user)
        {
            return _albums.Find(album => album.User == user).ToEnumerable();
        }

        public IEnumerable<Album> GetByPerformer(string performer)
        {
            return _albums.Find(album => album.Performer == performer).ToEnumerable();
        }

        public async Task<Album?> GetByMBidAsync(string mBid, CancellationToken cancellationToken)
        {
            var albums = await _albums.FindAsync(album => album.MBid == mBid, null, cancellationToken);
            return await albums.FirstAsync(cancellationToken);
        }

        public async Task<Album> CreateAsync(Album album, CancellationToken cancellationToken)
        {
            await _albums.InsertOneAsync(album, null, cancellationToken);
            return album;
        }

        public async Task<ReplaceOneResult?> UpdateAsync(Album albumInput, CancellationToken cancellationToken)
        {
            return await _albums.ReplaceOneAsync(book => book.Id == albumInput.Id, albumInput, new ReplaceOptions(), cancellationToken);
        }

        public async Task<DeleteResult?> DeleteAsync(string id, CancellationToken cancellationToken)
        {
            return await _albums.DeleteOneAsync(book => book.Id == id, cancellationToken);
        }
    }
}