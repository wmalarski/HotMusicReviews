using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using HotChocolate;
using HotChocolate.Types;
using HotMusicReviews.GraphQL.Users;
using HotMusicReviews.Models;

namespace HotMusicReviews.GraphQL.Performers
{
    public class PerformerType : ObjectType<Performer>
    {
        protected override void Configure(IObjectTypeDescriptor<Performer> descriptor)
        {
            descriptor
                .Field(t => t.User)
                .ResolveWith<PerformerResolvers>(t => t.GetUser(default!))
                .Name("user");
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