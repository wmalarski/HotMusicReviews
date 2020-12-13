using HotChocolate;
using HotChocolate.Types;
using HotMusicReviews.Models;
using HotMusicReviews.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HotMusicReviews.GraphQL.Performers
{

    [ExtendObjectType(Name = "Query")]
    public class PerformerQuery
    {
        public Task<List<Performer>> GetPerformersAsync([Service] PerformerService performerService) => performerService.GetAsync();

        public Task<Performer?> GetPerformerAsync(string id, [Service] PerformerService performerService) => performerService.GetAsync(id);
    }
}