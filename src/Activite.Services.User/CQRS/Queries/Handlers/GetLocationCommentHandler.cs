using System;
using System.Threading;
using System.Threading.Tasks;
using Activite.Services.User.DTOs;
using Activite.Services.User.Mongo.Documents;
using Convey.CQRS.Queries;
using Convey.Persistence.MongoDB;

namespace Activite.Services.User.CQRS.Queries.Handlers;

public class GetLocationCommentHandler : IQueryHandler<GetLocationComment, LocationCommentDto>
{
    private readonly IMongoRepository<LocationCommentDocument, Guid> _repository;

    public GetLocationCommentHandler(IMongoRepository<LocationCommentDocument, Guid> repository)
    {
        _repository = repository;
    }

    public async Task<LocationCommentDto> HandleAsync(GetLocationComment query, CancellationToken cancellationToken = default)
    {
        var comment = await _repository.GetAsync(query.Id);

        if (comment is null)
        {
            return new LocationCommentDto();
        }

        return new LocationCommentDto
        {
            Id = comment.Id,
            EventId = comment.EventId,
            Content = comment.Content,
            Type = comment.Type,
            CreatedAt = comment.CreatedAt,
            UpdatedAt = comment.UpdatedAt,
            LocationId = comment.LocationId,
            CustomerCommentId = comment.CustomerCommentId
        };
    }
}