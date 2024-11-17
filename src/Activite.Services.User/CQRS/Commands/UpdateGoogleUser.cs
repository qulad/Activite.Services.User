using System;
using Activite.Services.User.Attributes;
using Convey.CQRS.Commands;

namespace Activite.Services.User.CQRS.Commands;

[Contract]
public class UpdateGoogleCustomer : ICommand
{
    public Guid Id { get; set; }

    public string FirstName { get; set; }

    public string LastName { get; set; }

    public bool TermsAndServicesAccepted { get; set; }

    public bool Verified { get; set; }

    public UpdateGoogleCustomer(
        Guid id,
        string firstName,
        string lastName,
        bool termsAndServicesAccepted,
        bool verified)
    {
        Id = id;
        FirstName = firstName;
        LastName = lastName;
        TermsAndServicesAccepted = termsAndServicesAccepted;
        Verified = verified;
    }
}