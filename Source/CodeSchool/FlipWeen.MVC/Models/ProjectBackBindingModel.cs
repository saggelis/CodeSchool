using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using FlipWeen.MVC.Api;
using System.Web.Mvc;

namespace FlipWeen.MVC.Models
{
    public class ProjectBackBindingModel : ApiModel
    {
        [Required]
        [Display(Name = "Creation Date")]
        public DateTime CreationDate { get; set; }

        [Required]
        [Display(Name = "Amount Needed")]
        public double Amount { get; set; }

        [Required]
        public int UserId { get; set; }

        //[Required]
        public int ProjectId { get; set; }

      
        public int? PackageId { get; set; }

       
        public ProjectViewModel Project { get; set; }

       
        public PackageViewModel Package { get; set; }

    }
}
