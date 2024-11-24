using System;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Activite.Services.User.DTOs;
using Activite.Services.User.Mongo.Documents;
using Convey.CQRS.Queries;
using Convey.Persistence.MongoDB;

namespace Activite.Services.User.CQRS.Queries.Handlers;

public class GetMultipleTranslationsHandler : IQueryHandler<GetMultipleTranslations, PagedResult<TranslationDto>>
{
    private readonly IMongoRepository<TranslationDocument, Guid> _repository;

    public GetMultipleTranslationsHandler(IMongoRepository<TranslationDocument, Guid> repository)
    {
        _repository = repository;
    }

    public async Task<PagedResult<TranslationDto>> HandleAsync(GetMultipleTranslations query, CancellationToken cancellationToken = default)
    {
        var predicate = GetPredicate(query);

        var translations = await _repository.BrowseAsync(predicate, query);

        if (translations is null || translations.IsEmpty)
        {
            return PagedResult<TranslationDto>.Empty;
        }

        return translations.Map(translation => new TranslationDto
        {
            Id = translation.Id,
            Code = translation.Code,
            Content = translation.Content,
            Region = translation.Region,
            CreatedAt = translation.CreatedAt,
            UpdatedAt = translation.UpdatedAt
        });
    }

    private static Expression<Func<TranslationDocument, bool>> GetPredicate(GetMultipleTranslations query)
    {
        Expression<Func<TranslationDocument, bool>> expression = null;

        if (query.Id.HasValue)
        {
            expression = expression.And(translation => translation.Id == query.Id);
        }

        if (query.Ids is not null && query.Ids.Count > 0)
        {
            expression = expression.And(translation => query.Ids.Contains(translation.Id));
        }

        if (!string.IsNullOrWhiteSpace(query.Code))
        {
            expression = expression.And(translation => translation.Code == query.Code);
        }

        if (!string.IsNullOrWhiteSpace(query.Content))
        {
            expression = expression.And(translation => translation.Content == query.Content);
        }

        if (!string.IsNullOrWhiteSpace(query.SearchContent))
        {
            expression = expression.And(translation => translation.Content.Contains(query.SearchContent));
        }

        if (!string.IsNullOrWhiteSpace(query.Region))
        {
            expression = expression.And(translation => translation.Region == query.Region);
        }

        return expression;
    }
}