using System;

namespace Activite.Services.User.Mongo.Documents;

public class LocationWalletDocument : WalletDocument
{
    public Guid LocationId { get; set; }
}