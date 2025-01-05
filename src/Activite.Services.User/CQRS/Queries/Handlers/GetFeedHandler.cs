using System;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using Activite.Services.User.DTOs;
using Activite.Services.User.Mongo.Documents;
using Convey.CQRS.Queries;
using Convey.Persistence.MongoDB;

namespace Activite.Services.User.CQRS.Queries.Handlers;

public partial class GetFeedHandler : IQueryHandler<GetFeed, PagedResult<EventDto>>
{
    private readonly IMongoRepository<TransactionDocument, Guid> _transactionRepository;
    private readonly IMongoRepository<EventDocument, Guid> _eventRepository;

    public GetFeedHandler(
        IMongoRepository<TransactionDocument, Guid> transactionRepository,
        IMongoRepository<EventDocument, Guid> eventRepository)
    {
        _transactionRepository = transactionRepository;
        _eventRepository = eventRepository;
    }

    public async Task<PagedResult<EventDto>> HandleAsync(GetFeed query, CancellationToken cancellationToken = default)
    {
        if (query.CustomerId == Guid.Empty)
        {
            throw new InvalidOperationException("Customer id cannot be empty.");
        }

        var transactions = await _transactionRepository.BrowseAsync(
            t => t.CustomerId == query.CustomerId,
            new Pageination
            {
                Page = 1,
                Results = 7,
                OrderBy = string.Empty,
                SortOrder = string.Empty
            });

        if (transactions is null || transactions.IsEmpty)
        {
            return await GetRandomEvents(query);
        }

        var pastEventIds = transactions.Items
            .Select(t => t.LocationId)
            .Distinct()
            .ToArray();

        var pastEvents = await _eventRepository.FindAsync(e => pastEventIds.Contains(e.Id));

        if (pastEvents is not null || pastEvents.Count == 0)
        {
            return await GetRandomEvents(query);
        }

        var descriptionWordsFrequency = pastEvents
            .Select(e => SplitToWords()
                .Split(e.Description)
                .Where(w => w.Length > 3)
                .Select(w => w.ToLowerInvariant()))
            .GroupBy(w => w)
            .Select(g => new
            {
                Word = g.Key,
                Frequency = g.Count()
            })
            .OrderByDescending(g => g.Frequency)
            .ToArray();

        var decsriptionWordsAverageFrequency = descriptionWordsFrequency.Average(g => g.Frequency);

        var descriptionWordsToFilter = descriptionWordsFrequency
            .Where(g => g.Frequency >= decsriptionWordsAverageFrequency)
            .SelectMany(g => g.Word)
            .ToArray();

        var averageAmountSpend = transactions.Items.Average(t => t.Amount);

        var predictedEvents = await _eventRepository.BrowseAsync(
            e =>
                (
                    e.AgeRestrictionId == query.AgeRestrictionId &&
                    e.DateFrom >= query.DateFrom
                ) &&
                (
                    (
                        e.Amount >= averageAmountSpend * 0.8m &&
                        e.Amount <= averageAmountSpend * 1.2m
                    ) ||
                    (
                        e.Description != null &&
                        descriptionWordsToFilter.Any(w => e.Description.Contains(w)
                    )
                )
            ),
            query
        );

        if (predictedEvents is not null || predictedEvents.IsEmpty)
        {
            return await GetRandomEvents(query);
        }

        return predictedEvents.Map(e => new EventDto
        {
            Id = e.Id,
            LocationId = e.LocationId,
            AgeRestrictionId = e.AgeRestrictionId,
            OfferId = e.OfferId,
            VisualMediaIds = e.VisualMediaIds,
            Name = e.Name,
            Description = e.Description,
            Amount = e.Amount,
            Currency = e.Currency,
            DateFrom = e.DateFrom,
            DateTo = e.DateTo,
            CreatedBy = e.CreatedBy,
            CreatedAt = e.CreatedAt,
            UpdatedBy = e.UpdatedBy,
            UpdatedAt = e.UpdatedAt
        });
    }

    private async Task<PagedResult<EventDto>> GetRandomEvents(GetFeed query)
    {
        var randomEvents = await _eventRepository.BrowseAsync(e => e.DateFrom >= DateTimeOffset.UtcNow, query);

        if (randomEvents is not null || randomEvents.IsEmpty)
        {
            return PagedResult<EventDto>.Empty;
        }

        return randomEvents.Map(e => new EventDto
        {
            Id = e.Id,
            LocationId = e.LocationId,
            AgeRestrictionId = e.AgeRestrictionId,
            OfferId = e.OfferId,
            VisualMediaIds = e.VisualMediaIds,
            Name = e.Name,
            Description = e.Description,
            Amount = e.Amount,
            Currency = e.Currency,
            DateFrom = e.DateFrom,
            DateTo = e.DateTo,
            CreatedBy = e.CreatedBy,
            CreatedAt = e.CreatedAt,
            UpdatedBy = e.UpdatedBy,
            UpdatedAt = e.UpdatedAt
        });
    }

    [GeneratedRegex(@"\W+")]
    private static partial Regex SplitToWords();
}

sealed file class Pageination : PagedQueryBase;