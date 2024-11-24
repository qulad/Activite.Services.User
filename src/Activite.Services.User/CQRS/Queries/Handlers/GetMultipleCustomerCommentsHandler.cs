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

public class GetMultipleCustomerCommentsHandler : IQueryHandler<GetMultipleCustomerComments, PagedResult<CustomerCommentDto>>
{
    private readonly IMongoRepository<CustomerCommentDocument, Guid> _repository;

    public GetMultipleCustomerCommentsHandler(IMongoRepository<CustomerCommentDocument, Guid> repository)
    {
        _repository = repository;
    }

    public async Task<PagedResult<CustomerCommentDto>> HandleAsync(GetMultipleCustomerComments query, CancellationToken cancellationToken = default)
    {
        var predicate = GetPredicate(query);

        var comments = await _repository.BrowseAsync(predicate, query);

        if (comments is null || comments.IsEmpty)
        {
            return PagedResult<CustomerCommentDto>.Empty;
        }

        return comments.Map(comment => new CustomerCommentDto
        {
            Id = comment.Id,
            EventId = comment.EventId,
            Content = comment.Content,
            Type = comment.Type,
            CreatedAt = comment.CreatedAt,
            UpdatedAt = comment.UpdatedAt,
            CustomerId = comment.CustomerId,
            Rating = comment.Rating
        });
    }

    private static Expression<Func<CustomerCommentDocument, bool>> GetPredicate(GetMultipleCustomerComments query)
    {
        Expression<Func<CustomerCommentDocument, bool>> expression = x => x.Type == CommentTypes.Customer;

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

        if (query.CustomerId.HasValue)
        {
            expression = expression.And(comment => comment.CustomerId == query.CustomerId);
        }

        if (query.CustomerIds is not null && query.CustomerIds.Count > 0)
        {
            expression = expression.And(comment => query.CustomerIds.Contains(comment.CustomerId));
        }

        if (query.Rating.HasValue)
        {
            expression = expression.And(comment => comment.Rating == query.Rating);
        }

        if (query.RatingFrom.HasValue)
        {
            expression = expression.And(comment => comment.Rating >= query.RatingFrom);
        }

        if (query.RatingTo.HasValue)
        {
            expression = expression.And(comment => comment.Rating <= query.RatingTo);
        }

        return expression;
    }
}
