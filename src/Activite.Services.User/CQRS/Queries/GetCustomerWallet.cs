using System;
using Activite.Services.User.DTOs;
using Convey.CQRS.Queries;

namespace Activite.Services.User.CQRS.Queries;

public class GetCustomerWallet : IQuery<CustomerWalletDto>
{
    public Guid Id { get; set; }

    public GetCustomerWallet(Guid id)
    {
        Id = id;
    }
}