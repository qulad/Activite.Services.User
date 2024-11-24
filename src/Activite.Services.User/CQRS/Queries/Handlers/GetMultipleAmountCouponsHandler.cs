using System;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Activite.Services.User.DTOs;
using Activite.Services.User.Mongo.Documents;
using Convey.CQRS.Queries;
using Convey.Persistence.MongoDB;

namespace Activite.Services.User.CQRS.Queries.Handlers;

public class GetMultipleAmountCouponsHandler : IQueryHandler<GetMultipleAmountCoupons, PagedResult<AmountCouponDto>>
{
    private readonly IMongoRepository<AmountCouponDocument, Guid> _repository;

    public GetMultipleAmountCouponsHandler(IMongoRepository<AmountCouponDocument, Guid> repository)
    {
        _repository = repository;
    }

    public async Task<PagedResult<AmountCouponDto>> HandleAsync(GetMultipleAmountCoupons query, CancellationToken cancellationToken = default)
    {
        var predicate = GetPredicate(query);
        
        var amountCoupons = await _repository.BrowseAsync(predicate, query);

        if (amountCoupons is null || amountCoupons.IsEmpty)
        {
            return PagedResult<AmountCouponDto>.Empty;
        }

        return amountCoupons.Map(amountCoupon => new AmountCouponDto
        {
            Id = amountCoupon.Id,
            Description = amountCoupon.Description,
            Currency = amountCoupon.Currency,
            Name = amountCoupon.Name,
            Type = amountCoupon.Type,
            MinimalSpendingAmount = amountCoupon.MinimalSpendingAmount,
            ExpiresAt = amountCoupon.ExpiresAt,
            UsedAt = amountCoupon.UsedAt,
            CreatedAt = amountCoupon.CreatedAt,
            Amount = amountCoupon.Amount
        });
    }

    private static Expression<Func<AmountCouponDocument, bool>> GetPredicate(GetMultipleAmountCoupons query)
    {
        Expression<Func<AmountCouponDocument, bool>> expression = null;

        if (query.Id.HasValue)
        {
            expression = expression.And(amountCoupon => amountCoupon.Id == query.Id);
        }

        if (query.Ids is not null && query.Ids.Count > 0)
        {
            expression = expression.And(amountCoupon => query.Ids.Contains(amountCoupon.Id));
        }

        if (!string.IsNullOrEmpty(query.Description))
        {
            expression = expression.And(amountCoupon => amountCoupon.Description == query.Description);
        }

        if (!string.IsNullOrEmpty(query.SearchDescription))
        {
            expression = expression.And(amountCoupon => amountCoupon.Description.Contains(query.SearchDescription));
        }

        if (!string.IsNullOrEmpty(query.Currency))
        {
            expression = expression.And(amountCoupon => amountCoupon.Currency == query.Currency);
        }

        if (!string.IsNullOrEmpty(query.Name))
        {
            expression = expression.And(amountCoupon => amountCoupon.Name == query.Name);
        }

        if (!string.IsNullOrEmpty(query.SearchName))
        {
            expression = expression.And(amountCoupon => amountCoupon.Name.Contains(query.SearchName));
        }

        if (!string.IsNullOrEmpty(query.Type))
        {
            expression = expression.And(amountCoupon => amountCoupon.Type == query.Type);
        }

        if (query.MinimalSpendingAmount.HasValue)
        {
            expression = expression.And(amountCoupon => amountCoupon.MinimalSpendingAmount == query.MinimalSpendingAmount);
        }

        if (query.MinimalSpendingAmountFrom.HasValue)
        {
            expression = expression.And(amountCoupon => amountCoupon.MinimalSpendingAmount >= query.MinimalSpendingAmountFrom);
        }

        if (query.MinimalSpendingAmountTo.HasValue)
        {
            expression = expression.And(amountCoupon => amountCoupon.MinimalSpendingAmount <= query.MinimalSpendingAmountTo);
        }

        if (query.ExpiresAt.HasValue)
        {
            expression = expression.And(amountCoupon => amountCoupon.ExpiresAt == query.ExpiresAt);
        }

        if (query.ExpiresAtFrom.HasValue)
        {
            expression = expression.And(amountCoupon => amountCoupon.ExpiresAt >= query.ExpiresAtFrom);
        }

        if (query.ExpiresAtTo.HasValue)
        {
            expression = expression.And(amountCoupon => amountCoupon.ExpiresAt <= query.ExpiresAtTo);
        }

        if (query.UsedAt.HasValue)
        {
            expression = expression.And(amountCoupon => amountCoupon.UsedAt == query.UsedAt);
        }

        if (query.UsedAtFrom.HasValue)
        {
            expression = expression.And(amountCoupon => amountCoupon.UsedAt >= query.UsedAtFrom);
        }

        if (query.UsedAtTo.HasValue)
        {
            expression = expression.And(amountCoupon => amountCoupon.UsedAt <= query.UsedAtTo);
        }

        if (query.CreatedAt.HasValue)
        {
            expression = expression.And(amountCoupon => amountCoupon.CreatedAt == query.CreatedAt);
        }

        if (query.CreatedAtFrom.HasValue)
        {
            expression = expression.And(amountCoupon => amountCoupon.CreatedAt >= query.CreatedAtFrom);
        }

        if (query.CreatedAtTo.HasValue)
        {
            expression = expression.And(amountCoupon => amountCoupon.CreatedAt <= query.CreatedAtTo);
        }

        if (query.Used.HasValue)
        {
            expression = expression.And(amountCoupon => query.Used.Value ? amountCoupon.UsedAt.HasValue : !amountCoupon.UsedAt.HasValue);
        }

        if (query.Amount.HasValue)
        {
            expression = expression.And(amountCoupon => amountCoupon.Amount == query.Amount);
        }

        if (query.AmountFrom.HasValue)
        {
            expression = expression.And(amountCoupon => amountCoupon.Amount >= query.AmountFrom);
        }

        if (query.AmountTo.HasValue)
        {
            expression = expression.And(amountCoupon => amountCoupon.Amount <= query.AmountTo);
        }

        return expression;
    }
}