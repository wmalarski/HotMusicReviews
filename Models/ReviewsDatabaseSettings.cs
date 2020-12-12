namespace HotMusicReviews.Models
{
    public class ReviewsDatabaseSettings : IReviewsDatabaseSettings
    {
        public string ReviewsCollectionName { get; set; }
        public string PerformersCollectionName { get; set; }
        public string AlbumsCollectionName { get; set; }
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
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
