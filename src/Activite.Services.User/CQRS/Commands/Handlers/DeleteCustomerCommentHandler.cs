using System;
using System.Threading;
using System.Threading.Tasks;
using Activite.Services.User.Mongo.Documents;
using Convey.CQRS.Commands;
using Convey.Persistence.MongoDB;

namespace Activite.Services.User.CQRS.Commands.Handlers;

public class DeleteCustomerCommentHandler : ICommandHandler<DeleteCustomerComment>
{
    private readonly IMongoRepository<CustomerCommentDocument, Guid> _repository;

    public DeleteCustomerCommentHandler(IMongoRepository<CustomerCommentDocument, Guid> repository)
    {
        _repository = repository;
    }

    public async Task HandleAsync(DeleteCustomerComment command, CancellationToken cancellationToken = default)
    {
        if (command is null)
        {
            throw new ArgumentNullException(nameof(command));
        }

        if (command.Id == Guid.Empty)
        {
            throw new ArgumentException("Comment id cannot be empty.");
        }

        var customerComment = await _repository.GetAsync(command.Id);

        if (customerComment is null)
        {
            throw new InvalidOperationException($"Comment with id: '{command.Id}' was not found.");
        }

        await _repository.DeleteAsync(command.Id);
    }
}
