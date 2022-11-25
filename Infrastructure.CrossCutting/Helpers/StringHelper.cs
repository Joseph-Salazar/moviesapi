namespace Infrastructure.CrossCutting.Helpers;

public static class StringHelper
{
    public static string ToPascalCase(string text)
    {
        if (text == null) return null;
        if (text.Length < 2) return text.ToUpper();

        // Split the string into words.
        var words = text.Split(
            new char[] { },
            StringSplitOptions.RemoveEmptyEntries);

        // Combine the words.
        var result = "";
        foreach (var word in words)
        {
            result +=
                $"{word.Substring(0, 1).ToUpper()}{word.Substring(1)}";
        }

        return result;
    }
}