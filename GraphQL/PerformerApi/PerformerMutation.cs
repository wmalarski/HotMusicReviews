using System.Threading.Tasks;
using HotChocolate;
using HotMusicReviews.Models;
using HotMusicReviews.Services;

namespace HotMusicReviews.GraphQL.PerformerApi
{
    public class PerformerMutation
    {
        public async Task<AddPerformerPayload> AddPerformerAsync(
            AddPerformerInput input,
            [Service] PerformerService performerService)
        {
            var performer = new Performer
            {
                Name = input.Name,
                MBid = input.MBid,
                User = "TODO",
            };

            await performerService.Create(performer);

            return new AddPerformerPayload(performer);
        }
    }
}