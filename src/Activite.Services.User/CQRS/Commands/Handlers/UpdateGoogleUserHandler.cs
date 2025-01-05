using System;
using System.Threading;
using System.Threading.Tasks;
using Activite.Services.User.Constants;
using Activite.Services.User.Mongo.Documents;
using Convey.CQRS.Commands;
using Convey.Persistence.MongoDB;

namespace Activite.Services.User.CQRS.Commands.Handlers;

public class UpdateGoogleCustomerHandler : ICommandHandler<UpdateGoogleCustomer>
{
    private readonly IMongoRepository<GoogleCustomerDocument, Guid> _repository;

    public UpdateGoogleCustomerHandler(IMongoRepository<GoogleCustomerDocument, Guid> repository)
    {
        _repository = repository;
    }

    public async Task HandleAsync(UpdateGoogleCustomer command, CancellationToken cancellationToken = default)
    {
        if (command is null)
        {
            throw new ArgumentNullException(nameof(command));
        }

        if (command.Id == Guid.Empty)
        {
            throw new ArgumentException("Google customer id cannot be empty.");
        }

        var existingUser = await _repository.GetAsync(x => x.Id == command.Id && x.Type == UserTypes.GoogleCustomer)
            ?? throw new ArgumentException($"Google customer with Id: '{command.Id}' was not found.");

        existingUser.FirstName = command.FirstName;
        existingUser.LastName = command.LastName;
        existingUser.TermsAndServicesAccepted = command.TermsAndServicesAccepted;
        existingUser.UpdatedAt = DateTimeOffset.UtcNow;

        await _repository.UpdateAsync(existingUser);
    }
}