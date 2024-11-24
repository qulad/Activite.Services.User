using System;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Activite.Services.User.DTOs;
using Activite.Services.User.Mongo.Documents;
using Convey.CQRS.Queries;
using Convey.Persistence.MongoDB;

namespace Activite.Services.User.CQRS.Queries.Handlers;

public class GetMultipleAgeRestrictionsHandler : IQueryHandler<GetMultipleAgeRestrictions, PagedResult<AgeRestrictionDto>>
{
    private readonly IMongoRepository<AgeRestrictionDocument, Guid> _repository;

    public GetMultipleAgeRestrictionsHandler(IMongoRepository<AgeRestrictionDocument, Guid> repository)
    {
        _repository = repository;
    }

    public async Task<PagedResult<AgeRestrictionDto>> HandleAsync(GetMultipleAgeRestrictions query, CancellationToken cancellationToken = default)
    {
        var predicate = GetPredicate(query);

        var ageRestrictions = await _repository.BrowseAsync(predicate, query);

        if (ageRestrictions is null || ageRestrictions.IsEmpty)
        {
            return PagedResult<AgeRestrictionDto>.Empty;
        }

        return ageRestrictions.Map(ageRestriction => new AgeRestrictionDto
        {
            Id = ageRestriction.Id,
            Code = ageRestriction.Code,
            Age = ageRestriction.Age,
            CreatedAt = ageRestriction.CreatedAt,
            UpdatedAt = ageRestriction.UpdatedAt
        });
    }

    private static Expression<Func<AgeRestrictionDocument, bool>> GetPredicate(GetMultipleAgeRestrictions query)
    {
        Expression<Func<AgeRestrictionDocument, bool>> expression = null;

        if (query.Id.HasValue)
        {
            expression = expression.And(ageRestriction => ageRestriction.Id == query.Id);
        }

        if (query.Ids is not null && query.Ids.Count > 0)
        {
            expression = expression.And(ageRestriction => query.Ids.Contains(ageRestriction.Id));
        }

        if (!string.IsNullOrWhiteSpace(query.Code))
        {
            expression = expression.And(ageRestriction => ageRestriction.Code == query.Code);
        }

        if (query.Age.HasValue)
        {
            expression = expression.And(ageRestriction => ageRestriction.Age == query.Age);
        }

        if (query.AgeFrom.HasValue)
        {
            expression = expression.And(ageRestriction => ageRestriction.Age >= query.AgeFrom);
        }

        if (query.AgeTo.HasValue)
        {
            expression = expression.And(ageRestriction => ageRestriction.Age <= query.AgeTo);
        }

        if (query.CreatedAt.HasValue)
        {
            expression = expression.And(ageRestriction => ageRestriction.CreatedAt == query.CreatedAt);
        }

        if (query.CreatedAtFrom.HasValue)
        {
            expression = expression.And(ageRestriction => ageRestriction.CreatedAt >= query.CreatedAtFrom);
        }

        if (query.CreatedAtTo.HasValue)
        {
            expression = expression.And(ageRestriction => ageRestriction.CreatedAt <= query.CreatedAtTo);
        }

        if (query.UpdatedAt.HasValue)
        {
            expression = expression.And(ageRestriction => ageRestriction.UpdatedAt == query.UpdatedAt);
        }

        if (query.UpdatedAtFrom.HasValue)
        {
            expression = expression.And(ageRestriction => ageRestriction.UpdatedAt >= query.UpdatedAtFrom);
        }

        if (query.UpdatedAtTo.HasValue)
        {
            expression = expression.And(ageRestriction => ageRestriction.UpdatedAt <= query.UpdatedAtTo);
        }

        return expression;
    }
}