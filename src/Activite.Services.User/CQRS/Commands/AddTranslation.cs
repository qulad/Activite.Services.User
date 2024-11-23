using System;
using Activite.Services.User.Attributes;
using Convey.CQRS.Commands;

namespace Activite.Services.User.CQRS.Commands;

[Contract]
public class AddTranslation : ICommand
{
    public Guid Id { get; set; }

    public string Code { get; set; }

    public string Content { get; set; }

    public string Region { get; set; }

    public AddTranslation(
        Guid id,
        string code,
        string content,
        string region)
    {
        Id = id;
        Code = code;
        Content = content;
        Region = region;
    }
}