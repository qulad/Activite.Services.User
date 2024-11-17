using System;
using Activite.Services.User.Attributes;
using Convey.CQRS.Commands;

namespace Activite.Services.User.CQRS.Commands;

[Contract]
public class AddGoogleLocation : ICommand
{
    public Guid Id { get; set; }

    public string Address { get; set; }

    public string Name { get; set; }

    public string Description { get; set; }

    public DateOnly EstabilishedDate { get; set; }

    public string Email { get; set; }

    public string PhoneNumber { get; set; }

    public string Region { get; set; }

    public string GoogleId { get; set; }

    public AddGoogleLocation(
        Guid id,
        string address,
        string name,
        string description,
        DateOnly estabilishedDate,
        string email,
        string phoneNumber,
        string region,
        string googleId)
    {
        Id = id;
        Address = address;
        Name = name;
        Description = description;
        EstabilishedDate = estabilishedDate;
        Email = email;
        PhoneNumber = phoneNumber;
        Region = region;
        GoogleId = googleId;
    }
}