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

namespace FlipWeen.Controllers
{
    //[Authorize]
    public class ProjectsController : BaseController
    {
        IDataRepository _repository;
        public ProjectsController(IDataRepository repository)
        {
            _repository = repository;
        }
        // GET api/values
        [HttpGet]
        [Route("api/projects/latest")]
        public IEnumerable<ProjectViewModel> GetLatestProjects()
        {
            var projects = _repository.GetAll<Project>()
                .Take(10)
                .ToList();
            var projectsVm = Mapper.Map<IEnumerable<Project>, IEnumerable<ProjectViewModel>>(projects);

            return projectsVm;
        }

        [HttpGet]
        [Route("api/projects/category")]
        public IEnumerable<ProjectViewModel> GetProjectsByCategory(int categoryId)
        {
            var projects = _repository.GetAll<Project>()
                .Where(x=>x.CategoryId==categoryId)
                .ToList();
            var projectsVm = Mapper.Map<IEnumerable<Project>, IEnumerable<ProjectViewModel>>(projects);

            return projectsVm;
        }

        [HttpGet]
        [Route("api/projects/search")]
        public IEnumerable<ProjectViewModel> SearchProjects(string projectName)
        {
            var projects = _repository.GetAll<Project>()
                .Where(x=>x.Name.Contains(projectName))
                .ToList();
            var projectsVm = Mapper.Map<IEnumerable<Project>, IEnumerable<ProjectViewModel>>(projects);

            return projectsVm;
        }
        

        [HttpGet]
        [Route("api/projects/categories")]
        public IEnumerable<ProjectCategoryViewModel> GetProjectCategories()
        {
            var projectCategories = _repository.GetAll<ProjectCategory>()
                .ToList();
            var projectCategoriesVm = Mapper.Map<IEnumerable<ProjectCategory>, IEnumerable<ProjectCategoryViewModel>>(projectCategories);

            return projectCategoriesVm;
        }

    }
}
