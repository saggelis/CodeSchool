using FlipWeen.MVC.Responses.Responses;

namespace FlipWeen.MVC.Client
{
    using System.Threading.Tasks;
    using Models;
    using Responses;

    public interface IDataClient
    {
        Task<ProjectCategoriesResponse> GetProjectCategories();
        Task<ProjectsResponse> GetLatestProjects();
        Task<ProjectsResponse> GetProjectsByCategory(int categoryId);
        Task<ProjectCategoryResponse> GetProjectCategory(int categoryId);
        Task<ProjectsResponse> SearchProjects(string projectName);
        Task<ProjectResponse> GetProject(int projectId);
        Task<ProjectsResponse> Createproject(ProjectCreationBindingModel model);
        Task<ProjectBackResponse> BackProject(ProjectBackBindingModel model);
        Task<ProjectPackagesResponse> GetPackages();
        Task<ProjectPackageResponse> GetPackage(int packageId);

        Task<VivaResponse> VivaCreateOrder(OrderOptions model);

        Task<VivaResponse> VerifyVivaTransaction(TransactionResult result);

    }
}