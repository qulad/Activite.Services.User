using System;
using System.Threading;
using System.Threading.Tasks;
using Activite.Services.User.DTOs;
using Activite.Services.User.Mongo.Documents;
using Convey.CQRS.Queries;
using Convey.Persistence.MongoDB;

namespace Activite.Services.User.CQRS.Queries.Handlers;

public class GetAmountCouponHandler : IQueryHandler<GetAmountCoupon, AmountCouponDto>
{
    private readonly IMongoRepository<AmountCouponDocument, Guid> _repository;

    public GetAmountCouponHandler(IMongoRepository<AmountCouponDocument, Guid> repository)
    {
        _repository = repository;
    }

    public async Task<AmountCouponDto> HandleAsync(GetAmountCoupon query, CancellationToken cancellationToken = default)
    {
        var coupon = await _repository.GetAsync(query.Id);

        if (coupon is null)
        {
            return new AmountCouponDto();
        }

        return new AmountCouponDto
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
            Amount = coupon.Amount
        };
    }
}
