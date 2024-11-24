using System;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Activite.Services.User.DTOs;
using Activite.Services.User.Mongo.Documents;
using Convey.CQRS.Queries;
using Convey.Persistence.MongoDB;

namespace Activite.Services.User.CQRS.Queries.Handlers;

public class GetMultiplePercentageCouponsHandler : IQueryHandler<GetMultiplePercentageCoupons, PagedResult<PercentageCouponDto>>
{
    private readonly IMongoRepository<PercentageCouponDocument, Guid> _repository;

    public GetMultiplePercentageCouponsHandler(IMongoRepository<PercentageCouponDocument, Guid> repository)
    {
        _repository = repository;
    }

    public async Task<PagedResult<PercentageCouponDto>> HandleAsync(GetMultiplePercentageCoupons query, CancellationToken cancellationToken = default)
    {
        var predicate = GetPredicate(query);
        
        var percentageCoupons = await _repository.BrowseAsync(predicate, query);

        if (percentageCoupons is null || percentageCoupons.IsEmpty)
        {
            return PagedResult<PercentageCouponDto>.Empty;
        }

        return percentageCoupons.Map(percentageCoupon => new PercentageCouponDto
        {
            Id = percentageCoupon.Id,
            Description = percentageCoupon.Description,
            Currency = percentageCoupon.Currency,
            Name = percentageCoupon.Name,
            Type = percentageCoupon.Type,
            MinimalSpendingAmount = percentageCoupon.MinimalSpendingAmount,
            ExpiresAt = percentageCoupon.ExpiresAt,
            UsedAt = percentageCoupon.UsedAt,
            CreatedAt = percentageCoupon.CreatedAt,
            Percentage = percentageCoupon.Percentage,
            MaxDiscountAmount = percentageCoupon.MaxDiscountAmount
        });
    }

    private static Expression<Func<PercentageCouponDocument, bool>> GetPredicate(GetMultiplePercentageCoupons query)
    {
        Expression<Func<PercentageCouponDocument, bool>> expression = null;

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
        
        if (query.Percentage.HasValue)
        {
            expression = expression.And(amountCoupon => amountCoupon.Percentage == query.Percentage);
        }

        if (query.PercentageFrom.HasValue)
        {
            expression = expression.And(amountCoupon => amountCoupon.Percentage >= query.PercentageFrom);
        }

        if (query.PercentageTo.HasValue)
        {
            expression = expression.And(amountCoupon => amountCoupon.Percentage <= query.PercentageTo);
        }

        if (query.MaxDiscountAmount.HasValue)
        {
            expression = expression.And(amountCoupon => amountCoupon.MaxDiscountAmount == query.MaxDiscountAmount);
        }

        if (query.MaxDiscountAmountFrom.HasValue)
        {
            expression = expression.And(amountCoupon => amountCoupon.MaxDiscountAmount >= query.MaxDiscountAmountFrom);
        }

        if (query.MaxDiscountAmountTo.HasValue)
        {
            expression = expression.And(amountCoupon => amountCoupon.MaxDiscountAmount <= query.MaxDiscountAmountTo);
        }

        return expression;
    }
}