using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using FlipWeen.Common.Entities;

namespace FlipWeen.Common.ViewModels
{
    public class ProjectBackBindingModel
    {
        [Required]
        [Display(Name = "Creation Date")]
        public  DateTime CreationDate { get; set; }
        
        [Required]
        [Display(Name = "Amount Needed")]
        public  double Amount { get; set; }

        [Required]
        public  int UserId { get; set; }

        [Required]
        public  int ProjectId { get; set; }

        [Required]
        public int PackageId { get; set; }

    }
}
