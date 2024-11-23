using System;

namespace Activite.Services.User.Mongo.Documents;

public class CustomerWalletDocument : WalletDocument
{
    public Guid CustomerId { get; set; }
}