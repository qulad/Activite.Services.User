using System;
using System.Collections.Generic;
using Convey.Types;

namespace Activite.Services.User.Mongo.Documents;

public class OfferDocument : IIdentifiable<Guid>
{
    public const string CollectionName = "Offers";

    public Guid Id { get; set; }

    public Guid LocationId { get; set; }

    public IList<Guid> VisualMediaIds { get; set; }

    public string Name { get; set; }

    public string Description { get; set; }

    public Guid CreatedBy { get; set; }

    public DateTimeOffset CreatedAt { get; set; }

    public Guid? UpdatedBy { get; set; }

    public DateTimeOffset? UpdatedAt { get; set; }
}