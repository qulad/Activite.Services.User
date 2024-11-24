using System;
using System.Threading;
using System.Threading.Tasks;
using Activite.Services.User.DTOs;
using Activite.Services.User.Mongo.Documents;
using Convey.CQRS.Queries;
using Convey.Persistence.MongoDB;

namespace Activite.Services.User.CQRS.Queries.Handlers;

public class GetCommentHandler : IQueryHandler<GetComment, CommentDto>
{
    private readonly IMongoRepository<CommentDocument, Guid> _repository;

    public GetCommentHandler(IMongoRepository<CommentDocument, Guid> repository)
    {
        _repository = repository;
    }

    public async Task<CommentDto> HandleAsync(GetComment query, CancellationToken cancellationToken = default)
    {
        var comment = await _repository.GetAsync(query.Id);

        if (comment is null)
        {
            return new CommentDto();
        }

        return new CommentDto
        {
            Id = comment.Id,
            EventId = comment.EventId,
            Content = comment.Content,
            Type = comment.Type,
            CreatedAt = comment.CreatedAt,
            UpdatedAt = comment.UpdatedAt
        };
    }
}