using System;
using System.Globalization;

namespace cartonmohamad_sales.Utilities
{
    public static class PersianDateExtensions
    {
        private static readonly PersianCalendar pc = new PersianCalendar();

        public static string ToPersianDate(this DateTime dt, string sep = "/")
        {
            return $"{pc.GetYear(dt):0000}{sep}{pc.GetMonth(dt):00}{sep}{pc.GetDayOfMonth(dt):00}";
        }

        public static string ToPersianDateTime(this DateTime dt)
        {
            return $"{dt.ToPersianDate()} {pc.GetHour(dt):00}:{pc.GetMinute(dt):00}";
        }

        public static DateTime? ParsePersianDate(string input)  // 1403/07/28 یا 1403-07-28
        {
            if (string.IsNullOrWhiteSpace(input)) return null;
            input = input.Trim().Replace('-', '/').Replace('\\', '/');

            var parts = input.Split('/');
            if (parts.Length < 3) return null;
            if (!int.TryParse(parts[0], out int y)) return null;
            if (!int.TryParse(parts[1], out int m)) return null;
            if (!int.TryParse(parts[2], out int d)) return null;

            try { return new DateTime(y, m, d, pc); }
            catch { return null; }
        }
    }
}
