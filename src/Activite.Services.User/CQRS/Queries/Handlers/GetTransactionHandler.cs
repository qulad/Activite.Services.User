using System;
using System.Threading;
using System.Threading.Tasks;
using Activite.Services.User.DTOs;
using Activite.Services.User.Mongo.Documents;
using Convey.CQRS.Queries;
using Convey.Persistence.MongoDB;

namespace Activite.Services.User.CQRS.Queries.Handlers;

public class GetTransactionHandler : IQueryHandler<GetTransaction, TransactionDto>
{
    private readonly IMongoRepository<TransactionDocument, Guid> _repository;

    public GetTransactionHandler(IMongoRepository<TransactionDocument, Guid> repository)
    {
        _repository = repository;
    }

    public async Task<TransactionDto> HandleAsync(GetTransaction query, CancellationToken cancellationToken = default)
    {
        var transaction = await _repository.GetAsync(query.Id);

        if (transaction is null)
        {
            return new TransactionDto();
        }

        return new TransactionDto
        {
            Id = transaction.Id,
            CustomerId = transaction.CustomerId,
            LocationId = transaction.LocationId,
            Currency = transaction.Currency,
            Amount = transaction.Amount,
            CreatedAt = transaction.CreatedAt
        };
    }
}
