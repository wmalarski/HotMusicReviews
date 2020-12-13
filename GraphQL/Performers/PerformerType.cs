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

            descriptor
                .Field("albums")
                .ResolveWith<PerformerResolvers>(t => t.GetAlbumsAsync(default!, default!, default!));
        }

        private class PerformerResolvers
        {
            public User? GetUser(Performer performer)
            {
                return performer.User == null ? null : new User(performer.User);
            }

            public Task<List<Album>> GetAlbumsAsync(
                Performer performer,
                [Service] AlbumService albumService,
                CancellationToken cancellationToken
            )
            {
                return albumService.GetByPerformerAsync(performer.Id, cancellationToken);
            }
        }
    }
}