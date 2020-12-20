using System;
using System.Collections.Generic;
using HotMusicReviews.GraphQL.Common;
using HotMusicReviews.Models;

namespace HotMusicReviews.GraphQL.Performers
{

    public record CreatePerformerReviewInput(
        string Text,
        decimal Rating,
        DateTime CreatedAt,
        DateTime UpdatedAt
    );

    public record CreatePerformerAlbumInput(
        string MBid,
        string Name,
        int Year,
        List<CreatePerformerReviewInput>? reviews
    );

    public record CreatePerformerInput(
        string MBid,
        string Name,
        List<CreatePerformerAlbumInput> Albums
    );

    public class CreatePerformerPayload : PerformerPayloadBase
    {
        public CreatePerformerPayload(Performer performer) : base(performer)
        {
        }

        public CreatePerformerPayload(IReadOnlyList<UserError> errors) : base(errors)
        {
        }
    }
}