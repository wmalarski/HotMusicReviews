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

    public record SearchPerformerInput(
        string performer,
        int page,
        int limit
    );

    public record PerformerCorrectionInput(
        string performer
    );

    [ExtendObjectType(Name = "Query")]
    public class LastFmQuery
    {

        public async Task<AlbumSearch[]?> SearchAlbumsAsync(
            SearchAlbumsInput input,
            [Service] LastFmService lastFmService
        ) =>
            await lastFmService.SearchAlbums(input.album, input.limit, input.page);

        public async Task<PerformerSearch[]?> SearchPerformersAsync(
            SearchPerformerInput input,
            [Service] LastFmService lastFmService
        ) =>
            await lastFmService.SearchPerformer(input.performer, input.limit, input.page);


        public async Task<PerformerCorrection?> PerformerCorrectionAsync(
            PerformerCorrectionInput input,
            [Service] LastFmService lastFmService
        ) =>
            await lastFmService.PerformerCorrection(input.performer);

    }
}