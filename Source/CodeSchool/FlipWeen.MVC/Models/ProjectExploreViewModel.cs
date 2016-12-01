using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlipWeen.MVC.Models
{
    public class ProjectExploreViewModel
    {
       public ProjectCategoryViewModel ProjectCategory { get; set; }

       public IEnumerable<ProjectCategoryViewModel> AllCategories { get; set; }

    }
}
