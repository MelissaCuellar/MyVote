using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MyVote.Common.Models
{
    public class VoteRequest
    {
        [Required]
        public string Email { get; set; }

        [Required]
        public int CandidateId { get; set; }
    }
}
