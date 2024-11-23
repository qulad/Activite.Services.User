using System;
using System.Threading;
using System.Threading.Tasks;
using Activite.Services.User.Constants;
using Activite.Services.User.Mongo.Documents;
using Convey.CQRS.Commands;
using Convey.Persistence.MongoDB;

namespace Activite.Services.User.CQRS.Commands.Handlers;

public class AddCustomerCommentHandler : ICommandHandler<AddCustomerComment>
{
    private readonly IMongoRepository<CustomerCommentDocument, Guid> _repository;

    public AddCustomerCommentHandler(IMongoRepository<CustomerCommentDocument, Guid> repository)
    {
        _repository = repository;
    }

    public async Task HandleAsync(AddCustomerComment command, CancellationToken cancellationToken = default)
    {
        if (command is null)
        {
            throw new ArgumentNullException(nameof(command));
        }

        if (command.Id == Guid.Empty)
        {
            throw new ArgumentException("Customer comment id cannot be empty.");
        }

        if (command.EventId == Guid.Empty)
        {
            throw new ArgumentException("Customer comment event id cannot be empty.");
        }

        if (string.IsNullOrEmpty(command.Content))
        {
            throw new ArgumentException("Customer comment content cannot be empty.");
        }

        if (command.CustomerId == Guid.Empty)
        {
            throw new ArgumentException("Customer comment customer id cannot be empty.");
        }

        var customerComment = new CustomerCommentDocument
        {
            Id = command.Id,
            EventId = command.EventId,
            Content = command.Content,
            Type = CommentTypes.Customer,
            CustomerId = command.CustomerId,
            Rating = command.Rating,
            CreatedAt = DateTimeOffset.UtcNow,
            UpdatedAt = null
        };

        await _repository.AddAsync(customerComment);
    }
}
