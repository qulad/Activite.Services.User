using System;
using System.Threading;
using System.Threading.Tasks;
using Activite.Services.User.DTOs;
using Activite.Services.User.Mongo.Documents;
using Convey.CQRS.Queries;
using Convey.Persistence.MongoDB;
using Microsoft.Extensions.Logging;

namespace Activite.Services.User.CQRS.Queries.Handlers;

public class GetGoogleLocationHandler : IQueryHandler<GetGoogleLocation, GoogleLocationDto>
{
    private readonly IMongoRepository<GoogleLocationDocument, Guid> _repository;
    private readonly ILogger<GetGoogleLocationHandler> _logger;

    public GetGoogleLocationHandler(IMongoRepository<GoogleLocationDocument, Guid> repository, ILogger<GetGoogleLocationHandler> logger)
    {
        _repository = repository;
        _logger = logger;
    }

    public async Task<GoogleLocationDto> HandleAsync(GetGoogleLocation query, CancellationToken cancellationToken = default)
    {
        var user = await _repository.GetAsync(query.Id);

        if (user is null)
        {
            _logger.LogWarning("User with Id: {UserId} was not found.", query.Id);

            throw new ArgumentException($"User with Id: '{query.Id}' was not found.");
        }

        return new GoogleLocationDto
        {
            Id = user.Id,
            Email = user.Email,
            PhoneNumber = user.PhoneNumber,
            Region = user.Region,
            Type = user.Type,
            GoogleId = user.GoogleId,
            Location = user.Location,
            TermsAndServicesAccepted = user.TermsAndServicesAccepted,
            Verified = user.Verified,
            CreatedAt = user.CreatedAt,
            UpdatedAt = user.UpdatedAt
        };
    }
}