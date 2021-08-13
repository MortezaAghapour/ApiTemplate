using System;
using System.Collections.Generic;
using System.Text;
using ApiTemplate.Common.Markers.Configurations;

namespace ApiTemplate.Core.Configurations.Weathers
{
    public class WeatherConfiguration:IAppSetting
    {
        public string Key { get; set; }
        public string BaseUrl { get; set; }
        public string CurrentWeatherMethodName { get; set; }
        public string ForecastWeatherMethodName { get; set; }
        public int SendWeatherInfoAfterMinute { get; set; }
    }
}
