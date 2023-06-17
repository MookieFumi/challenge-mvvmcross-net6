using System;
using Newtonsoft.Json;

namespace Challenge.Services.Marvel.Model
{
    public class Series
    {
        [JsonProperty("resourceURI")]
        public Uri ResourceUri { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }
    }
}