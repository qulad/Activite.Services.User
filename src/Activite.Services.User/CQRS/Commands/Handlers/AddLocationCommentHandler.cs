using System;
using System.Threading;
using System.Threading.Tasks;
using Activite.Services.User.Constants;
using Activite.Services.User.Mongo.Documents;
using Convey.CQRS.Commands;
using Convey.Persistence.MongoDB;

namespace Activite.Services.User.CQRS.Commands.Handlers;

public class AddLocationCommentHandler : ICommandHandler<AddLocationComment>
{
    private readonly IMongoRepository<LocationCommentDocument, Guid> _repository;

    public AddLocationCommentHandler(IMongoRepository<LocationCommentDocument, Guid> repository)
    {
        _repository = repository;
    }

    public async Task HandleAsync(AddLocationComment command, CancellationToken cancellationToken = default)
    {
        if (command is null)
        {
            throw new ArgumentNullException(nameof(command));
        }

        if (command.Id == Guid.Empty)
        {
            throw new ArgumentException("Comment id cannot be empty.");
        }

        if (command.EventId == Guid.Empty)
        {
            throw new ArgumentException("Comment event id cannot be empty.");
        }

        if (string.IsNullOrEmpty(command.Content))
        {
            throw new ArgumentException("Comment content cannot be empty.");
        }

        if (command.LocationId == Guid.Empty)
        {
            throw new ArgumentException("Comment location id cannot be empty.");
        }

        if (command.CustomerCommentId == Guid.Empty)
        {
            throw new ArgumentException("Comment customer comment id cannot be empty.");
        }

        var customerComment = new LocationCommentDocument
        {
            Id = command.Id,
            EventId = command.EventId,
            Content = command.Content,
            Type = CommentTypes.Location,
            LocationId = command.LocationId,
            CustomerCommentId = command.CustomerCommentId,
            CreatedAt = DateTimeOffset.UtcNow,
            UpdatedAt = null
        };

        await _repository.AddAsync(customerComment);
    }
}
