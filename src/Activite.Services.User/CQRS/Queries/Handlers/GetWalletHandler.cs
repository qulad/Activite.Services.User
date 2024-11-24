using System;
using System.Threading;
using System.Threading.Tasks;
using Activite.Services.User.DTOs;
using Activite.Services.User.Mongo.Documents;
using Convey.CQRS.Queries;
using Convey.Persistence.MongoDB;

namespace Activite.Services.User.CQRS.Queries.Handlers;

public class GetWalletHandler : IQueryHandler<GetWallet, WalletDto>
{
    private readonly IMongoRepository<WalletDocument, Guid> _repository;

    public GetWalletHandler(IMongoRepository<WalletDocument, Guid> repository)
    {
        _repository = repository;
    }

    public async Task<WalletDto> HandleAsync(GetWallet query, CancellationToken cancellationToken = default)
    {
        var wallet = await _repository.GetAsync(query.Id);

        if (wallet is null)
        {
            return new WalletDto();
        }

        return new WalletDto
        {
            Id = wallet.Id,
            Currency = wallet.Currency,
            Type = wallet.Type,
            Amount = wallet.Amount,
            CreatedAt = wallet.CreatedAt,
            UpdatedAt = wallet.UpdatedAt
        };
    }
}
