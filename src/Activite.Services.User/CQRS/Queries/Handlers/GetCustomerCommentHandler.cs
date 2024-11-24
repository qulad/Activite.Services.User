using System;
using System.Threading;
using System.Threading.Tasks;
using Activite.Services.User.DTOs;
using Activite.Services.User.Mongo.Documents;
using Convey.CQRS.Queries;
using Convey.Persistence.MongoDB;

namespace Activite.Services.User.CQRS.Queries.Handlers;

public class GetCustomerCommentHandler : IQueryHandler<GetCustomerComment, CustomerCommentDto>
{
    private readonly IMongoRepository<CustomerCommentDocument, Guid> _repository;

    public GetCustomerCommentHandler(IMongoRepository<CustomerCommentDocument, Guid> repository)
    {
        _repository = repository;
    }

    public async Task<CustomerCommentDto> HandleAsync(GetCustomerComment query, CancellationToken cancellationToken = default)
    {
        var comment = await _repository.GetAsync(query.Id);

        if (comment is null)
        {
            return new CustomerCommentDto();
        }

        return new CustomerCommentDto
        {
            Id = comment.Id,
            EventId = comment.EventId,
            Content = comment.Content,
            Type = comment.Type,
            CreatedAt = comment.CreatedAt,
            UpdatedAt = comment.UpdatedAt,
            CustomerId = comment.CustomerId,
            Rating = comment.Rating
        };
    }
}