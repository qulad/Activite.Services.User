using System;
using System.Collections.Generic;
using Activite.Services.User.Constants;
using Activite.Services.User.DTOs;
using Convey.CQRS.Queries;

namespace Activite.Services.User.CQRS.Queries;

public class GetMultipleCustomerComments : PagedQueryBase, IQuery<PagedResult<CustomerCommentDto>>
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

    public Guid? CustomerId { get; set; }

    public IList<Guid> CustomerIds { get; set; }

    public int? Rating { get; set; }

    public int? RatingFrom { get; set; }

    public int? RatingTo { get; set; }

    public GetMultipleCustomerComments(
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
        Guid? customerId = null,
        IList<Guid> customerIds = null,
        int? rating = null,
        int? ratingFrom = null,
        int? ratingTo = null)
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
        CustomerId = customerId;
        CustomerIds = customerIds;
        Rating = rating;
        RatingFrom = ratingFrom;
        RatingTo = ratingTo;
    }
}