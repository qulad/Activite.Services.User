using System;
using System.Threading;
using System.Threading.Tasks;
using Activite.Services.User.DTOs;
using Activite.Services.User.Mongo.Documents;
using Convey.CQRS.Queries;
using Convey.Persistence.MongoDB;

namespace Activite.Services.User.CQRS.Queries.Handlers;

public class GetEventHandler : IQueryHandler<GetEvent, EventDto>
{
    private readonly IMongoRepository<EventDocument, Guid> _repository;

    public GetEventHandler(IMongoRepository<EventDocument, Guid> repository)
    {
        _repository = repository;
    }

    public async Task<EventDto> HandleAsync(GetEvent query, CancellationToken cancellationToken = default)
    {
        var @event = await _repository.GetAsync(query.Id);

        if (@event is null)
        {
            return new EventDto();
        }

        return new EventDto
        {
            Id = @event.Id,
            LocationId = @event.LocationId,
            AgeRestrictionId = @event.AgeRestrictionId,
            OfferId = @event.OfferId,
            VisualMediaIds = @event.VisualMediaIds ?? Array.Empty<Guid>(),
            Name = @event.Name,
            Description = @event.Description,
            Amount = @event.Amount,
            Currency = @event.Currency,
            DateFrom = @event.DateFrom,
            DateTo = @event.DateTo,
            CreatedBy = @event.CreatedBy,
            CreatedAt = @event.CreatedAt,
            UpdatedBy = @event.UpdatedBy,
            UpdatedAt = @event.UpdatedAt
        };
    }
}
