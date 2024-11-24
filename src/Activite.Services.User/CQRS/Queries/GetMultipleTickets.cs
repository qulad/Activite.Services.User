using System;
using System.Collections.Generic;
using Activite.Services.User.Constants;
using Activite.Services.User.DTOs;
using Convey.CQRS.Queries;

namespace Activite.Services.User.CQRS.Queries;

public class GetMultipleTickets : PagedQueryBase, IQuery<PagedResult<TicketDto>>
{
    public Guid? Id { get; set; }

    public IList<Guid> Ids { get; set; }

    public Guid? CustomerId { get; set; }

    public IList<Guid> CustomerIds { get; set; }

    public Guid? EventId { get; set; }

    public IList<Guid> EventIds { get; set; }

    public Guid? CouponId { get; set; }

    public IList<Guid> CouponIds { get; set; }

    public decimal? Amount { get; set; }

    public decimal? AmountFrom { get; set; }

    public decimal? AmountTo { get; set; }

    public string Currency { get; set; }

    public DateTimeOffset? CreatedAt { get; set; }

    public DateTimeOffset? CreatedAtFrom { get; set; }

    public DateTimeOffset? CreatedAtTo { get; set; }

    public GetMultipleTickets(
        Guid? id = null,
        IList<Guid> ids = null,
        Guid? customerId = null,
        IList<Guid> customerIds = null,
        Guid? eventId = null,
        IList<Guid> eventIds = null,
        Guid? couponId = null,
        IList<Guid> couponIds = null,
        decimal? amount = null,
        decimal? amountFrom = null,
        decimal? amountTo = null,
        string currency = null,
        DateTimeOffset? createdAt = null,
        DateTimeOffset? createdAtFrom = null,
        DateTimeOffset? createdAtTo = null,
        int page = Pagination.DefaultPage,
        int results = Pagination.DefaultResults,
        string orderBy = Pagination.DefaultOrderBy,
        string sortOrder = Pagination.DefaultSortOrder)
    {
        Id = id;
        Ids = ids;
        CustomerId = customerId;
        CustomerIds = customerIds;
        EventId = eventId;
        EventIds = eventIds;
        CouponId = couponId;
        CouponIds = couponIds;
        Amount = amount;
        AmountFrom = amountFrom;
        AmountTo = amountTo;
        Currency = currency;
        CreatedAt = createdAt;
        CreatedAtFrom = createdAtFrom;
        CreatedAtTo = createdAtTo;
        Page = page;
        Results = results;
        OrderBy = orderBy;
        SortOrder = sortOrder;
    }
}