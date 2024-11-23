using System;
using System.Collections.Generic;
using Activite.Services.User.Attributes;
using Convey.CQRS.Commands;

namespace Activite.Services.User.CQRS.Commands;

[Contract]
public class UpdateOffer : ICommand
{
    public Guid Id { get; set; }

    public Guid LocationId { get; set; }

    public IList<Guid> VisualMediaIds { get; set; }

    public string Name { get; set; }

    public string Description { get; set; }

    public UpdateOffer(
        Guid id,
        Guid locationId,
        IList<Guid> visualMediaIds,
        string name,
        string description)
    {
        Id = id;
        LocationId = locationId;
        VisualMediaIds = visualMediaIds;
        Name = name;
        Description = description;
    }
}