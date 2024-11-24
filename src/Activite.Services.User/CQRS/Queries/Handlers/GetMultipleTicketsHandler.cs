using System;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Activite.Services.User.DTOs;
using Activite.Services.User.Mongo.Documents;
using Convey.CQRS.Queries;
using Convey.Persistence.MongoDB;

namespace Activite.Services.User.CQRS.Queries.Handlers;

public class GetMultipleTicketsHandler : IQueryHandler<GetMultipleTickets, PagedResult<TicketDto>>
{
    private readonly IMongoRepository<TicketDocument, Guid> _repository;

    public GetMultipleTicketsHandler(IMongoRepository<TicketDocument, Guid> repository)
    {
        _repository = repository;
    }

    public async Task<PagedResult<TicketDto>> HandleAsync(GetMultipleTickets query, CancellationToken cancellationToken = default)
    {
        var predicate = GetPredicate(query);

        var tickets = await _repository.BrowseAsync(predicate, query);

        if (tickets is null || tickets.IsEmpty)
        {
            return PagedResult<TicketDto>.Empty;
        }

        return tickets.Map(ticket => new TicketDto
        {
            Id = ticket.Id,
            CustomerId = ticket.CustomerId,
            EventId = ticket.EventId,
            CouponId = ticket.CouponId,
            Amount = ticket.Amount,
            Currency = ticket.Currency,
            CreatedAt = ticket.CreatedAt
        });
    }

    private static Expression<Func<TicketDocument, bool>> GetPredicate(GetMultipleTickets query)
    {
        Expression<Func<TicketDocument, bool>> expression = null;

        if (query.Id.HasValue)
        {
            expression = expression.And(ticket => ticket.Id == query.Id);
        }

        if (query.Ids is not null && query.Ids.Count > 0)
        {
            expression = expression.And(ticket => query.Ids.Contains(ticket.Id));
        }

        if (query.CustomerId.HasValue)
        {
            expression = expression.And(ticket => ticket.CustomerId == query.CustomerId);
        }

        if (query.CustomerIds is not null && query.CustomerIds.Count > 0)
        {
            expression = expression.And(ticket => query.CustomerIds.Contains(ticket.CustomerId));
        }

        if (query.EventId.HasValue)
        {
            expression = expression.And(ticket => ticket.EventId == query.EventId);
        }

        if (query.EventIds is not null && query.EventIds.Count > 0)
        {
            expression = expression.And(ticket => query.EventIds.Contains(ticket.EventId));
        }

        if (query.CouponId.HasValue)
        {
            expression = expression.And(ticket => ticket.CouponId == query.CouponId);
        }

        if (query.CouponIds is not null && query.CouponIds.Count > 0)
        {
            expression = expression.And(ticket => query.CouponIds.Contains(ticket.CouponId.Value));
        }

        if (query.Amount.HasValue)
        {
            expression = expression.And(ticket => ticket.Amount == query.Amount);
        }

        if (query.AmountFrom.HasValue)
        {
            expression = expression.And(ticket => ticket.Amount >= query.AmountFrom);
        }

        if (query.AmountTo.HasValue)
        {
            expression = expression.And(ticket => ticket.Amount <= query.AmountTo);
        }

        if (!string.IsNullOrWhiteSpace(query.Currency))
        {
            expression = expression.And(ticket => ticket.Currency == query.Currency);
        }
        
        if (query.CreatedAt.HasValue)
        {
            expression = expression.And(ticket => ticket.CreatedAt == query.CreatedAt);
        }

        if (query.CreatedAtFrom.HasValue)
        {
            expression = expression.And(ticket => ticket.CreatedAt >= query.CreatedAtFrom);
        }

        if (query.CreatedAtTo.HasValue)
        {
            expression = expression.And(ticket => ticket.CreatedAt <= query.CreatedAtTo);
        }

        return expression;
    }
}