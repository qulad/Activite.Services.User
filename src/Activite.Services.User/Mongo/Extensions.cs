using System;
using System.Threading.Tasks;
using Activite.Services.User.Mongo.Documents;
using Convey;
using Convey.Persistence.MongoDB;
using Convey.Types;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;

namespace Activite.Services.User.Mongo;

public static class Extensions
{
    public static IConveyBuilder AddMongoRepositories(this IConveyBuilder builder)
    {
        builder
            .AddMongo()
            .AddMongoRepository<UserDocument, Guid>(UserDocument.CollectionName)
            .AddMongoRepository<GoogleUserDocument, Guid>(GoogleUserDocument.CollectionName);

        return builder;
    }

    public static IApplicationBuilder UseMongoIndexes(this IApplicationBuilder app)
    {
        using var scope = app.ApplicationServices.CreateScope();

        scope.CreateIdIndex<UserDocument>();

        return app;
    }

    private static IServiceScope CreateIdIndex<TDocument>(this IServiceScope scope) where TDocument : IIdentifiable<Guid>
    {
        var collection = scope.ServiceProvider.GetRequiredService<IMongoRepository<TDocument, Guid>>().Collection;
        var colletionBuilder = Builders<TDocument>.IndexKeys;
        Task.Run(async () => await collection.Indexes.CreateOneAsync(
            new CreateIndexModel<TDocument>(colletionBuilder.Ascending(static c => c.Id),
                new CreateIndexOptions { Unique = true })
        ));

        return scope;
    }
}