using System;
using Activite.Services.User.DTOs;
using Convey.CQRS.Queries;

namespace Activite.Services.User.CQRS.Queries;

public class GetAmountCoupon : IQuery<AmountCouponDto>
{
    public Guid Id { get; set; }

    public GetAmountCoupon(Guid id)
    {
        Id = id;
    }
}