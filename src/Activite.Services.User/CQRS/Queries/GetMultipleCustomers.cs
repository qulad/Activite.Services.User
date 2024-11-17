using System;
using Activite.Services.User.Constants;
using Activite.Services.User.DTOs;
using Convey.CQRS.Queries;

namespace Activite.Services.User.CQRS.Queries;

public class GetMultipleCustomers : PagedQueryBase, IQuery<PagedResult<CustomerDto>>
{
    public Guid? Id { get; set; }

    public string Email { get; set; }

    public string PhoneNumber { get; set; }

    public string Region { get; set; }

    public string FirstName { get; set; }

    public string LastName { get; set; }

    public DateOnly? DateOfBirth { get; set; }

    public DateOnly? DateOfBirthFrom { get; set; }

    public DateOnly? DateOfBirthTo { get; set; }

    public bool? TermsAndServicesAccepted { get; set; }

    public bool? Verified { get; set; }

    public DateTimeOffset? CreatedAt { get; set; }

    public DateTimeOffset? CreatedAtFrom { get; set; }

    public DateTimeOffset? CreatedAtTo { get; set; }

    public DateTimeOffset? UpdatedAt { get; set; }

    public DateTimeOffset? UpdatedAtFrom { get; set; }

    public DateTimeOffset? UpdatedAtTo { get; set; }

    public GetMultipleCustomers(
        Guid? id = null,
        string email = null,
        string phoneNumber = null,
        string region = null,
        string firstName = null,
        string lastName = null,
        DateOnly? dateOfBirth = null,
        DateOnly? dateOfBirthFrom = null,
        DateOnly? dateOfBirthTo = null,
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
        FirstName = firstName;
        LastName = lastName;
        DateOfBirth = dateOfBirth;
        DateOfBirthFrom = dateOfBirthFrom;
        DateOfBirthTo = dateOfBirthTo;
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