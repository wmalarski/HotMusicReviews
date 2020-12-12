using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace HotMusicReviews.Models
{
    public class Album
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string MBid { get; set; }
        public string Name { get; set; }
        public string Performer { get; set; }
        public int Year { get; set; }
        public string User { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

    }
}
