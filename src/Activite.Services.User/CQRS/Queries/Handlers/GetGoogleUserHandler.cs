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

public class GetGoogleUserHandler : IQueryHandler<GetGoogleUser, GoogleUserDto>
{
    private readonly IMongoRepository<GoogleUserDocument, Guid> _repository;
    private readonly ILogger<GetGoogleUserHandler> _logger;

    public GetGoogleUserHandler(IMongoRepository<GoogleUserDocument, Guid> repository, ILogger<GetGoogleUserHandler> logger)
    {
        _repository = repository;
        _logger = logger;
    }

    public async Task<GoogleUserDto> HandleAsync(GetGoogleUser query, CancellationToken cancellationToken = default)
    {
        var user = await _repository.GetAsync(x => x.Id == query.Id && x.Type == UserTypes.Google);

        if (user is null)
        {
            _logger.LogWarning("User with Id: {UserId} was not found.", query.Id);

            throw new ArgumentException($"User with Id: '{query.Id}' was not found.");
        }

        return new GoogleUserDto
        {
            Id = user.Id,
            Email = user.Email,
            PhoneNumber = user.PhoneNumber,
            GoogleId = user.GoogleId,
            TermsAndServicesAccepted = user.TermsAndServicesAccepted,
            Verified = user.Verified,
            CreatedAt = user.CreatedAt,
            UpdatedAt = user.UpdatedAt
        };
    }
}