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
            .AddMongoRepository<AgeRestrictionDocument, Guid>(AgeRestrictionDocument.CollectionName)
            .AddMongoRepository<AmountCouponDocument, Guid>(AmountCouponDocument.CollectionName)
            .AddMongoRepository<AppleCustomerDocument, Guid>(AppleCustomerDocument.CollectionName)
            .AddMongoRepository<CommentDocument, Guid>(CommentDocument.CollectionName)
            .AddMongoRepository<CouponDocument, Guid>(CouponDocument.CollectionName)
            .AddMongoRepository<CustomerCommentDocument, Guid>(CustomerCommentDocument.CollectionName)
            .AddMongoRepository<CustomerDocument, Guid>(CustomerDocument.CollectionName)
            .AddMongoRepository<CustomerWalletDocument, Guid>(CustomerWalletDocument.CollectionName)
            .AddMongoRepository<EventDocument, Guid>(EventDocument.CollectionName)
            .AddMongoRepository<GoogleLocationDocument, Guid>(GoogleLocationDocument.CollectionName)
            .AddMongoRepository<GoogleCustomerDocument, Guid>(GoogleCustomerDocument.CollectionName)
            .AddMongoRepository<LocationCommentDocument, Guid>(LocationCommentDocument.CollectionName)
            .AddMongoRepository<LocationDocument, Guid>(LocationDocument.CollectionName)
            .AddMongoRepository<LocationWalletDocument, Guid>(LocationWalletDocument.CollectionName)
            .AddMongoRepository<OfferDocument, Guid>(OfferDocument.CollectionName)
            .AddMongoRepository<PercentageDocument, Guid>(PercentageDocument.CollectionName)
            .AddMongoRepository<TicketDocument, Guid>(TicketDocument.CollectionName)
            .AddMongoRepository<TransactionDocument, Guid>(TransactionDocument.CollectionName)
            .AddMongoRepository<TranslationDocument, Guid>(TranslationDocument.CollectionName)
            .AddMongoRepository<UserDocument, Guid>(UserDocument.CollectionName)
            .AddMongoRepository<VisualMediaDocument, Guid>(VisualMediaDocument.CollectionName)
            .AddMongoRepository<WalletDocument, Guid>(WalletDocument.CollectionName);

        return builder;
    }

    public static IApplicationBuilder UseMongoIndexes(this IApplicationBuilder app)
    {
        using var scope = app.ApplicationServices.CreateScope();

        scope.CreateIndex<AgeRestrictionDocument>(static x => x.Id);
        scope.CreateIndex<CommentDocument>(static x => x.Id);
        scope.CreateIndex<CouponDocument>(static x => x.Id);
        scope.CreateIndex<CustomerCommentDocument>(static x => x.Id);
        scope.CreateIndex<EventDocument>(static x => x.Id);
        scope.CreateIndex<LocationCommentDocument>(static x => x.Id);
        scope.CreateIndex<OfferDocument>(static x => x.Id);
        scope.CreateIndex<TicketDocument>(static x => x.Id);
        scope.CreateIndex<TransactionDocument>(static x => x.Id);
        scope.CreateIndex<TranslationDocument>(static x => x.Id);
        scope.CreateIndex<UserDocument>(static x => x.Id);
        scope.CreateIndex<UserDocument>(static x => x.Email);
        scope.CreateIndex<VisualMediaDocument>(static x => x.Id);
        scope.CreateIndex<WalletDocument>(static x => x.Id);

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