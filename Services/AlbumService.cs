using HotMusicReviews.Models;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;

namespace HotMusicReviews.Services
{
    public class AlbumService
    {
        private readonly IMongoCollection<Album> _albums;

        public AlbumService(IMongoCollection<Album> albums)
        {
            _albums = albums;
        }
        public List<Album> Get() => _albums.Find(_ => true).ToList();
    }
}