namespace HotMusicReviews.Models
{
    public class AuthSettings : IAuthSettings
    {
        public string Authority { get; set; } = default!;
        public string Audience { get; set; } = default!;
    }

    public interface IAuthSettings
    {
        string Authority { get; set; }
        string Audience { get; set; }
    }
}
