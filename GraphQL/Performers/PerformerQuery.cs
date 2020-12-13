using HotChocolate;
using HotChocolate.Data;
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
        [UsePaging(typeof(NonNullType<PerformerType>))]
        [UseFiltering]
        [UseSorting]
        public IEnumerable<Performer> GetPerformers(
            [Service] PerformerService performerService
        ) => performerService.Get();

        public Task<Performer?> GetPerformerAsync(
            [ID(nameof(Performer))] string id,
            [Service] PerformerService performerService,
            CancellationToken cancellationToken
        ) =>
            performerService.GetAsync(id, cancellationToken);
    }
}