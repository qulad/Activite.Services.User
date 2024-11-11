using System;
using System.Threading;
using System.Threading.Tasks;
using Activite.Services.User.Mongo.Documents;
using Convey.CQRS.Commands;
using Convey.Persistence.MongoDB;

namespace Activite.Services.User.CQRS.Commands.Handlers;

public class UpdateUserHandler : ICommandHandler<UpdateUser>
{
    private readonly IMongoRepository<UserDocument, Guid> _repository;

    public UpdateUserHandler(IMongoRepository<UserDocument, Guid> repository)
    {
        _repository = repository;
    }

    public async Task HandleAsync(UpdateUser command, CancellationToken cancellationToken = default)
    {
        if (command is null)
        {
            throw new ArgumentNullException(nameof(command));
        }

        if (command.Id == Guid.Empty)
        {
            throw new ArgumentException("User id cannot be empty.");
        }

        var existingUser = await _repository.GetAsync(x => x.Id == command.Id)
            ?? throw new ArgumentException($"User with Id: '{command.Id}' was not found.");

        existingUser.PhoneNumber = command.PhoneNumber;
        existingUser.Region = command.Region;
        existingUser.TermsAndServicesAccepted = command.TermsAndServicesAccepted;
        existingUser.Verified = command.Verified;
        existingUser.UpdatedAt = DateTimeOffset.UtcNow;

        await _repository.UpdateAsync(existingUser);
    }
}