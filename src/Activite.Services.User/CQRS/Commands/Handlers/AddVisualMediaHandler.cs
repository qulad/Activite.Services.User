using System;
using System.Threading;
using System.Threading.Tasks;
using Activite.Services.User.Constants;
using Activite.Services.User.Mongo.Documents;
using Convey.CQRS.Commands;
using Convey.Persistence.MongoDB;

namespace Activite.Services.User.CQRS.Commands.Handlers;

public class AddVisualMediaHandler : ICommandHandler<AddVisualMedia>
{
    private readonly IMongoRepository<VisualMediaDocument, Guid> _repository;

    public AddVisualMediaHandler(IMongoRepository<VisualMediaDocument, Guid> repository)
    {
        _repository = repository;
    }

    public async Task HandleAsync(AddVisualMedia command, CancellationToken cancellationToken = default)
    {
        if (command is null)
        {
            throw new ArgumentNullException(nameof(command));
        }

        if (command.Id == Guid.Empty)
        {
            throw new ArgumentException("Visual media id cannot be empty.");
        }

        if (string.IsNullOrEmpty(command.Type))
        {
            throw new ArgumentException("Visual media type cannot be empty.");
        }

        if (!VisualMediaTypes.All.Contains(command.Type))
        {
            throw new ArgumentException("Visual media type is invalid.");
        }

        if (string.IsNullOrEmpty(command.Content))
        {
            throw new ArgumentException("Visual media content cannot be empty.");
        }

        var visualMedia = new VisualMediaDocument
        {
            Id = command.Id,
            Type = command.Type,
            Content = command.Content,
            CreatedAt = DateTimeOffset.UtcNow,
            UpdatedAt = null
        };

        await _repository.AddAsync(visualMedia);
    }
}