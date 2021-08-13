using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ApiTemplate.Model.Weathers
{
    public class CurrentWeatherByLatLonRequestModel
    {
       
        public double Lat { get; set; }
        public double Lon { get; set; }
     
    }
}
