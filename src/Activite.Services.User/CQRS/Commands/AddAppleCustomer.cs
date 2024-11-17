using System;
using Activite.Services.User.Attributes;
using Convey.CQRS.Commands;

namespace Activite.Services.User.CQRS.Commands;

[Contract]
public class AddAppleCustomer : ICommand
{
    public Guid Id { get; set; }

    public string FirstName { get; set; }

    public string LastName { get; set; }

    public DateOnly DateOfBirth { get; set; }

    public string Email { get; set; }

    public string PhoneNumber { get; set; }

    public string Region { get; set; }

    public string AppleId { get; set; }

    public AddAppleCustomer(
        Guid id,
        string firstName,
        string lastName,
        DateOnly dateOfBirth,
        string email,
        string phoneNumber,
        string region,
        string appleId)
    {
        Id = id;
        FirstName = firstName;
        LastName = lastName;
        DateOfBirth = dateOfBirth;
        Email = email;
        PhoneNumber = phoneNumber;
        Region = region;
        AppleId = appleId;
    }
}