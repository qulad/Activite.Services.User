using System;
using Convey.Types;

namespace Activite.Services.User.Mongo.Documents;

public class AgeRestrictionDocument : IIdentifiable<Guid>
{
    public const string CollectionName = "AgeRestrictions";

    public Guid Id { get; set; }

    public string Code { get; set; }

    public int Age { get; set; }

    public DateTimeOffset CreatedAt { get; set; }

    public DateTimeOffset? UpdatedAt { get; set; }
}