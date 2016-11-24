using FlipWeen.MVC.Api;
using FlipWeen.MVC.Responses.Responses;

namespace FlipWeen.MVC.Client
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
  
    using Models;
    using Responses;

    public class DataClient : ClientBase, IDataClient
    {
        private const string projectsUri = "api/projects/getprojects";
        private const string projectCategoriesUri = "api/projects/getcategories";

        public DataClient(IApiClient apiClient) : base(apiClient)
        {
        }

        public async Task<ProjectCategoryResponse> GetProjectCategories()
        {
            var response = await ApiClient.GetFormEncodedContent(projectCategoriesUri);
            return await CreateJsonResponse<ProjectCategoryResponse>(response);
        }

        public async Task<ProjectResponse> GetProjects(int? categoryId=null)
        {
          
            var response = await ApiClient.GetFormEncodedContent(projectsUri);
            return  await CreateJsonResponse<ProjectResponse>(response);
               

        }
    }
}