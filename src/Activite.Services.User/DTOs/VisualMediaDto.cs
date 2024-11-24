using System;

namespace Activite.Services.User.DTOs;

public class VisualMediaDto
{
    public Guid Id { get; set; }

    public string Type { get; set; }

    public string Content { get; set; }

    public DateTimeOffset CreatedAt { get; set; }

    public DateTimeOffset? UpdatedAt { get; set; }
}