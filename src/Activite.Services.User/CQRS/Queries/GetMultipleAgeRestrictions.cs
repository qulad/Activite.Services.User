using System;
using System.Collections.Generic;
using Activite.Services.User.Constants;
using Activite.Services.User.DTOs;
using Convey.CQRS.Queries;

namespace Activite.Services.User.CQRS.Queries;

public class GetMultipleAgeRestrictions : PagedQueryBase, IQuery<PagedResult<AgeRestrictionDto>>
{
    public Guid? Id { get; set; }

    public IList<Guid> Ids { get; set; }

    public string Code { get; set; }

    public int? Age { get; set; }

    public int? AgeFrom { get; set; }

    public int? AgeTo { get; set; }

    public DateTimeOffset? CreatedAt { get; set; }

    public DateTimeOffset? CreatedAtFrom { get; set; }

    public DateTimeOffset? CreatedAtTo { get; set; }

    public DateTimeOffset? UpdatedAt { get; set; }

    public DateTimeOffset? UpdatedAtFrom { get; set; }

    public DateTimeOffset? UpdatedAtTo { get; set; }
    
    public GetMultipleAgeRestrictions(
        Guid? id = null,
        IList<Guid> ids = null,
        string code = null,
        int? age = null,
        int? ageFrom = null,
        int? ageTo = null,
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
        Code = code;
        Age = age;
        AgeFrom = ageFrom;
        AgeTo = ageTo;
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