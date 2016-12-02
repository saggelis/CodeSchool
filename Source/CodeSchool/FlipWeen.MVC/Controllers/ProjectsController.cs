using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FlipWeen.MVC.Api;
using FlipWeen.MVC.Client;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using FlipWeen.MVC.Models;
using System.Threading.Tasks;

namespace CodeSchool.Controllers
{

    public class ProjectsController : Controller
    {

        private readonly IDataClient _dataClient;
        private readonly ITokenContainer _tokenContainer;

        public ProjectsController()
        {
            _tokenContainer = new TokenContainer();
            var apiClient = new ApiClient(HttpClientInstance.Instance, _tokenContainer);
            _dataClient = new DataClient(apiClient);
        }
        public ProjectsController(IDataClient loginClient, ITokenContainer tokenContainer)
        {
            this._dataClient = loginClient;
            this._tokenContainer = tokenContainer;
        }

       
        [HttpGet]
        public async Task<ActionResult> ProjectPage(int projectId)
        {
            var response = await _dataClient.GetProject(projectId);
          
            return View(response.Data);
        }

        
        [HttpGet]
        public async Task<ActionResult> Explore(int categoryId)
        {
            var response = await _dataClient.GetProjectCategory(categoryId);
            var allCategories = await _dataClient.GetProjectCategories();
            var model = new ProjectExploreViewModel
            {
                ProjectCategory = response.Data,
                AllCategories = allCategories.Data
            };
            return View(model);
        }

        [HttpPost]            
        public async Task<ActionResult> Search(string projectName)
        {
            var response = await _dataClient.SearchProjects(projectName);
            var allCategories = await _dataClient.GetProjectCategories();
            var model = new ProjectSearchViewModel
            {
                SearchResultCount = response.Data !=null ? response.Data.Count() :0 ,
                SearchResults = response.Data != null ? response.Data : new List<ProjectViewModel>(),
                AllCategories = allCategories.Data != null ? allCategories.Data : new List<ProjectCategoryViewModel>(),
            };
            return View(model);
        }

        [Authorize]
        [HttpGet]
        public async Task<ActionResult> ProjectCreation()
        {
            var response = await _dataClient.GetProjectCategories();
            var model = new ProjectCreationBindingModel();
            if (response.StatusIsSuccessful)
            {
                if (response.Data != null)
                    model.Categories = response.Data.ToList().Select(x => new SelectListItem
                    {
                        Value = x.Id.ToString(),
                        Text = x.Name
                    });
            }
            if(response.ResponseCode==System.Net.HttpStatusCode.Unauthorized)
            {
                return RedirectToAction("Login", "Account");
            }

            return View(model);
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult> ProjectCreation(ProjectCreationBindingModel model)
        {
            var response = await _dataClient.GetProjectCategories();
            if (response.StatusIsSuccessful)
            {
                if (response.Data != null)
                    model.Categories = response.Data.ToList().Select(x => new SelectListItem
                    {
                        Value = x.Id.ToString(),
                        Text = x.Name
                    });
            }
            if (!ModelState.IsValid)
            {

            }
            model.UserId = _tokenContainer.UserId.Value;
            var responseCreate = await _dataClient.Createproject(model);
            //return View(model);
            if (responseCreate.StatusIsSuccessful)
            {
                TempData["Success"] = "Added Successfully!";
            }
            else
                TempData["Error"] = "Did not add project";

            return RedirectToAction("ProjectCreation", "Projects");
        }

    }

}