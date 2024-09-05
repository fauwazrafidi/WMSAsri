using SHARED.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SHARED.Contracts
{
    public interface IWeather
    {
        Task<WeatherForecast[]> GetWeatherForecast();
        Task<WeatherForecast[]> GetWeatherForecastByAdmin();
    }
}
