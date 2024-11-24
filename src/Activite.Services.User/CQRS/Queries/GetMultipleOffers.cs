using System;
using System.Collections.Generic;
using Activite.Services.User.Constants;
using Activite.Services.User.DTOs;
using Convey.CQRS.Queries;

namespace Activite.Services.User.CQRS.Queries;

public class GetMultipleOffers : PagedQueryBase, IQuery<PagedResult<OfferDto>>
{
    public Guid? Id { get; set; }

    public IList<Guid> Ids { get; set; }

    public Guid? LocationId { get; set; }

    public IList<Guid> LocationIds { get; set; }

    public IList<Guid> VisualMediaIds { get; set; }

    public string Name { get; set; }

    public string SearchName { get; set; }

    public string Description { get; set; }

    public string SearchDescription { get; set; }

    public Guid? CreatedBy { get; set; }

    public DateTimeOffset? CreatedAt { get; set; }

    public DateTimeOffset? CreatedAtFrom { get; set; }

    public DateTimeOffset? CreatedAtTo { get; set; }

    public Guid? UpdatedBy { get; set; }

    public DateTimeOffset? UpdatedAt { get; set; }

    public DateTimeOffset? UpdatedAtFrom { get; set; }

    public DateTimeOffset? UpdatedAtTo { get; set; }

    public GetMultipleOffers(
        Guid? id = null,
        IList<Guid> ids = null,
        Guid? locationId = null,
        IList<Guid> locationIds = null,
        IList<Guid> visualMediaIds = null,
        string name = null,
        string searchName = null,
        string description = null,
        string searchDescription = null,
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
        VisualMediaIds = visualMediaIds;
        Name = name;
        SearchName = searchName;
        Description = description;
        SearchDescription = searchDescription;
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