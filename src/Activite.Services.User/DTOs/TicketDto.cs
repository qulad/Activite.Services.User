using System;

namespace Activite.Services.User.DTOs;

public class TicketDto
{
    public Guid Id { get; set; }

    public Guid CustomerId { get; set; }

    public Guid EventId { get; set; }

    public Guid? CouponId { get; set; }

    public decimal Amount { get; set; }

    public string Currency { get; set; }

    public DateTimeOffset CreatedAt { get; set; }
}