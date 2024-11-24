using System;
using System.Collections.Generic;
using Activite.Services.User.Constants;
using Activite.Services.User.DTOs;
using Convey.CQRS.Queries;

namespace Activite.Services.User.CQRS.Queries;

public class GetMultipleEvents : PagedQueryBase, IQuery<PagedResult<EventDto>>
{
    public Guid? Id { get; set; }

    public IList<Guid> Ids { get; set; }

    public Guid? LocationId { get; set; }

    public IList<Guid> LocationIds { get; set; }

    public Guid? AgeRestrictionId { get; set; }

    public IList<Guid> AgeRestrictionIds { get; set; }

    public Guid? OfferId { get; set; }

    public IList<Guid> OfferIds { get; set; }

    public IList<Guid> VisualMediaIds { get; set; }

    public string Name { get; set; }

    public string SearchName { get; set; }

    public string Description { get; set; }

    public string SearchDescription { get; set; }

    public decimal? Amount { get; set; }

    public decimal? AmountFrom {get;set;}

    public decimal? AmountTo {get;set;}

    public string Currency {get;set;}

    public DateTimeOffset? DateFrom { get; set; }

    public DateTimeOffset? DateFromFrom { get; set; }

    public DateTimeOffset? DateFromTo { get; set; }

    public DateTimeOffset? DateTo { get; set; }

    public DateTimeOffset? DateToFrom { get; set; }

    public DateTimeOffset? DateToTo { get; set; }

    public DateTimeOffset? EffectiveDate { get; set; }

    public Guid? CreatedBy { get; set; }

    public DateTimeOffset? CreatedAt { get; set; }

    public DateTimeOffset? CreatedAtFrom { get; set; }

    public DateTimeOffset? CreatedAtTo { get; set; }

    public Guid? UpdatedBy { get; set; }

    public DateTimeOffset? UpdatedAt { get; set; }

    public DateTimeOffset? UpdatedAtFrom { get; set; }

    public DateTimeOffset? UpdatedAtTo { get; set; }

    public GetMultipleEvents(
        Guid? id = null,
        IList<Guid> ids = null,
        Guid? locationId = null,
        IList<Guid> locationIds = null,
        Guid? ageRestrictionId = null,
        IList<Guid> ageRestrictionIds = null,
        Guid? offerId = null,
        IList<Guid> offerIds = null,
        IList<Guid> visualMediaIds = null,
        string name = null,
        string searchName = null,
        string description = null,
        string searchDescription = null,
        decimal? amount = null,
        decimal? amountFrom = null,
        decimal? amountTo = null,
        string currency = null,
        DateTimeOffset? dateFrom = null,
        DateTimeOffset? dateFromFrom = null,
        DateTimeOffset? dateFromTo = null,
        DateTimeOffset? dateTo = null,
        DateTimeOffset? dateToFrom = null,
        DateTimeOffset? dateToTo = null,
        DateTimeOffset? effectiveDate = null,
        Guid? createdBy = null,
        DateTimeOffset? createdAt = null,
        DateTimeOffset? createdAtFrom = null,
        DateTimeOffset? createdAtTo = null,
        Guid? updatedBy = null,
        DateTimeOffset? updatedAt = null,
        DateTimeOffset? updatedAtFrom = null,
        DateTimeOffset? updatedAtTo = null,
        int page = Pagination.DefaultPage,
        int results = Pagination.DefaultResults,
        string orderBy = Pagination.DefaultOrderBy,
        string sortOrder = Pagination.DefaultSortOrder)
    {
        Id = id;
        Ids = ids;
        LocationId = locationId;
        LocationIds = locationIds;
        AgeRestrictionId = ageRestrictionId;
        AgeRestrictionIds = ageRestrictionIds;
        OfferId = offerId;
        OfferIds = offerIds;
        VisualMediaIds = visualMediaIds;
        Name = name;
        SearchName = searchName;
        Description = description;
        SearchDescription = searchDescription;
        Amount = amount;
        AmountFrom = amountFrom;
        AmountTo = amountTo;
        Currency = currency;
        DateFrom = dateFrom;
        DateFromFrom = dateFromFrom;
        DateFromTo = dateFromTo;
        DateTo = dateTo;
        DateToFrom = dateToFrom;
        DateToTo = dateToTo;
        EffectiveDate = effectiveDate;
        CreatedBy = createdBy;
        CreatedAt = createdAt;
        CreatedAtFrom = createdAtFrom;
        CreatedAtTo = createdAtTo;
        UpdatedBy = updatedBy;
        UpdatedAt = updatedAt;
        UpdatedAtFrom = updatedAtFrom;
        UpdatedAtTo = updatedAtTo;
        Page = page;
        Results = results;
        OrderBy = orderBy;
        SortOrder = sortOrder;
    }
}