using System;
using System.Threading;
using System.Threading.Tasks;
using Activite.Services.User.Constants;
using Activite.Services.User.DTOs;
using Activite.Services.User.Mongo.Documents;
using Convey.CQRS.Queries;
using Convey.Persistence.MongoDB;

namespace Activite.Services.User.CQRS.Queries.Handlers;

public class GetAppleCustomerHandler : IQueryHandler<GetAppleCustomer, AppleCustomerDto>
{
    private readonly IMongoRepository<AppleCustomerDocument, Guid> _repository;

    public GetAppleCustomerHandler(IMongoRepository<AppleCustomerDocument, Guid> repository)
    {
        _repository = repository;
    }

    public async Task<AppleCustomerDto> HandleAsync(GetAppleCustomer query, CancellationToken cancellationToken = default)
    {
        var user =
            await _repository.GetAsync(
                x => x.Id == query.Id && 
                x.Type == UserTypes.AppleCustomer);

        if (user is null)
        {
            return new AppleCustomerDto();
        }

        return new AppleCustomerDto
        {
            Id = user.Id,
            Email = user.Email,
            PhoneNumber = user.PhoneNumber,
            AppleId = user.AppleId,
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