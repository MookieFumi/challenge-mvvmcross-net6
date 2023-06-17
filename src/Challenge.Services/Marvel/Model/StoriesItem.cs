using System;
using Newtonsoft.Json;

namespace Challenge.Services.Marvel.Model
{
    public class StoriesItem
    {
        [JsonProperty("resourceURI")]
        public Uri ResourceUri { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        //[JsonProperty("type")]
        //public ItemType Type { get; set; }
    }
}