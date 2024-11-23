using System;
using System.Threading;
using System.Threading.Tasks;
using Activite.Services.User.Mongo.Documents;
using Convey.CQRS.Commands;
using Convey.Persistence.MongoDB;

namespace Activite.Services.User.CQRS.Commands.Handlers;

public class UpdatePercentageCouponHandler : ICommandHandler<UpdatePercentageCoupon>
{
    private readonly IMongoRepository<PercentageCouponDocument, Guid> _repository;

    public UpdatePercentageCouponHandler(IMongoRepository<PercentageCouponDocument, Guid> repository)
    {
        _repository = repository;
    }

    public async Task HandleAsync(UpdatePercentageCoupon command, CancellationToken cancellationToken = default)
    {
        if (command is null)
        {
            throw new ArgumentNullException(nameof(command));
        }

        if (command.Id == Guid.Empty)
        {
            throw new ArgumentException("Coupon id cannot be empty.");
        }

        if (command.UsedAt == default)
        {
            throw new ArgumentException("Used at cannot be default.");
        }

        var coupon = await _repository.GetAsync(command.Id);

        if (coupon is null)
        {
            throw new InvalidOperationException($"Coupon with id: '{command.Id}' was not found.");
        }

        if (coupon.UsedAt.HasValue)
        {
            throw new InvalidOperationException($"Coupon with id: '{command.Id}' was already used at: '{coupon.UsedAt}'.");
        }

        coupon.UsedAt = command.UsedAt;

        await _repository.UpdateAsync(coupon);
    }
}
