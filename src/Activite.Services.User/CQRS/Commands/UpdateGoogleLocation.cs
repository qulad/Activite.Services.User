using System;
using Activite.Services.User.Attributes;
using Convey.CQRS.Commands;

namespace Activite.Services.User.CQRS.Commands;

[Contract]
public class UpdateGoogleLocation : ICommand
{
    public Guid Id { get; set; }

    public string PhoneNumber { get; set; }

    public string Region { get; set; }

    public string Location { get; set; }

    public bool TermsAndServicesAccepted { get; set; }

    public bool Verified { get; set; }

    public UpdateGoogleLocation(
        Guid id,
        string phoneNumber,
        string region,
        string location,
        bool termsAndServicesAccepted,
        bool verified)
    {
        Id = id;
        PhoneNumber = phoneNumber;
        Region = region;
        Location = location;
        TermsAndServicesAccepted = termsAndServicesAccepted;
        Verified = verified;
    }
}