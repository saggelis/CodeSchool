using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using FlipWeen.Common.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Security.Claims;
using System.Transactions;
using FlipWeen.Common;
using FlipWeen.Common.Entities;

namespace FlipWeen.Data
{
    public class ApiDataRepository : BaseDataRepository,IApiDataRepository
    {
     
        public ApiDataRepository(IDbContext context):base(context)
        {
           
        }

        Project IApiDataRepository.GetProject(int projectId)
        {
            return  this.GetQuery<Project>()
                .Where(x => x.Id == projectId)
                .FirstOrDefault();
        }

        IEnumerable<Project> IApiDataRepository.GetLatestProjects()
        {
            return this.GetQuery<Project>()
                .Take(10)
                .ToList();
        }
      
        IEnumerable<Project> IApiDataRepository.GetProjectsByCategory(int categoryId)
        {
            return this.GetQuery<Project>()
                .Where(x => x.CategoryId == categoryId)
                .ToList();
        }

        ProjectCategory IApiDataRepository.GetProjectCategory(int categoryId)
        {
            return this.GetQuery<ProjectCategory>()
                .Include(x => x.Projects)
                .FirstOrDefault(x => x.Id == categoryId);
        }

        IEnumerable<Project> IApiDataRepository.SearchProjects(string projectName)
        {
            return this.GetQuery<Project>()
                .Where(x => x.Name.Contains(projectName))
                .ToList();
        }
        
        IEnumerable<ProjectCategory> IApiDataRepository.GetProjectCategories()
        {
           return this.GetQuery<ProjectCategory>()
                .ToList();
        }
        
        void  IApiDataRepository.CreateProject(Project project)
        {
            this.Add(project);
            this.SaveChanges();
        }

    }
}
