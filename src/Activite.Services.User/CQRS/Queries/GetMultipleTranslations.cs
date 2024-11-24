using System;
using System.Collections.Generic;
using Activite.Services.User.Constants;
using Activite.Services.User.DTOs;
using Convey.CQRS.Queries;

namespace Activite.Services.User.CQRS.Queries;

public class GetMultipleTranslations : PagedQueryBase, IQuery<PagedResult<TranslationDto>>
{
    public Guid? Id { get; set; }

    public IList<Guid> Ids { get; set; }

    public string Code { get; set; }

    public string Content { get; set; }

    public string SearchContent { get; set; }

    public string Region { get; set; }

    public GetMultipleTranslations(
        Guid? id = null,
        IList<Guid> ids = null,
        string code = null,
        string content = null,
        string searchContent = null,
        string region = null,
        int page = Pagination.DefaultPage,
        int results = Pagination.DefaultResults,
        string orderBy = Pagination.DefaultOrderBy,
        string sortOrder = Pagination.DefaultSortOrder)
    {
        Id = id;
        Ids = ids;
        Code = code;
        Content = content;
        SearchContent = searchContent;
        Region = region;
        Page = page;
        Results = results;
        OrderBy = orderBy;
        SortOrder = sortOrder;
    }
}