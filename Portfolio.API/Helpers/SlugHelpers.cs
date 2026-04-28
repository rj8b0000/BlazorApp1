using System.Text.RegularExpressions;

namespace Portfolio_Backend.Helpers;

public class SlugHelpers
{
    public static string Generate(string title)
    {
        var slug = title.ToLower().Trim();

        slug = Regex.Replace(slug, @"[^a-z0-9\s-]", "");

        slug = Regex.Replace(slug, @"\s+", "-");

        return slug;
    }
}