using System;
using Activite.Services.User.Attributes;
using Convey.CQRS.Commands;

namespace Activite.Services.User.CQRS.Commands;

[Contract]
public class AddVisualMedia : ICommand
{
    public Guid Id { get; set; }

    public string Type { get; set; }

    public string Content { get; set; }

    public AddVisualMedia(
        Guid id,
        string type,
        string content)
    {
        Id = id;
        Type = type;
        Content = content;
    }
}