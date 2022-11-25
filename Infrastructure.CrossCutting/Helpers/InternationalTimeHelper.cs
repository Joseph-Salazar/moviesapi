namespace Infrastructure.CrossCutting.Helpers;

public static class InternationalTimeHelper
{
    private static readonly Dictionary<string, string> TimeZones = new()
    {
        {"AR", "Argentina Standard Time"},
        {"BO", "SA Western Standard Time"},
        {"CL", "Pacific SA Standard Time"},
        {"CO", "SA Pacific Standard Time"},
        {"CR", "Central America Standard Time"},
        {"EC", "SA Pacific Standard Time"},
        {"SV", "Central America Standard Time"},
        {"GT", "Central America Standard Time"},
        {"MX", "Central Standard Time (Mexico)"},
        {"PA", "Central America Standard Time"},
        {"PE", "SA Pacific Standard Time"},
        {"PR", "SA Western Standard Time"},
        {"DO", "SA Western Standard Time"},
        {"EU", "Central America Standard Time"},
        {"VE", "Venezuela Standard Time"}
    };

    public static DateTime GetDateInSpecificCountry(string destinyCountry, DateTime date)
    {
        var countryRealDate = TimeZoneInfo.ConvertTime(date,
            TimeZoneInfo.Utc, TimeZoneInfo.FindSystemTimeZoneById(TimeZones[destinyCountry]));

        return countryRealDate;
    }

    public static DateTime GetDateNowInSpecificCountry(string destinyCountry)
    {
        var date = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow,
            TimeZoneInfo.FindSystemTimeZoneById(TimeZones[destinyCountry]));
        var dateUtc = GetDateInSpecificCountry(destinyCountry, date);

        return dateUtc;
    }

    public static DateTime ChangeToUtcDate(string destinyCountry, DateTime theDateToChange)
    {
        var estTimeZone = TimeZoneInfo.FindSystemTimeZoneById(TimeZones[destinyCountry]);
        var dateUtc = TimeZoneInfo.ConvertTimeToUtc(DateTime.SpecifyKind(theDateToChange, DateTimeKind.Unspecified),
            estTimeZone);

        return dateUtc;
    }

    public static TimeZoneInfo GetTimeZoneInfoBySpecificCountry(string destinyCountry)
    {
        return TimeZoneInfo.FindSystemTimeZoneById(TimeZones[destinyCountry]);
    }
}