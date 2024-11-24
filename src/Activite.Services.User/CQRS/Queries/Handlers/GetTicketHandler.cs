using System;
using System.Threading;
using System.Threading.Tasks;
using Activite.Services.User.DTOs;
using Activite.Services.User.Mongo.Documents;
using Convey.CQRS.Queries;
using Convey.Persistence.MongoDB;

namespace Activite.Services.User.CQRS.Queries.Handlers;

public class GetTicketHandler : IQueryHandler<GetTicket, TicketDto>
{
    private readonly IMongoRepository<TicketDocument, Guid> _repository;

    public GetTicketHandler(IMongoRepository<TicketDocument, Guid> repository)
    {
        _repository = repository;
    }

    public async Task<TicketDto> HandleAsync(GetTicket query, CancellationToken cancellationToken = default)
    {
        var ticket = await _repository.GetAsync(query.Id);

        if (ticket is null)
        {
            return new TicketDto();
        }
        
        return new TicketDto
        {
            Id = ticket.Id,
            CustomerId = ticket.CustomerId,
            EventId = ticket.EventId,
            CouponId = ticket.CouponId,
            Amount = ticket.Amount,
            Currency = ticket.Currency,
            CreatedAt = ticket.CreatedAt
        };
    }
}
