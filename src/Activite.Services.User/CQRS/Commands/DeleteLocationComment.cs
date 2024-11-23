using System;
using Activite.Services.User.Attributes;
using Convey.CQRS.Commands;

namespace Activite.Services.User.CQRS.Commands;

[Contract]
public class DeleteLocationComment : ICommand
{
    public Guid Id { get; set;}

    public DeleteLocationComment(Guid id)
    {
        Id = id;
    }
}