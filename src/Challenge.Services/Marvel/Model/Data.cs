using System.Collections.Generic;
using Newtonsoft.Json;

namespace Challenge.Services.Marvel.Model
{
    public class Data<T> where T : class
    {
        public Data()
        {
            Results = new List<T>();
        }

        [JsonProperty("offset")]
        public long Offset { get; set; }

        [JsonProperty("limit")]
        public long Limit { get; set; }

        [JsonProperty("total")]
        public long Total { get; set; }

        [JsonProperty("count")]
        public long Count { get; set; }

        [JsonProperty("results")]
        public List<T> Results { get; set; }
    }
}