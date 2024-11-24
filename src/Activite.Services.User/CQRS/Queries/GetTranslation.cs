using System;
using Activite.Services.User.DTOs;
using Convey.CQRS.Queries;

namespace Activite.Services.User.CQRS.Queries;

public class GetTranslation : IQuery<TranslationDto>
{
    public Guid Id { get; set; }

    public GetTranslation(Guid id)
    {
        Id = id;
    }
}