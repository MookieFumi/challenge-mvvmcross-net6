using Newtonsoft.Json;

namespace Challenge.Services.Marvel.Model
{
    public class MarvelResult<T> where T : class, new()
    {
        public MarvelResult()
        {
            Data = new Data<T>();
        }

        [JsonProperty("code")]
        public long Code { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("copyright")]
        public string Copyright { get; set; }

        [JsonProperty("attributionText")]
        public string AttributionText { get; set; }

        [JsonProperty("attributionHTML")]
        public string AttributionHtml { get; set; }

        [JsonProperty("etag")]
        public string Etag { get; set; }

        [JsonProperty("data")]
        public Data<T> Data { get; set; }
    }
}
