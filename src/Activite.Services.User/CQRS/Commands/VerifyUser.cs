using System;
using Convey.CQRS.Commands;

namespace Activite.Services.User.CQRS.Commands;

public class VerifyUser : ICommand
{
    public Guid UserId { get; }
    public string VerificationCode { get; }

    public VerifyUser(Guid userId, string verificationCode)
    {
        UserId = userId;
        VerificationCode = verificationCode;
    }
}