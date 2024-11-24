using System;

namespace Activite.Services.User.DTOs;

public class LocationWalletDto : WalletDto
{
    public Guid LocationId { get; set; }
}