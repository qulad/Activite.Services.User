using System.Collections.Generic;

namespace Activite.Services.User.Constants;

public static class CouponTypes
{
    public const string Amount = nameof(Amount);

    public const string Percentage = nameof(Percentage);

    public static HashSet<string> All => new HashSet<string>
    {
        Amount,
        Percentage
    };
}