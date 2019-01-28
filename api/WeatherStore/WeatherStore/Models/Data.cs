using Newtonsoft.Json;

namespace WeatherStore.Models
{
    public class Data
    {
        [JsonProperty("mac")]
        public string PhysicalAddress { get; set; }

        [JsonProperty("temp")]
        public decimal Temperature { get; set; }

        [JsonProperty("hum")]
        public decimal Humidity { get; set; }

        [JsonProperty("pres")]
        public decimal Pressure { get; set; }

    }
}
