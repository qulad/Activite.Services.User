using System;
using Activite.Services.User.Attributes;
using Convey.CQRS.Commands;

namespace Activite.Services.User.CQRS.Commands;

[Contract]
public class AddAgeRestriction : ICommand
{
    public Guid Id { get; set; }

    public string Code { get; set; }

    public int Age { get; set; }

    public AddAgeRestriction(
        Guid id,
        string code,
        int age)
    {
        Id = id;
        Code = code;
        Age = age;
    }
}