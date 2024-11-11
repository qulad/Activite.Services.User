using System;
using Activite.Services.User.Attributes;
using Activite.Services.User.DTOs;
using Convey.CQRS.Queries;

namespace Activite.Services.User.CQRS.Queries;

[Contract]
public class GetAppleUser : IQuery<AppleUserDto>
{
    public Guid Id { get; set; }

    public GetAppleUser(Guid id)
    {
        Id = id;
    }
}