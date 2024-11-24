using System;

namespace Activite.Services.User.DTOs;

public class WalletDto
{
    public Guid Id { get; set; }

    public string Currency { get; set; }

    public string Type { get; set; }

    public decimal Amount { get; set; }

    public DateTimeOffset CreatedAt { get; set; }

    public DateTimeOffset? UpdatedAt { get; set; }
}