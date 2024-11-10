using System;
using Activite.Services.User.Attributes;
using Convey.CQRS.Commands;

namespace Activite.Services.User.CQRS.Commands;

[Contract]
public class AddUser : ICommand
{
    public Guid Id { get; set; }

    public string Email { get; set; }

    public string PhoneNumber { get; set; }

    public string Region { get; set; }

    public string Type { get; set; }

    public AddUser(
        Guid id,
        string email,
        string phoneNumber,
        string region,
        string type)
    {
        Id = id;
        Email = email;
        PhoneNumber = phoneNumber;
        Region = region;
        Type = type;
    }
}