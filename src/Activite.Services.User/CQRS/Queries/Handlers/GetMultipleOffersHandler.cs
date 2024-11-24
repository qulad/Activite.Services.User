using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Activite.Services.User.DTOs;
using Activite.Services.User.Mongo.Documents;
using Convey.CQRS.Queries;
using Convey.Persistence.MongoDB;

namespace Activite.Services.User.CQRS.Queries.Handlers;

public class GetMultipleOffersHandler : IQueryHandler<GetMultipleOffers, PagedResult<OfferDto>>
{
    private readonly IMongoRepository<OfferDocument, Guid> _repository;

    public GetMultipleOffersHandler(IMongoRepository<OfferDocument, Guid> repository)
    {
        _repository = repository;
    }

    public async Task<PagedResult<OfferDto>> HandleAsync(GetMultipleOffers query, CancellationToken cancellationToken = default)
    {
        var predicate = GetPredicate(query);

        var offers = await _repository.BrowseAsync(predicate, query);

        if (offers is null || offers.IsEmpty)
        {
            return PagedResult<OfferDto>.Empty;
        }

        return offers.Map(offer => new OfferDto
        {
            Id = offer.Id,
            LocationId = offer.LocationId,
            VisualMediaIds = offer.VisualMediaIds,
            Name = offer.Name,
            Description = offer.Description,
            CreatedBy = offer.CreatedBy,
            CreatedAt = offer.CreatedAt,
            UpdatedBy = offer.UpdatedBy,
            UpdatedAt = offer.UpdatedAt
        });
    }

    private static Expression<Func<OfferDocument, bool>> GetPredicate(GetMultipleOffers query)
    {
        Expression<Func<OfferDocument, bool>> expression = null;

        if (query.Id.HasValue)
        {
            expression = expression.And(offer => offer.Id == query.Id);
        }

        if (query.Ids is not null && query.Ids.Count > 0)
        {
            expression = expression.And(offer => query.Ids.Contains(offer.Id));
        }

        if (query.LocationId.HasValue)
        {
            expression = expression.And(offer => offer.LocationId == query.LocationId);
        }

        if (query.LocationIds is not null && query.Ids.Count > 0)
        {
            expression = expression.And(offer => query.LocationIds.Contains(offer.LocationId));
        }

        if (query.VisualMediaIds is not null && query.VisualMediaIds.Count > 0)
        {
            expression = expression.And(offer => offer.VisualMediaIds.Any(x => query.VisualMediaIds.Contains(x)));
        }

        if (!string.IsNullOrEmpty(query.Name))
        {
            expression = expression.And(offer => offer.Name == query.Name);
        }

        if (!string.IsNullOrEmpty(query.SearchName))
        {
            expression = expression.And(offer => offer.Name.Contains(query.SearchName));
        }

        if (!string.IsNullOrEmpty(query.Description))
        {
            expression = expression.And(offer => offer.Description == query.Description);
        }

        if (!string.IsNullOrEmpty(query.SearchDescription))
        {
            expression = expression.And(offer => offer.Description.Contains(query.SearchDescription));
        }

        if (query.CreatedBy.HasValue)
        {
            expression = expression.And(offer => offer.CreatedBy == query.CreatedBy);
        }

        if (query.CreatedAt.HasValue)
        {
            expression = expression.And(offer => offer.CreatedAt == query.CreatedAt);
        }

        if (query.CreatedAtFrom.HasValue)
        {
            expression = expression.And(offer => offer.CreatedAt >= query.CreatedAtFrom);
        }

        if (query.CreatedAtTo.HasValue)
        {
            expression = expression.And(offer => offer.CreatedAt <= query.CreatedAtTo);
        }

        if (query.UpdatedBy.HasValue)
        {
            expression = expression.And(offer => offer.UpdatedBy == query.UpdatedBy);
        }

        if (query.UpdatedAt.HasValue)
        {
            expression = expression.And(offer => offer.UpdatedAt == query.UpdatedAt);
        }

        if (query.UpdatedAtFrom.HasValue)
        {
            expression = expression.And(offer => offer.UpdatedAt >= query.UpdatedAtFrom);
        }

        if (query.UpdatedAtTo.HasValue)
        {
            expression = expression.And(offer => offer.UpdatedAt <= query.UpdatedAtTo);
        }

        return expression;
    }
}