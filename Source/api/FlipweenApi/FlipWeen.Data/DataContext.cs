using FlipWeen.Common.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using FlipWeen.Common;
using Microsoft.AspNet.Identity.EntityFramework;

namespace FlipWeen.Data
{
    public class DataContext : IdentityDbContext<ApplicationUser, Role, int, UserLogin, UserRole, UserClaim>
    {
        public DataContext() : base("DefaultConnection")
        {

        }

        //public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<Project> Projects { get; set; }
        //protected override void OnModelCreating(DbModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<Book>().Property(x => x.Price).HasPrecision(16, 3);
        //}
       
        public static DataContext Create()
        {

            return new DataContext();

             

        }
    }

    public class UserStore : UserStore<ApplicationUser, Role, int, UserLogin, UserRole, UserClaim>
    {
        public UserStore(DataContext context) : base(context)
        {
        }
    }

    public class RoleStore : RoleStore<Role, int, UserRole>
    {
        public RoleStore(DataContext context) : base(context)
        {
        }
    }

}
