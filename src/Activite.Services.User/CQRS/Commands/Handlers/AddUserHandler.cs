using System;
using System.Threading;
using System.Threading.Tasks;
using Activite.Services.User.Constants;
using Activite.Services.User.Mongo.Documents;
using Convey.CQRS.Commands;
using Convey.Persistence.MongoDB;

namespace Activite.Services.User.CQRS.Commands.Handlers;

public class AddUserHandler : ICommandHandler<AddUser>
{
    private readonly IMongoRepository<UserDocument, Guid> _repository;

    public AddUserHandler(IMongoRepository<UserDocument, Guid> repository)
    {
        _repository = repository;
    }
    public Task HandleAsync(AddUser command, CancellationToken cancellationToken = default)
    {
        if (command is null)
        {
            throw new ArgumentNullException(nameof(command));
        }

        if (command.Id == Guid.Empty)
        {
            throw new ArgumentException("User id cannot be empty.");
        }

        if (string.IsNullOrWhiteSpace(command.Email))
        {
            throw new ArgumentException("User email cannot be empty.");
        }

        if (string.IsNullOrWhiteSpace(command.Region))
        {
            throw new ArgumentException("User region cannot be empty.");
        }

        if (string.IsNullOrWhiteSpace(command.Type) && !UserTypes.All.Contains(command.Type))
        {
            throw new ArgumentException("User type is invalid.");
        }

        var user = new UserDocument
        {
            Id = command.Id,
            Email = command.Email,
            PhoneNumber = command.PhoneNumber,
            Region = command.Region,
            Type = command.Type,
            TermsAndServicesAccepted = false,
            Verified = false,
            CreatedAt = DateTimeOffset.UtcNow
        };

        return _repository.AddAsync(user);
    }
}