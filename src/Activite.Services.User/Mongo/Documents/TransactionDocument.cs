using System;
using Convey.Types;

namespace Activite.Services.User.Mongo.Documents;

public class TransactionDocument : IIdentifiable<Guid>
{
    public const string CollectionName = "Transactions";

    public Guid Id { get; set; }

    public Guid CustomerId { get; set; }

    public Guid LocationId { get; set; }

    public string Currency { get; set; }

    public decimal Amount { get; set; }

    public DateTimeOffset CreatedAt { get; set; }
}