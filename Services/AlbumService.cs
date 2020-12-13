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
        public async Task<List<Album>> GetAsync(CancellationToken cancellationToken)
        {
            var albums = await _albums.FindAsync(_ => true, null, cancellationToken);
            return await albums.ToListAsync(cancellationToken);
        }

        public async Task<Album?> GetAsync(string id, CancellationToken cancellationToken)
        {
            var albums = await _albums.FindAsync(album => album.Id == id, null, cancellationToken);
            return await albums.FirstAsync(cancellationToken);
        }

        public async Task<List<Album>> GetByUserAsync(string user, CancellationToken cancellationToken)
        {
            var albums = await _albums.FindAsync(album => album.User == user, null, cancellationToken);
            return await albums.ToListAsync(cancellationToken);
        }

        public async Task<List<Album>> GetByPerformerAsync(string performer, CancellationToken cancellationToken)
        {
            var albums = await _albums.FindAsync(album => album.Performer == performer, null, cancellationToken);
            return await albums.ToListAsync(cancellationToken);
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