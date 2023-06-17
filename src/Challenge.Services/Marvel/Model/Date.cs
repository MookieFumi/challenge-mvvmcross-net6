using Newtonsoft.Json;

namespace Challenge.Services.Marvel.Model
{
    public class Date
    {
        //[JsonProperty("type")]
        //public DateType Type { get; set; }

        [JsonProperty("date")]
        public string DateDate { get; set; }
    }
}