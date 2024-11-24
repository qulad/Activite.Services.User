using System;
using Activite.Services.User.DTOs;
using Convey.CQRS.Queries;

namespace Activite.Services.User.CQRS.Queries;

public class GetWallet : IQuery<WalletDto>
{
    public Guid Id { get; set; }

    public GetWallet(Guid id)
    {
        Id = id;
    }
}