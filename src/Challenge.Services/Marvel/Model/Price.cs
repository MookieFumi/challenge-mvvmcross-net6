using Newtonsoft.Json;

namespace Challenge.Services.Marvel.Model
{
    public class Price
    {
        //[JsonProperty("type")]
        //public PriceType Type { get; set; }

        [JsonProperty("price")]
        public double PricePrice { get; set; }
    }
}