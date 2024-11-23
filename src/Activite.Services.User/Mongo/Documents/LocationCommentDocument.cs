using System;

namespace Activite.Services.User.Mongo.Documents;

public class LocationCommentDocument : CommentDocument
{
    public Guid LocationId { get; set; }

    public Guid CustomerCommentId { get; set; }
}