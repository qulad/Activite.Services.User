using System;
using System.Linq.Expressions;
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
            .AddMongoRepository<AppleCustomerDocument, Guid>(AppleCustomerDocument.CollectionName)
            .AddMongoRepository<CustomerDocument, Guid>(CustomerDocument.CollectionName)
            .AddMongoRepository<GoogleLocationDocument, Guid>(GoogleLocationDocument.CollectionName)
            .AddMongoRepository<GoogleCustomerDocument, Guid>(GoogleCustomerDocument.CollectionName)
            .AddMongoRepository<LocationDocument, Guid>(LocationDocument.CollectionName)
            .AddMongoRepository<UserDocument, Guid>(UserDocument.CollectionName);

        return builder;
    }

    public static IApplicationBuilder UseMongoIndexes(this IApplicationBuilder app)
    {
        using var scope = app.ApplicationServices.CreateScope();

        scope.CreateIndex<UserDocument>(static x => x.Id);
        scope.CreateIndex<UserDocument>(static x => x.Email);

        return app;
    }

    private static IServiceScope CreateIndex<TDocument>(this IServiceScope scope, Expression<Func<TDocument, object>> field) where TDocument : IIdentifiable<Guid>
    {
        var collection = scope.ServiceProvider.GetRequiredService<IMongoRepository<TDocument, Guid>>().Collection;
        var colletionBuilder = Builders<TDocument>.IndexKeys;
        Task.Run(async () => await collection.Indexes.CreateOneAsync(
            new CreateIndexModel<TDocument>(colletionBuilder.Ascending(field),
                new CreateIndexOptions { Unique = true })
        ));

        return scope;
    }
}