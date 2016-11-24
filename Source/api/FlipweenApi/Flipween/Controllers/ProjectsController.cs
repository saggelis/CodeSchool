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
        [Route("api/projects/getprojects")]
        public IEnumerable<ProjectViewModel> GetProjects()
        {
            var projects = _repository.GetAll<Project>().Take(10).ToList();
            var projectsVm = Mapper.Map<IEnumerable<Project>, IEnumerable<ProjectViewModel>>(projects);

            return projectsVm;
        }

        // GET api/values
        [HttpGet]
        [Route("api/projects/categories")]
        public IEnumerable<ProjectCategoryViewModel> GetProjectCategories()
        {
            var projectCategories = _repository.GetAll<ProjectCategory>().Take(10).ToList();
            var projectCategoriesVm = Mapper.Map<IEnumerable<ProjectCategory>, IEnumerable<ProjectCategoryViewModel>>(projectCategories);

            return projectCategoriesVm;
        }

        //// GET api/values/5
        //public string Get(int id)
        //{
        //    return "value";
        //}

        //// POST api/values
        //public void Post([FromBody]string value)
        //{
        //}

        //// PUT api/values/5
        //public void Put(int id, [FromBody]string value)
        //{
        //}

        //// DELETE api/values/5
        //public void Delete(int id)
        //{
        //}
    }
}
