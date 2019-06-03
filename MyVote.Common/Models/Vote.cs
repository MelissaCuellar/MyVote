using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyVote.Common.Models
{
    public class Vote
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("user")]
        public User User { get; set; }
    }
}
