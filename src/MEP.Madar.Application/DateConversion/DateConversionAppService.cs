using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;

namespace MEP.Madar.DateConversion
{
    public class DateConversionAppService : ApplicationService, IDateConversionAppService
    {
        private CultureInfo _cultureInfo;
        public DateConversionAppService()
        {
             _cultureInfo = new CultureInfo("ar-SA");
            _cultureInfo.DateTimeFormat.Calendar = new HijriCalendar();
            ((HijriCalendar)_cultureInfo.DateTimeFormat.Calendar).HijriAdjustment = -1;
        }
        public string ConvertGregorianToHijri(DateTime Date, string Type="sa")
        {
            if (Type == "sa")
            {
            string date = Date.ToString("D", _cultureInfo);
            return date;
            }
            else
            {
                return Date.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture);
            }
        }

        public string ConvertHijriToGregorian(string hijriDate)
        {
            var formats = new[] { "yyyy/M/d", "yyyy/MM/dd" };
            try
            {
                var dateTime = DateTime.ParseExact(hijriDate, formats, _cultureInfo, DateTimeStyles.None);
                return dateTime.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture);
            }
            catch (FormatException e)
            {
                return $"Error parsing date: {e.Message}";
            }
        }
    }
}
