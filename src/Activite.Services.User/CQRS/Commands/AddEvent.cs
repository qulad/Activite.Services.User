using System;
using System.Collections.Generic;
using Activite.Services.User.Attributes;
using Convey.CQRS.Commands;

namespace Activite.Services.User.CQRS.Commands;

[Contract]
public class AddEvent : ICommand
{
    public Guid Id { get; set; }

    public Guid LocationId { get; set; }

    public Guid? AgeRestrictionId { get; set; }

    public Guid? OfferId { get; set; }

    public IList<Guid> VisualMediaIds { get; set; }

    public string Name { get; set; }

    public string Description { get; set; }

    public decimal Amount { get; set; }

    public string Currency { get; set; }

    public DateTimeOffset DateFrom { get; set; }

    public DateTimeOffset DateTo { get; set; }

    public AddEvent(
        Guid id,
        Guid locationId,
        Guid? ageRestrictionId,
        Guid? offerId,
        IList<Guid> visualMediaIds,
        string name,
        string description,
        decimal amount,
        string currency,
        DateTimeOffset dateFrom,
        DateTimeOffset dateTo)
    {
        Id = id;
        LocationId = locationId;
        AgeRestrictionId = ageRestrictionId;
        OfferId = offerId;
        VisualMediaIds = visualMediaIds;
        Name = name;
        Description = description;
        Amount = amount;
        Currency = currency;
        DateFrom = dateFrom;
        DateTo = dateTo;
    }
}