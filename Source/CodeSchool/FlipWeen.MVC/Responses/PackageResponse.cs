using FlipWeen.MVC.Api;
using FlipWeen.MVC.Models;
using System.Collections.Generic;

namespace FlipWeen.MVC.Responses.Responses
{
   
    public class ProjectPackagesResponse : ApiResponse<IEnumerable<PackageViewModel>>
    {
    }

    public class ProjectPackageResponse : ApiResponse<PackageViewModel>
    {
    }
}