using System;
using System.Collections.Generic;
using System.Text;

namespace ApiTemplate.Model.Weathers
{
    public class ForecastWeatherByLatLonRequestModel
    {
        public long Lat { get; set; }
        public long Lon { get; set; }
        public string City { get; set; }
        public string ZipCode { get; set; }
        public int Time { get; set; }

    }
}
