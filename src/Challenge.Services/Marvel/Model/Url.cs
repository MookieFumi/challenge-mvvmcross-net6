using System;
using Newtonsoft.Json;

namespace Challenge.Services.Marvel.Model
{
    public class Url
    {
        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("url")]
        public Uri UrlUrl { get; set; }
    }
}