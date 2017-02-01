using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Data;

namespace OneVK.Converters
{
    /// <summary>
    /// Time converter to display elapsed time relatively to the present.
    /// </summary>
    /// <QualityBand>Preview</QualityBand>
    public class RelativeTimeConverter : IValueConverter
    {
        /// <summary>
        /// A minute defined in seconds.
        /// </summary>
        private const double Minute = 60.0;

        /// <summary>
        /// An hour defined in seconds.
        /// </summary>
        private const double Hour = 60.0 * Minute;

        /// <summary>
        /// A day defined in seconds.
        /// </summary>
        private const double Day = 24 * Hour;

        /// <summary>
        /// A week defined in seconds.
        /// </summary>
        private const double Week = 7 * Day;

        /// <summary>
        /// A month defined in seconds.
        /// </summary>
        private const double Month = 30.5 * Day;

        /// <summary>
        /// A year defined in seconds.
        /// </summary>
        private const double Year = 365 * Day;
        
        /// <summary>
        /// Four different strings to express hours in plural.
        /// </summary>
        private string[] PluralHourStrings;

        /// <summary>
        /// Four different strings to express minutes in plural.
        /// </summary>
        private string[] PluralMinuteStrings;

        /// <summary>
        /// Four different strings to express seconds in plural.
        /// </summary>
        private string[] PluralSecondStrings;
        
        /// <summary>
        /// Возвращает текстовую строку месяца.
        /// </summary>
        /// <param name="month">Номер месяца.</param>
        private static string GetPluralMonth(int month)
        {
            if (month >= 2 && month <= 4)
                return String.Format("{0} месяца назад", month);
            else if (month >= 5 && month <= 12)
                return String.Format("{0} месяцев назад", month);
            else
                throw new ArgumentException("Недопустимый номер месяца.");
        }

        /// <summary>
        /// Returns a localized text string to express time units in plural.
        /// </summary>
        /// <param name="units">
        /// Number of time units, e.g. 5 for five months.
        /// </param>
        /// <param name="resources">
        /// Resources related to the specified time unit.
        /// </param>
        /// <returns>Localized text string.</returns>
        //private static string GetPluralTimeUnits(int units, string[] resources)
        //{
        //    int modTen = units % 10;
        //    int modHundred = units % 100;

        //    if (units <= 1)
        //    {
        //        throw new ArgumentException();
        //    }
        //    else if (units >= 2 && units <= 4)
        //    {
        //        return string.Format(CultureInfo.CurrentUICulture, resources[0], units.ToString(ControlResources.Culture));
        //    }
        //    else if (modTen == 1 && modHundred != 11)
        //    {
        //        return string.Format(CultureInfo.CurrentUICulture, resources[1], units.ToString(ControlResources.Culture));
        //    }
        //    else if ((modTen >= 2 && modTen <= 4) && !(modHundred >= 12 && modHundred <= 14))
        //    {
        //        return string.Format(CultureInfo.CurrentUICulture, resources[2], units.ToString(ControlResources.Culture));
        //    }
        //    else
        //    {
        //        return string.Format(CultureInfo.CurrentUICulture, resources[3], units.ToString(ControlResources.Culture));
        //    }
        //}

        /// <summary>
        /// Returns a localized text string for the "ast" + "day of week as {0}".
        /// </summary>
        /// <param name="dow">Last Day of week.</param>
        /// <returns>Localized text string.</returns>
        private static string GetLastDayOfWeek(DayOfWeek dow)
        {
            string result;

            switch (dow)
            {
                case DayOfWeek.Monday:
                    result = "в прошлый понедельник";
                    break;
                case DayOfWeek.Tuesday:
                    result = "в прошлый вторник";
                    break;
                case DayOfWeek.Wednesday:
                    result = "в прошлую среду";
                    break;
                case DayOfWeek.Thursday:
                    result = "в прошлый четверг";
                    break;
                case DayOfWeek.Friday:
                    result = "в прошлую пятницу";
                    break;
                case DayOfWeek.Saturday:
                    result = "в прошлую субботу";
                    break;
                case DayOfWeek.Sunday:
                    result = "в прошлое воскресенье";
                    break;
                default:
                    result = "в прошлое воскресенье";
                    break;
            }

            return result;
        }


