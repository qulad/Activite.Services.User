using System;
using System.Threading;
using System.Threading.Tasks;
using Activite.Services.User.Constants;
using Activite.Services.User.DTOs;
using Activite.Services.User.Mongo.Documents;
using Convey.CQRS.Queries;
using Convey.Persistence.MongoDB;

namespace Activite.Services.User.CQRS.Queries.Handlers;

public class GetCustomerHandler : IQueryHandler<GetCustomer, CustomerDto>
{
    private readonly IMongoRepository<CustomerDocument, Guid> _repository;

    public GetCustomerHandler(IMongoRepository<CustomerDocument, Guid> repository)
    {
        _repository = repository;
    }

    public async Task<CustomerDto> HandleAsync(GetCustomer query, CancellationToken cancellationToken = default)
    {
        var user =
            await _repository.GetAsync(
                x => x.Id == query.Id && 
                (x.Type == UserTypes.GoogleCustomer || x.Type == UserTypes.AppleCustomer || x.Type == UserTypes.Customer));

        if (user is null)
        {
            return new CustomerDto();
        }

        return new CustomerDto
        {
            Id = user.Id,
            Email = user.Email,
            PhoneNumber = user.PhoneNumber,
            FirstName = user.FirstName,
            LastName = user.LastName,
            DateOfBirth = user.DateOfBirth,
            Region = user.Region,
            Type = user.Type,
            TermsAndServicesAccepted = user.TermsAndServicesAccepted,
            Verified = user.Verified,
            VerificationCode = user.VerificationCode,
            CreatedAt = user.CreatedAt,
            UpdatedAt = user.UpdatedAt
        };
    }
}