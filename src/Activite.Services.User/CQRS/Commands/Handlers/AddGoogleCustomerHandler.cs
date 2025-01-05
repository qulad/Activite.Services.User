using System;
using System.Threading;
using System.Threading.Tasks;
using Activite.Services.User.Constants;
using Activite.Services.User.DTOs.Integration;
using Activite.Services.User.Mongo.Documents;
using Activite.Services.User.Services;
using Convey.CQRS.Commands;
using Convey.Persistence.MongoDB;

namespace Activite.Services.User.CQRS.Commands.Handlers;

public class AddGoogleCustomerHandler : ICommandHandler<AddGoogleCustomer>
{
    private readonly IntegrationService _intergrationService;
    private readonly IMongoRepository<GoogleCustomerDocument, Guid> _repository;
    private readonly IMongoRepository<CustomerWalletDocument, Guid> _walletRepository;

    public AddGoogleCustomerHandler(
        IntegrationService intergrationService,
        IMongoRepository<GoogleCustomerDocument, Guid> repository,
        IMongoRepository<CustomerWalletDocument, Guid> walletRepository)
    {
        _intergrationService = intergrationService;
        _repository = repository;
        _walletRepository = walletRepository;
    }

    public async Task HandleAsync(AddGoogleCustomer command, CancellationToken cancellationToken = default)
    {
        if (command is null)
        {
            throw new ArgumentNullException(nameof(command));
        }

        if (command.Id == Guid.Empty)
        {
            throw new ArgumentException("Google customer id cannot be empty.");
        }

        if (string.IsNullOrEmpty(command.FirstName))
        {
            throw new ArgumentException("Google customer first name cannot be empty.");
        }

        if (string.IsNullOrEmpty(command.LastName))
        {
            throw new ArgumentException("Google customer last name cannot be empty.");
        }

        if (command.DateOfBirth == DateOnly.MinValue)
        {
            throw new ArgumentException("Google customer date of birth is invalid.");
        }

        if (string.IsNullOrEmpty(command.Email))
        {
            throw new ArgumentException("Google customer email cannot be empty.");
        }

        if (string.IsNullOrEmpty(command.Region))
        {
            throw new ArgumentException("Google customer region cannot be empty.");
        }

        if (string.IsNullOrEmpty(command.GoogleId))
        {
            throw new ArgumentException("Google customer type is invalid.");
        }
        
        var existingWallet = await _walletRepository.GetAsync(w => w.CustomerId == command.Id);

        if (existingWallet is not null)
        {
            throw new InvalidOperationException($"Customer wallet with customer id: '{command.Id}' already exists.");
        }

        var wallet = new CustomerWalletDocument
        {
            Id = Guid.NewGuid(),
            CustomerId = command.Id,
            Currency = Currencies.TRY,
            Type = WalletTypes.Customer,
            Amount = 0,
            CreatedAt = DateTimeOffset.UtcNow,
            UpdatedAt = null
        };

        await _walletRepository.AddAsync(wallet);

        var random = new Random();

        var verificationCode = random.Next(123456, 987654).ToString();

        await _intergrationService.GetGoogleTokenAsync(new SendEmailVerificationDto
        {
            Email = command.Email,
            Code = verificationCode,
            Username = $"{command.FirstName} {command.LastName}"
        });

        var user = new GoogleCustomerDocument
        {
            Id = command.Id,
            Email = command.Email,
            PhoneNumber = command.PhoneNumber,
            Region = command.Region,
            Type = UserTypes.GoogleCustomer,
            TermsAndServicesAccepted = false,
            Verified = false,
            VerificationCode = verificationCode,
            CreatedAt = DateTimeOffset.UtcNow,
            UpdatedAt = null,
            FirstName = command.FirstName,
            LastName = command.LastName,
            DateOfBirth = command.DateOfBirth,
            GoogleId = command.GoogleId
        };

        await _repository.AddAsync(user);
    }
}