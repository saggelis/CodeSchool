using FlipWeen.Common.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Migrations;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using FlipWeen.Common;
using Microsoft.AspNet.Identity.EntityFramework;

namespace FlipWeen.Data
{
    public class DataContext : IdentityDbContext<ApplicationUser, Role, int, UserLogin, UserRole, UserClaim> , IDbContext
    {
        public DataContext() : base("DefaultConnection")
        {
            Database.SetInitializer(new CreateDatabaseIfNotExists<DataContext>());
        }

        //public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<Project> Projects { get; set; }

        public DbSet<ProjectCategory> ProjectCategories { get; set; }

        public DbSet<ProjectBacker> ProjectBackers { get; set; }

        public DbSet<ProjectPackage> ProjectPackages { get; set; }

        public DbSet<Package> Packages { get; set; }

        public DbSet<Reward> Rewards { get; set; }

        public DbSet<Transaction> Transactions { get; set; }


        //protected override void OnModelCreating(DbModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<Book>().Property(x => x.Price).HasPrecision(16, 3);
        //}

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
        }

        public static DataContext Create()
        {

            return new DataContext();

             

        }

    

        int IDbContext.SaveChanges()
        {
           return SaveChanges();
        }
        IDbSet<TEntity> IDbContext.Set<TEntity>()
        {
            return base.Set<TEntity>();
        }

        //IDbSet<TEntity> IDbContext.Set<TEntity>()
        //{
        //    throw new NotImplementedException();
        //}
    }

    public interface IDbContext : IDisposable
    {
        Database Database { get; }
        DbEntityEntry Entry(object entity);
        IDbSet<TEntity> Set<TEntity>() where TEntity : class;
        int SaveChanges();
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

    public class DatabaseConfiguration : DbMigrationsConfiguration<DataContext>
    {
        public DatabaseConfiguration()
        {
            this.AutomaticMigrationsEnabled = true;
        }
    }

}
