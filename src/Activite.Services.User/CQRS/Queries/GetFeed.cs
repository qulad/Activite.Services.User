using System;
using Activite.Services.User.Constants;
using Activite.Services.User.DTOs;
using Convey.CQRS.Queries;

namespace Activite.Services.User.CQRS.Queries;

public class GetFeed : PagedQueryBase, IQuery<PagedResult<EventDto>>
{
    public Guid CustomerId { get; set; }

    public Guid? AgeRestrictionId { get; set; }

    public DateTimeOffset DateFrom { get; set; }

    public GetFeed(
        Guid customerId,
        Guid? ageRestrictionId,
        DateTimeOffset dateFrom,
        int page = Pagination.DefaultPage,
        int results = Pagination.DefaultResults,
        string orderBy = Pagination.DefaultOrderBy,
        string sortOrder = Pagination.DefaultSortOrder)
    {
        CustomerId = customerId;
        AgeRestrictionId = ageRestrictionId;
        DateFrom = dateFrom;
        Page = page;
        Results = results;
        OrderBy = orderBy;
        SortOrder = sortOrder;
    }
}