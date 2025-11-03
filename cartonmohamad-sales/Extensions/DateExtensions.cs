using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;

namespace cartonmohamad_sales.Extensions
{
    public static class DateExtensions
    {
        private static readonly DateTime PcMin = new DateTime(622, 3, 22);
        private static readonly DateTime PcMax = new DateTime(9999, 12, 31, 23, 59, 59);

        public static string ToShamsiSafe(this DateTime? dt, bool withTime = false)
        {
            if (!dt.HasValue) return "—";
            var d = dt.Value;
            if (d < PcMin || d > PcMax) return "—";

            var pc = new PersianCalendar();
            var y = pc.GetYear(d);
            var m = pc.GetMonth(d);
            var day = pc.GetDayOfMonth(d);

            if (!withTime) return $"{y:0000}/{m:00}/{day:00}";
            var hh = pc.GetHour(d);
            var mm = pc.GetMinute(d);
            return $"{y:0000}/{m:00}/{day:00} {hh:00}:{mm:00}";
        }
    }
}