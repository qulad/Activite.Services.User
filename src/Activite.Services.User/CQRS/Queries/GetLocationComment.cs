using System;
using Activite.Services.User.DTOs;
using Convey.CQRS.Queries;

namespace Activite.Services.User.CQRS.Queries;

public class GetLocationComment : IQuery<LocationCommentDto>
{
    public Guid Id { get; set; }

    public GetLocationComment(Guid id)
    {
        Id = id;
    }
}