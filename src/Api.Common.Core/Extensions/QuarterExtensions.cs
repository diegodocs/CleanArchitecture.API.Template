using System;
using System.Collections.Generic;
using System.Text;

namespace Api.Common.Core.Extensions
{
    public static class QuarterExtensions
    {
        public static int GetQuarter(this DateTime datetime)
        {
            if (datetime.Month <= 3)
                return 1;
            if (datetime.Month <= 6)
                return 2;
            if (datetime.Month <= 9)
                return 3;

            return 4;
        }

        public static string GetQuarterDefinition(this DateTime datetime)
        {
            var quarter = datetime.GetQuarter();
            return $"Q{quarter}";
        }

        public static DateTime GetQuarterEndDate(this int quarter, int year)
        {
            switch (quarter)
            {
                case 1:
                    return new DateTime(year, 4, 1).AddDays(-1);
                case 2:
                    return new DateTime(year, 7, 1).AddDays(-1);
                case 3:
                    return new DateTime(year, 10, 1).AddDays(-1);
                case 4:
                    return new DateTime(year, 12, 31);
                default:
                    return DateTime.MinValue;
            }
        }

        public static DateTime GetQuarterEndDate(this DateTime datetime)
        {
            var quarter = datetime.GetQuarter();
            switch (quarter)
            {
                case 1:
                    return new DateTime(datetime.Year, 4, 1).AddDays(-1);
                case 2:
                    return new DateTime(datetime.Year, 7, 1).AddDays(-1);
                case 3:
                    return new DateTime(datetime.Year, 10, 1).AddDays(-1);
                case 4:
                    return new DateTime(datetime.Year, 12, 31);
                default:
                    return DateTime.MinValue;
            }
        }

        public static DateTime GetQuarterStartDate(this DateTime datetime)
        {
            var quarter = datetime.GetQuarter();
            switch (quarter)
            {
                case 1:
                    return new DateTime(datetime.Year, 1, 1);
                case 2:
                    return new DateTime(datetime.Year, 4, 1);
                case 3:
                    return new DateTime(datetime.Year, 7, 1);
                case 4:
                    return new DateTime(datetime.Year, 10, 1);
                default:
                    return DateTime.MinValue;
            }
        }
    }
}
