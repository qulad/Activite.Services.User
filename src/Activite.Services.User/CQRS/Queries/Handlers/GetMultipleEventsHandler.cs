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

public class GetMultipleEventsHandler : IQueryHandler<GetMultipleEvents, PagedResult<EventDto>>
{
    private readonly IMongoRepository<EventDocument, Guid> _repository;

    public GetMultipleEventsHandler(IMongoRepository<EventDocument, Guid> repository)
    {
        _repository = repository;
    }

    public async Task<PagedResult<EventDto>> HandleAsync(GetMultipleEvents query, CancellationToken cancellationToken = default)
    {
        var predicate = GetPredicate(query);

        var @events = await _repository.BrowseAsync(predicate, query);

        if (@events is null || @events.IsEmpty)
        {
            return PagedResult<EventDto>.Empty;
        }

        return @events.Map(@event => new EventDto
        {
            Id = @event.Id,
            LocationId = @event.LocationId,
            AgeRestrictionId = @event.AgeRestrictionId,
            OfferId = @event.OfferId,
            VisualMediaIds = @event.VisualMediaIds,
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
        });
    }

    private static Expression<Func<EventDocument, bool>> GetPredicate(GetMultipleEvents query)
    {
        Expression<Func<EventDocument, bool>> expression = null;

        if (query.Id.HasValue)
        {
            expression = expression.And(@event => @event.Id == query.Id);
        }

        if (query.Ids is not null && query.Ids.Count > 0)
        {
            expression = expression.And(@event => query.Ids.Contains(@event.Id));
        }

        if (query.LocationId.HasValue)
        {
            expression = expression.And(@event => @event.LocationId == query.LocationId);
        }

        if (query.LocationIds is not null && query.Ids.Count > 0)
        {
            expression = expression.And(@event => query.LocationIds.Contains(@event.LocationId));
        }

        if (query.AgeRestrictionId.HasValue)
        {
            expression = expression.And(@event => @event.AgeRestrictionId == query.AgeRestrictionId);
        }

        if (query.AgeRestrictionIds is not null && query.AgeRestrictionIds.Count > 0)
        {
            expression = expression.And(@event => query.AgeRestrictionIds.Contains(@event.AgeRestrictionId.Value));
        }

        if (query.OfferId.HasValue)
        {
            expression = expression.And(@event => @event.OfferId == query.OfferId);
        }

        if (query.OfferIds is not null && query.OfferIds.Count > 0)
        {
            expression = expression.And(@event => query.OfferIds.Contains(@event.OfferId.Value));
        }

        if (query.VisualMediaIds is not null && query.VisualMediaIds.Count > 0)
        {
            expression = expression.And(@event => @event.VisualMediaIds.Any(x => query.VisualMediaIds.Contains(x)));
        }

        if (!string.IsNullOrEmpty(query.Name))
        {
            expression = expression.And(@event => @event.Name == query.Name);
        }

        if (!string.IsNullOrEmpty(query.SearchName))
        {
            expression = expression.And(@event => @event.Name.Contains(query.SearchName));
        }

        if (!string.IsNullOrEmpty(query.Description))
        {
            expression = expression.And(@event => @event.Description == query.Description);
        }

        if (!string.IsNullOrEmpty(query.SearchDescription))
        {
            expression = expression.And(@event => @event.Description.Contains(query.SearchDescription));
        }

        if (query.Amount.HasValue)
        {
            expression = expression.And(@event => @event.Amount == query.Amount);
        }

        if (query.AmountFrom.HasValue)
        {
            expression = expression.And(@event => @event.Amount >= query.AmountFrom);
        }

        if (query.AmountTo.HasValue)
        {
            expression = expression.And(@event => @event.Amount <= query.AmountTo);
        }

        if (!string.IsNullOrEmpty(query.Currency))
        {
            expression = expression.And(@event => @event.Currency == query.Currency);
        }

        if (query.DateFrom.HasValue)
        {
            expression = expression.And(@event => @event.DateFrom == query.DateFrom);
        }

        if (query.DateFromFrom.HasValue)
        {
            expression = expression.And(@event => @event.DateFrom >= query.DateFromFrom);
        }

        if (query.DateFromTo.HasValue)
        {
            expression = expression.And(@event => @event.DateFrom <= query.DateFromTo);
        }

        if (query.DateTo.HasValue)
        {
            expression = expression.And(@event => @event.DateTo == query.DateTo);
        }

        if (query.DateToFrom.HasValue)
        {
            expression = expression.And(@event => @event.DateTo >= query.DateToFrom);
        }

        if (query.DateToTo.HasValue)
        {
            expression = expression.And(@event => @event.DateTo <= query.DateToTo);
        }

        if (query.EffectiveDate.HasValue)
        {
            expression = expression.And(@event => @event.DateFrom <= query.EffectiveDate && @event.DateTo >= query.EffectiveDate);
        }

        if (query.CreatedBy.HasValue)
        {
            expression = expression.And(@event => @event.CreatedBy == query.CreatedBy);
        }

        if (query.CreatedAt.HasValue)
        {
            expression = expression.And(@event => @event.CreatedAt == query.CreatedAt);
        }

        if (query.CreatedAtFrom.HasValue)
        {
            expression = expression.And(@event => @event.CreatedAt >= query.CreatedAtFrom);
        }

        if (query.CreatedAtTo.HasValue)
        {
            expression = expression.And(@event => @event.CreatedAt <= query.CreatedAtTo);
        }

        if (query.UpdatedBy.HasValue)
        {
            expression = expression.And(@event => @event.UpdatedBy == query.UpdatedBy);
        }

        if (query.UpdatedAt.HasValue)
        {
            expression = expression.And(@event => @event.UpdatedAt == query.UpdatedAt);
        }

        if (query.UpdatedAtFrom.HasValue)
        {
            expression = expression.And(@event => @event.UpdatedAt >= query.UpdatedAtFrom);
        }

        if (query.UpdatedAtTo.HasValue)
        {
            expression = expression.And(@event => @event.UpdatedAt <= query.UpdatedAtTo);
        }

        return expression;
    }
}