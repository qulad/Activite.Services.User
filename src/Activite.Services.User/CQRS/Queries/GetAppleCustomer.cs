using System;
using Activite.Services.User.Attributes;
using Activite.Services.User.DTOs;
using Convey.CQRS.Queries;

namespace Activite.Services.User.CQRS.Queries;

[Contract]
public class GetAppleCustomer : IQuery<AppleCustomerDto>
{
    public Guid Id { get; set; }

    public GetAppleCustomer(Guid id)
    {
        Id = id;
    }
}