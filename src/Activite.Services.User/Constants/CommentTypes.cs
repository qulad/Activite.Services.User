using System.Collections.Generic;

namespace Activite.Services.User.Constants;

public static class CommentTypes
{
    public const string Customer = nameof(Customer);

    public const string Location = nameof(Location);

    public static HashSet<string> All => new HashSet<string>
    {
        Customer,
        Location
    };
}