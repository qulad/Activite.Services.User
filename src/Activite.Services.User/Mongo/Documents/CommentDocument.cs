using System;
using Convey.Types;

namespace Activite.Services.User.Mongo.Documents;

public class CommentDocument : IIdentifiable<Guid>
{
    public const string CollectionName = "Comments";

    public Guid Id { get; set; }

    public Guid EventId { get; set; }

    public string Content { get; set; }

    public string Type { get; set; }

    public DateTimeOffset CreatedAt { get; set; }

    public DateTimeOffset? UpdatedAt { get; set; }
}