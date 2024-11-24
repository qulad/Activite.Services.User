using System;

namespace Activite.Services.User.DTOs;

public class AgeRestrictionDto
{
    public Guid Id { get; set; }

    public string Code { get; set; }

    public int Age { get; set; }

    public DateTimeOffset CreatedAt { get; set; }

    public DateTimeOffset? UpdatedAt { get; set; }
}
