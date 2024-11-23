using System;
using Activite.Services.User.Attributes;
using Convey.CQRS.Commands;

namespace Activite.Services.User.CQRS.Commands;

[Contract]
public class AddTicket : ICommand
{
    public Guid Id { get; set; }

    public Guid CustomerId { get; set; }

    public Guid EventId { get; set; }

    public Guid? CouponId { get; set; }

    public decimal Amount { get; set; }

    public string Currency { get; set; }

    public AddTicket(
        Guid id,
        Guid customerId,
        Guid eventId,
        Guid? couponId,
        decimal amount,
        string currency)
    {
        Id = id;
        CustomerId = customerId;
        EventId = eventId;
        CouponId = couponId;
        Amount = amount;
        Currency = currency;
    }
}