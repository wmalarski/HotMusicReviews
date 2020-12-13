using System.Collections.Generic;
using HotMusicReviews.GraphQL.Common;
using HotMusicReviews.Models;

namespace HotMusicReviews.GraphQL.Albums
{
    public class AlbumPayloadBase : Payload
    {
        protected AlbumPayloadBase(Album album)
        {
            Album = album;
        }

        protected AlbumPayloadBase(IReadOnlyList<UserError> errors) : base(errors)
        {
        }

        public Album? Album { get; }
    }
}