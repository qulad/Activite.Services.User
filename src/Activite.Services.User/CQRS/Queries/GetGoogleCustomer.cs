using System;
using Activite.Services.User.Attributes;
using Activite.Services.User.DTOs;
using Convey.CQRS.Queries;

namespace Activite.Services.User.CQRS.Queries;

[Contract]
public class GetGoogleCustomer : IQuery<GoogleCustomerDto>
{
    public Guid Id { get; set; }

    public GetGoogleCustomer(Guid id)
    {
        Id = id;
    }
}