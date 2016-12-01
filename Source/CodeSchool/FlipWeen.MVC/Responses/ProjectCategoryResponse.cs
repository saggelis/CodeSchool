using FlipWeen.MVC.Api;
using FlipWeen.MVC.Models;
using System.Collections.Generic;

namespace FlipWeen.MVC.Responses.Responses
{
   
    public class ProjectCategoriesResponse : ApiResponse<IEnumerable<ProjectCategoryViewModel>>
    {
    }

    public class ProjectCategoryResponse : ApiResponse<ProjectCategoryViewModel>
    {
    }
}