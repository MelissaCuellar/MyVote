using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MyVote.Web.Models
{
    public class CandidateViewModel
    {
        public int VotingEventId { get; set; }

        public int CandidateId { get; set; }

        [MaxLength(50, ErrorMessage = "The field {0} only can contain {1} characters length.")]
        [Required]
        public string Name { get; set; }

        [MaxLength(150, ErrorMessage = "The field {0} only can contain {1} characters length.")]
        [Required]
        public string Proposal { get; set; }

        [Display(Name = "Image")]
        public string ImageUrl { get; set; }
    }
}
