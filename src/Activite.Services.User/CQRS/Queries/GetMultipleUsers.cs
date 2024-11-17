using System;
using Activite.Services.User.Constants;
using Activite.Services.User.DTOs;
using Convey.CQRS.Queries;

namespace Activite.Services.User.CQRS.Queries;

public class GetMultipleUsers : PagedQueryBase, IQuery<PagedResult<UserDto>>
{
    public Guid? Id { get; set; }

    public string Email { get; set; }

    public string PhoneNumber { get; set; }

    public string Region { get; set; }

    public bool? TermsAndServicesAccepted { get; set; }

    public bool? Verified { get; set; }

    public DateTimeOffset? CreatedAt { get; set; }

    public DateTimeOffset? CreatedAtFrom { get; set; }

    public DateTimeOffset? CreatedAtTo { get; set; }

    public DateTimeOffset? UpdatedAt { get; set; }

    public DateTimeOffset? UpdatedAtFrom { get; set; }

    public DateTimeOffset? UpdatedAtTo { get; set; }

    public GetMultipleUsers(
        Guid? id = null,
        string email = null,
        string phoneNumber = null,
        string region = null,
        bool? termsAndServicesAccepted = null,
        bool? verified = null,
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
        Email = email;
        PhoneNumber = phoneNumber;
        Region = region;
        TermsAndServicesAccepted = termsAndServicesAccepted;
        Verified = verified;
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