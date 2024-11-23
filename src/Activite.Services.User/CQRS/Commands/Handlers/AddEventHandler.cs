using System;
using System.Threading;
using System.Threading.Tasks;
using Activite.Services.User.Constants;
using Activite.Services.User.Mongo.Documents;
using Convey.CQRS.Commands;
using Convey.Persistence.MongoDB;

namespace Activite.Services.User.CQRS.Commands.Handlers;

public class AddEventHandler : ICommandHandler<AddEvent>
{
    private readonly IMongoRepository<EventDocument, Guid> _repository;

    public AddEventHandler(IMongoRepository<EventDocument, Guid> repository)
    {
        _repository = repository;
    }

    public async Task HandleAsync(AddEvent command, CancellationToken cancellationToken = default)
    {
        if (command is null)
        {
            throw new ArgumentNullException(nameof(command));
        }

        if (command.Id == Guid.Empty)
        {
            throw new ArgumentException("Event id cannot be empty.");
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

        var @event = new EventDocument
        {
            Id = command.Id,
            LocationId = command.LocationId,
            AgeRestrictionId = command.AgeRestrictionId,
            OfferId = command.OfferId,
            VisualMediaIds = command.VisualMediaIds ?? Array.Empty<Guid>(),
            Name = command.Name,
            Description = command.Description,
            Amount = command.Amount,
            Currency = command.Currency,
            DateFrom = command.DateFrom,
            DateTo = command.DateTo,
            CreatedBy = command.LocationId,
            CreatedAt = DateTimeOffset.UtcNow,
            UpdatedBy = null,
            UpdatedAt = null
        };

        await _repository.AddAsync(@event);
    }
}