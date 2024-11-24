using System;

namespace Activite.Services.User.DTOs;

public class TransactionDto
{
    public Guid Id { get; set; }

    public Guid CustomerId { get; set; }

    public Guid LocationId { get; set; }

    public string Currency { get; set; }

    public decimal Amount { get; set; }

    public DateTimeOffset CreatedAt { get; set; }
}