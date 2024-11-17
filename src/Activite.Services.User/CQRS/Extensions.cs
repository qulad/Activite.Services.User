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
using Convey.WebApi.CQRS.Builders;
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
            .UseAppleCustomerEndpoints()
            .UseGoogleLocationEndpoints()
            .UseGoogleCustomerEndpoints()
            .UseCustomerEndpoints()
            .UseGoogleLocationEndpoints();

        return endpoints;
    }

    private static IDispatcherEndpointsBuilder UsePingEndpoint(this IDispatcherEndpointsBuilder endpoints)
    {
        endpoints.Get("/ping", ctx => ctx.Response.WriteAsync("pong"));

        return endpoints;
    }

    private static IDispatcherEndpointsBuilder UseAppleCustomerEndpoints(this IDispatcherEndpointsBuilder endpoints)
    {
        endpoints
            .Get<GetAppleCustomer, AppleCustomerDto>("/AppleCustomer/{id}")
            .Get<GetMultipleAppleCustomers, PagedResult<AppleCustomerDto>>("/AppleCustomer")
            .Post<AddAppleCustomer>("/AppleCustomer")
            .Put<UpdateAppleCustomer>("/AppleCustomer");

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

    private static IDispatcherEndpointsBuilder UseGoogleCustomerEndpoints(this IDispatcherEndpointsBuilder endpoints)
    {
        endpoints
            .Get<GetGoogleCustomer, GoogleCustomerDto>("/GoogleCustomer/{id}")
            .Get<GetMultipleGoogleCustomers, PagedResult<GoogleCustomerDto>>("/GoogleCustomer")
            .Post<AddGoogleCustomer>("/GoogleCustomer")
            .Put<UpdateGoogleCustomer>("/GoogleCustomer");

        return endpoints;
    }

    private static IDispatcherEndpointsBuilder UseCustomerEndpoints(this IDispatcherEndpointsBuilder endpoints)
    {
        endpoints
            .Get<GetCustomer, CustomerDto>("/Customer/{id}")
            .Get<GetMultipleCustomers, PagedResult<CustomerDto>>("/Customer");

        return endpoints;
    }

    private static IDispatcherEndpointsBuilder UseLocationEndpoints(this DispatcherEndpointsBuilder endpoints)
    {
        endpoints
            .Get<GetLocation, LocationDto>("/Location/{id}")
            .Get<GetMultipleLocations, PagedResult<LocationDto>>("/Location");

        return endpoints;
    }
}