using System;
using Activite.Services.User.Attributes;
using Activite.Services.User.DTOs;
using Convey.CQRS.Queries;

namespace Activite.Services.User.CQRS.Queries;

[Contract]
public class GetGoogleLocation : IQuery<GoogleLocationDto>
{
    public Guid Id { get; set; }

    public GetGoogleLocation(Guid id)
    {
        Id = id;
    }
}