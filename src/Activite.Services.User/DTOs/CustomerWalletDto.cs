using System;

namespace Activite.Services.User.DTOs;

public class CustomerWalletDto : WalletDto
{
    public Guid CustomerId { get; set; }
}