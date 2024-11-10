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
            .UseGoogleUserEndpoints()
            .UseUserEndpoints();

        return endpoints;
    }

    private static IDispatcherEndpointsBuilder UsePingEndpoint(this IDispatcherEndpointsBuilder endpoints)
    {
        endpoints.Get("/ping", ctx => ctx.Response.WriteAsync("pong"));

        return endpoints;
    }

    private static IDispatcherEndpointsBuilder UseGoogleUserEndpoints(this IDispatcherEndpointsBuilder endpoints)
    {
        endpoints
            .Get<GetMultipleGoogleUsers, PagedResult<GoogleUserDto>>("/GoogleUser");

        return endpoints;
    }

    private static IDispatcherEndpointsBuilder UseUserEndpoints(this IDispatcherEndpointsBuilder endpoints)
    {
        endpoints
            .Get<GetUser, UserDto>("/User/{id}")
            .Get<GetMultipleUsers, PagedResult<UserDto>>("/User")
            .Post<AddUser>("/User");

        return endpoints;
    }
}