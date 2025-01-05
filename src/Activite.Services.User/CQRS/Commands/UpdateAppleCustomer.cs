using System;
using Activite.Services.User.Attributes;
using Convey.CQRS.Commands;

namespace Activite.Services.User.CQRS.Commands;

[Contract]
public class UpdateAppleCustomer : ICommand
{
    public Guid Id { get; set; }

    public string FirstName { get; set; }

    public string LastName { get; set; }

    public bool TermsAndServicesAccepted { get; set; }

    public UpdateAppleCustomer(
        Guid id,
        string firstName,
        string lastName,
        bool termsAndServicesAccepted)
    {
        Id = id;
        FirstName = firstName;
        LastName = lastName;
        TermsAndServicesAccepted = termsAndServicesAccepted;
    }
}