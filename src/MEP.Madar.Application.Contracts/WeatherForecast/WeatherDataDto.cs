using System;
using System.Collections.Generic;
using System.Text;

namespace MEP.Madar.WeatherForecast
{
    public class WeatherDataDto
    {
        //public DateOnly Date { get; set; }
        public DateTime Date { get; set; }

        public int TemperatureC { get; set; }

        public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);

        public string? Summary { get; set; }
    }
}
