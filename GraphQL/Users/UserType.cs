using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using HotChocolate;
using HotChocolate.Types;
using HotMusicReviews.Models;
using HotMusicReviews.Services;

namespace HotMusicReviews.GraphQL.Users
{

    public record UserRecord(string id);

    public class UserType : ObjectType<UserRecord>
    {
        protected override void Configure(IObjectTypeDescriptor<UserRecord> descriptor)
        {
            descriptor
                .Field("performers")
                .ResolveWith<UserResolvers>(t => t.GetPerformersAsync(default!, default!, default));

            descriptor.Field(f => f.id);
        }

        private class UserResolvers
        {
            public async Task<IEnumerable<Performer>> GetPerformersAsync(
                UserRecord record,
                [Service] PerformerService performerService,
                CancellationToken cancellationToken) =>
               await performerService.GetByUserAsync(record.id, cancellationToken);
        }
    }
}