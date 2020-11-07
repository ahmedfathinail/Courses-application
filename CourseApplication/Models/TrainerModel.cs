using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CourseApplication.Models
{
    public class TrainerModel
    {

        public int ID { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        [EmailAddress(ErrorMessage ="Please, Write Email in Correct Format")]
        public string E_mail { get; set; }
        [Url(ErrorMessage ="Please, Enter Correct URL")]
        public string Website { get; set; }
        public System.DateTime Ceartion_date { get; set; }
        [StringLength(250,MinimumLength =10)]
        public string Description { get; set; }
    }
}