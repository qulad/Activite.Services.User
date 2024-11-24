using System;
using Activite.Services.User.DTOs;
using Convey.CQRS.Queries;

namespace Activite.Services.User.CQRS.Queries;

public class GetOffer : IQuery<OfferDto>
{
    public Guid Id { get; set; }

    public GetOffer(Guid id)
    {
        Id = id;
    }
}