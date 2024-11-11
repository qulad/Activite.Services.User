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

public class GetAppleUserHandler : IQueryHandler<GetAppleUser, AppleUserDto>
{
    private readonly IMongoRepository<AppleUserDocument, Guid> _repository;
    private readonly ILogger<GetAppleUserHandler> _logger;

    public GetAppleUserHandler(IMongoRepository<AppleUserDocument, Guid> repository, ILogger<GetAppleUserHandler> logger)
    {
        _repository = repository;
        _logger = logger;
    }

    public async Task<AppleUserDto> HandleAsync(GetAppleUser query, CancellationToken cancellationToken = default)
    {
        var user = await _repository.GetAsync(x => x.Id == query.Id && x.Type == UserTypes.Apple);

        if (user is null)
        {
            _logger.LogWarning("User with Id: {UserId} was not found.", query.Id);

            throw new ArgumentException($"User with Id: '{query.Id}' was not found.");
        }

        return new AppleUserDto
        {
            Id = user.Id,
            Email = user.Email,
            PhoneNumber = user.PhoneNumber,
            Region = user.Region,
            Type = user.Type,
            AppleId = user.AppleId,
            TermsAndServicesAccepted = user.TermsAndServicesAccepted,
            Verified = user.Verified,
            CreatedAt = user.CreatedAt,
            UpdatedAt = user.UpdatedAt
        };
    }
}