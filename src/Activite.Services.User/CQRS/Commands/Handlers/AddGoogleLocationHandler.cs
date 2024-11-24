using System;
using System.Threading;
using System.Threading.Tasks;
using Activite.Services.User.Constants;
using Activite.Services.User.Mongo.Documents;
using Convey.CQRS.Commands;
using Convey.Persistence.MongoDB;

namespace Activite.Services.User.CQRS.Commands.Handlers;

public class AddGoogleLocationHandler : ICommandHandler<AddGoogleLocation>
{
    private readonly IMongoRepository<GoogleLocationDocument, Guid> _repository;
    private readonly IMongoRepository<LocationWalletDocument, Guid> _walletRepository;

    public AddGoogleLocationHandler(
        IMongoRepository<GoogleLocationDocument, Guid> repository,
        IMongoRepository<LocationWalletDocument, Guid> walletRepository)
    {
        _repository = repository;
        _walletRepository = walletRepository;
    }

    public async Task HandleAsync(AddGoogleLocation command, CancellationToken cancellationToken = default)
    {
        if (command is null)
        {
            throw new ArgumentNullException(nameof(command));
        }

        if (command.Id == Guid.Empty)
        {
            throw new ArgumentException("Google location id cannot be empty.");
        }

        if (string.IsNullOrEmpty(command.Address))
        {
            throw new ArgumentException("Google location address cannot be empty.");
        }

        if (string.IsNullOrEmpty(command.Name))
        {
            throw new ArgumentException("Google location name cannot be empty.");
        }

        if (string.IsNullOrEmpty(command.Description))
        {
            throw new ArgumentException("Google location description cannot be empty.");
        }

        if (command.EstabilishedDate == DateOnly.MinValue)
        {
            throw new ArgumentException("Google location estabilished date of birth is invalid.");
        }

        if (string.IsNullOrEmpty(command.Email))
        {
            throw new ArgumentException("Google location email cannot be empty.");
        }

        if (string.IsNullOrEmpty(command.Region))
        {
            throw new ArgumentException("Google location region cannot be empty.");
        }

        if (string.IsNullOrEmpty(command.GoogleId))
        {
            throw new ArgumentException("Google location type is invalid.");
        }

        var existingWallet = await _walletRepository.GetAsync(w => w.LocationId == command.Id);

        if (existingWallet is not null)
        {
            throw new InvalidOperationException($"Location wallet with location id: '{command.Id}' already exists.");
        }

        var wallet = new LocationWalletDocument
        {
            Id = Guid.NewGuid(),
            LocationId = command.Id,
            Currency = Currencies.TRY,
            Type = WalletTypes.Location,
            Amount = 0,
            CreatedAt = DateTimeOffset.UtcNow,
            UpdatedAt = null
        };

        await _walletRepository.AddAsync(wallet);

        var user = new GoogleLocationDocument
        {
            Id = command.Id,
            Email = command.Email,
            PhoneNumber = command.PhoneNumber,
            Region = command.Region,
            Type = UserTypes.GoogleLocation,
            TermsAndServicesAccepted = false,
            Verified = false,
            CreatedAt = DateTimeOffset.UtcNow,
            UpdatedAt = null,
            Address = command.Address,
            Name = command.Name,
            Description = command.Description,
            EstabilishedDate = command.EstabilishedDate,
            GoogleId = command.GoogleId,
        };

        await _repository.AddAsync(user);
    }
}