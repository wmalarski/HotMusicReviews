namespace HotMusicReviews.Models
{
    public class ReviewsDatabaseSettings : IReviewsDatabaseSettings
    {
        public string ReviewsCollectionName { get; set; } = default!;
        public string PerformersCollectionName { get; set; } = default!;
        public string AlbumsCollectionName { get; set; } = default!;
        public string ConnectionString { get; set; } = default!;
        public string DatabaseName { get; set; } = default!;
    }

    public interface IReviewsDatabaseSettings
    {
        string ReviewsCollectionName { get; set; }
        string PerformersCollectionName { get; set; }
        string AlbumsCollectionName { get; set; }
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
    }
}
