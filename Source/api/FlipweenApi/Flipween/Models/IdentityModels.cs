﻿using System;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;

namespace FlipWeen.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    //public class UserRole : IdentityUserRole<int>
    //{
    //}

    //public class UserClaim : IdentityUserClaim<int>
    //{
    //}

    //public class UserLogin : IdentityUserLogin<int>
    //{
    //}

    //public class Role : IdentityRole<int, UserRole>
    //{
    //    public Role() { }
    //    public Role(string name) { Name = name; }
    //}

    //public class UserStore : UserStore<ApplicationUser, Role, int, UserLogin, UserRole, UserClaim>
    //{
    //    public UserStore(ApplicationDbContext context) : base(context)
    //    {
    //    }
    //}

    //public class RoleStore : RoleStore<Role, int, UserRole>
    //{
    //    public RoleStore(ApplicationDbContext context) : base(context)
    //    {
    //    }
    //}

    //// You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    //public class ApplicationUser : IdentityUser<int, UserLogin, UserRole, UserClaim>
    //{
    //    public DateTime? ActiveUntil;

    //    public async Task<ClaimsIdentity> GenerateUserIdentityAsync(ApplicationUserManager manager, string authenticationType)
    //    {
    //        // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
    //        var userIdentity = await manager.CreateIdentityAsync(this, authenticationType);
    //        // Add custom user claims here
    //        return userIdentity;
    //    }
    //}


    //public class ApplicationDbContext : IdentityDbContext<ApplicationUser, Role, int, UserLogin, UserRole, UserClaim>
    //{
    //    public ApplicationDbContext()
    //        : base("DefaultConnection")
    //    {
    //    }

    //    public static ApplicationDbContext Create()
    //    {
    //        return new ApplicationDbContext();
    //    }
    //}

}