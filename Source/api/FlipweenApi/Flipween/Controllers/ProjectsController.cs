using AutoMapper;
using Flipween.Controllers;
using FlipWeen.Common.Data;
using FlipWeen.Common.Entities;
using System;
using System.Collections.Generic;
using System.Web.Http;
using FlipWeen.Common.ViewModels;

using System.Threading.Tasks;

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

        [Authorize]
        [HttpPost]
        [Route("api/projects/createprojects")]
        public  IHttpActionResult CreateProject(ProjectCreationBindingModel model)
        {
            var project = new Project() {
                Name = model.Name,
                Description = model.Description,
                CreationDate = model.CreationDate,
                EndDate = model.EndDate,
                TargetAmount = model.TargetAmount,
                UserId = model.UserId,
                CategoryId = model.CategoryId,
                Image = model.Image,
                Video = model.Video,
                GlobalId=Guid.NewGuid().ToString(),

            };
            _repository.CreateProject(project);
          
            return Ok();
        }


        [Authorize]
        [HttpPost]
        [Route("api/projects/back")]
        public async Task<IHttpActionResult> Back(ProjectBackBindingModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var transaction = new Transaction()
            {
                Amount = model.Amount,
                CreationDate=model.CreationDate,
                PackageId = model.PackageId,
                ProjectId = model.ProjectId,
                UserId = model.UserId,
                GlobalId = Guid.NewGuid().ToString(),

            };
            _repository.CreateTransaction(transaction);

            return Ok();
        }

    }
}
