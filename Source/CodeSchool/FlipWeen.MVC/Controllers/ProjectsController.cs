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
        public async Task<ActionResult> BackSuccess(long s, string t, string Lang)
        {


            var response = await _dataClient.VerifyVivaTransaction(new TransactionResult {

                OrderId=s,
                TransactionId=t

            });
            TempData["Success"] = "Backed Project Successfully!";
           
           
          return View();
        }

        [HttpGet]
        public async Task<ActionResult> BackFail()
        {

            TempData["Success"] = "Backed Project Successfully!";
            return View();
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
                SearchResultCount = response.Data != null ? response.Data.Count() : 0,
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
            if (response.ResponseCode == System.Net.HttpStatusCode.Unauthorized)
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

        [Authorize]
        [HttpGet]
        public async Task<ActionResult> Back(int projectId , int? packageId)
        {
        
            var response = await _dataClient.GetProject(projectId);
               
            var model = new ProjectBackBindingModel();
            if (response.StatusIsSuccessful)
            {
                if(response.Data!=null)
                {
                    model.Project = response.Data;
                    model.ProjectId = response.Data.Id;
                }
               
                if (packageId.HasValue)
                {
                    var packageResponse = await _dataClient.GetPackage(packageId.Value);
                    if (packageResponse.Data != null)
                    {
                        model.Package = packageResponse.Data;
                        model.PackageId = packageResponse.Data.Id;
                    }
                     

                }
            }
                
            if (response.ResponseCode == System.Net.HttpStatusCode.Unauthorized)
            {
                return RedirectToAction("Login", "Account");
            }

            return View(model);
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult> Back(ProjectBackBindingModel model)
        {

            var response = await _dataClient.GetProject(model.ProjectId);
            
            if (response.StatusIsSuccessful)
            {
                model.Project = response.Data;
                if (model.PackageId.HasValue)
                {
                    var packageResponse = await _dataClient.GetPackage(model.PackageId.Value);
                    if (packageResponse.Data != null)
                        model.Package = packageResponse.Data;

                }
            }
            if (!ModelState.IsValid)
            {

            }
            model.UserId = _tokenContainer.UserId.Value;
          
            var request = Request;
            var urlBase = string.Format("{0}://{1}", request.Url.Scheme, request.Url.Authority);
            var vivaOrderResponse = await _dataClient.VivaCreateOrder(new OrderOptions
            {
                Amount = Convert.ToInt64(model.Amount * 100)
               // ReturnUrl= urlBase + "/BackResults"


            });

            if(vivaOrderResponse.StatusIsSuccessful)
            {
                model.VivaOrderId = vivaOrderResponse.Data.OrderCode;
                var responseCreate = await _dataClient.BackProject(model);
                return new RedirectResult(vivaOrderResponse.Data.RedirectUrl,true);
            
            }
          
            //if (responseCreate.StatusIsSuccessful)
            //{
            //    TempData["Success"] = "Backed Project Successfully!";
            //}
            //else
            //    TempData["Error"] = "Did not back project";

            return RedirectToAction("Back", "Projects",new { projectId= model.ProjectId });
        }


    }


}

