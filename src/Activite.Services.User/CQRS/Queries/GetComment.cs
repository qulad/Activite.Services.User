using System;
using Activite.Services.User.DTOs;
using Convey.CQRS.Queries;

namespace Activite.Services.User.CQRS.Queries;

public class GetComment : IQuery<CommentDto>
{
    public Guid Id { get; set; }

    public GetComment(Guid id)
    {
        Id = id;
    }
}