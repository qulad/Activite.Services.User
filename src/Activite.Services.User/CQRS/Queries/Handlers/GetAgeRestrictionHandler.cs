using System;
using System.Threading;
using System.Threading.Tasks;
using Activite.Services.User.DTOs;
using Activite.Services.User.Mongo.Documents;
using Convey.CQRS.Queries;
using Convey.Persistence.MongoDB;

namespace Activite.Services.User.CQRS.Queries.Handlers;

public class GetAgeRestrictionHandler : IQueryHandler<GetAgeRestriction, AgeRestrictionDto>
{
    private readonly IMongoRepository<AgeRestrictionDocument, Guid> _repository;

    public GetAgeRestrictionHandler(IMongoRepository<AgeRestrictionDocument, Guid> repository)
    {
        _repository = repository;
    }

    public async Task<AgeRestrictionDto> HandleAsync(GetAgeRestriction query, CancellationToken cancellationToken = default)
    {
        var ageRestriction = await _repository.GetAsync(query.Id);

        if (ageRestriction is null)
        {
            return new AgeRestrictionDto();
        }

        return new AgeRestrictionDto
        {
            Id = ageRestriction.Id,
            Code = ageRestriction.Code,
            Age = ageRestriction.Age,
            CreatedAt = ageRestriction.CreatedAt,
            UpdatedAt = ageRestriction.UpdatedAt
        };
    }
}