namespace HotMusicReviews.Models
{
    public class LastFmSettings : ILastFmSettings
    {
        public string ApiKey { get; set; } = default!;
        public string ApiUrl { get; set; } = default!;
    }

    public interface ILastFmSettings
    {
        string ApiKey { get; set; }
        string ApiUrl { get; set; }
    }
}
