using System;
using Activite.Services.User.Attributes;
using Activite.Services.User.DTOs;
using Convey.CQRS.Queries;

namespace Activite.Services.User.CQRS.Queries;

[Contract]
public class GetUser : IQuery<UserDto>
{
    public Guid Id { get; set; }

    public GetUser(Guid id)
    {
        Id = id;
    }
}