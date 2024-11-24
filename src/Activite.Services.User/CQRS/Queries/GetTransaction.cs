using System;
using Activite.Services.User.DTOs;
using Convey.CQRS.Queries;

namespace Activite.Services.User.CQRS.Queries;

public class GetTransaction : IQuery<TransactionDto>
{
    public Guid Id { get; set; }

    public GetTransaction(Guid id)
    {
        Id = id;
    }
}