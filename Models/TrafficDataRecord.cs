using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TrafficUserSite.Models
{
    class TrafficDataRecord
    {
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }
        [JsonProperty(PropertyName = "speed")]
        public double Speed { get; set; }
        [JsonProperty(PropertyName = "travelTime")]
        public double TravelTime { get; set; }
        [JsonProperty(PropertyName = "status")]
        public string Status { get; set; }
        [JsonProperty(PropertyName = "dateAsOf")]
        public DateTime DataAsOf { get; set; }
        [JsonProperty(PropertyName = "linkId")]
        public string LinkId { get; set; }
        [JsonProperty(PropertyName = "linkPoints")]
        public string LinkPoints { get; set; }
        [JsonProperty(PropertyName = "encodedPolyLine")]
        public string EncodedPolyLine { get; set; }
        [JsonProperty(PropertyName = "encodedPolyLineLvls")]
        public string EncodedPolyLineLvls { get; set; }
        [JsonProperty(PropertyName = "owner")]
        public string Owner { get; set; }
        [JsonProperty(PropertyName = "transcomId")]
        public string TranscomID { get; set; }
        [JsonProperty(PropertyName = "borough")]
        public string Borough { get; set; }
        [JsonProperty(PropertyName = "linkName")]
        public string LinkName { get; set; }
        [JsonProperty(PropertyName = "processed")]
        public DateTime Processed { get; set; }
    }
}