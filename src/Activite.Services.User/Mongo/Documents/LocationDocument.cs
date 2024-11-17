using System;

namespace Activite.Services.User.Mongo.Documents;

public class LocationDocument : UserDocument
{
    public string Address { get; set; }

    public string Name { get; set; }

    public string Description { get; set; }

    public DateOnly EstabilishedDate { get; set; }
}