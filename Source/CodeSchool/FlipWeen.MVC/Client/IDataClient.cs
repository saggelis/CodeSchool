using FlipWeen.MVC.Responses.Responses;

namespace FlipWeen.MVC.Client
{
    using System.Threading.Tasks;
    using Models;
    using Responses;

    public interface IDataClient
    {
        Task<ProjectCategoryResponse> GetProjectCategories();
        Task<ProjectsResponse> GetLatestProjects();
        Task<ProjectsResponse> GetProjectsByCategory(int categoryId);
        Task<ProjectsResponse> SearchProjects(string projectName);
        Task<ProjectsResponse> Createproject(ProjectCreationBindingModel model);

    }
}