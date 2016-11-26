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
        public async Task<ActionResult> ProjectCreation()
        {
            var response = await _dataClient.GetProjectCategories();

            var model = new ProjectCreationBindingModel();

            model.Categories = response.Data.ToList().Select(x => new SelectListItem
            {
                Value = x.Id.ToString(),
                Text = x.Name
            });
       
            return View(model);
        }

        [HttpPost]
        public async Task<ActionResult> ProjectCreation(ProjectCreationBindingModel model)
        {
            var response = await _dataClient.GetProjectCategories();
            model.Categories = response.Data.ToList().Select(x => new SelectListItem
            {
                Value = x.Id.ToString(),
                Text = x.Name
            });

            if (!ModelState.IsValid)
            {
               
            }
            model.UserId = _tokenContainer.UserId.Value;
            var responseCreate = await _dataClient.Createproject(model);
            return View(model);
        }
    }

}