using System;
using Convey.Types;

namespace Activite.Services.User.Mongo.Documents;

public class WalletDocument : IIdentifiable<Guid>
{
    public const string CollectionName = "Wallets";

    public Guid Id { get; set; }

    public string Currency { get; set; }

    public string Type { get; set; }

    public decimal Amount { get; set; }

    public DateTimeOffset CreatedAt { get; set; }

    public DateTimeOffset? UpdatedAt { get; set; }
}