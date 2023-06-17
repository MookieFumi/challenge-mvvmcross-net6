using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Challenge.Services.Marvel.Model
{
    public class Characters
    {
        [JsonProperty("available")]
        public long Available { get; set; }

        [JsonProperty("collectionURI")]
        public Uri CollectionUri { get; set; }

        [JsonProperty("items")]
        public List<Series> Items { get; set; }

        [JsonProperty("returned")]
        public long Returned { get; set; }
    }
}