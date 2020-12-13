using System.Collections.Generic;
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
                .UsePaging()
                .UseFiltering()
                .ResolveWith<PerformerResolvers>(t => t.GetAlbums(default!, default!));
        }

        private class PerformerResolvers
        {
            public User? GetUser(Performer performer)
            {
                return performer.User == null ? null : new User(performer.User);
            }

            public IEnumerable<Album> GetAlbums(
                Performer performer,
                [Service] AlbumService albumService
            )
            {
                return albumService.GetByPerformer(performer.Id);
            }
        }
    }
}