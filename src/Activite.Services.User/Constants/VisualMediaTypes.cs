using System.Collections.Generic;

namespace Activite.Services.User.Constants;

public static class VisualMediaTypes
{
    public const string Picture = nameof(Picture);

    public const string Video = nameof(Video);

    public static HashSet<string> All => new HashSet<string>
    {
        Picture,
        Video
    };
}