using Activite.Services.User.CQRS;
using Activite.Services.User.Mongo;
using Convey;
using Convey.Logging;
using Convey.WebApi.CQRS;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

#if DEBUG

DotNetEnv.Env.Load();

#endif

var host = WebHost.CreateDefaultBuilder(args)
    .ConfigureAppConfiguration((hostingContext, config) =>
    {
        config.AddEnvironmentVariables();
    })
    .ConfigureServices((context, services) =>
    {
        services
            .AddConvey()
            .AddCQRS()
            .AddMongoRepositories()
            .Build();
    })
    .Configure(app =>
    {
        app
            .UseConvey()
            .UseMongoIndexes()
            .UseDispatcherEndpoints(endpoints => endpoints.UseEndpoints());
    })
    .UseLogging()
    .Build();

await host.RunAsync();