using System;
using Activite.Services.User.Attributes;
using Convey.CQRS.Commands;

namespace Activite.Services.User.CQRS.Commands;

[Contract]
public class AddLocationComment : ICommand
{
    public Guid Id { get; set; }

    public Guid EventId { get; set; }

    public string Content { get; set; }

    public Guid LocationId { get; set; }

    public Guid CustomerCommentId { get; set; }

    public AddLocationComment(
        Guid id,
        Guid eventId,
        string content,
        Guid locationId,
        Guid customerCommentId)
    {
        Id = id;
        EventId = eventId;
        Content = content;
        LocationId = locationId;
        CustomerCommentId = customerCommentId;
    }
}