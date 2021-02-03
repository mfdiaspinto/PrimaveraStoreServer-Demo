using System;
using System.Text.Json.Serialization;

namespace PrimaveraStoreServer.Resources
{
    public class CompanyResource
    {
        [JsonPropertyName("companyKey")]
        public string CompanyKey { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }
        [JsonPropertyName("searchTerm")]
        public string SearchTerm { get; set; }

        [JsonPropertyName("companyTaxID")]
        public string CompanyTaxID { get; set; }

        [JsonPropertyName("country")]
        public string Country { get; set; }

        [JsonPropertyName("naturalKey")]
        public string NaturalKey { get; set; }

        [JsonPropertyName("id")]
        public Guid Id { get; set; }
    }
}
