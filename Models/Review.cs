using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace HotMusicReviews.Models
{
    public class Review
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string Album { get; set; }
        public string Text { get; set; }
        public decimal Rating { get; set; }
        public string User { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
