using System;

namespace Activite.Services.User.DTOs;

public class LocationCommentDto : CommentDto
{
    public Guid LocationId { get; set; }

    public Guid CustomerCommentId { get; set; }
}