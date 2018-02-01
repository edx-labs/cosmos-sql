using Microsoft.Azure.Documents.Spatial;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace RealEstateCatalog.Models
{
    public class Home
    {
        [JsonProperty("id")]
        public string Id { get; set; }
        public string Address { get; set; }
        public int BedroomCount { get; set; }
        public int BathroomCount { get; set; }
        public string Description { get; set; }
        public int SquareFeet { get; set; }
        public int DaysOnMarket { get; set; }
        public Status Status { get; set; }
        public string Realtor { get; set; }
        public decimal Price { get; set; }
        public Uri Thumbnail { get; set; }
        public Point Location { get; set; }
        public List<string> Tags { get; set; }
    }
}