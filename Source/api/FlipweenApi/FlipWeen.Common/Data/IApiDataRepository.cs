using FlipWeen.Common.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace FlipWeen.Common.Data
{
    public interface IApiDataRepository :IDataRepository
    {
        Project GetProject(int projectId);
        IEnumerable<Project> GetLatestProjects();
        IEnumerable<Project> GetProjectsByCategory(int categoryId);
        ProjectCategory GetProjectCategory(int categoryId);
        IEnumerable<Project> SearchProjects(string projectName);
        IEnumerable<ProjectCategory> GetProjectCategories();
        void CreateProject(Project project);
    }
}
