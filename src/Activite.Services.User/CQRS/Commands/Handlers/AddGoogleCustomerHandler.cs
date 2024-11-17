using System;
using System.Threading;
using System.Threading.Tasks;
using Activite.Services.User.Constants;
using Activite.Services.User.Mongo.Documents;
using Convey.CQRS.Commands;
using Convey.Persistence.MongoDB;

namespace Activite.Services.User.CQRS.Commands.Handlers;

public class AddGoogleCustomerHandler : ICommandHandler<AddGoogleCustomer>
{
    private readonly IMongoRepository<GoogleCustomerDocument, Guid> _repository;

    public AddGoogleCustomerHandler(IMongoRepository<GoogleCustomerDocument, Guid> repository)
    {
        _repository = repository;
    }

    public async Task HandleAsync(AddGoogleCustomer command, CancellationToken cancellationToken = default)
    {
        if (command is null)
        {
            throw new ArgumentNullException(nameof(command));
        }

        if (command.Id == Guid.Empty)
        {
            throw new ArgumentException("Google customer id cannot be empty.");
        }

        if (string.IsNullOrEmpty(command.FirstName))
        {
            throw new ArgumentException("Google customer first name cannot be empty.");
        }

        if (string.IsNullOrEmpty(command.LastName))
        {
            throw new ArgumentException("Google customer last name cannot be empty.");
        }

        if (command.DateOfBirth == DateOnly.MinValue)
        {
            throw new ArgumentException("Google customer date of birth is invalid.");
        }

        if (string.IsNullOrEmpty(command.Email))
        {
            throw new ArgumentException("Google customer email cannot be empty.");
        }

        if (string.IsNullOrEmpty(command.Region))
        {
            throw new ArgumentException("Google customer region cannot be empty.");
        }

        if (string.IsNullOrEmpty(command.GoogleId))
        {
            throw new ArgumentException("Google customer type is invalid.");
        }

        var user = new GoogleCustomerDocument
        {
            Id = command.Id,
            Email = command.Email,
            PhoneNumber = command.PhoneNumber,
            Region = command.Region,
            Type = UserTypes.GoogleCustomer,
            TermsAndServicesAccepted = false,
            Verified = false,
            CreatedAt = DateTimeOffset.UtcNow,
            UpdatedAt = null,
            FirstName = command.FirstName,
            LastName = command.LastName,
            DateOfBirth = command.DateOfBirth,
            GoogleId = command.GoogleId
        };

        await _repository.AddAsync(user);
    }
}