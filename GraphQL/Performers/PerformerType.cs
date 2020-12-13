using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using HotChocolate;
using HotChocolate.Types;
using HotMusicReviews.GraphQL.Users;
using HotMusicReviews.Models;
using HotMusicReviews.Services;

namespace HotMusicReviews.GraphQL.Performers
{
    public class PerformerType : ObjectType<Performer>
    {
        protected override void Configure(IObjectTypeDescriptor<Performer> descriptor)
        {
            descriptor
                .ImplementsNode()
                .IdField(t => t.Id)
                .ResolveNode((ctx, id) => ctx.Service<PerformerService>().GetAsync(id, default!)!);

            descriptor
                .Field(t => t.User)
                .Name("user")
                .ResolveWith<PerformerResolvers>(t => t.GetUser(default!));
        }

        private class PerformerResolvers
        {
            public UserRecord? GetUser(Performer performer)
            {
                return performer.User == null ? null : new UserRecord(performer.User);
            }
        }
    }
}