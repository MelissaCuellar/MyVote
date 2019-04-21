
namespace MyVote.Common.Models
{
    using System;
    using Newtonsoft.Json;

    public partial class Candidate
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("proposal")]
        public string Proposal { get; set; }

        [JsonProperty("numVotos")]
        public long NumVotos { get; set; }

        [JsonProperty("imageUrl")]
        public string ImageUrl { get; set; }

        [JsonProperty("imageFullPath")]
        public Uri ImageFullPath { get; set; }
    }
}
