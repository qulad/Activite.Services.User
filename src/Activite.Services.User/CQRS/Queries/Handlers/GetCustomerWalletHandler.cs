using System;
using System.Threading;
using System.Threading.Tasks;
using Activite.Services.User.DTOs;
using Activite.Services.User.Mongo.Documents;
using Convey.CQRS.Queries;
using Convey.Persistence.MongoDB;

namespace Activite.Services.User.CQRS.Queries.Handlers;

public class GetCustomerWalletHandler : IQueryHandler<GetCustomerWallet, CustomerWalletDto>
{
    private readonly IMongoRepository<CustomerWalletDocument, Guid> _repository;

    public GetCustomerWalletHandler(IMongoRepository<CustomerWalletDocument, Guid> repository)
    {
        _repository = repository;
    }

    public async Task<CustomerWalletDto> HandleAsync(GetCustomerWallet query, CancellationToken cancellationToken = default)
    {
        var wallet = await _repository.GetAsync(query.Id);

        if (wallet is null)
        {
            return new CustomerWalletDto();
        }

        return new CustomerWalletDto
        {
            Id = wallet.Id,
            Currency = wallet.Currency,
            Type = wallet.Type,
            Amount = wallet.Amount,
            CreatedAt = wallet.CreatedAt,
            UpdatedAt = wallet.UpdatedAt,
            CustomerId = wallet.CustomerId
        };
    }
}
