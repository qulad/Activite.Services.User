using System;
using System.Threading;
using System.Threading.Tasks;
using Activite.Services.User.Constants;
using Activite.Services.User.Mongo.Documents;
using Convey.CQRS.Commands;
using Convey.Persistence.MongoDB;

namespace Activite.Services.User.CQRS.Commands.Handlers;

public class AddGoogleLocationHandler : ICommandHandler<AddGoogleLocation>
{
    private readonly IMongoRepository<GoogleLocationDocument, Guid> _repository;

    public AddGoogleLocationHandler(IMongoRepository<GoogleLocationDocument, Guid> repository)
    {
        _repository = repository;
    }

    public async Task HandleAsync(AddGoogleLocation command, CancellationToken cancellationToken = default)
    {
        if (command is null)
        {
            throw new ArgumentNullException(nameof(command));
        }

        if (command.Id == Guid.Empty)
        {
            throw new ArgumentException("User id cannot be empty.");
        }

        if (string.IsNullOrEmpty(command.Email))
        {
            throw new ArgumentException("User email cannot be empty.");
        }

        if (string.IsNullOrEmpty(command.Region))
        {
            throw new ArgumentException("User region cannot be empty.");
        }

        if (string.IsNullOrEmpty(command.GoogleId))
        {
            throw new ArgumentException("User type is invalid.");
        }

        var user = new GoogleLocationDocument
        {
            Id = command.Id,
            Email = command.Email,
            PhoneNumber = command.PhoneNumber,
            Region = command.Region,
            Type = UserTypes.GoogleLocation,
            TermsAndServicesAccepted = false,
            Verified = false,
            GoogleId = command.GoogleId,
            CreatedAt = DateTimeOffset.UtcNow
        };

        await _repository.AddAsync(user);
    }
}