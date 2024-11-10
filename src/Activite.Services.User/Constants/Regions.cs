using System.Collections.Generic;

namespace Activite.Services.User.Constants;

public static class Regions
{
    public const string Turkey = "TR";

    public static HashSet<string> All => new HashSet<string>
    {
        Turkey
    };
}