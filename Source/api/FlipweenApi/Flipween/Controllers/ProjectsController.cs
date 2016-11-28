using AutoMapper;
using Flipween.Controllers;
using FlipWeen.Common.Data;
using FlipWeen.Common.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using FlipWeen.Common.ViewModels;
using FlipWeen.Data;
using FlipWeen.Models;
using FlipWeen.Providers;
using FlipWeen.Results;
using System.Threading.Tasks;

namespace FlipWeen.Controllers
{
    [Authorize]
    public class ProjectsController : BaseController
    {
        IApiDataRepository _repository;
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
        [Route("api/projects/category")]
        public IEnumerable<ProjectViewModel> GetProjectsByCategory(int categoryId)
        {
            var projects = _repository.GetProjectsByCategory(categoryId);
            var projectsVm = Mapper.Map<IEnumerable<Project>, IEnumerable<ProjectViewModel>>(projects);

            return projectsVm;
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

    }
}
