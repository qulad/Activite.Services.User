using System;
using Activite.Services.User.Attributes;
using Convey.CQRS.Commands;

namespace Activite.Services.User.CQRS.Commands;

[Contract]
public class UpdateLocationComment : ICommand
{
    public Guid Id { get; set; }

    public string Content { get; set; }

    public UpdateLocationComment(Guid id, string content)
    {
        Id = id;
        Content = content;
    }
}