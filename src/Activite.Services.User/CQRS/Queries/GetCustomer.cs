using System;
using Activite.Services.User.Attributes;
using Activite.Services.User.DTOs;
using Convey.CQRS.Queries;

namespace Activite.Services.User.CQRS.Queries;

[Contract]
public class GetCustomer : IQuery<CustomerDto>
{
    public Guid Id { get; set; }

    public GetCustomer(Guid id)
    {
        Id = id;
    }
}