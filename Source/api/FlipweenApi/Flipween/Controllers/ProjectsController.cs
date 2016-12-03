using AutoMapper;
using Flipween.Controllers;
using FlipWeen.Common.Data;
using FlipWeen.Common.Entities;
using System;
using System.Collections.Generic;
using System.Web.Http;
using FlipWeen.Common.ViewModels;

using System.Threading.Tasks;
using RestSharp;
using System.Net.Http;
using System.Net;
using RestSharp.Authenticators;

namespace FlipWeen.Controllers
{
    
    public class ProjectsController : BaseController
    {
        readonly IApiDataRepository _repository;
        public ProjectsController(IApiDataRepository repository)
        {
            _repository = repository;
        }
        // GET api/values
        [HttpGet]
        [Route("api/projects/latest")]
        public IEnumerable<ProjectViewModel> GetLatestProjects()
        {
            var projects = _repository.GetLatestProjects();
            var projectsVm = Mapper.Map<IEnumerable<Project>, IEnumerable<ProjectViewModel>>(projects);

            return projectsVm;
        }

        [HttpGet]
        [Route("api/projects/project")]
        public ProjectViewModel GetProject(int projectId)
        {
            var project = _repository.GetProject(projectId);
            var projectVm = Mapper.Map<Project, ProjectViewModel>(project);

            return projectVm;
        }

        [HttpGet]
        [Route("api/projects/bycategory")]
        public IEnumerable<ProjectViewModel> GetProjectsByCategory(int categoryId)
        {
            var projects = _repository.GetProjectsByCategory(categoryId);
            var projectsVm = Mapper.Map<IEnumerable<Project>, IEnumerable<ProjectViewModel>>(projects);

            return projectsVm;
        }

        [HttpGet]
        [Route("api/projects/category")]
        public ProjectCategoryViewModel GetProjectCategory(int categoryId)
        {
            var category = _repository.GetProjectCategory(categoryId);
            var categoryVm = Mapper.Map<ProjectCategory, ProjectCategoryViewModel>(category);

            return categoryVm;
        }

        [HttpGet]
        [Route("api/projects/search")]
        public IEnumerable<ProjectViewModel> SearchProjects(string projectName)
        {
            var projects = _repository.SearchProjects(projectName);
            var projectsVm = Mapper.Map<IEnumerable<Project>, IEnumerable<ProjectViewModel>>(projects);

            return projectsVm;
        }
        

        [HttpGet]
        [Route("api/projects/categories")]
        public IEnumerable<ProjectCategoryViewModel> GetProjectCategories()
        {
            var projectCategories = _repository.GetProjectCategories();
            var projectCategoriesVm = Mapper.Map<IEnumerable<ProjectCategory>, IEnumerable<ProjectCategoryViewModel>>(projectCategories);

            return projectCategoriesVm;
        }

        [HttpGet]
        [Route("api/projects/packages")]
        public IEnumerable<PackageViewModel> GetPackages()
        {
            var packages = _repository.GetPackages();
            var packagesVm = Mapper.Map<IEnumerable<Package>, IEnumerable<PackageViewModel>>(packages);

            return packagesVm;
        }

        [Authorize]
        [HttpPost]
        [Route("api/projects/createprojects")]
        public  HttpResponseMessage CreateProject(ProjectCreationBindingModel model)
        {
            var project = new Project() {
                Name = model.Name,
                Description = model.Description,
                CreationDate = DateTime.UtcNow,
                EndDate = model.EndDate,
                TargetAmount = model.TargetAmount,
                UserId = model.UserId,
                CategoryId = model.CategoryId,
                Image = model.Image,
                Video = model.Video,
                GlobalId=Guid.NewGuid().ToString(),

            };
            _repository.CreateProject(project);
          
            return Request.CreateResponse(HttpStatusCode.OK);
        }


        [Authorize]
        [HttpPost]
        [Route("api/projects/back")]
        public async Task<HttpResponseMessage> Back(ProjectBackBindingModel model)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest,ModelState);
            }

            var transaction = new Transaction()
            {
                Amount = model.Amount,
                CreationDate=DateTime.UtcNow,
                PackageId = model.PackageId,
                ProjectId = model.ProjectId,
                UserId = model.UserId,
                GlobalId = Guid.NewGuid().ToString(),
                VivaOrderId=model.VivaOrderId

            };
        
            _repository.CreateTransaction(transaction);

            return Request.CreateResponse(HttpStatusCode.OK);
        }

        [Authorize]
        [HttpPost]
        [Route("api/projects/verifyviva")]
        public async Task<HttpResponseMessage> VerifyVivaTransaction(TransactionResult transactionResult)
        {

            _repository.VerifyVivaTransaction(transactionResult.OrderId, transactionResult.TransactionId);
          
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        [Authorize]
        [HttpPost]
        [Route("api/projects/vivaorder")]
        public HttpResponseMessage VivaCreateOrder(OrderOptions options)
        {
            var cl = new RestClient("http://demo.vivapayments.com/");
      
            var req = new RestRequest("api/orders", Method.POST);
            options.SourceCode = "4308";
            req.AddObject(options);


            string vivaPaymentFormURL = "http://demo.vivapayments.com/web/newtransaction.aspx?ref=";
            //string vivaPaymentFormURL = "https://www.vivapayments.com/web/newtransaction.aspx?ref="; // production URL

            // class that contains the order options that will be sent
            var apiKey = "4661510a-989d-47fd-84d6-a4128b9a3544";
            var apiPassword = "V/dMT7";
            cl.Authenticator = new HttpBasicAuthenticator(apiKey, apiPassword);

            // Do the post 
            var res = cl.Execute<OrderResult>(req);

            res.Data.RedirectUrl = vivaPaymentFormURL + res.Data.OrderCode.ToString();
            // once the order code is successfully created, redirect to payment form to complete the payment
            if (res.Data.ErrorCode == 0)
            {
                return Request.CreateResponse(HttpStatusCode.OK, res.Data);
                //Response.Redirect(this.vivaPaymentFormURL + res.Data.OrderCode.ToString());
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "The Order  failed");
            }
        }

    }
}
