using System;
using System.Collections.Generic;
using System.Text;

namespace MEP.Madar.DateConversion
{
    public interface IDateConversionAppService
    {
        //string ConvertGregorianToHijri(DateTime gregorianDate);
        string ConvertGregorianToHijri(DateTime Date, string Type = "sa");
        string ConvertHijriToGregorian(string hijriDate);
    }
}
