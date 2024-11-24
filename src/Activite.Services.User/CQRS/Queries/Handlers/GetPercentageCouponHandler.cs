using System;
using System.Threading;
using System.Threading.Tasks;
using Activite.Services.User.DTOs;
using Activite.Services.User.Mongo.Documents;
using Convey.CQRS.Queries;
using Convey.Persistence.MongoDB;

namespace Activite.Services.User.CQRS.Queries.Handlers;

public class GetPercentageCouponHandler : IQueryHandler<GetPercentageCoupon, PercentageCouponDto>
{
    private readonly IMongoRepository<PercentageCouponDocument, Guid> _repository;

    public GetPercentageCouponHandler(IMongoRepository<PercentageCouponDocument, Guid> repository)
    {
        _repository = repository;
    }

    public async Task<PercentageCouponDto> HandleAsync(GetPercentageCoupon query, CancellationToken cancellationToken = default)
    {
        var coupon = await _repository.GetAsync(query.Id);

        if (coupon is null)
        {
            return new PercentageCouponDto();
        }

        return new PercentageCouponDto
        {
            Id = coupon.Id,
            Description = coupon.Description,
            Currency = coupon.Currency,
            Name = coupon.Name,
            Type = coupon.Type,
            MinimalSpendingAmount = coupon.MinimalSpendingAmount,
            ExpiresAt = coupon.ExpiresAt,
            UsedAt = coupon.UsedAt,
            CreatedAt = coupon.CreatedAt,
            Percentage = coupon.Percentage,
            MaxDiscountAmount = coupon.MaxDiscountAmount
        };
    }
}
