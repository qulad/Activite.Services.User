using System;
using Convey.Types;

namespace Activite.Services.User.Mongo.Documents;

public class TranslationDocument : IIdentifiable<Guid>
{
    public const string CollectionName = "Translations";

    public Guid Id { get; set; }

    public string Code { get; set; }

    public string Content { get; set; }

    public decimal Region { get; set; }

    public Guid CreatedBy { get; set; }

    public DateTimeOffset CreatedAt { get; set; }

    public Guid? UpdatedBy { get; set; }

    public DateTimeOffset? UpdatedAt { get; set; }
}