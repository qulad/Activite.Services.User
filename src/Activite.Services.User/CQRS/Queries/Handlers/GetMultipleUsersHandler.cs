using System;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Activite.Services.User.Constants;
using Activite.Services.User.DTOs;
using Activite.Services.User.Mongo.Documents;
using Convey.CQRS.Queries;
using Convey.Persistence.MongoDB;
using Microsoft.Extensions.Logging;

namespace Activite.Services.User.CQRS.Queries.Handlers;

public class GetMultipleUsersHandler : IQueryHandler<GetMultipleUsers, PagedResult<UserDto>>
{
    private readonly IMongoRepository<UserDocument, Guid> _repository;
    private readonly ILogger<GetMultipleUsersHandler> _logger;

    public GetMultipleUsersHandler(IMongoRepository<UserDocument, Guid> repository, ILogger<GetMultipleUsersHandler> logger)
    {
        _repository = repository;
        _logger = logger;
    }

    public async Task<PagedResult<UserDto>> HandleAsync(GetMultipleUsers query, CancellationToken cancellationToken = default)
    {
        var predicate = GetPredicate(query);

        var users = await _repository.BrowseAsync(predicate, query);

        if (users is null || users.IsEmpty)
        {
            _logger.LogWarning("No users were found.");

            return PagedResult<UserDto>.Empty;
        }

        return users.Map(item => new UserDto
        {
            Id = item.Id,
            Email = item.Email,
            PhoneNumber = item.PhoneNumber,
            Region = item.Region,
            Type = item.Type,
            TermsAndServicesAccepted = item.TermsAndServicesAccepted,
            Verified = item.Verified,
            CreatedAt = item.CreatedAt,
            UpdatedAt = item.UpdatedAt
        });
    }

    private static Expression<Func<UserDocument, bool>> GetPredicate(GetMultipleUsers query)
    {
        Expression<Func<UserDocument, bool>> expression = user => true;

        if (query.Id.HasValue)
        {
            expression = expression.And(user => user.Id == query.Id);
        }

        if (!string.IsNullOrEmpty(query.Email))
        {
            expression = expression.And(user => user.Email == query.Email);
        }

        if (!string.IsNullOrEmpty(query.PhoneNumber))
        {
            expression = expression.And(user => user.PhoneNumber == query.PhoneNumber);
        }

        if (!string.IsNullOrEmpty(query.Region) && Regions.All.Contains(query.Region))
        {
            expression = expression.And(user => user.Region == query.Region);
        }

        if (!string.IsNullOrEmpty(query.Type) && UserTypes.All.Contains(query.Type))
        {
            expression = expression.And(user => user.Type == query.Type);
        }

        if (query.TermsAndServicesAccepted.HasValue)
        {
            expression = expression.And(user => user.TermsAndServicesAccepted == query.TermsAndServicesAccepted);
        }

        if (query.Verified.HasValue)
        {
            expression = expression.And(user => user.Verified == query.Verified);
        }

        if (query.CreatedAt.HasValue)
        {
            expression = expression.And(user => user.CreatedAt == query.CreatedAt);
        }

        if (query.CreatedAtFrom.HasValue)
        {
            expression = expression.And(user => user.CreatedAt >= query.CreatedAtFrom);
        }

        if (query.CreatedAtTo.HasValue)
        {
            expression = expression.And(user => user.CreatedAt <= query.CreatedAtTo);
        }

        if (query.UpdatedAt.HasValue)
        {
            expression = expression.And(user => user.UpdatedAt == query.UpdatedAt);
        }

        if (query.UpdatedAtFrom.HasValue)
        {
            expression = expression.And(user => user.UpdatedAt >= query.UpdatedAtFrom);
        }

        if (query.UpdatedAtTo.HasValue)
        {
            expression = expression.And(user => user.UpdatedAt <= query.UpdatedAtTo);
        }

        return expression;
    }
}