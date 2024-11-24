using System;
using Activite.Services.User.DTOs;
using Convey.CQRS.Queries;

namespace Activite.Services.User.CQRS.Queries;

public class GetCustomerComment : IQuery<CustomerCommentDto>
{
    public Guid Id { get; set; }

    public GetCustomerComment(Guid id)
    {
        Id = id;
    }
}