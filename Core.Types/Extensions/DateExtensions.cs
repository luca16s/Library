namespace Core.Types.Extensions;
public static class DateExtensions
{
    public static DateOnly ToDateOnly(
        this DateTime dateTime
    ) => new(dateTime.Year, dateTime.Month, dateTime.Day);
}
