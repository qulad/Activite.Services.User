using System;

namespace Activite.Services.User.DTOs;

public class TranslationDto
{
    public Guid Id { get; set; }

    public string Code { get; set; }

    public string Content { get; set; }

    public string Region { get; set; }

    public DateTimeOffset CreatedAt { get; set; }

    public DateTimeOffset? UpdatedAt { get; set; }
}