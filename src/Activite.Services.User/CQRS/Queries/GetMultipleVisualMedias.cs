using System;
using System.Collections.Generic;
using Activite.Services.User.Constants;
using Activite.Services.User.DTOs;
using Convey.CQRS.Queries;

namespace Activite.Services.User.CQRS.Queries;

public class GetMultipleVisualMedias : PagedQueryBase, IQuery<PagedResult<VisualMediaDto>>
{
    public Guid? Id { get; set; }

    public IList<Guid> Ids { get; set; }

    public string Type { get; set; }

    public string Content { get; set; }

    public DateTimeOffset? CreatedAt { get; set; }

    public DateTimeOffset? CreatedAtFrom { get; set; }

    public DateTimeOffset? CreatedAtTo { get; set; }

    public DateTimeOffset? UpdatedAt { get; set; }

    public DateTimeOffset? UpdatedAtFrom { get; set; }

    public DateTimeOffset? UpdatedAtTo { get; set; }

    public GetMultipleVisualMedias(
        Guid? id = null,
        IList<Guid> ids = null,
        string type = null,
        string content = null,
        DateTimeOffset? createdAt = null,
        DateTimeOffset? createdAtFrom = null,
        DateTimeOffset? createdAtTo = null,
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
        Type = type;
        Content = content;
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
    }
}