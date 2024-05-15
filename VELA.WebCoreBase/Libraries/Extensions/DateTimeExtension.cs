using System.Runtime.InteropServices;

namespace VELA.WebCoreBase.Libraries.Extensions;

public static class DateTimeExtension
{
    private static readonly TimeZoneInfo Gmt7TimeZoneInfo;
    static DateTimeExtension()
    {
        if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
        {
            Gmt7TimeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById("SE Asia Standard Time");
        }
        else
        {
            Gmt7TimeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById("Asia/Ho_Chi_Minh");
        }
    }
    public static string CreateMySqlFormat(string defaultFormat = "yyyy-MM-dd HH:mm:ss")
    {
        return DateTime.UtcNow.ToString(defaultFormat);
    }

    public static string CreateDisplayFormat(string defaultFormat = "yyMMddHHmmss")
    {
        return DateTime.UtcNow.ToString(defaultFormat);
    }

    public static DateTime GetNextDays(this DateTime dateTime, int days = 1)
    {
        return dateTime.AddDays(days);
    }

    public static DateTime GetTomorrow(this DateTime dateTime)
    {
        return dateTime.GetNextDays();
    }

    public static bool CompareDate(this DateTime dateTime1, DateTime? dateTime2)
    {
        return dateTime2 is null || dateTime1.Date == dateTime2.GetValueOrDefault().Date;
    }

    public static bool ValidDuration(DateTime? validFrom, DateTime? validTo)
    {
        return validFrom.GetValueOrDefault().Date < validTo.GetValueOrDefault().Date
            && validTo.GetValueOrDefault().Date > DateTime.UtcNow.Date;

    }

    public static bool ValidNow(this DateTime? validDate)
    {
        return validDate.GetValueOrDefault().Date < DateTime.UtcNow.Date;
    }

    public static int ShowHighlight(this DateTime? validTo)
    {
        if (validTo <= DateTime.UtcNow.AddDays(15))
        {
            return 1;
        }
        if (DateTime.UtcNow.AddDays(15) < validTo && validTo <= DateTime.UtcNow.AddDays(30))
        {
            return 2;
        }
        if (DateTime.UtcNow.AddDays(30) < validTo && validTo <= DateTime.UtcNow.AddDays(45))
        {
            return 3;
        }
        return 0;
    }
    public static DateTime ConvertUtcToGtm7(this DateTime date)
    {
        return TimeZoneInfo.ConvertTimeFromUtc(date, Gmt7TimeZoneInfo);
    }
}