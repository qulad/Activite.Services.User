using System;
using System.Threading;
using System.Threading.Tasks;
using Activite.Services.User.Constants;
using Activite.Services.User.Mongo.Documents;
using Convey.CQRS.Commands;
using Convey.Persistence.MongoDB;

namespace Activite.Services.User.CQRS.Commands.Handlers;

public class UpdateEventHandler : ICommandHandler<UpdateEvent>
{
    private readonly IMongoRepository<EventDocument, Guid> _repository;

    public UpdateEventHandler(IMongoRepository<EventDocument, Guid> repository)
    {
        _repository = repository;
    }

    public async Task HandleAsync(UpdateEvent command, CancellationToken cancellationToken = default)
    {
        if (command is null)
        {
            throw new ArgumentNullException(nameof(command));
        }

        if (command.Id == Guid.Empty)
        {
            throw new ArgumentException("Event id cannot be empty.");
        }

        var @event = await _repository.GetAsync(command.Id);

        if (@event is null)
        {
            throw new InvalidOperationException($"Event with id: '{command.Id}' was not found.");
        }

        if (command.LocationId == Guid.Empty)
        {
            throw new ArgumentException("Event location id cannot be empty.");
        }

        if (string.IsNullOrEmpty(command.Name))
        {
            throw new ArgumentException("Event name cannot be empty.");
        }

        if (string.IsNullOrEmpty(command.Description))
        {
            throw new ArgumentException("Event description cannot be empty.");
        }

        if (string.IsNullOrEmpty(command.Currency))
        {
            throw new ArgumentException("Event currency cannot be empty.");
        }

        if (!Currencies.All.Contains(command.Currency))
        {
            throw new ArgumentException("Event currency is invalid.");
        }

        if (command.DateFrom == default)
        {
            throw new ArgumentException("Event date from is invalid.");
        }

        if (command.DateTo == default)
        {
            throw new ArgumentException("Event date to is invalid.");
        }

        @event.LocationId = command.LocationId;
        @event.AgeRestrictionId = command.AgeRestrictionId;
        @event.OfferId = command.OfferId;
        @event.VisualMediaIds = command.VisualMediaIds ?? Array.Empty<Guid>();
        @event.Name = command.Name;
        @event.Description = command.Description;
        @event.Amount = command.Amount;
        @event.Currency = command.Currency;
        @event.DateFrom = command.DateFrom;
        @event.DateTo = command.DateTo;
        @event.UpdatedBy = command.LocationId;
        @event.UpdatedAt = DateTimeOffset.UtcNow;

        await _repository.UpdateAsync(@event);
    }
}