using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.Linq;

namespace Challenge.Services.Marvel.Model
{
    public class Creators
    {
        public Creators()
        {
            Items = new List<CreatorsItem>();
        }

        [JsonProperty("available")]
        public long Available { get; set; }

        [JsonProperty("collectionURI")]
        public Uri CollectionUri { get; set; }

        [JsonProperty("items")]
        public List<CreatorsItem> Items { get; set; }

        [JsonProperty("returned")]
        public long Returned { get; set; }

        public string Summary { get { return Items.Any()?$"{Items.First().Name} - {Items.First().Role}":""; } }                
    }
}
