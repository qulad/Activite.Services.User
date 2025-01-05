using System;
using System.Threading;
using System.Threading.Tasks;
using Activite.Services.User.Constants;
using Activite.Services.User.Mongo.Documents;
using Convey.CQRS.Commands;
using Convey.Persistence.MongoDB;

namespace Activite.Services.User.CQRS.Commands.Handlers;

public class UpdateAppleCustomerHandler : ICommandHandler<UpdateAppleCustomer>
{
    private readonly IMongoRepository<AppleCustomerDocument, Guid> _repository;

    public UpdateAppleCustomerHandler(IMongoRepository<AppleCustomerDocument, Guid> repository)
    {
        _repository = repository;
    }

    public async Task HandleAsync(UpdateAppleCustomer command, CancellationToken cancellationToken = default)
    {
        if (command is null)
        {
            throw new ArgumentNullException(nameof(command));
        }

        if (command.Id == Guid.Empty)
        {
            throw new ArgumentException("Apple customer id cannot be empty.");
        }

        var existingUser = await _repository.GetAsync(x => x.Id == command.Id && x.Type == UserTypes.AppleCustomer)
            ?? throw new ArgumentException($"Apple customer with Id: '{command.Id}' was not found.");

        existingUser.FirstName = command.FirstName;
        existingUser.LastName = command.LastName;
        existingUser.TermsAndServicesAccepted = command.TermsAndServicesAccepted;
        existingUser.UpdatedAt = DateTimeOffset.UtcNow;

        await _repository.UpdateAsync(existingUser);
    }
}