using System;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Activite.Services.User.DTOs;
using Activite.Services.User.Mongo.Documents;
using Convey.CQRS.Queries;
using Convey.Persistence.MongoDB;

namespace Activite.Services.User.CQRS.Queries.Handlers;

public class GetMultipleTransactionsHandler : IQueryHandler<GetMultipleTransactions, PagedResult<TransactionDto>>
{
    private readonly IMongoRepository<TransactionDocument, Guid> _repository;

    public GetMultipleTransactionsHandler(IMongoRepository<TransactionDocument, Guid> repository)
    {
        _repository = repository;
    }

    public async Task<PagedResult<TransactionDto>> HandleAsync(GetMultipleTransactions query, CancellationToken cancellationToken = default)
    {
        var predicate = GetPredicate(query);

        var transactions = await _repository.BrowseAsync(predicate, query);

        if (transactions is null || transactions.IsEmpty)
        {
            return PagedResult<TransactionDto>.Empty;
        }

        return transactions.Map(transaction => new TransactionDto
        {
            Id = transaction.Id,
            CustomerId = transaction.CustomerId,
            LocationId = transaction.LocationId,
            Currency = transaction.Currency,
            Amount = transaction.Amount,
            CreatedAt = transaction.CreatedAt
        });
    }

    private static Expression<Func<TransactionDocument, bool>> GetPredicate(GetMultipleTransactions query)
    {
        Expression<Func<TransactionDocument, bool>> expression = null;

        if (query.Id.HasValue)
        {
            expression = expression.And(transaction => transaction.Id == query.Id);
        }

        if (query.Ids is not null && query.Ids.Count > 0)
        {
            expression = expression.And(transaction => query.Ids.Contains(transaction.Id));
        }

        if (query.CustomerId.HasValue)
        {
            expression = expression.And(transaction => transaction.CustomerId == query.CustomerId);
        }

        if (query.CustomerIds is not null && query.CustomerIds.Count > 0)
        {
            expression = expression.And(transaction => query.CustomerIds.Contains(transaction.CustomerId));
        }

        if (query.LocationId.HasValue)
        {
            expression = expression.And(transaction => transaction.LocationId == query.LocationId);
        }

        if (query.LocationIds is not null && query.LocationIds.Count > 0)
        {
            expression = expression.And(transaction => query.LocationIds.Contains(transaction.LocationId));
        }

        if (!string.IsNullOrEmpty(query.Currency))
        {
            expression = expression.And(transaction => transaction.Currency == query.Currency);
        }

        if (query.Amount.HasValue)
        {
            expression = expression.And(transaction => transaction.Amount == query.Amount);
        }

        if (query.AmountFrom.HasValue)
        {
            expression = expression.And(transaction => transaction.Amount >= query.AmountFrom);
        }

        if (query.AmountTo.HasValue)
        {
            expression = expression.And(transaction => transaction.Amount <= query.AmountTo);
        }

        if (query.CreatedAt.HasValue)
        {
            expression = expression.And(transaction => transaction.CreatedAt == query.CreatedAt);
        }

        if (query.CreatedAtFrom.HasValue)
        {
            expression = expression.And(transaction => transaction.CreatedAt >= query.CreatedAtFrom);
        }

        if (query.CreatedAtTo.HasValue)
        {
            expression = expression.And(transaction => transaction.CreatedAt <= query.CreatedAtTo);
        }

        return expression;
    }
}
