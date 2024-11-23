using System;
using Activite.Services.User.Attributes;
using Convey.CQRS.Commands;

namespace Activite.Services.User.CQRS.Commands;

[Contract]
public class AddAmountCoupon : ICommand
{
    public Guid Id { get; set; }

    public string Description { get; set; }

    public string Currency { get; set; }

    public string Name { get; set; }

    public decimal MinimalSpendingAmount { get; set; }

    public decimal Amount { get; set; }

    public AddAmountCoupon(
        Guid id,
        string description,
        string currency,
        string name,
        decimal minimalSpendingAmount,
        decimal amount)
    {
        Id = id;
        Description = description;
        Currency = currency;
        Name = name;
        MinimalSpendingAmount = minimalSpendingAmount;
        Amount = amount;
    }
}