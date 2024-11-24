using System;
using System.Threading;
using System.Threading.Tasks;
using Activite.Services.User.DTOs;
using Activite.Services.User.Mongo.Documents;
using Convey.CQRS.Queries;
using Convey.Persistence.MongoDB;

namespace Activite.Services.User.CQRS.Queries.Handlers;

public class GetOfferHandler : IQueryHandler<GetOffer, OfferDto>
{
    private readonly IMongoRepository<OfferDocument, Guid> _repository;

    public GetOfferHandler(IMongoRepository<OfferDocument, Guid> repository)
    {
        _repository = repository;
    }

    public async Task<OfferDto> HandleAsync(GetOffer query, CancellationToken cancellationToken = default)
    {
        var offer = await _repository.GetAsync(query.Id);

        if (offer is null)
        {
            return new OfferDto();
        }

        return new OfferDto
        {
            Id = offer.Id,
            LocationId = offer.LocationId,
            VisualMediaIds = offer.VisualMediaIds ?? Array.Empty<Guid>(),
            Name = offer.Name,
            Description = offer.Description,
            CreatedBy = offer.CreatedBy,
            CreatedAt = offer.CreatedAt,
            UpdatedBy = offer.UpdatedBy,
            UpdatedAt = offer.UpdatedAt
        };
    }
}
