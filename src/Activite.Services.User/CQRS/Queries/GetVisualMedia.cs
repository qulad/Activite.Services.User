using System;
using Activite.Services.User.DTOs;
using Convey.CQRS.Queries;

namespace Activite.Services.User.CQRS.Queries;

public class GetVisualMedia : IQuery<VisualMediaDto>
{
    public Guid Id { get; set; }

    public GetVisualMedia(Guid id)
    {
        Id = id;
    }
}