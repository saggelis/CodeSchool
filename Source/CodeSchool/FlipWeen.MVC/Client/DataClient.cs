using FlipWeen.MVC.Api;
using FlipWeen.MVC.Responses.Responses;

namespace FlipWeen.MVC.Client
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Models;
    using System;
    public class DataClient : ClientBase, IDataClient
    {
        private const string projectsLatestUri = "api/projects/latest";
        private const string projectsByCategoryUri = "api/projects/bycategory";
        private const string projectsSearchUri = "api/projects/search";
        private const string categoriesUri = "api/projects/categories";
        private const string categoryUri = "api/projects/category";
        private const string projectCreateUri = "api/projects/createprojects";
        private const string projectByIdUri = "api/projects/project";
        private const string projectbackUri = "api/projects/back";
        private const string packagesUri = "api/projects/packages";
        private const string packageUri = "api/projects/package";

        public DataClient(IApiClient apiClient) : base(apiClient)
        {
        }

        async Task<ProjectCategoriesResponse> IDataClient.GetProjectCategories()
        {
          return await this.GetJsonDecodedContent<ProjectCategoriesResponse, IEnumerable<ProjectCategoryViewModel>>(categoriesUri);
          
        }

        async Task<ProjectPackagesResponse> IDataClient.GetPackages()
        {
            return await this.GetJsonDecodedContent<ProjectPackagesResponse, IEnumerable<PackageViewModel>>(packagesUri);

        }

        async Task<ProjectPackageResponse> IDataClient.GetPackage( int packageId)
        {
            return await this.GetJsonDecodedContent<ProjectPackageResponse, PackageViewModel>(packageUri);

        }

        async Task<ProjectCategoryResponse> IDataClient.GetProjectCategory(int categoryId)
        {
            return await this.GetJsonDecodedContent<ProjectCategoryResponse, ProjectCategoryViewModel>(categoryUri, "categoryId".AsPair(categoryId.ToString()));

        }

        async Task<ProjectsResponse> IDataClient.GetLatestProjects()
        {
            var response = await ApiClient.GetFormEncodedContent(projectsLatestUri);
            return  await CreateJsonResponse<ProjectsResponse>(response);
        }

        async Task<ProjectsResponse> IDataClient.GetProjectsByCategory(int categoryId)
        {
            var response = await ApiClient.GetFormEncodedContent(projectsByCategoryUri, "categoryId".AsPair(categoryId.ToString()));
            return await CreateJsonResponse<ProjectsResponse>(response);
        }

        async Task<ProjectsResponse> IDataClient.SearchProjects(string projectName)
        {
            return await this.GetJsonDecodedContent<ProjectsResponse, IEnumerable<ProjectViewModel>>(projectsSearchUri, "projectName".AsPair(projectName));
          
        }

        async Task<ProjectResponse> IDataClient.GetProject(int projectId)
        {
            return await this.GetJsonDecodedContent<ProjectResponse,ProjectViewModel>(projectByIdUri, "projectId".AsPair(projectId.ToString()));
        }

        async Task<ProjectsResponse> IDataClient.Createproject(ProjectCreationBindingModel model)
        {

            var projectModel = new ProjectCreationBindingModel
            {
                Name = model.Name,
                Image = model.Image,
                CategoryId = model.CategoryId,
                CreationDate = DateTime.Now,
                EndDate = model.EndDate,
                TargetAmount = model.TargetAmount,
                Description = model.Description,
                Video = model.Video,
                UserId = model.UserId
                
            };
            var response = await ApiClient.PostJsonEncodedContent(projectCreateUri, projectModel);
            var createprojectResponse = await CreateJsonResponse<ProjectsResponse>(response);
            return createprojectResponse;

        }

        async Task<ProjectBackResponse> IDataClient.BackProject(ProjectBackBindingModel model)
        {

            var backingModel = new ProjectBackBindingModel
            {
                Amount = model.Amount,
                CreationDate=model.CreationDate,
                PackageId = model.PackageId,
                ProjectId = model.ProjectId,
                UserId = model.UserId
            };

            var response = await ApiClient.PostJsonEncodedContent(projectbackUri, backingModel);
            return await CreateJsonResponse<ProjectBackResponse>(response);
           
        }

    }
}