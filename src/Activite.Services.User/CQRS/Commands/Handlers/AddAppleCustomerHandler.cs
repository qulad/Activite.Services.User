using System;
using System.Threading;
using System.Threading.Tasks;
using Activite.Services.User.Constants;
using Activite.Services.User.Mongo.Documents;
using Convey.CQRS.Commands;
using Convey.Persistence.MongoDB;

namespace Activite.Services.User.CQRS.Commands.Handlers;

public class AddAppleCustomerHandler : ICommandHandler<AddAppleCustomer>
{
    private readonly IMongoRepository<AppleCustomerDocument, Guid> _repository;

    public AddAppleCustomerHandler(IMongoRepository<AppleCustomerDocument, Guid> repository)
    {
        _repository = repository;
    }

    public async Task HandleAsync(AddAppleCustomer command, CancellationToken cancellationToken = default)
    {
        if (command is null)
        {
            throw new ArgumentNullException(nameof(command));
        }

        if (command.Id == Guid.Empty)
        {
            throw new ArgumentException("Apple customer id cannot be empty.");
        }

        if (string.IsNullOrEmpty(command.FirstName))
        {
            throw new ArgumentException("Apple customer first name cannot be empty.");
        }

        if (string.IsNullOrEmpty(command.LastName))
        {
            throw new ArgumentException("Apple customer last name cannot be empty.");
        }

        if (command.DateOfBirth == DateOnly.MinValue)
        {
            throw new ArgumentException("Apple customer date of birth is invalid.");
        }

        if (string.IsNullOrEmpty(command.Email))
        {
            throw new ArgumentException("Apple customer email cannot be empty.");
        }

        if (string.IsNullOrEmpty(command.Region))
        {
            throw new ArgumentException("Apple customer region cannot be empty.");
        }

        if (string.IsNullOrEmpty(command.AppleId))
        {
            throw new ArgumentException("Apple customer type is invalid.");
        }

        var user = new AppleCustomerDocument
        {
            Id = command.Id,
            Email = command.Email,
            PhoneNumber = command.PhoneNumber,
            Region = command.Region,
            Type = UserTypes.AppleCustomer,
            TermsAndServicesAccepted = false,
            Verified = false,
            CreatedAt = DateTimeOffset.UtcNow,
            UpdatedAt = null,
            FirstName = command.FirstName,
            LastName = command.LastName,
            DateOfBirth = command.DateOfBirth,
            AppleId = command.AppleId
        };

        await _repository.AddAsync(user);
    }
}