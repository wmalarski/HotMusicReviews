using HotChocolate;
using HotChocolate.Types;
using HotChocolate.Types.Relay;
using HotMusicReviews.Models;
using HotMusicReviews.Services;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace HotMusicReviews.GraphQL.Performers
{

    [ExtendObjectType(Name = "Query")]
    public class PerformerQuery
    {
        public Task<List<Performer>> GetPerformersAsync(
            [Service] PerformerService performerService,
            CancellationToken cancellationToken
        ) => performerService.GetAsync(cancellationToken);

        public Task<Performer?> GetPerformerAsync(
            [ID(nameof(Performer))] string id,
            [Service] PerformerService performerService,
            CancellationToken cancellationToken
        ) =>
            performerService.GetAsync(id, cancellationToken);
    }
}