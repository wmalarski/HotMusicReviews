using System.Text.Json.Serialization;

namespace HotMusicReviews.Models
{

    public class ImageDao
    {
        [JsonPropertyName("#text")]
        public string? text { get; set; }

        public string? size { get; set; }
    }

    public class WikiDao
    {
        public string? published { get; set; }

        public string summary { get; set; } = "";

        public string content { get; set; } = "";

    }

    public class AlbumDao
    {
        public string? url { get; set; }

        public ImageDao[] image { get; set; } = default!;

        public WikiDao? wiki { get; set; }
    }

    public class AlbumResultDao
    {
        public AlbumDao? album { get; set; }

    }

    public class AlbumSearchDao
    {
        public string name { get; set; } = "";

        public string artist { get; set; } = "";

        public ImageDao[] image { get; set; } = default!;

        public string? mbid { get; set; }
    }

    public class AlbumsSearchDao
    {
        public AlbumSearchDao[] album { get; set; } = default!;
    }

    public class AlbumsSearchResultDao
    {
        public AlbumsSearchDao? albummatches { get; set; }
    }

    public class AlbumsSearchResultsDao
    {
        public AlbumsSearchResultDao? results { get; set; }
    }

    public class ArtistCorrectionDao
    {
        public string? name { get; set; }
        public string? mbid { get; set; }
        public string? url { get; set; }
    }

    public class CorrectionDao
    {
        public ArtistCorrectionDao? artist { get; set; }
    }

    public class CorrectionsDao
    {
        public CorrectionDao? correction { get; set; }
    }

    public class CorrectionsResultDao
    {
        public CorrectionsDao? corrections { get; set; }
    }

}