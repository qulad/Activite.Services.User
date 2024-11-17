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

public class GetMultipleGoogleCustomersHandler : IQueryHandler<GetMultipleGoogleCustomers, PagedResult<GoogleCustomerDto>>
{
    private readonly IMongoRepository<GoogleCustomerDocument, Guid> _repository;

    public GetMultipleGoogleCustomersHandler(IMongoRepository<GoogleCustomerDocument, Guid> repository)
    {
        _repository = repository;
    }

    public async Task<PagedResult<GoogleCustomerDto>> HandleAsync(GetMultipleGoogleCustomers query, CancellationToken cancellationToken = default)
    {
        var predicate = GetPredicate(query);

        var users = await _repository.BrowseAsync(predicate, query);

        if (users is null || users.IsEmpty)
        {
            return PagedResult<GoogleCustomerDto>.Empty;
        }

        return users.Map(user => new GoogleCustomerDto
        {
            Id = user.Id,
            Email = user.Email,
            PhoneNumber = user.PhoneNumber,
            GoogleId = user.GoogleId,
            FirstName = user.FirstName,
            LastName = user.LastName,
            DateOfBirth = user.DateOfBirth,
            Region = user.Region,
            Type = user.Type,
            TermsAndServicesAccepted = user.TermsAndServicesAccepted,
            Verified = user.Verified,
            CreatedAt = user.CreatedAt,
            UpdatedAt = user.UpdatedAt
        });
    }

    private static Expression<Func<GoogleCustomerDocument, bool>> GetPredicate(GetMultipleGoogleCustomers query)
    {
        Expression<Func<GoogleCustomerDocument, bool>> expression = user => user.Type == UserTypes.GoogleCustomer;

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

        if (!string.IsNullOrEmpty(query.GoogleId))
        {
            expression = expression.And(user => user.GoogleId == query.GoogleId);
        }
        
        if (!string.IsNullOrEmpty(query.FirstName))
        {
            expression = expression.And(user => user.FirstName == query.FirstName);
        }

        if (!string.IsNullOrEmpty(query.LastName))
        {
            expression = expression.And(user => user.LastName == query.LastName);
        }

        if (query.DateOfBirth.HasValue)
        {
            expression = expression.And(user => user.DateOfBirth == query.DateOfBirth);
        }

        if (query.DateOfBirthFrom.HasValue)
        {
            expression = expression.And(user => user.DateOfBirth >= query.DateOfBirthFrom);
        }

        if (query.DateOfBirthTo.HasValue)
        {
            expression = expression.And(user => user.DateOfBirth <= query.DateOfBirthTo);
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