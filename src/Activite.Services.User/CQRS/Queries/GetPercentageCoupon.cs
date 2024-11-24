using System;
using Activite.Services.User.DTOs;
using Convey.CQRS.Queries;

namespace Activite.Services.User.CQRS.Queries;

public class GetPercentageCoupon : IQuery<PercentageCouponDto>
{
    public Guid Id { get; set; }

    public GetPercentageCoupon(Guid id)
    {
        Id = id;
    }
}