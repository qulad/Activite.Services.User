using System;
using Activite.Services.User.Attributes;
using Convey.CQRS.Commands;

namespace Activite.Services.User.CQRS.Commands;

[Contract]
public class UpdateAppleUser : ICommand
{
    public Guid Id { get; set; }

    public string PhoneNumber { get; set; }

    public string Region { get; set; }

    public bool TermsAndServicesAccepted { get; set; }

    public bool Verified { get; set; }

    public UpdateAppleUser(
        Guid id,
        string phoneNumber,
        string region,
        bool termsAndServicesAccepted,
        bool verified)
    {
        Id = id;
        PhoneNumber = phoneNumber;
        Region = region;
        TermsAndServicesAccepted = termsAndServicesAccepted;
        Verified = verified;
    }
}