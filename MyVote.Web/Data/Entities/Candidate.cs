
namespace MyVote.Web.Data.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    public class Candidate: IEntity
    {
        public int Id { get; set; }

        [MaxLength(50, ErrorMessage = "The field {0} only can contain {1} characters length.")]
        [Required]
        public string Name { get; set; }

        [MaxLength(150, ErrorMessage = "The field {0} only can contain {1} characters length.")]
        [Required]
        public string Proposal { get; set; }

        public ICollection<Vote> Votes { get; set; }

        [Display(Name = "#Votes")]
        public int NumVotes { get { return this.Votes == null ? 0 : this.Votes.Count; } }

        [Display(Name = "Image")]
        public string ImageUrl { get; set; }

        public string ImageFullPath {
            get
            {
                if(string.IsNullOrEmpty(ImageUrl))
                {
                    return null;
                }
                return $"https://myvotesystem.azurewebsites.net{this.ImageUrl.Substring(1)}";
            }
        }

    }
}
