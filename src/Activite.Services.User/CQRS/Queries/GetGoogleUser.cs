using System;
using Activite.Services.User.Attributes;
using Activite.Services.User.DTOs;
using Convey.CQRS.Queries;

namespace Activite.Services.User.CQRS.Queries;

[Contract]
public class GetGoogleUser : IQuery<GoogleUserDto>
{
    public Guid Id { get; set; }

    public GetGoogleUser(Guid id)
    {
        Id = id;
    }
}