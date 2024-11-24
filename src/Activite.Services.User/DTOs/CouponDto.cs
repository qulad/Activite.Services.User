using System;

namespace Activite.Services.User.DTOs;

public class CouponDto
{
    public Guid Id { get; set; }

    public string Description { get; set; }

    public string Currency { get; set; }

    public string Name { get; set; }

    public string Type { get; set; }

    public decimal MinimalSpendingAmount { get; set; }

    public DateTimeOffset ExpiresAt { get; set; }

    public DateTimeOffset? UsedAt { get; set; }

    public DateTimeOffset CreatedAt { get; set; }
}