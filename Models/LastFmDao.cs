using System.Text.Json.Serialization;

namespace HotMusicReviews.Models
{

    public class Image
    {
        [JsonPropertyName("#text")]
        public string? url { get; set; }

        public string? size { get; set; }
    }

    public class Wiki
    {
        public string? published { get; set; }

        public string summary { get; set; } = "";

        public string content { get; set; } = "";

    }

    public class Tag
    {
        public string name { get; set; } = "";
    }

    public class Tags
    {
        public Tag[] tag { get; set; } = default!;
    }

    public class AlbumDetails
    {

        [JsonPropertyName("mbid")]
        public string mBid { get; set; } = "";

        public Image[] image { get; set; } = default!;

        public Wiki? wiki { get; set; }

        public Tags? tags { get; set; }
    }

    public class AlbumDetailsResult
    {
        public AlbumDetails? album { get; set; }

    }

    public class AlbumSearch
    {
        public string name { get; set; } = "";

        [JsonPropertyName("artist")]
        public string performer { get; set; } = "";

        public Image[] image { get; set; } = default!;

        public string? mbid { get; set; }
    }

    public class AlbumSearchNodes
    {
        public AlbumSearch[] album { get; set; } = default!;
    }

    public class AlbumSearchMatches
    {
        public AlbumSearchNodes? albummatches { get; set; }
    }

    public class AlbumsSearchResult
    {
        public AlbumSearchMatches? results { get; set; }
    }

    public class PerformerCorrection
    {
        public string? name { get; set; }

        public string? mbid { get; set; }

    }

    public class PerformerCorrectionNode
    {
        public PerformerCorrection? artist { get; set; }
    }

    public class PerformerCorrectionEdge
    {
        public PerformerCorrectionNode? correction { get; set; }
    }

    public class PerformerCorrectionResult
    {
        public PerformerCorrectionEdge? corrections { get; set; }
    }

    public class PerformerDetails
    {
        [JsonPropertyName("mbid")]
        public string mBid { get; set; } = "";

        public Image[] image { get; set; } = default!;

        public Wiki? bio { get; set; }

        public Tags? tags { get; set; }
    }

    public class PerformerDetailsResult
    {
        public PerformerDetails? artist { get; set; }
    }

    public class PerformerSearch
    {
        public string name { get; set; } = "";

        public Image[] image { get; set; } = default!;

        public string? mbid { get; set; }
    }

    public class PerformerSearchNodes
    {
        public PerformerSearch[] artist { get; set; } = default!;
    }

    public class PerformerSearchMatches
    {
        public PerformerSearchNodes? artistmatches { get; set; }
    }

    public class PerformerSearchResult
    {
        public PerformerSearchMatches? results { get; set; }
    }

}