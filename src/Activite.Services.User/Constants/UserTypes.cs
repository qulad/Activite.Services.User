using System.Collections.Generic;

namespace Activite.Services.User.Constants;

public static class UserTypes
{
    public const string AppleCustomer = nameof(AppleCustomer);
    public const string Customer = nameof(Customer);
    public const string GoogleCustomer = nameof(GoogleCustomer);
    public const string GoogleLocation = nameof(GoogleLocation);
    public const string Location = nameof(Location);

    public static HashSet<string> All => new()
    {
        AppleCustomer,
        Customer,
        GoogleCustomer,
        GoogleLocation,
        Location
    };
}