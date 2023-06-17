using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Challenge.Services.Marvel.Model
{
    public class Stories
    {
        [JsonProperty("available")]
        public long Available { get; set; }

        [JsonProperty("collectionURI")]
        public Uri CollectionUri { get; set; }

        [JsonProperty("items")]
        public List<StoriesItem> Items { get; set; }

        [JsonProperty("returned")]
        public long Returned { get; set; }
    }
}