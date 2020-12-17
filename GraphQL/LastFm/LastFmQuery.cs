using HotChocolate;
using HotChocolate.Types;
using HotMusicReviews.Models;
using HotMusicReviews.Services;
using System.Threading.Tasks;

namespace HotMusicReviews.GraphQL.LastFm
{

    public record SearchAlbumsInput(
        string album,
        int page,
        int limit
    );

    public record SearchArtistInput(
        string artist,
        int page,
        int limit
    );

    public record ArtistCorrectionInput(
        string artist
    );

    [ExtendObjectType(Name = "Query")]
    public class LastFmQuery
    {

        public async Task<AlbumSearchDao[]?> SearchAlbumsAsync(
            SearchAlbumsInput input,
            [Service] LastFmService lastFmService
        ) =>
            await lastFmService.SearchAlbums(input.album, input.limit, input.page);

        public async Task<ArtistSearchDao[]?> SearchArtistsAsync(
            SearchArtistInput input,
            [Service] LastFmService lastFmService
        ) =>
            await lastFmService.SearchArtist(input.artist, input.limit, input.page);


        public async Task<ArtistCorrectionDao?> ArtistCorrectionAsync(
            ArtistCorrectionInput input,
            [Service] LastFmService lastFmService
        ) =>
            await lastFmService.ArtistCorrection(input.artist);

    }
}