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

public class GetMultipleGoogleLocationsHandler : IQueryHandler<GetMultipleGoogleLocations, PagedResult<GoogleLocationDto>>
{
    private readonly IMongoRepository<GoogleLocationDocument, Guid> _repository;

    public GetMultipleGoogleLocationsHandler(IMongoRepository<GoogleLocationDocument, Guid> repository)
    {
        _repository = repository;
    }

    public async Task<PagedResult<GoogleLocationDto>> HandleAsync(GetMultipleGoogleLocations query, CancellationToken cancellationToken = default)
    {
        var predicate = GetPredicate(query);

        var users = await _repository.BrowseAsync(predicate, query);

        if (users is null || users.IsEmpty)
        {
            return PagedResult<GoogleLocationDto>.Empty;
        }

        return users.Map(user => new GoogleLocationDto
        {
            Id = user.Id,
            Email = user.Email,
            PhoneNumber = user.PhoneNumber,
            Address = user.Address,
            Name = user.Name,
            Description = user.Description,
            EstabilishedDate = user.EstabilishedDate,
            Region = user.Region,
            GoogleId = user.GoogleId,
            Type = user.Type,
            TermsAndServicesAccepted = user.TermsAndServicesAccepted,
            Verified = user.Verified,
            VerificationCode = user.VerificationCode,
            CreatedAt = user.CreatedAt,
            UpdatedAt = user.UpdatedAt
        });
    }

    private static Expression<Func<GoogleLocationDocument, bool>> GetPredicate(GetMultipleGoogleLocations query)
    {
        Expression<Func<GoogleLocationDocument, bool>> expression = user => user.Type == UserTypes.GoogleLocation;

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
        
        if (!string.IsNullOrEmpty(query.Address))
        {
            expression = expression.And(user => user.Address == query.Address);
        }

        if (!string.IsNullOrEmpty(query.Name))
        {
            expression = expression.And(user => user.Name == query.Name);
        }

        if (!string.IsNullOrEmpty(query.Description))
        {
            expression = expression.And(user => user.Description == query.Description);
        }

        if (query.EstabilishedDate.HasValue)
        {
            expression = expression.And(user => user.EstabilishedDate == query.EstabilishedDate);
        }

        if (query.EstabilishedDateFrom.HasValue)
        {
            expression = expression.And(user => user.EstabilishedDate >= query.EstabilishedDateFrom);
        }

        if (query.EstabilishedDateTo.HasValue)
        {
            expression = expression.And(user => user.EstabilishedDate <= query.EstabilishedDateTo);
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