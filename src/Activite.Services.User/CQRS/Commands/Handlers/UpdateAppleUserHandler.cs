using System;
using System.Threading;
using System.Threading.Tasks;
using Activite.Services.User.Constants;
using Activite.Services.User.Mongo.Documents;
using Convey.CQRS.Commands;
using Convey.Persistence.MongoDB;

namespace Activite.Services.User.CQRS.Commands.Handlers;

public class UpdateAppleUserHandler : ICommandHandler<UpdateAppleUser>
{
    private readonly IMongoRepository<AppleUserDocument, Guid> _repository;

    public UpdateAppleUserHandler(IMongoRepository<AppleUserDocument, Guid> repository)
    {
        _repository = repository;
    }

    public async Task HandleAsync(UpdateAppleUser command, CancellationToken cancellationToken = default)
    {
        if (command is null)
        {
            throw new ArgumentNullException(nameof(command));
        }

        if (command.Id == Guid.Empty)
        {
            throw new ArgumentException("User id cannot be empty.");
        }

        var existingUser = await _repository.GetAsync(command.Id);

        if (existingUser is null || existingUser.Type != UserTypes.Apple)
        {
            throw new ArgumentException($"User with Id: '{command.Id}' was not found.");
        }

        existingUser.PhoneNumber = command.PhoneNumber;
        existingUser.Region = command.Region;
        existingUser.UpdatedAt = DateTimeOffset.UtcNow;

        await _repository.UpdateAsync(existingUser);
    }
}