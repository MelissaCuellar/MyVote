
namespace MyVote.Web.Data.Entities
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class VotingEvent: IEntity
    {
        public int Id { get; set; }

        [MaxLength(50, ErrorMessage = "The field {0} only can contain {1} characters length.")]
        [Required]
        public string Name { get; set; }

        [MaxLength(100, ErrorMessage = "The field {0} only can contain {1} characters length.")]
        [Required]
        public string Description { get; set; }

        [Display(Name = "Start Date")]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime StartDate { get; set; }

        [Display(Name = "End Date")]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime EndDate { get; set; }

        public User User { get; set; }
    }
}
