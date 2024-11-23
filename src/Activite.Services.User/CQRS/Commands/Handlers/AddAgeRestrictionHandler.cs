using System;
using System.Threading;
using System.Threading.Tasks;
using Activite.Services.User.Mongo.Documents;
using Convey.CQRS.Commands;
using Convey.Persistence.MongoDB;

namespace Activite.Services.User.CQRS.Commands.Handlers;

public class AddAgeRestrictionHandler : ICommandHandler<AddAgeRestriction>
{
    private readonly IMongoRepository<AgeRestrictionDocument, Guid> _repository;

    public AddAgeRestrictionHandler(IMongoRepository<AgeRestrictionDocument, Guid> repository)
    {
        _repository = repository;
    }

    public async Task HandleAsync(AddAgeRestriction command, CancellationToken cancellationToken = default)
    {
        if (command is null)
        {
            throw new ArgumentNullException(nameof(command));
        }

        if (command.Id == Guid.Empty)
        {
            throw new ArgumentException("Age restriction id cannot be empty.");
        }

        if (command.Age == 0)
        {
            throw new ArgumentException("Age restriction age cannot be empty.");
        }

        if (string.IsNullOrEmpty(command.Code))
        {
            throw new ArgumentException("Age restriction code cannot be empty.");
        }

        var ageRestriction = new AgeRestrictionDocument
        {
            Id = command.Id,
            Age = command.Age,
            Code = command.Code,
            CreatedAt = DateTimeOffset.UtcNow,
            UpdatedAt = null
        };

        await _repository.AddAsync(ageRestriction);
    }
}
