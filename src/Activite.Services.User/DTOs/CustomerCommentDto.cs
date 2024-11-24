using System;

namespace Activite.Services.User.DTOs;

public class CustomerCommentDto : CommentDto
{
    public Guid CustomerId { get; set; }

    public int Rating { get; set; }
}