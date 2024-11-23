using System;
using System.Threading;
using System.Threading.Tasks;
using Activite.Services.User.Mongo.Documents;
using Convey.CQRS.Commands;
using Convey.Persistence.MongoDB;

namespace Activite.Services.User.CQRS.Commands.Handlers;

public class AddOfferHandler : ICommandHandler<AddOffer>
{
    private readonly IMongoRepository<OfferDocument, Guid> _repository;

    public AddOfferHandler(IMongoRepository<OfferDocument, Guid> repository)
    {
        _repository = repository;
    }

    public async Task HandleAsync(AddOffer command, CancellationToken cancellationToken = default)
    {
        if (command is null)
        {
            throw new ArgumentNullException(nameof(command));
        }

        if (command.Id == Guid.Empty)
        {
            throw new ArgumentException("Offer id cannot be empty.");
        }

        if (command.LocationId == Guid.Empty)
        {
            throw new ArgumentException("Offer location id cannot be empty.");
        }

        if (string.IsNullOrEmpty(command.Name))
        {
            throw new ArgumentException("Offer name cannot be empty.");
        }

        if (string.IsNullOrEmpty(command.Description))
        {
            throw new ArgumentException("Offer description cannot be empty.");
        }

        var offer = new OfferDocument
        {
            Id = command.Id,
            LocationId = command.LocationId,
            VisualMediaIds = command.VisualMediaIds ?? Array.Empty<Guid>(),
            Name = command.Name,
            Description = command.Description,
            CreatedBy = command.LocationId,
            CreatedAt = DateTimeOffset.UtcNow,
            UpdatedBy = null,
            UpdatedAt = null
        };

        await _repository.AddAsync(offer);
    }
}