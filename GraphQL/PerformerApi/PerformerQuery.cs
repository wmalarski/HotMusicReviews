using HotChocolate;
using HotMusicReviews.Models;
using HotMusicReviews.Services;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotMusicReviews.GraphQL.PerformerApi
{
    public class PerformerQuery
    {
        public List<Performer> GetPerformers([Service] PerformerService performerService) => performerService.Get();
    }
}