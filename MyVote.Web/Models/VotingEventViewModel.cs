

namespace MyVote.Web.Models
{
    using System.ComponentModel.DataAnnotations;
    using Data.Entities;
    using Microsoft.AspNetCore.Http;

    public class VotingEventViewModel : VotingEvent
    {
        [Display(Name = "Image")]
        public IFormFile ImageFile { get; set; }
    }
}
