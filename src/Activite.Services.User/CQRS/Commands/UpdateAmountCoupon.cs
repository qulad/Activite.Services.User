using System;
using Activite.Services.User.Attributes;
using Convey.CQRS.Commands;

namespace Activite.Services.User.CQRS.Commands;

[Contract]
public class UpdateAmountCoupon : ICommand
{
    public Guid Id { get; set; }

    public DateTimeOffset UsedAt { get; set; }

    public UpdateAmountCoupon(Guid id, DateTimeOffset usedAt)
    {
        Id = id;
        UsedAt = usedAt;
    }
}