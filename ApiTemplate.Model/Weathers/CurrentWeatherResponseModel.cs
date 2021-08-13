using System.Text.Json.Serialization;

namespace ApiTemplate.Model.Weathers
{
    public class CurrentWeatherResponseModel
    {
        [JsonPropertyName("location")]
        public Location Location { get; set; }=new Location();
       [JsonPropertyName("current")]
        public Current Current { get; set; }=new Current();
    }

  
    public class Location
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }
        [JsonPropertyName("region")]
        public string Region { get; set; }
        [JsonPropertyName("country")]
        public string Country { get; set; }
        [JsonPropertyName("lat")]
        public double Lat { get; set; }
        [JsonPropertyName("lon")]
        public double Lon { get; set; }
          [JsonPropertyName("tz_id")]
        public string TzId { get; set; }
        [JsonPropertyName("localtime_epoch")]
        public long LocaltimeEpoch { get; set; }
          [JsonPropertyName("localtime")]
        public string LocalTime { get; set; }

    }

    public class Current
    {      
        [JsonPropertyName("last_updated_epoch")]
        public long LastUpdatedEpoch { get; set; }
          [JsonPropertyName("last_updated")]
        public string LastUpdated { get; set; }
        [JsonPropertyName("temp_c")]
        public float TempC { get; set; }      
        [JsonPropertyName("temp_f")]
        public float TempF { get; set; }
        [JsonPropertyName("is_day")]
        public int IsDay { get; set; }
         [JsonPropertyName("condition")]
        public Condition Condition { get; set; } =new Condition();
          [JsonPropertyName("wind_mph")]
        public float WindMph { get; set; }
         [JsonPropertyName("wind_kph")]
        public float WindKph { get; set; }
          [JsonPropertyName("wind_degree")]
        public int WindDegree { get; set; }
          [JsonPropertyName("wind_dir")]
        public string WindDir { get; set; }
          [JsonPropertyName("pressure_mb")]
        public double PressureMb { get; set; }
         [JsonPropertyName("pressure_in")]
        public float PressureIn { get; set; }
          [JsonPropertyName("precip_mm")]
        public float PrecipMm { get; set; }  
          [JsonPropertyName("precip_in")]
        public float PrecipIn { get; set; } 
          [JsonPropertyName("humidity")]
        public int Humidity { get; set; }    
          [JsonPropertyName("cloud")]
        public int Cloud { get; set; }  
          [JsonPropertyName("feelslike_c")]
        public float FeelslikeC { get; set; }   
          [JsonPropertyName("feelslike_f")]
        public float FeelslikeF { get; set; }     
          [JsonPropertyName("vis_km")]
        public float VisKm { get; set; }    
          [JsonPropertyName("vis_miles")]
        public float Vis_Miles { get; set; }
          [JsonPropertyName("uv")]
        public float Uv { get; set; }   
          [JsonPropertyName("gust_mph")]
        public float Gust_Mph { get; set; } 
          [JsonPropertyName("gust_kph")]
        public float GustKph { get; set; }
    }

    public class Condition
    {
        [JsonPropertyName("text")]
        public string Text { get; set; }
         [JsonPropertyName("icon")]
        public string Icon { get; set; }   
         [JsonPropertyName("code")]
        public string Code { get; set; }
    }
}