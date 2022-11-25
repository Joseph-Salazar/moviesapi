using System.Dynamic;
using System.Globalization;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.StaticFiles;
using MimeTypes;

namespace Infrastructure.CrossCutting.Helpers;

public static class CoreHelper
{
    public static string GetSha256(string str)
    {
        var sha256 = SHA256.Create();
        var enconding = new ASCIIEncoding();
        var sb = new StringBuilder();
        var stream = sha256.ComputeHash(enconding.GetBytes(str));
        foreach (var item in stream) sb.AppendFormat("{0:x2}", item);

        return sb.ToString();
    }

    public static bool IsValidEmail(string email)
    {
        return Regex.IsMatch(email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$", RegexOptions.IgnoreCase);
    }

    public static DateTime GetDateTimePeru()
    {
        DateTime dateTime = TimeZoneInfo.ConvertTime(DateTime.Now, GetTimeZonePeru());
        return dateTime;
    }

    public static TimeZoneInfo GetTimeZonePeru()
    {
        string displayName = "(GMT-05:00) Peru Time";
        string standardName = "Peru Time";
        TimeSpan offset = new TimeSpan(-5, 0, 0);
        return TimeZoneInfo.CreateCustomTimeZone(standardName, offset, displayName, standardName);
    }

    public static string Base64Encode(string plainText)
    {
        var plainTextBytes = Encoding.UTF8.GetBytes(plainText);
        return Convert.ToBase64String(plainTextBytes);
    }

    public static string Base64Decode(string base64EncodedData)
    {
        var base64EncodedBytes = Convert.FromBase64String(base64EncodedData);
        return Encoding.UTF8.GetString(base64EncodedBytes);
    }

    public static string GetExtension(string contentType)
    {
        return MimeTypeMap.GetExtension(contentType);
    }

    public static DateTime GetNextWeekday(int day)
    {
        DateTime result = GetDateTimePeru().AddDays(1);
        while ((int) result.DayOfWeek != day)
            result = result.AddDays(1);
        return result;
    }

    public static DateTime GetNextWeekday(DateTime date, int day)
    {
        while ((int) date.DayOfWeek != day)
            date = date.AddDays(1);
        return date;
    }

    public static IEnumerable<(string Key, string Name)> GetNameMonthRange(DateTime desde, DateTime hasta)
    {
        return Enumerable.Range(0, 11)
            .Select(p => desde.AddMonths(p))
            .TakeWhile(p => p <= hasta)
            .Select(p => (
                Key: p.ToString("MM/yyyy"),
                Name: p.GetNameMonth()
            ))
            .ToList();
    }

    public static string ToInvarianTitleCase(this string self)
    {
        if (string.IsNullOrWhiteSpace(self))
            return self;

        return CultureInfo.InvariantCulture.TextInfo.ToTitleCase(self);
    }

    public static string GetNameMonth(this DateTime self)
    {
        return self.ToString("MMMM", CultureInfo.CreateSpecificCulture("es")).ToInvarianTitleCase();
    }

    public static void CreateDirectory(string path)
    {
        if (!Directory.Exists(path)) Directory.CreateDirectory(path);
    }

    public static async Task SaveFile(string folderPath, string fileName, string fileBase64)
    {
        var fullPathFile = Path.Combine(folderPath, fileName);
        CreateDirectory(folderPath);

        byte[] fileBytes = Convert.FromBase64String(fileBase64);
        await File.WriteAllBytesAsync(fullPathFile, fileBytes);
    }

    public static string ToUniqueName(this string self)
    {
        if (string.IsNullOrEmpty(self))
            return self;

        int extensionIndex = self.LastIndexOf('.');
        extensionIndex = extensionIndex != -1 ? extensionIndex : self.Length;
        return self.Insert(extensionIndex, $"_{DateTime.Now:yyyyMMddHHmmss}");
    }

    public static string? GetHeader(this HttpRequest request, string key)
    {
        return request.Headers.FirstOrDefault(x => x.Key == key).Value.FirstOrDefault();
    }

    /// <summary>
    /// Remove multiple spaces and line breaks from a string (\t, \n, \r)
    /// </summary>
    /// <param name="text"></param>
    /// <returns></returns>
    public static string RemoveExtraWhiteSpace(this string text)
    {
        return Regex.Replace(text, @"\s+|\t|\n|\r", " ").Trim();
    }

    public static dynamic ToExpandoObject(object value)
    {
        IDictionary<string, object> dapperRowProperties = value as IDictionary<string, object>;

        IDictionary<string, object> expando = new ExpandoObject();

        foreach (KeyValuePair<string, object> property in dapperRowProperties)
            expando.Add(property.Key, property.Value);

        return (ExpandoObject) expando;
    }

    public static string PropertyNameValidator(this string self, string propertyName)
    {
        return string.IsNullOrWhiteSpace(self) ? self : self.Replace("{PropertyName}", propertyName);
    }

    private static string GetMimeType(string fileName)
    {
        var provider = new FileExtensionContentTypeProvider();
        if (!provider.TryGetContentType(fileName, out var contentType))
            contentType = "application/octet-stream";
        return contentType;
    }

    public static string GenerateNameGuidFile(string fileName)
    {
        return Guid.NewGuid() + Path.GetExtension(fileName);
    }
}