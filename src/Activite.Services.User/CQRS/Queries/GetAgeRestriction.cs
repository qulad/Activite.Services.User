using System;
using Activite.Services.User.DTOs;
using Convey.CQRS.Queries;

namespace Activite.Services.User.CQRS.Queries;

public class GetAgeRestriction : IQuery<AgeRestrictionDto>
{
    public Guid Id { get; set; }

    public GetAgeRestriction(Guid id)
    {
        Id = id;
    }
}