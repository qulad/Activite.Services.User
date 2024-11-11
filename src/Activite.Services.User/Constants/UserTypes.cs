using System.Collections.Generic;

namespace Activite.Services.User.Constants;

public static class UserTypes
{
    public const string Google = nameof(Google);
    public const string GoogleLocation = nameof(GoogleLocation);
    public const string Apple = nameof(Apple);

    public static HashSet<string> All => new()
    {
        Google,
        GoogleLocation,
        Apple
    };
}