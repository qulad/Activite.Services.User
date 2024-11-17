using System;
using Activite.Services.User.Attributes;
using Convey.CQRS.Commands;

namespace Activite.Services.User.CQRS.Commands;

[Contract]
public class UpdateGoogleLocation : ICommand
{
    public Guid Id { get; set; }

    public string Address { get; set; }

    public string Name { get; set; }

    public string Description { get; set; }

    public bool TermsAndServicesAccepted { get; set; }

    public bool Verified { get; set; }

    public UpdateGoogleLocation(
        Guid id,
        string address,
        string name,
        string description,
        bool termsAndServicesAccepted,
        bool verified)
    {
        Id = id;
        Address = address;
        Name = name;
        Description = description;
        TermsAndServicesAccepted = termsAndServicesAccepted;
        Verified = verified;
    }
}