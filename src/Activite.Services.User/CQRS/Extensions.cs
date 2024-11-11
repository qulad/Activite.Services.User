using Activite.Services.User.CQRS.Commands;
using Activite.Services.User.CQRS.Queries;
using Activite.Services.User.DTOs;
using Convey;
using Convey.CQRS.Commands;
using Convey.CQRS.Queries;
using Convey.Discovery.Consul;
using Convey.HTTP;
using Convey.WebApi;
using Convey.WebApi.CQRS;
using Microsoft.AspNetCore.Http;

namespace Activite.Services.User.CQRS;

public static class Extensions
{
    public static IConveyBuilder AddCQRS(this IConveyBuilder builder)
    {
        builder
            .AddWebApi()
            .AddHttpClient()
            .AddQueryHandlers()
            .AddInMemoryQueryDispatcher()
            .AddCommandHandlers()
            .AddInMemoryCommandDispatcher()
            .AddConsul();

        return builder;
    }

    public static IDispatcherEndpointsBuilder UseEndpoints(this IDispatcherEndpointsBuilder endpoints)
    {
        endpoints
            .UsePingEndpoint()
            .UseAppleUserEndpoints()
            .UseGoogleLocationEndpoints()
            .UseGoogleUserEndpoints()
            .UseUserEndpoints();

        return endpoints;
    }

    private static IDispatcherEndpointsBuilder UsePingEndpoint(this IDispatcherEndpointsBuilder endpoints)
    {
        endpoints.Get("/ping", ctx => ctx.Response.WriteAsync("pong"));

        return endpoints;
    }

    private static IDispatcherEndpointsBuilder UseAppleUserEndpoints(this IDispatcherEndpointsBuilder endpoints)
    {
        endpoints
            .Get<GetAppleUser, AppleUserDto>("/AppleUser/{id}")
            .Get<GetMultipleAppleUsers, PagedResult<AppleUserDto>>("/AppleUser")
            .Post<AddAppleUser>("/AppleUser")
            .Put<UpdateAppleUser>("/AppleUser");

        return endpoints;
    }

    private static IDispatcherEndpointsBuilder UseGoogleLocationEndpoints(this IDispatcherEndpointsBuilder endpoints)
    {
        endpoints
            .Get<GetGoogleLocation, GoogleLocationDto>("/GoogleLocation/{id}")
            .Get<GetMultipleGoogleLocations, PagedResult<GoogleLocationDto>>("/GoogleLocation")
            .Post<AddGoogleLocation>("/GoogleLocation")
            .Put<UpdateGoogleLocation>("/GoogleLocation");

        return endpoints;
    }

    private static IDispatcherEndpointsBuilder UseGoogleUserEndpoints(this IDispatcherEndpointsBuilder endpoints)
    {
        endpoints
            .Get<GetGoogleUser, GoogleUserDto>("/GoogleUser/{id}")
            .Get<GetMultipleGoogleUsers, PagedResult<GoogleUserDto>>("/GoogleUser")
            .Post<AddGoogleUser>("/GoogleUser")
            .Put<UpdateGoogleUser>("/GoogleUser");

        return endpoints;
    }

    private static IDispatcherEndpointsBuilder UseUserEndpoints(this IDispatcherEndpointsBuilder endpoints)
    {
        endpoints
            .Get<GetUser, UserDto>("/User/{id}")
            .Get<GetMultipleUsers, PagedResult<UserDto>>("/User")
            .Post<AddUser>("/User")
            .Put<UpdateUser>("/User");

        return endpoints;
    }
}