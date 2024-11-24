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
            .UseAppleCustomerEndpoints()
            .UseGoogleLocationEndpoints()
            .UseGoogleCustomerEndpoints()
            .UseCustomerEndpoints()
            .UseLocationEndpoints()
            .UseUserEndpoints()
            .UseVisualMediaEndpoints()
            .UseOfferEndpoints()
            .UseAgeRestrictionEndpoints()
            .UseTranslationEndpoints()
            .UseCustomerCommentEndpoints()
            .UseLocationCommentEndpoints()
            .UseEventEndpoints()
            .UseTicketEndpoints()
            .UsePercentageCouponEndpoints()
            .UseAmountCouponEndpoints()
            .UseTransactionEndpoints();

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

    private static IDispatcherEndpointsBuilder UseLocationEndpoints(this IDispatcherEndpointsBuilder endpoints)
    {
        endpoints
            .Get<GetLocation, LocationDto>("/Location/{id}")
            .Get<GetMultipleLocations, PagedResult<LocationDto>>("/Location");

        return endpoints;
    }

    private static IDispatcherEndpointsBuilder UseUserEndpoints(this IDispatcherEndpointsBuilder endpoints)
    {
        endpoints
            .Get<GetMultipleUsers, PagedResult<UserDto>>("/User");

        return endpoints;
    }

    private static IDispatcherEndpointsBuilder UseVisualMediaEndpoints(this IDispatcherEndpointsBuilder endpoints)
    {
        endpoints
            .Get<GetVisualMedia, VisualMediaDto>("/VisualMedia/{id}")
            .Get<GetMultipleVisualMedias, PagedResult<VisualMediaDto>>("/VisualMedia")
            .Post<AddVisualMedia>("/VisualMedia");

        return endpoints;
    }

    private static IDispatcherEndpointsBuilder UseOfferEndpoints(this IDispatcherEndpointsBuilder endpoints)
    {
        endpoints
            .Get<GetOffer, OfferDto>("/Offer/{id}")
            .Get<GetMultipleOffers, PagedResult<OfferDto>>("/Offer")
            .Post<AddOffer>("/Offer")
            .Put<UpdateOffer>("/Offer");

        return endpoints;
    }

    private static IDispatcherEndpointsBuilder UseAgeRestrictionEndpoints(this IDispatcherEndpointsBuilder endpoints)
    {
        endpoints
            .Get<GetAgeRestriction, AgeRestrictionDto>("/AgeRestriction/{id}")
            .Get<GetMultipleAgeRestrictions, PagedResult<AgeRestrictionDto>>("/AgeRestriction")
            .Post<AddAgeRestriction>("/AgeRestriction");

        return endpoints;
    }

    private static IDispatcherEndpointsBuilder UseTranslationEndpoints(this IDispatcherEndpointsBuilder endpoints)
    {
        endpoints
            .Get<GetTranslation, TranslationDto>("/Translation/{id}")
            .Get<GetMultipleTranslations, PagedResult<TranslationDto>>("/Translation")
            .Post<AddTranslation>("/Translation");

        return endpoints;
    }

    private static IDispatcherEndpointsBuilder UseCustomerCommentEndpoints(this IDispatcherEndpointsBuilder endpoints)
    {
        endpoints
            .Get<GetCustomerComment, CustomerCommentDto>("/CustomerComment/{id}")
            .Get<GetMultipleCustomerComments, PagedResult<CustomerCommentDto>>("/CustomerComment")
            .Post<AddCustomerComment>("/CustomerComment")
            .Put<UpdateCustomerComment>("/CustomerComment")
            .Delete<DeleteCustomerComment>("/CustomerComment");

        return endpoints;
    }

    private static IDispatcherEndpointsBuilder UseLocationCommentEndpoints(this IDispatcherEndpointsBuilder endpoints)
    {
        endpoints
            .Get<GetLocationComment, LocationCommentDto>("/LocationComment/{id}")
            .Get<GetMultipleLocationComments, PagedResult<LocationCommentDto>>("/LocationComment")
            .Post<AddLocationComment>("/LocationComment")
            .Put<UpdateLocationComment>("/LocationComment")
            .Delete<DeleteLocationComment>("/LocationComment");

        return endpoints;
    }

    private static IDispatcherEndpointsBuilder UseEventEndpoints(this IDispatcherEndpointsBuilder endpoints)
    {
        endpoints
            .Get<GetEvent, EventDto>("/Event/{id}")
            .Get<GetMultipleEvents, PagedResult<EventDto>>("/Event")
            .Post<AddEvent>("/Event")
            .Put<UpdateEvent>("/Event");

        return endpoints;
    }

    private static IDispatcherEndpointsBuilder UseTicketEndpoints(this IDispatcherEndpointsBuilder endpoints)
    {
        endpoints
            .Get<GetTicket, TicketDto>("/Ticket/{id}")
            .Get<GetMultipleTickets, PagedResult<TicketDto>>("/Ticket")
            .Post<AddTicket>("/Ticket");

        return endpoints;
    }

    private static IDispatcherEndpointsBuilder UsePercentageCouponEndpoints(this IDispatcherEndpointsBuilder endpoints)
    {
        endpoints
            .Get<GetPercentageCoupon, PercentageCouponDto>("/PercentageCoupon/{id}")
            .Get<GetMultiplePercentageCoupons, PagedResult<PercentageCouponDto>>("/PercentageCoupon")
            .Post<AddPercentageCoupon>("/PercentageCoupon")
            .Put<UpdatePercentageCoupon>("/PercentageCoupon");

        return endpoints;
    }

    private static IDispatcherEndpointsBuilder UseAmountCouponEndpoints(this IDispatcherEndpointsBuilder endpoints)
    {
        endpoints
            .Get<GetAmountCoupon, AmountCouponDto>("/AmountCoupon/{id}")
            .Get<GetMultipleAmountCoupons, PagedResult<AmountCouponDto>>("/AmountCoupon")
            .Post<AddAmountCoupon>("/AmountCoupon")
            .Put<UpdateAmountCoupon>("/AmountCoupon");

        return endpoints;
    }

    private static IDispatcherEndpointsBuilder UseTransactionEndpoints(this IDispatcherEndpointsBuilder endpoints)
    {
        endpoints
            .Get<GetTransaction, TransactionDto>("/Transaction/{id}")
            .Get<GetMultipleTransactions, PagedResult<TransactionDto>>("/Transaction");

        return endpoints;
    }
}