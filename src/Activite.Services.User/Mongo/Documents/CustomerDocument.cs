using System;

namespace Activite.Services.User.Mongo.Documents;

public class CustomerDocument : UserDocument
{
    public string FirstName { get; set; }

    public string LastName { get; set; }

    public DateOnly DateOfBirth { get; set; }
}