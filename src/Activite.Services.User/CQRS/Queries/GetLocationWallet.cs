using System;
using Activite.Services.User.DTOs;
using Convey.CQRS.Queries;

namespace Activite.Services.User.CQRS.Queries;

public class GetLocationWallet : IQuery<LocationWalletDto>
{
    public Guid Id { get; set; }

    public GetLocationWallet(Guid id)
    {
        Id = id;
    }
}