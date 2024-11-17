using System;

namespace Activite.Services.User.DTOs;

public class LocationDto : UserDto
{
    public string Address { get; set; }

    public string Name { get; set; }

    public string Description { get; set; }

    public DateOnly EstabilishedDate { get; set; }
}