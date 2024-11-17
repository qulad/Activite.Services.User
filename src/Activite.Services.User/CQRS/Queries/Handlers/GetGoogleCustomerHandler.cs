using System;
using System.Threading;
using System.Threading.Tasks;
using Activite.Services.User.Constants;
using Activite.Services.User.DTOs;
using Activite.Services.User.Mongo.Documents;
using Convey.CQRS.Queries;
using Convey.Persistence.MongoDB;

namespace Activite.Services.User.CQRS.Queries.Handlers;

public class GetGoogleCustomerHandler : IQueryHandler<GetGoogleCustomer, GoogleCustomerDto>
{
    private readonly IMongoRepository<GoogleCustomerDocument, Guid> _repository;

    public GetGoogleCustomerHandler(IMongoRepository<GoogleCustomerDocument, Guid> repository)
    {
        _repository = repository;
    }

    public async Task<GoogleCustomerDto> HandleAsync(GetGoogleCustomer query, CancellationToken cancellationToken = default)
    {
        var user =
            await _repository.GetAsync(
                x => x.Id == query.Id && 
                x.Type == UserTypes.GoogleCustomer);

        if (user is null)
        {
            return new GoogleCustomerDto();
        }

        return new GoogleCustomerDto
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
        };
    }
}