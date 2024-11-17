using System;
using Activite.Services.User.Attributes;
using Convey.CQRS.Commands;

namespace Activite.Services.User.CQRS.Commands;

[Contract]
public class AddGoogleCustomer : ICommand
{
    public Guid Id { get; set; }

    public string FirstName { get; set; }

    public string LastName { get; set; }

    public DateOnly DateOfBirth { get; set; }

    public string Email { get; set; }

    public string PhoneNumber { get; set; }

    public string Region { get; set; }

    public string GoogleId { get; set; }

    public AddGoogleCustomer(
        Guid id,
        string firstName,
        string lastName,
        DateOnly dateOfBirth,
        string email,
        string phoneNumber,
        string region,
        string googleId)
    {
        Id = id;
        FirstName = firstName;
        LastName = lastName;
        DateOfBirth = dateOfBirth;
        Email = email;
        PhoneNumber = phoneNumber;
        Region = region;
        GoogleId = googleId;
    }
}