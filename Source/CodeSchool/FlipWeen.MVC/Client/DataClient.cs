using FlipWeen.MVC.Api;
using FlipWeen.MVC.Responses.Responses;

namespace FlipWeen.MVC.Client
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Models;
    using Responses;
    using System.Dynamic;
    using System;
    public class DataClient : ClientBase, IDataClient
    {
        private const string projectsLatestUri = "api/projects/latest";
        private const string projectsByCategoryUri = "api/projects/category";
        private const string projectsSearchUri = "api/projects/search";
        private const string categoriesUri = "api/projects/categories";
        private const string projectCreateUri = "api/projects/createprojects";
        private const string projectByIdUri = "api/projects/project";

        public DataClient(IApiClient apiClient) : base(apiClient)
        {
        }

        public async Task<ProjectCategoryResponse> GetProjectCategories()
        {
          return await this.GetJsonDecodedContent<ProjectCategoryResponse, IEnumerable<ProjectCategoryViewModel>>(categoriesUri);
          
        }
        
        public async Task<ProjectsResponse> GetLatestProjects()
        {
            var response = await ApiClient.GetFormEncodedContent(projectsLatestUri);
            return  await CreateJsonResponse<ProjectsResponse>(response);
        }

        public async Task<ProjectsResponse> GetProjectsByCategory(int categoryId)
        {
            var response = await ApiClient.GetFormEncodedContent(projectsByCategoryUri, "categoryId".AsPair(categoryId.ToString()));
            return await CreateJsonResponse<ProjectsResponse>(response);
        }

        public async Task<ProjectsResponse> SearchProjects(string projectName)
        {
            var response = await ApiClient.GetFormEncodedContent(projectsSearchUri, "projectName".AsPair(projectName));
            return await CreateJsonResponse<ProjectsResponse>(response);
        }

        public async Task<ProjectResponse> GetProject(int projectId)
        {
            return await this.GetJsonDecodedContent<ProjectResponse,ProjectViewModel>(projectByIdUri, "projectId".AsPair(projectId.ToString()));
           // return await CreateJsonResponse<ProjectResponse>(response);
        }

        public async Task<ProjectsResponse> Createproject(ProjectCreationBindingModel model)
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

    }
}