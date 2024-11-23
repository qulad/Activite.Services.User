using System;
using System.Threading;
using System.Threading.Tasks;
using Activite.Services.User.Constants;
using Activite.Services.User.Mongo.Documents;
using Convey.CQRS.Commands;
using Convey.Persistence.MongoDB;

namespace Activite.Services.User.CQRS.Commands.Handlers;

public class AddAmountCouponHandler : ICommandHandler<AddAmountCoupon>
{
    private readonly IMongoRepository<AmountCouponDocument, Guid> _repository;

    public AddAmountCouponHandler(IMongoRepository<AmountCouponDocument, Guid> repository)
    {
        _repository = repository;
    }

    public async Task HandleAsync(AddAmountCoupon command, CancellationToken cancellationToken = default)
    {
        if (command is null)
        {
            throw new ArgumentNullException(nameof(command));
        }

        if (command.Id == Guid.Empty)
        {
            throw new ArgumentException("Coupon id cannot be empty.");
        }

        if (string.IsNullOrEmpty(command.Description))
        {
            throw new ArgumentException("Coupon description cannot be empty.");
        }

        if (string.IsNullOrEmpty(command.Currency))
        {
            throw new ArgumentException("Coupon currency cannot be empty.");
        }

        if (!Currencies.All.Contains(command.Currency))
        {
            throw new ArgumentException("Coupon currency is invalid.");
        }

        if (string.IsNullOrEmpty(command.Name))
        {
            throw new ArgumentException("Coupon name cannot be empty.");
        }

        if (command.ExpiresAt == default)
        {
            throw new ArgumentException("Coupon expiration date cannot be empty.");
        }

        var amountCoupon = new AmountCouponDocument
        {
            Id = command.Id,
            Description = command.Description,
            Currency = command.Currency,
            Name = command.Name,
            Type = CouponTypes.Amount,
            MinimalSpendingAmount = command.MinimalSpendingAmount,
            ExpiresAt = command.ExpiresAt,
            UsedAt = null,
            CreatedAt = DateTimeOffset.UtcNow,
            Amount = command.Amount
        };

        await _repository.AddAsync(amountCoupon);
    }
}