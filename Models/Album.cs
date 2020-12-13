using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace HotMusicReviews.Models
{
    public class Album
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } = default!;
        public string MBid { get; set; } = default!;
        public string Name { get; set; } = default!;
        public string Performer { get; set; } = default!;
        public int Year { get; set; }
        public string? User { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;

    }
}
