using System;
using System.Threading;
using System.Threading.Tasks;
using Activite.Services.User.Constants;
using Activite.Services.User.Mongo.Documents;
using Convey.CQRS.Commands;
using Convey.Persistence.MongoDB;

namespace Activite.Services.User.CQRS.Commands.Handlers;

public class UpdateCustomerCommentHandler : ICommandHandler<UpdateCustomerComment>
{
    private readonly IMongoRepository<CustomerCommentDocument, Guid> _repository;

    public UpdateCustomerCommentHandler(IMongoRepository<CustomerCommentDocument, Guid> repository)
    {
        _repository = repository;
    }

    public async Task HandleAsync(UpdateCustomerComment command, CancellationToken cancellationToken = default)
    {
        if (command is null)
        {
            throw new ArgumentNullException(nameof(command));
        }

        if (command.Id == Guid.Empty)
        {
            throw new ArgumentException("Comment id cannot be empty.");
        }

        var comment = await _repository.GetAsync(command.Id);

        if (comment is null)
        {
            throw new InvalidOperationException($"Comment with id: '{command.Id}' was not found.");
        }

        if (string.IsNullOrEmpty(command.Content))
        {
            throw new ArgumentException("Comment content cannot be empty.");
        }

        comment.Content = command.Content;

        await _repository.UpdateAsync(comment);
    }
}
