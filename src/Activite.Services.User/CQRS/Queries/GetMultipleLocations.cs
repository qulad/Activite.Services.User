using System;
using Activite.Services.User.Constants;
using Activite.Services.User.DTOs;
using Convey.CQRS.Queries;

namespace Activite.Services.User.CQRS.Queries;

public class GetMultipleLocations : PagedQueryBase, IQuery<PagedResult<LocationDto>>
{
    public Guid? Id { get; set; }

    public string Email { get; set; }

    public string PhoneNumber { get; set; }

    public string Region { get; set; }

    public string Address { get; set; }

    public string Name { get; set; }

    public string Description { get; set; }

    public DateOnly? EstabilishedDate { get; set; }

    public DateOnly? EstabilishedDateFrom { get; set; }

    public DateOnly? EstabilishedDateTo { get; set; }

    public bool? TermsAndServicesAccepted { get; set; }

    public bool? Verified { get; set; }

    public DateTimeOffset? CreatedAt { get; set; }

    public DateTimeOffset? CreatedAtFrom { get; set; }

    public DateTimeOffset? CreatedAtTo { get; set; }

    public DateTimeOffset? UpdatedAt { get; set; }

    public DateTimeOffset? UpdatedAtFrom { get; set; }

    public DateTimeOffset? UpdatedAtTo { get; set; }

    public GetMultipleLocations(
        Guid? id = null,
        string email = null,
        string phoneNumber = null,
        string region = null,
        string address = null,
        string name = null,
        string description = null,
        DateOnly? estabilishedDate = null,
        DateOnly? estabilishedDateFrom = null,
        DateOnly? estabilishedDateTo = null,
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
        Address = address;
        Name = name;
        Description = description;
        EstabilishedDate = estabilishedDate;
        EstabilishedDateFrom = estabilishedDateFrom;
        EstabilishedDateTo = estabilishedDateTo;
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