using System;
using Activite.Services.User.Attributes;
using Convey.CQRS.Commands;

namespace Activite.Services.User.CQRS.Commands;

[Contract]
public class AddCustomerComment : ICommand
{
    public Guid Id { get; set; }

    public Guid EventId { get; set; }

    public string Content { get; set; }

    public Guid CustomerId { get; set; }

    public int Rating { get; set; }

    public AddCustomerComment(
        Guid id,
        Guid eventId,
        string content,
        Guid customerId,
        int rating)
    {
        Id = id;
        EventId = eventId;
        Content = content;
        CustomerId = customerId;
        Rating = rating;
    }
}