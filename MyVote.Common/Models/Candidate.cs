
namespace MyVote.Common.Models
{
    using System;
    using System.Collections.Generic;
    using Newtonsoft.Json;

    public partial class Candidate
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("proposal")]
        public string Proposal { get; set; }

        [JsonProperty("votes")]
        public List<Vote> Votes { get; set; }

        [JsonProperty("numVotes")]
        public int NumVotes { get; set; }

        [JsonProperty("imageUrl")]
        public string ImageUrl { get; set; }

        [JsonProperty("imageFullPath")]
        public Uri ImageFullPath { get; set; }
    }
}
