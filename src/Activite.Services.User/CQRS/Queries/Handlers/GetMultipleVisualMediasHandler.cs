using System;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Activite.Services.User.DTOs;
using Activite.Services.User.Mongo.Documents;
using Convey.CQRS.Queries;
using Convey.Persistence.MongoDB;

namespace Activite.Services.User.CQRS.Queries.Handlers;

public class GetMultipleVisualMediasHandler : IQueryHandler<GetMultipleVisualMedias, PagedResult<VisualMediaDto>>
{
    private readonly IMongoRepository<VisualMediaDocument, Guid> _repository;

    public GetMultipleVisualMediasHandler(IMongoRepository<VisualMediaDocument, Guid> repository)
    {
        _repository = repository;
    }

    public async Task<PagedResult<VisualMediaDto>> HandleAsync(GetMultipleVisualMedias query, CancellationToken cancellationToken = default)
    {
        var predicate = GetPredicate(query);

        var visualMedias = await _repository.BrowseAsync(predicate, query);

        if (visualMedias is null || visualMedias.IsEmpty)
        {
            return PagedResult<VisualMediaDto>.Empty;
        }

        return visualMedias.Map(visualMedia => new VisualMediaDto
        {
            Id = visualMedia.Id,
            Type = visualMedia.Type,
            Content = visualMedia.Content,
            CreatedAt = visualMedia.CreatedAt,
            UpdatedAt = visualMedia.UpdatedAt
        });
    }

    private static Expression<Func<VisualMediaDocument, bool>> GetPredicate(GetMultipleVisualMedias query)
    {
        Expression<Func<VisualMediaDocument, bool>> expression = null;

        if (query.Id.HasValue)
        {
            expression = expression.And(visualMedia => visualMedia.Id == query.Id);
        }

        if (query.Ids is not null && query.Ids.Count > 0)
        {
            expression = expression.And(visualMedia => query.Ids.Contains(visualMedia.Id));
        }

        if (!string.IsNullOrEmpty(query.Type))
        {
            expression = expression.And(visualMedia => visualMedia.Type == query.Type);
        }

        if (!string.IsNullOrEmpty(query.Content))
        {
            expression = expression.And(visualMedia => visualMedia.Content == query.Content);
        }

        if (query.CreatedAt.HasValue)
        {
            expression = expression.And(visualMedia => visualMedia.CreatedAt == query.CreatedAt);
        }

        if (query.CreatedAtFrom.HasValue)
        {
            expression = expression.And(visualMedia => visualMedia.CreatedAt >= query.CreatedAtFrom);
        }

        if (query.CreatedAtTo.HasValue)
        {
            expression = expression.And(visualMedia => visualMedia.CreatedAt <= query.CreatedAtTo);
        }

        if (query.UpdatedAt.HasValue)
        {
            expression = expression.And(visualMedia => visualMedia.UpdatedAt == query.UpdatedAt);
        }

        if (query.UpdatedAtFrom.HasValue)
        {
            expression = expression.And(visualMedia => visualMedia.UpdatedAt >= query.UpdatedAtFrom);
        }

        if (query.UpdatedAtTo.HasValue)
        {
            expression = expression.And(visualMedia => visualMedia.UpdatedAt <= query.UpdatedAtTo);
        }

        return expression;
    }
}
