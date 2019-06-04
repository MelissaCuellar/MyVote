using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MyVote.Web.Data.Entities
{
    public class Vote : IEntity
    {
        public int Id { get; set; }


        [Required]
        public User User { get; set; }
    }
}
