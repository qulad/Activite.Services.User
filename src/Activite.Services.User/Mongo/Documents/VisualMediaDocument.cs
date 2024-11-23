using System;
using Convey.Types;

namespace Activite.Services.User.Mongo.Documents;

public class VisualMediaDocument : IIdentifiable<Guid>
{
    public const string CollectionName = "VisualMedias";

    public Guid Id { get; set; }

    public string Type { get; set; }

    public string Content { get; set; }

    public DateTimeOffset CreatedAt { get; set; }

    public DateTimeOffset? UpdatedAt { get; set; }
}