using System.Collections.Generic;

namespace Activite.Services.User.Constants;

public static class Currencies
{
    public const string TRY = nameof(TRY);

    public static HashSet<string> All => new HashSet<string>
    {
        TRY
    };
}