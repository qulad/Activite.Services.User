using System;
using System.Threading;
using System.Threading.Tasks;
using Activite.Services.User.DTOs;
using Activite.Services.User.Mongo.Documents;
using Convey.CQRS.Queries;
using Convey.Persistence.MongoDB;

namespace Activite.Services.User.CQRS.Queries.Handlers;

public class GetLocationWalletHandler : IQueryHandler<GetLocationWallet, LocationWalletDto>
{
    private readonly IMongoRepository<LocationWalletDocument, Guid> _repository;

    public GetLocationWalletHandler(IMongoRepository<LocationWalletDocument, Guid> repository)
    {
        _repository = repository;
    }

    public async Task<LocationWalletDto> HandleAsync(GetLocationWallet query, CancellationToken cancellationToken = default)
    {
        var wallet = await _repository.GetAsync(query.Id);

        if (wallet is null)
        {
            return new LocationWalletDto();
        }

        return new LocationWalletDto
        {
            Id = wallet.Id,
            Currency = wallet.Currency,
            Type = wallet.Type,
            Amount = wallet.Amount,
            CreatedAt = wallet.CreatedAt,
            UpdatedAt = wallet.UpdatedAt,
            LocationId = wallet.LocationId
        };
    }
}
