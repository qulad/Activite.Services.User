using System;

namespace Activite.Services.User.DTOs;

public class CommentDto
{
    public Guid Id { get; set; }

    public Guid EventId { get; set; }

    public string Content { get; set; }

    public string Type { get; set; }

    public DateTimeOffset CreatedAt { get; set; }

    public DateTimeOffset? UpdatedAt { get; set; }
}