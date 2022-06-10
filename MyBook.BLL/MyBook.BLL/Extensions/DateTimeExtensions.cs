using System.Globalization;

namespace MyBook.BLL.Extensions;

public static class DateTimeExtensions
{
    public static int GetWeeksInYear(this DateTime time)
    {
        var dfi = DateTimeFormatInfo.CurrentInfo;
        var date1 = new DateTime(time.Year, 12, 31);
        var cal = dfi.Calendar;
        return cal.GetWeekOfYear(date1, dfi.CalendarWeekRule,
            dfi.FirstDayOfWeek);
    }
    
    public static int GetWeekOfYear(this DateTime time)
    {
        var day = CultureInfo.InvariantCulture.Calendar.GetDayOfWeek(time);
        if (day >= DayOfWeek.Monday && day <= DayOfWeek.Wednesday) 
            time = time.AddDays(3);

        return CultureInfo.InvariantCulture.Calendar.GetWeekOfYear(time, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);
    }
    
    public static DateTime GetWeekStartDate(this DateTime time, int week)
    {
        var jan1 = new DateTime(time.Year, 1, 1);
        var day = (int)jan1.DayOfWeek - 1;
        var diff = (day < 4 ? -day : 7 - day) + 7 * (week - 1);

        return jan1.AddDays(diff);
    }
}