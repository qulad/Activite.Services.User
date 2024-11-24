using System;
using System.Collections.Generic;

namespace Activite.Services.User.DTOs;

public class OfferDto
{
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