using System;
using System.Threading;
using System.Threading.Tasks;
using Activite.Services.User.Constants;
using Activite.Services.User.Mongo.Documents;
using Convey.CQRS.Commands;
using Convey.Persistence.MongoDB;

namespace Activite.Services.User.CQRS.Commands.Handlers;

public class UpdateGoogleLocationHandler : ICommandHandler<UpdateGoogleLocation>
{
    private readonly IMongoRepository<GoogleLocationDocument, Guid> _repository;

    public UpdateGoogleLocationHandler(IMongoRepository<GoogleLocationDocument, Guid> repository)
    {
        _repository = repository;
    }

    public async Task HandleAsync(UpdateGoogleLocation command, CancellationToken cancellationToken = default)
    {
        if (command is null)
        {
            throw new ArgumentNullException(nameof(command));
        }

        if (command.Id == Guid.Empty)
        {
            throw new ArgumentException("Google location id cannot be empty.");
        }

        var existingUser = await _repository.GetAsync(x => x.Id == command.Id && x.Type == UserTypes.GoogleLocation)
            ?? throw new ArgumentException($"Google location with Id: '{command.Id}' was not found.");

        existingUser.Address = command.Address;
        existingUser.Name = command.Name;
        existingUser.Description = command.Description;
        existingUser.TermsAndServicesAccepted = command.TermsAndServicesAccepted;
        existingUser.Verified = command.Verified;
        existingUser.UpdatedAt = DateTimeOffset.UtcNow;

        await _repository.UpdateAsync(existingUser);
    }
}