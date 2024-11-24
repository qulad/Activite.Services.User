using System;
using System.Threading;
using System.Threading.Tasks;
using Activite.Services.User.DTOs;
using Activite.Services.User.Mongo.Documents;
using Convey.CQRS.Queries;
using Convey.Persistence.MongoDB;

namespace Activite.Services.User.CQRS.Queries.Handlers;

public class GetVisualMediaHandler : IQueryHandler<GetVisualMedia, VisualMediaDto>
{
    private readonly IMongoRepository<VisualMediaDocument, Guid> _repository;

    public GetVisualMediaHandler(IMongoRepository<VisualMediaDocument, Guid> repository)
    {
        _repository = repository;
    }

    public async Task<VisualMediaDto> HandleAsync(GetVisualMedia query, CancellationToken cancellationToken = default)
    {
        var visualMedia = await _repository.GetAsync(query.Id);

        if (visualMedia is null)
        {
            return new VisualMediaDto();
        }

        return new VisualMediaDto
        {
            Id = visualMedia.Id,
            Type = visualMedia.Type,
            Content = visualMedia.Content,
            CreatedAt = visualMedia.CreatedAt,
            UpdatedAt = visualMedia.UpdatedAt
        };
    }
}
