using System;
using System.Collections.Generic;
using Activite.Services.User.Constants;
using Activite.Services.User.DTOs;
using Convey.CQRS.Queries;

namespace Activite.Services.User.CQRS.Queries;

public class GetMultipleAmountCoupons : PagedQueryBase, IQuery<PagedResult<AmountCouponDto>>
{
    public Guid? Id { get; set; }

    public IList<Guid> Ids { get; set; }

    public string Description { get; set; }

    public string SearchDescription { get; set; }

    public string Currency { get; set; }

    public string Name { get; set; }

    public string SearchName { get; set; }

    public string Type { get; set; }

    public decimal? MinimalSpendingAmount { get; set; }

    public decimal? MinimalSpendingAmountFrom { get; set; }

    public decimal? MinimalSpendingAmountTo { get; set; }

    public DateTimeOffset? ExpiresAt { get; set; }

    public DateTimeOffset? ExpiresAtFrom { get; set; }

    public DateTimeOffset? ExpiresAtTo { get; set; }

    public DateTimeOffset? UsedAt { get; set; }

    public DateTimeOffset? UsedAtFrom { get; set; }

    public DateTimeOffset? UsedAtTo { get; set; }

    public DateTimeOffset? CreatedAt { get; set; }

    public DateTimeOffset? CreatedAtFrom { get; set; }

    public DateTimeOffset? CreatedAtTo { get; set; }

    public bool? Used { get; set; }

    public decimal? Amount { get; set; }

    public decimal? AmountFrom { get; set; }

    public decimal? AmountTo { get; set; }

    public GetMultipleAmountCoupons(
        Guid? id = null,
        IList<Guid> ids = null,
        string description = null,
        string searchDescription = null,
        string currency = null,
        string name = null,
        string searchName = null,
        string type = null,
        decimal? minimalSpendingAmount = null,
        decimal? minimalSpendingAmountFrom = null,
        decimal? minimalSpendingAmountTo = null,
        DateTimeOffset? expiresAt = null,
        DateTimeOffset? expiresAtFrom = null,
        DateTimeOffset? expiresAtTo = null,
        DateTimeOffset? usedAt = null,
        DateTimeOffset? usedAtFrom = null,
        DateTimeOffset? usedAtTo = null,
        DateTimeOffset? createdAt = null,
        DateTimeOffset? createdAtFrom = null,
        DateTimeOffset? createdAtTo = null,
        bool? used = null,
        decimal? amount = null,
        decimal? amountFrom = null,
        decimal? amountTo = null,
        int page = Pagination.DefaultPage,
        int results = Pagination.DefaultResults,
        string orderBy = Pagination.DefaultOrderBy,
        string sortOrder = Pagination.DefaultSortOrder
    )
    {
        Id = id;
        Ids = ids;
        Description = description;
        SearchDescription = searchDescription;
        Currency = currency;
        Name = name;
        SearchName = searchName;
        Type = type;
        MinimalSpendingAmount = minimalSpendingAmount;
        MinimalSpendingAmountFrom = minimalSpendingAmountFrom;
        MinimalSpendingAmountTo = minimalSpendingAmountTo;
        ExpiresAt = expiresAt;
        ExpiresAtFrom = expiresAtFrom;
        ExpiresAtTo = expiresAtTo;
        UsedAt = usedAt;
        UsedAtFrom = usedAtFrom;
        UsedAtTo = usedAtTo;
        CreatedAt = createdAt;
        CreatedAtFrom = createdAtFrom;
        CreatedAtTo = createdAtTo;
        Used = used;
        Amount = amount;
        AmountFrom = amountFrom;
        AmountTo = amountTo;
        Page = page;
        Results = results;
        OrderBy = orderBy;
        SortOrder = sortOrder;
    }
}