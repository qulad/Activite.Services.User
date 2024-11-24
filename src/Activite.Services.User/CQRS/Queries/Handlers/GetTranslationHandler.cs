using System;
using System.Threading;
using System.Threading.Tasks;
using Activite.Services.User.DTOs;
using Activite.Services.User.Mongo.Documents;
using Convey.CQRS.Queries;
using Convey.Persistence.MongoDB;

namespace Activite.Services.User.CQRS.Queries.Handlers;

public class GetTranslationHandler : IQueryHandler<GetTranslation, TranslationDto>
{
    private readonly IMongoRepository<TranslationDocument, Guid> _repository;

    public GetTranslationHandler(IMongoRepository<TranslationDocument, Guid> repository)
    {
        _repository = repository;
    }

    public async Task<TranslationDto> HandleAsync(GetTranslation query, CancellationToken cancellationToken = default)
    {
        var translation = await _repository.GetAsync(query.Id);

        if (translation is null)
        {
            return new TranslationDto();
        }

        return new TranslationDto
        {
            Id = translation.Id,
            Code = translation.Code,
            Content = translation.Content,
            Region = translation.Region,
            CreatedAt = translation.CreatedAt,
            UpdatedAt = translation.UpdatedAt
        };
    }
}
