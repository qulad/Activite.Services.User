using System;
using Activite.Services.User.Attributes;
using Activite.Services.User.DTOs;
using Convey.CQRS.Queries;

namespace Activite.Services.User.CQRS.Queries;

[Contract]
public class GetLocation : IQuery<LocationDto>
{
    public Guid Id { get; set; }

    public GetLocation(Guid id)
    {
        Id = id;
    }
}