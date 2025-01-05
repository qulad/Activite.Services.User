namespace Activite.Services.User.DTOs.Integration;

public class SendEmailVerificationDto
{
    public string Username { get; set;}

    public string Email { get; set; }

    public string Code { get; set; }
}