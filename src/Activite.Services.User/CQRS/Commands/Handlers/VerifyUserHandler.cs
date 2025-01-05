using System;
using System.Threading;
using System.Threading.Tasks;
using Activite.Services.User.Mongo.Documents;
using Convey.CQRS.Commands;
using Convey.Persistence.MongoDB;

namespace Activite.Services.User.CQRS.Commands.Handlers;

public class VerifyUserHandler : ICommandHandler<VerifyUser>
{
    private readonly IMongoRepository<UserDocument, Guid> _repository;

    public VerifyUserHandler(IMongoRepository<UserDocument, Guid> repository)
    {
        _repository = repository;
    }

    public async Task HandleAsync(VerifyUser command, CancellationToken cancellationToken = default)
    {
        var user = await _repository.GetAsync(command.UserId);

        if (user is null)
        {
            throw new InvalidOperationException($"User with ID: '{command.UserId}' was not found.");
        }

        if (user.VerificationCode != command.VerificationCode)
        {
            throw new InvalidOperationException("Invalid verification code.");
        }

        user.Verified = true;
        user.UpdatedAt = DateTimeOffset.UtcNow;

        await _repository.UpdateAsync(user);
    }
}