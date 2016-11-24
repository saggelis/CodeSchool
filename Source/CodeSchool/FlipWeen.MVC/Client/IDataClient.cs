using FlipWeen.MVC.Responses.Responses;

namespace FlipWeen.MVC.Client
{
    using System.Threading.Tasks;
    using Models;
    using Responses;

    public interface IDataClient
    {
        Task<ProjectCategoryResponse> GetProjectCategories();
        Task<ProjectResponse> GetProjects(int? categoryId = null);
    }
}