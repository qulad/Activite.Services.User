using System;
using System.Threading;
using System.Threading.Tasks;
using Activite.Services.User.Constants;
using Activite.Services.User.DTOs;
using Activite.Services.User.Mongo.Documents;
using Convey.CQRS.Queries;
using Convey.Persistence.MongoDB;

namespace Activite.Services.User.CQRS.Queries.Handlers;

public class GetLocationHandler : IQueryHandler<GetLocation, LocationDto>
{
    private readonly IMongoRepository<LocationDocument, Guid> _repository;

    public GetLocationHandler(IMongoRepository<LocationDocument, Guid> repository)
    {
        _repository = repository;
    }

    public async Task<LocationDto> HandleAsync(GetLocation query, CancellationToken cancellationToken = default)
    {
        var user =
            await _repository.GetAsync(
                x => x.Id == query.Id && 
                (x.Type == UserTypes.Location || x.Type == UserTypes.GoogleLocation));

        if (user is null)
        {
            return new LocationDto();
        }

        return new LocationDto
        {
            Id = user.Id,
            Email = user.Email,
            PhoneNumber = user.PhoneNumber,
            Address = user.Address,
            Name = user.Name,
            Description = user.Description,
            EstabilishedDate = user.EstabilishedDate,
            Region = user.Region,
            Type = user.Type,
            TermsAndServicesAccepted = user.TermsAndServicesAccepted,
            Verified = user.Verified,
            VerificationCode = user.VerificationCode,
            CreatedAt = user.CreatedAt,
            UpdatedAt = user.UpdatedAt
        };
    }
}