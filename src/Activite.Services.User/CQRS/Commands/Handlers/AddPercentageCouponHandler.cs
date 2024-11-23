using System;
using System.Threading;
using System.Threading.Tasks;
using Activite.Services.User.Constants;
using Activite.Services.User.Mongo.Documents;
using Convey.CQRS.Commands;
using Convey.Persistence.MongoDB;

namespace Activite.Services.User.CQRS.Commands.Handlers;

public class AddPercentageCouponHandler : ICommandHandler<AddPercentageCoupon>
{
    private readonly IMongoRepository<PercentageCouponDocument, Guid> _repository;

    public AddPercentageCouponHandler(IMongoRepository<PercentageCouponDocument, Guid> repository)
    {
        _repository = repository;
    }

    public async Task HandleAsync(AddPercentageCoupon command, CancellationToken cancellationToken = default)
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

        var percentageCoupon = new PercentageCouponDocument
        {
            Id = command.Id,
            Description = command.Description,
            Currency = command.Currency,
            Name = command.Name,
            Type = CouponTypes.Percentage,
            MinimalSpendingAmount = command.MinimalSpendingAmount,
            UsedAt = null,
            CreatedAt = DateTimeOffset.UtcNow,
            Percentage = command.Percentage,
            MaxDiscountAmount = command.MaxDiscountAmount
        };

        await _repository.AddAsync(percentageCoupon);
    }
}