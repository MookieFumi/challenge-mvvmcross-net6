using System;
using Newtonsoft.Json;

namespace Challenge.Services.Marvel.Model
{
    public class Thumbnail
    {
        [JsonProperty("path")]
        public Uri Path { get; set; }

        [JsonProperty("extension")]
        public string Extension { get; set; }
    }
}