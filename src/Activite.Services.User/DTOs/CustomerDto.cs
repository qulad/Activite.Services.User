using System;

namespace Activite.Services.User.DTOs;

public class CustomerDto : UserDto
{
    public string FirstName { get; set; }

    public string LastName { get; set; }

    public DateOnly DateOfBirth { get; set; }
}
