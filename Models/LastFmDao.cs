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

    public class TagDao
    {
        public string name { get; set; } = "";
    }

    public class TagsDao
    {
        public TagDao[] tag { get; set; } = default!;
    }

    public class AlbumDao
    {
        public string? url { get; set; }

        public ImageDao[] image { get; set; } = default!;

        public WikiDao? wiki { get; set; }

        public TagsDao? tags { get; set; }
    }

    public class AlbumResultDao
    {
        public AlbumDao? album { get; set; }

    }

    public class AlbumSearchDao
    {
        public string name { get; set; } = "";

        public string? url { get; set; } = "";

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

    public class ArtistDao
    {
        public string? url { get; set; }

        public ImageDao[] image { get; set; } = default!;

        public WikiDao? bio { get; set; }

        public TagsDao? tags { get; set; }
    }

    public class ArtistResultDao
    {
        public ArtistDao? artist { get; set; }
    }

    public class ArtistSearchDao
    {
        public string name { get; set; } = "";

        public string? url { get; set; } = "";

        public ImageDao[] image { get; set; } = default!;

        public string? mbid { get; set; }
    }

    public class ArtistsSearchDao
    {
        public ArtistSearchDao[] artist { get; set; } = default!;
    }

    public class ArtistsSearchResultDao
    {
        public ArtistsSearchDao? artistmatches { get; set; }
    }

    public class ArtistsSearchResultsDao
    {
        public ArtistsSearchResultDao? results { get; set; }
    }

}