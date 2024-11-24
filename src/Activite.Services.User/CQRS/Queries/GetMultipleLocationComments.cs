using System;
using System.Collections.Generic;
using Activite.Services.User.Constants;
using Activite.Services.User.DTOs;
using Convey.CQRS.Queries;

namespace Activite.Services.User.CQRS.Queries;

public class GetMultipleLocationComments : PagedQueryBase, IQuery<PagedResult<LocationCommentDto>>
{
    public Guid? Id { get; set; }

    public IList<Guid> Ids { get; set; }

    public Guid? EventId { get; set; }

    public IList<Guid> EventIds { get; set; }

    public string Content { get; set; }

    public string SearchContent { get; set; }

    public DateTimeOffset? CreatedAt { get; set; }

    public DateTimeOffset? CreatedAtFrom { get; set; }

    public DateTimeOffset? CreatedAtTo { get; set; }

    public DateTimeOffset? UpdatedAt { get; set; }

    public DateTimeOffset? UpdatedAtFrom { get; set; }

    public DateTimeOffset? UpdatedAtTo { get; set; }

    public Guid? LocationId { get; set; }

    public IList<Guid> LocationIds { get; set; }
    
    public Guid? CustomerCommentId { get; set; }

    public IList<Guid> CustomerCommentIds { get; set; }

    public GetMultipleLocationComments(
        Guid? id = null,
        IList<Guid> ids = null,
        Guid? eventId = null,
        IList<Guid> eventIds = null,
        string content = null,
        string searchContent = null,
        DateTimeOffset? createdAt = null,
        DateTimeOffset? createdAtFrom = null,
        DateTimeOffset? createdAtTo = null,
        DateTimeOffset? updatedAt = null,
        DateTimeOffset? updatedAtFrom = null,
        DateTimeOffset? updatedAtTo = null,
        int page = Pagination.DefaultPage,
        int results = Pagination.DefaultResults,
        string orderBy = Pagination.DefaultOrderBy,
        string sortOrder = Pagination.DefaultSortOrder,
        Guid? locationId = null,
        IList<Guid> locationIds = null,
        Guid? customerCommentId = null,
        IList<Guid> customerCommentIds = null)
    {
        Id = id;
        Ids = ids;
        EventId = eventId;
        EventIds = eventIds;
        Content = content;
        SearchContent = searchContent;
        CreatedAt = createdAt;
        CreatedAtFrom = createdAtFrom;
        CreatedAtTo = createdAtTo;
        UpdatedAt = updatedAt;
        UpdatedAtFrom = updatedAtFrom;
        UpdatedAtTo = updatedAtTo;
        Page = page;
        Results = results;
        OrderBy = orderBy;
        SortOrder = sortOrder;
        LocationId = locationId;
        LocationIds = locationIds;
        CustomerCommentId = customerCommentId;
        CustomerCommentIds = customerCommentIds;
    }
}