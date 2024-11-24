using System;
using System.Threading;
using System.Threading.Tasks;
using Activite.Services.User.DTOs;
using Activite.Services.User.Mongo.Documents;
using Convey.CQRS.Queries;
using Convey.Persistence.MongoDB;

namespace Activite.Services.User.CQRS.Queries.Handlers;

public class GetCouponHandler : IQueryHandler<GetCoupon, CouponDto>
{
    private readonly IMongoRepository<CouponDocument, Guid> _repository;

    public GetCouponHandler(IMongoRepository<CouponDocument, Guid> repository)
    {
        _repository = repository;
    }

    public async Task<CouponDto> HandleAsync(GetCoupon query, CancellationToken cancellationToken = default)
    {
        var coupon = await _repository.GetAsync(query.Id);

        if (coupon is null)
        {
            return new CouponDto();
        }

        return new CouponDto
        {
            Id = coupon.Id,
            Description = coupon.Description,
            Currency = coupon.Currency,
            Name = coupon.Name,
            Type = coupon.Type,
            MinimalSpendingAmount = coupon.MinimalSpendingAmount,
            ExpiresAt = coupon.ExpiresAt,
            UsedAt = coupon.UsedAt,
            CreatedAt = coupon.CreatedAt
        };
    }
}
