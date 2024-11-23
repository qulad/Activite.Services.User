using System;
using System.Threading;
using System.Threading.Tasks;
using Activite.Services.User.Constants;
using Activite.Services.User.Mongo.Documents;
using Convey.CQRS.Commands;
using Convey.Persistence.MongoDB;

namespace Activite.Services.User.CQRS.Commands.Handlers;

public class AddTicketHandler : ICommandHandler<AddTicket>
{
    private readonly IMongoRepository<TicketDocument, Guid> _repository;
    private readonly IMongoRepository<CustomerWalletDocument, Guid> _customerWalletRepository;
    private readonly IMongoRepository<TransactionDocument, Guid> _transactionRepository;
    private readonly IMongoRepository<EventDocument, Guid> _eventRepository;

    public AddTicketHandler(
        IMongoRepository<TicketDocument, Guid> repository,
        IMongoRepository<CustomerWalletDocument, Guid> walletRepository,
        IMongoRepository<TransactionDocument, Guid> transactionRepository,
        IMongoRepository<EventDocument, Guid> eventRepository)
    {
        _repository = repository;
        _customerWalletRepository = walletRepository;
        _transactionRepository = transactionRepository;
        _eventRepository = eventRepository;
    }

    public async Task HandleAsync(AddTicket command, CancellationToken cancellationToken = default)
    {
        if (command is null)
        {
            throw new ArgumentNullException(nameof(command));
        }

        if (command.Id == Guid.Empty)
        {
            throw new ArgumentException("Ticket id cannot be empty.");
        }

        if (command.CustomerId == Guid.Empty)
        {
            throw new ArgumentException("Ticket customer id cannot be empty.");
        }

        if (command.EventId == Guid.Empty)
        {
            throw new ArgumentException("Ticket event id cannot be empty.");
        }

        if (string.IsNullOrEmpty(command.Currency))
        {
            throw new ArgumentException("Ticket currency cannot be empty.");
        }

        if (Currencies.All.Contains(command.Currency))
        {
            throw new ArgumentException("Ticket currency is invalid.");
        }

        if (command.Amount <= 0)
        {
            throw new ArgumentException("Ticket price is invalid.");
        }

        var wallet = await _customerWalletRepository.GetAsync(w => w.CustomerId == command.CustomerId);

        if (wallet is null)
        {
            throw new InvalidOperationException($"Wallet for customer with id: '{command.CustomerId}' was not found.");
        }

        var @event = await _eventRepository.GetAsync(command.EventId);

        if (@event is null)
        {
            throw new InvalidOperationException($"Event with id: '{command.EventId}' was not found.");
        }

        if (wallet.Currency != command.Currency)
        {
            throw new InvalidOperationException($"Wallet for customer with id: '{command.CustomerId}' has invalid currency.");
        }

        if (wallet.Amount < command.Amount)
        {
            throw new InvalidOperationException($"Customer with id: '{command.CustomerId}' has insufficient funds.");
        }

        wallet.Amount -= command.Amount;

        await _customerWalletRepository.UpdateAsync(wallet);

        var transaction = new TransactionDocument
        {
            Id = Guid.NewGuid(),
            CustomerId = command.CustomerId,
            LocationId = @event.LocationId,
            Amount = command.Amount,
            Currency = command.Currency,
            CreatedAt = DateTimeOffset.UtcNow
        };

        await _transactionRepository.AddAsync(transaction);

        var ticket = new TicketDocument
        {
            Id = command.Id,
            CustomerId = command.CustomerId,
            EventId = command.EventId,
            CouponId = command.CouponId,
            Amount = command.Amount,
            Currency = command.Currency,
            CreatedAt = DateTimeOffset.UtcNow
        };

        await _repository.AddAsync(ticket);
    }
}