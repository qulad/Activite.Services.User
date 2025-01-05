using System.Threading.Tasks;
using Activite.Services.User.DTOs.Integration;
using Convey.HTTP;

namespace Activite.Services.User.Services;

public class IntegrationService
{
    private const string EmailVerificationUrl = "/EmailVerification";

    private readonly string _webService1Url;
    private readonly IHttpClient _httpClient;

    public IntegrationService(IHttpClient httpClient, HttpClientOptions options)
    {
        _httpClient = httpClient;
        _webService1Url = options.Services["integration"];
    }

    public Task GetGoogleTokenAsync(SendEmailVerificationDto dto)
        => _httpClient.PostAsync($"{_webService1Url}{EmailVerificationUrl}", data: dto);
}