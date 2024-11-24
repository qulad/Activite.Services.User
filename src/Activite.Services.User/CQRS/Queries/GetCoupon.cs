using System;
using Activite.Services.User.DTOs;
using Convey.CQRS.Queries;

namespace Activite.Services.User.CQRS.Queries;

public class GetCoupon : IQuery<CouponDto>
{
    public Guid Id { get; set; }

    public GetCoupon(Guid id)
    {
        Id = id;
    }
}