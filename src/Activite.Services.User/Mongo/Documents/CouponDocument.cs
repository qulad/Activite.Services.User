using System;
using Convey.Types;

namespace Activite.Services.User.Mongo.Documents;

public class CouponDocument : IIdentifiable<Guid>
{
    public const string CollectionName = "Coupons";

    public Guid Id { get; set; }

    public string Description { get; set; }

    public string Name { get; set; }

    public string Type { get; set; }

    public decimal MinimalSpendingAmount { get; set; }

    public DateTimeOffset? UsedAt { get; set; }

    public DateTimeOffset CreatedAt { get; set; }
}