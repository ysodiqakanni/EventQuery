using System;
using Newtonsoft.Json;

namespace EventQuery.Models.Eventbrite
{
    public class Address
    {
        [JsonProperty(PropertyName = "address_1")]
        public String address1 { get; set; }
        [JsonProperty(PropertyName = "address_2")]
        public String address2 { get; set; }

        public String city { get; set; }
        public String region { get; set; }
        public String country { get; set; }
        [JsonProperty(PropertyName = "postal_code")]
        public String postalCode { get; set; }

    }
}