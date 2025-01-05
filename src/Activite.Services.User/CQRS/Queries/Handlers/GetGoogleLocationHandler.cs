using System;
using System.Threading;
using System.Threading.Tasks;
using Activite.Services.User.Constants;
using Activite.Services.User.DTOs;
using Activite.Services.User.Mongo.Documents;
using Convey.CQRS.Queries;
using Convey.Persistence.MongoDB;
using Microsoft.Extensions.Logging;

namespace Activite.Services.User.CQRS.Queries.Handlers;

public class GetGoogleLocationHandler : IQueryHandler<GetGoogleLocation, GoogleLocationDto>
{
    private readonly IMongoRepository<GoogleLocationDocument, Guid> _repository;

    public GetGoogleLocationHandler(IMongoRepository<GoogleLocationDocument, Guid> repository)
    {
        _repository = repository;
    }

    public async Task<GoogleLocationDto> HandleAsync(GetGoogleLocation query, CancellationToken cancellationToken = default)
    {
        var user =
            await _repository.GetAsync(
                x => x.Id == query.Id &&
                x.Type == UserTypes.GoogleLocation);

        if (user is null)
        {
            return new GoogleLocationDto();
        }

        return new GoogleLocationDto
        {
            Id = user.Id,
            Email = user.Email,
            PhoneNumber = user.PhoneNumber,
            GoogleId = user.GoogleId,
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