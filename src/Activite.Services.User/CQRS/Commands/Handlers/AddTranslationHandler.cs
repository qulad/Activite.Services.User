using System;
using System.Threading;
using System.Threading.Tasks;
using Activite.Services.User.Constants;
using Activite.Services.User.Mongo.Documents;
using Convey.CQRS.Commands;
using Convey.Persistence.MongoDB;

namespace Activite.Services.User.CQRS.Commands.Handlers;

public class AddTranslationHandler : ICommandHandler<AddTranslation>
{
    private readonly IMongoRepository<TranslationDocument, Guid> _repository;

    public AddTranslationHandler(IMongoRepository<TranslationDocument, Guid> repository)
    {
        _repository = repository;
    }

    public async Task HandleAsync(AddTranslation command, CancellationToken cancellationToken = default)
    {
        if (command is null)
        {
            throw new ArgumentNullException(nameof(command));
        }

        if (command.Id == Guid.Empty)
        {
            throw new ArgumentException("Translation id cannot be empty.");
        }

        if (string.IsNullOrEmpty(command.Code))
        {
            throw new ArgumentException("Translation code cannot be empty.");
        }

        if (string.IsNullOrEmpty(command.Content))
        {
            throw new ArgumentException("Translation content cannot be empty.");
        }

        if (string.IsNullOrEmpty(command.Region))
        {
            throw new ArgumentException("Translation language cannot be empty.");
        }

        if (Regions.All.Contains(command.Region))
        {
            throw new ArgumentException("Translation language is invalid.");
        }

        var translation = new TranslationDocument
        {
            Id = command.Id,
            Code = command.Code,
            Content = command.Content,
            Region = command.Region,
            CreatedAt = DateTimeOffset.UtcNow,
            UpdatedAt = null
        };

        await _repository.AddAsync(translation);
    }
}