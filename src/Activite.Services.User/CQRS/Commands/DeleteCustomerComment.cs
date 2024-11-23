using System;
using Activite.Services.User.Attributes;
using Convey.CQRS.Commands;

namespace Activite.Services.User.CQRS.Commands;

[Contract]
public class DeleteCustomerComment : ICommand
{
    public Guid Id { get; set;}

    public DeleteCustomerComment(Guid id)
    {
        Id = id;
    }
}