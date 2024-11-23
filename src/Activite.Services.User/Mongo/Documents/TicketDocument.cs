using System;
using Convey.Types;

namespace Activite.Services.User.Mongo.Documents;

public class TicketDocument : IIdentifiable<Guid>
{
    public const string CollectionName = "Tickets";

    public Guid Id { get; set; }

    public Guid CustomerId { get; set; }

    public Guid EventId { get; set; }

    public Guid? CouponId { get; set; }

    public decimal Amount { get; set; }

    public string Currency { get; set; }

    public DateTimeOffset CreatedAt { get; set; }
}