using System;
using Activite.Services.User.Attributes;
using Convey.CQRS.Commands;

namespace Activite.Services.User.CQRS.Commands;

[Contract]
public class AddPercentageCoupon : ICommand
{
    public Guid Id { get; set; }

    public string Description { get; set; }

    public string Currency { get; set; }

    public string Name { get; set; }

    public decimal MinimalSpendingAmount { get; set; }

    public DateTimeOffset ExpiresAt { get; set; }

    public int Percentage { get; set; }

    public decimal MaxDiscountAmount { get; set; }

    public AddPercentageCoupon(
        Guid id,
        string description,
        string currency,
        string name,
        decimal minimalSpendingAmount,
        DateTimeOffset expiresAt,
        int percentage,
        decimal maxDiscountAmount)
    {
        Id = id;
        Description = description;
        Currency = currency;
        Name = name;
        MinimalSpendingAmount = minimalSpendingAmount;
        ExpiresAt = expiresAt;
        Percentage = percentage;
        MaxDiscountAmount = maxDiscountAmount;
    }
}