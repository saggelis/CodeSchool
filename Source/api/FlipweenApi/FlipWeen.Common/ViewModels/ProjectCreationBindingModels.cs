using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace FlipWeen.Common.ViewModels
{
    public class ProjectCreationBindingModel
    {
        [Required, MaxLength(100)]
        [Display(Name = "Project Name")]
        public  string Name { get; set; }

        [MaxLength(4000)]
        [Display(Name = "Project Description")]
        public  string Description { get; set; }

        [Required]
        [Display(Name = "Creation Date")]
        public  DateTime CreationDate { get; set; }

        [Required]
        [Display(Name = "End Date")]
        public  DateTime EndDate { get; set; }

        [Required]
        [Display(Name = "Amount Needed")]
        public  double TargetAmount { get; set; }

        [Required]
        public  int UserId { get; set; }

        [Required]
        public  int CategoryId { get; set; }

        [MaxLength(500)]
        [Display(Name = "Please Insert Image Url")]
        public  string Image { get; set; }

        [MaxLength(500)]
        [Display(Name = "Please Insert Video Url")]
        public  string Video { get; set; }

    }
}
