using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlipWeen.MVC.Models
{
    public class ProjectSearchViewModel
    {
       public int SearchResultCount { get; set; }

       public IEnumerable<ProjectViewModel> SearchResults { get; set; }

       public IEnumerable<ProjectCategoryViewModel> AllCategories { get; set; }

    }
}
