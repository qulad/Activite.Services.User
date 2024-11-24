using System;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Activite.Services.User.Constants;
using Activite.Services.User.DTOs;
using Activite.Services.User.Mongo.Documents;
using Convey.CQRS.Queries;
using Convey.Persistence.MongoDB;

namespace Activite.Services.User.CQRS.Queries.Handlers;

public class GetMultipleLocationCommentsHandler : IQueryHandler<GetMultipleLocationComments, PagedResult<LocationCommentDto>>
{
    private readonly IMongoRepository<LocationCommentDocument, Guid> _repository;

    public GetMultipleLocationCommentsHandler(IMongoRepository<LocationCommentDocument, Guid> repository)
    {
        _repository = repository;
    }

    public async Task<PagedResult<LocationCommentDto>> HandleAsync(GetMultipleLocationComments query, CancellationToken cancellationToken = default)
    {
        var predicate = GetPredicate(query);

        var comments = await _repository.BrowseAsync(predicate, query);

        if (comments is null || comments.IsEmpty)
        {
            return PagedResult<LocationCommentDto>.Empty;
        }

        return comments.Map(comment => new LocationCommentDto
        {
            Id = comment.Id,
            EventId = comment.EventId,
            Content = comment.Content,
            Type = comment.Type,
            CreatedAt = comment.CreatedAt,
            UpdatedAt = comment.UpdatedAt,
            LocationId = comment.LocationId,
            CustomerCommentId = comment.CustomerCommentId
        });
    }

    private static Expression<Func<LocationCommentDocument, bool>> GetPredicate(GetMultipleLocationComments query)
    {
        Expression<Func<LocationCommentDocument, bool>> expression = x => x.Type == CommentTypes.Location;

        if (query.Id.HasValue)
        {
            expression = expression.And(comment => comment.Id == query.Id);
        }

        if (query.Ids is not null && query.Ids.Count > 0)
        {
            expression = expression.And(comment => query.Ids.Contains(comment.Id));
        }

        if (query.EventId.HasValue)
        {
            expression = expression.And(comment => comment.EventId == query.EventId);
        }

        if (query.EventIds is not null && query.EventIds.Count > 0)
        {
            expression = expression.And(comment => query.EventIds.Contains(comment.EventId));
        }

        if (!string.IsNullOrWhiteSpace(query.Content))
        {
            expression = expression.And(comment => comment.Content == query.Content);
        }

        if (!string.IsNullOrWhiteSpace(query.SearchContent))
        {
            expression = expression.And(comment => comment.Content.Contains(query.SearchContent));
        }

        if (query.CreatedAt.HasValue)
        {
            expression = expression.And(comment => comment.CreatedAt == query.CreatedAt);
        }

        if (query.CreatedAtFrom.HasValue)
        {
            expression = expression.And(comment => comment.CreatedAt >= query.CreatedAtFrom);
        }

        if (query.CreatedAtTo.HasValue)
        {
            expression = expression.And(comment => comment.CreatedAt <= query.CreatedAtTo);
        }

        if (query.UpdatedAt.HasValue)
        {
            expression = expression.And(comment => comment.UpdatedAt == query.UpdatedAt);
        }

        if (query.UpdatedAtFrom.HasValue)
        {
            expression = expression.And(comment => comment.UpdatedAt >= query.UpdatedAtFrom);
        }

        if (query.UpdatedAtTo.HasValue)
        {
            expression = expression.And(comment => comment.UpdatedAt <= query.UpdatedAtTo);
        }

        if (query.LocationId.HasValue)
        {
            expression = expression.And(comment => comment.LocationId == query.LocationId);
        }

        if (query.LocationIds is not null && query.LocationIds.Count > 0)
        {
            expression = expression.And(comment => query.LocationIds.Contains(comment.LocationId));
        }
        
        if (query.LocationId.HasValue)
        {
            expression = expression.And(comment => comment.LocationId == query.LocationId);
        }

        if (query.LocationIds is not null && query.LocationIds.Count > 0)
        {
            expression = expression.And(comment => query.LocationIds.Contains(comment.LocationId));
        }

        if (query.CustomerCommentId.HasValue)
        {
            expression = expression.And(comment => comment.CustomerCommentId == query.CustomerCommentId);
        }

        if (query.CustomerCommentIds is not null && query.CustomerCommentIds.Count > 0)
        {
            expression = expression.And(comment => query.CustomerCommentIds.Contains(comment.CustomerCommentId));
        }

        return expression;
    }
}