        /// <summary>
        /// Returns a localized text string to express "on {0}"
        /// where {0} is a day of the week, e.g. Sunday.
        /// </summary>
        /// <param name="dow">Day of week.</param>
        /// <returns>Localized text string.</returns>
        private static string GetOnDayOfWeek(DayOfWeek dow)
        {
            string result;

            switch (dow)
            {
                case DayOfWeek.Monday:
                    result = "понедельник";
                    break;
                case DayOfWeek.Tuesday:
                    result = "вторник";
                    break;
                case DayOfWeek.Wednesday:
                    result = "среда";
                    break;
                case DayOfWeek.Thursday:
                    result = "четверг";
                    break;
                case DayOfWeek.Friday:
                    result = "пятница";
                    break;
                case DayOfWeek.Saturday:
                    result = "суббота";
                    break;
                case DayOfWeek.Sunday:
                    result = "воскресенье";
                    break;
                default:
                    result = "воскресенье";
                    break;
            }

            return result;
        }

        
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (!(value is DateTime))
                throw new ArgumentException("TargetType должен являться DateTime.");

            string result;

            DateTime given = ((DateTime)value).ToLocalTime();

            DateTime current = DateTime.Now;

            TimeSpan difference = current - given;
            
            //if (DateTimeFormatHelper.IsFutureDateTime(current, given))
            //{
            //    result = GetPluralTimeUnits(2, PluralSecondStrings);
            //}

            if (difference.TotalSeconds > Year)
            {
                // "over a year ago"
                result = "больше года назад";
            }
            else if (difference.TotalSeconds > (1.5 * Month))
            {
                // "x months ago"
                int nMonths = (int)((difference.TotalSeconds + Month / 2) / Month);
                result = GetPluralMonth(nMonths);
            }
            else if (difference.TotalSeconds >= (3.5 * Week))
            {
                // "about a month ago"
                result = "около месяца назад";
            }
            else if (difference.TotalSeconds >= Week)
            {
                int nWeeks = (int)(difference.TotalSeconds / Week);
                if (nWeeks > 1)
                {
                    // "x weeks ago"
                    result = String.Format("{0} недель назад", nWeeks);
                }
                else
                {
                    // "about a week ago"
                    result = "около недели назад";
                }
            }
            else if (difference.TotalSeconds >= (5 * Day))
            {
                // "last <dayofweek>"    
                result = GetLastDayOfWeek(given.DayOfWeek);
            }
            else if (difference.TotalSeconds >= Day)
            {
                // "on <dayofweek>"
                result = GetOnDayOfWeek(given.DayOfWeek);
            }
            else if (difference.TotalSeconds >= (2 * Hour))
            {
                // "x hours ago"
                int nHours = (int)(difference.TotalSeconds / Hour);
                result = String.Format("{0} часов назад", nHours);
            }
            else if (difference.TotalSeconds >= Hour)
            {
                // "about an hour ago"
                result = "около часа назад";
            }
            else if (difference.TotalSeconds >= (2 * Minute))
            {
                // "x minutes ago"
                int nMinutes = (int)(difference.TotalSeconds / Minute);
                result = String.Format("{0} минут назад", nMinutes);
            }
            else if (difference.TotalSeconds >= Minute)
            {
                // "about a minute ago"
                result = "около минуты назад";
            }
            else
            {
                // "x seconds ago" or default to "2 seconds ago" if less than two seconds.
                int nSeconds = ((int)difference.TotalSeconds > 1.0) ? (int)difference.TotalSeconds : 2;
                result = String.Format("{0} секунд назад", nSeconds);
            }

            return result;
        }

        /// <summary>
        /// Not implemented.
        /// </summary>
        /// <param name="value">(Not used).</param>
        /// <param name="targetType">(Not used).</param>
        /// <param name="parameter">(Not used).</param>
        /// <param name="culture">(Not used).</param>
        /// <returns>null</returns>
        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
