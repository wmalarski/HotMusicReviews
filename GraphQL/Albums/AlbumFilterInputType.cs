using HotChocolate.Data.Filters;
using HotMusicReviews.Models;

namespace HotMusicReviews.GraphQL.Albums
{
    public class AlbumFilterInputType : FilterInputType<Album>
    {
        protected override void Configure(IFilterInputTypeDescriptor<Album> descriptor)
        {
            descriptor.Ignore(t => t.Id);
            descriptor.Ignore(t => t.MBid);
            descriptor.Ignore(t => t.Performer);
            descriptor.Ignore(t => t.User);
        }
    }
}