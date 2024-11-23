using System;

namespace Activite.Services.User.Mongo.Documents;

public class CustomerCommentDocument : CommentDocument
{
    public Guid CustomerId { get; set; }

    public int Rating { get; set; }
}