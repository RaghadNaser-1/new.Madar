using MEP.Madar.WeatherForecast.Dto;
using MEP.Madar.WeatherForecast;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;
using Volo.Abp.Timing;
using System.Globalization;

namespace MEP.Madar.WeatherForecast
{
    public class WeatherForecastAppService : ApplicationService, IWeatherForecastAppService
    {

        private readonly HttpClient _httpClient;
        //private readonly IConfiguration _configuration;
        private const string ApiBaseUrl = "http://api.openweathermap.org/data/2.5/weather";
        private readonly IConfiguration _configuration;
        private readonly IClock _clock;

        public WeatherForecastAppService(IConfiguration configuration)
        {
            _httpClient = new HttpClient();
            _configuration = configuration;
        }
        public async Task<WeatherDataDto> GetAsync(string city = "Gujranwala")
        {

            string apiKey = _configuration["OpenWeatherMapApiKey"];
            string url = $"{ApiBaseUrl}?q={city}&appid={apiKey}&units=metric";
            var response = new WeatherDataDto();

            try
            {
                response = await _httpClient.GetFromJsonAsync<WeatherDataDto>(url);

            }
            catch (Exception e)
            {
                throw new HttpRequestException("Failed to retrieve weather data");
            }
            return response;
        }
    }
}

