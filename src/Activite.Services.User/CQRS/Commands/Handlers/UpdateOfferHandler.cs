using System;
using System.Threading;
using System.Threading.Tasks;
using Activite.Services.User.Mongo.Documents;
using Convey.CQRS.Commands;
using Convey.Persistence.MongoDB;

namespace Activite.Services.User.CQRS.Commands.Handlers;

public class UpdateOfferHandler : ICommandHandler<UpdateOffer>
{
    private readonly IMongoRepository<OfferDocument, Guid> _repository;

    public UpdateOfferHandler(IMongoRepository<OfferDocument, Guid> repository)
    {
        _repository = repository;
    }

    public async Task HandleAsync(UpdateOffer command, CancellationToken cancellationToken = default)
    {
        if (command is null)
        {
            throw new ArgumentNullException(nameof(command));
        }

        if (command.Id == Guid.Empty)
        {
            throw new ArgumentException("Offer id cannot be empty.");
        }

        var offer = await _repository.GetAsync(command.Id);

        if (offer is null)
        {
            throw new InvalidOperationException($"Offer with id: '{command.Id}' was not found.");
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

        offer.LocationId = command.LocationId;
        offer.VisualMediaIds = command.VisualMediaIds ?? Array.Empty<Guid>();
        offer.Name = command.Name;
        offer.Description = command.Description;
        offer.UpdatedBy = command.LocationId;
        offer.UpdatedAt = DateTimeOffset.UtcNow;

        await _repository.UpdateAsync(offer);
    }
}