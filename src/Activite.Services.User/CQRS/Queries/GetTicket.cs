using System;
using Activite.Services.User.DTOs;
using Convey.CQRS.Queries;

namespace Activite.Services.User.CQRS.Queries;

public class GetTicket : IQuery<TicketDto>
{
    public Guid Id { get; set; }

    public GetTicket(Guid id)
    {
        Id = id;
    }
}