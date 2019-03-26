using System;

namespace PmsEteck.Data.Extensions
{
    public static class DateTimeExtension
    {
        //public static DateTime GetLastDayOfPreviousQuarter(this DateTime date)
        //{
        //    if (date.Month < 4)
        //    {
        //        return new DateTime(date.AddYears(-1).Year, 12, 31);
        //    }
        //    else if (date.Month < 7)
        //    {
        //        return new DateTime(date.Year, 3, 31);
        //    }
        //    else if (date.Month < 10)
        //    {
        //        return new DateTime(date.Year, 6, 30);
        //    }
        //    else
        //    {
        //        return new DateTime(date.Year, 9, 30);
        //    }
        //}

        public static DateTime GetFirstDayOfLastMonthFromPreviousQuarter(this DateTime date)
        {
            if (date.Month < 4)
            {
                return new DateTime(date.AddYears(-1).Year, 12, 1);
            }
            else if (date.Month < 7)
            {
                return new DateTime(date.Year, 3, 1);
            }
            else if (date.Month < 10)
            {
                return new DateTime(date.Year, 6, 1);
            }
            else
            {
                return new DateTime(date.Year, 9, 1);
            }
        }

        public static DateTime LastDayOfMonth(this DateTime dateTime)
        {
            return new DateTime(dateTime.Year, dateTime.Month, DateTime.DaysInMonth(dateTime.Year, dateTime.Month));
        }

        public static DateTime FirstDayOfMonth(this DateTime dateTime)
        {
            return new DateTime(dateTime.Year, dateTime.Month, 1);
        }

        public static DateTime FirstDayOfNextMonth(this DateTime dateTime)
        {
            return dateTime.FirstDayOfMonth().AddMonths(1);
        }

        public static DateTime FirstDayOfYear(this DateTime dateTime)
        {
            return new DateTime(dateTime.Year, 1, 1);
        }

        public static DateTime FirstDayOfQuarter(this DateTime dateTime)
        {
            return dateTime.Month < 4 ? new DateTime(dateTime.Year, 1, 1) : dateTime.Month < 7 ? new DateTime(dateTime.Year, 4, 1) : dateTime.Month < 10 ? new DateTime(dateTime.Year, 7, 1) : new DateTime(dateTime.Year, 10, 1);
        }
        public static int NumberOfMonthsBetweenDates(this DateTime dateTime1, DateTime dateTime2)
        {
            return (dateTime2.Month - dateTime1.Month + 1) + ((dateTime2.Year - dateTime1.Year) * 12);
        }

        public static int NumberOfMonthsBetweenDatesMAARDANGOED(this DateTime dateTime1, DateTime dateTime2)
        {
            return (dateTime2.Month - dateTime1.Month) + ((dateTime2.Year - dateTime1.Year) * 12);
        }

    }
}
