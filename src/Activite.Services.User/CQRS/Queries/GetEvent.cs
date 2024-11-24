using System;
using Activite.Services.User.DTOs;
using Convey.CQRS.Queries;

namespace Activite.Services.User.CQRS.Queries;

public class GetEvent : IQuery<EventDto>
{
    public Guid Id { get; set; }

    public GetEvent(Guid id)
    {
        Id = id;
    }
}