using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using Castle.MicroKernel.Registration;
using Flipween.Core.IoC;
using FlipWeen.Common;
using FlipWeen.Common.Entities;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.Google;
using Microsoft.Owin.Security.OAuth;
using Owin;
using FlipWeen.Models;
using FlipWeen.Providers;
using Microsoft.AspNet.Identity.Owin;
using FlipWeen.Data;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.DataHandler.Serializer;
using Microsoft.Owin.Security.DataProtection;
using Microsoft.Owin.Security.DataHandler;

namespace FlipWeen
{
    public partial class Startup
    {
        public static OAuthAuthorizationServerOptions OAuthOptions { get; private set; }

        public static string PublicClientId { get; private set; }

        // For more information on configuring authentication, please visit http://go.microsoft.com/fwlink/?LinkId=301864
        public void ConfigureAuth(IAppBuilder app)
        {

            var container = CastleHelper.Container;

            container.Register(
                Component
                    .For<IAppBuilder>()
                    .Instance(app),
                Component
                    .For<IDbContext>()
                    .ImplementedBy<DataContext>()
                    .DependsOn(Dependency.OnValue<string>("DefaultConnection"))
                    .LifestyleTransient(),
                Component
                    .For<IUserStore<ApplicationUser, int>>()
                    .ImplementedBy<UserStore<ApplicationUser, Role, int, UserLogin, UserRole, UserClaim>>()
                    .DependsOn(Dependency.OnComponent<DbContext, DataContext>())
                    .LifestyleTransient(),
                Component
                    .For<ApplicationUserManager>()
                    .UsingFactoryMethod(kernel =>
                        CreateCustomUserManager(
                            kernel.Resolve<IUserStore<ApplicationUser, int>>(),
                            kernel.Resolve<IAppBuilder>()))
                    .LifestyleTransient(),
                Component
                    .For<IRoleStore<Role,int>>()
                    .ImplementedBy<RoleStore<Role, int,UserRole>>()
                    .DependsOn(Dependency.OnComponent<DbContext, DataContext>())
                    .LifestyleTransient(),
                //Component
                //    .For<ApplicationRoleManager>()
                //    .LifestyleTransient(),
                Component
                    .For<IAuthenticationManager>()
                    .UsingFactoryMethod(kernel => HttpContext.Current.GetOwinContext().Authentication)
                    .LifestyleTransient()
                //Component
                //    .For<ApplicationSignInManager>()
                //    .LifestyleTransient());
                , Component
                    .For<IDataSerializer<AuthenticationTicket>>()
                    .ImplementedBy<TicketSerializer>()
                    , Component
                    .For<ISecureDataFormat<AuthenticationTicket>>()
                    .ImplementedBy<TicketDataFo‌​rmat>()
                   , Component
                    .For<IDataProtector>()
                     .UsingFactoryMethod(kernel =>
                       new DpapiDataProtectionProvider().Create("ASP.NET Identity"))

                );


       //     builder.RegisterType<TicketSerializer>()
       //.As<IDataSerializer<AuthenticationTicket>>();
       //     builder.Register(c => new DpapiDataProtectionProvider().Create("ASP.NET Identity"))
       //            .As<IDataProtector>();
            app.CreatePerOwinContext(() => container.Resolve<ApplicationUserManager>());

         
            // Configure the db context and user manager to use a single instance per request
            //app.CreatePerOwinContext(DataContext.Create);
            //app.CreatePerOwinContext<ApplicationUserManager>(ApplicationUserManager.Create);
            

            BaseDataRepository repo = new BaseDataRepository(DataContext.Create());
            // Enable the application to use a cookie to store information for the signed in user
            // and to use a cookie to temporarily store information about a user logging in with a third party login provider
            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                Provider = new CookieAuthenticationProvider
                {
                    OnValidateIdentity = SecurityStampValidator
                .OnValidateIdentity<ApplicationUserManager, ApplicationUser, int>(
                    validateInterval: TimeSpan.FromMinutes(30),
                    regenerateIdentityCallback: (manager, user) =>
                        repo.GenerateUserIdentityAsync(user,manager, DefaultAuthenticationTypes.ApplicationCookie),
                    getUserIdCallback: (id) => (id.GetUserId<int>()))
                }
            });
            app.UseExternalSignInCookie(DefaultAuthenticationTypes.ExternalCookie);

            // Configure the application for OAuth based flow
            PublicClientId = "self";
            OAuthOptions = new OAuthAuthorizationServerOptions
            {
                TokenEndpointPath = new PathString("/Token"),
                Provider = new ApplicationOAuthProvider(PublicClientId),
                AuthorizeEndpointPath = new PathString("/api/Account/ExternalLogin"),
                AccessTokenExpireTimeSpan = TimeSpan.FromDays(1),
                // In production mode set AllowInsecureHttp = false
                AllowInsecureHttp = true,
              
            };

            // Enable the application to use bearer tokens to authenticate users
            app.UseOAuthBearerTokens(OAuthOptions);

            // Uncomment the following lines to enable logging in with third party login providers
            //app.UseMicrosoftAccountAuthentication(
            //    clientId: "",
            //    clientSecret: "");

            //app.UseTwitterAuthentication(
            //    consumerKey: "",
            //    consumerSecret: "");

            //app.UseFacebookAuthentication(
            //    appId: "",
            //    appSecret: "");

            //app.UseGoogleAuthentication(new GoogleOAuth2AuthenticationOptions()
            //{
            //    ClientId = "",
            //    ClientSecret = ""
            //});
        }

        private static ApplicationUserManager CreateCustomUserManager(
            IUserStore<ApplicationUser,int> store,
            IAppBuilder appBuilder)
        {
            var manager = new ApplicationUserManager(store);

            manager.UserValidator = new UserValidator<ApplicationUser,int>(manager)
            {
                AllowOnlyAlphanumericUserNames = true,
                RequireUniqueEmail = true
            };

            manager.PasswordValidator = new PasswordValidator
            {
                RequiredLength = 6,
                RequireNonLetterOrDigit = false,
                RequireDigit = false,
                RequireLowercase = false,
                RequireUppercase = false
            };

            manager.UserLockoutEnabledByDefault = true;
            //manager.DefaultAccountLockoutTimeSpan = Common.Constants.AccountLockoutTimeSpan;
            //manager.MaxFailedAccessAttemptsBeforeLockout = Common.Constants.MaxFailedAccessAttemptsBeforeLockout;

            var dataProtectionProvider = appBuilder.GetDataProtectionProvider();
            if (dataProtectionProvider != null)
            {
                var dataProtector = dataProtectionProvider.Create("ASP.NET Identity");
                manager.UserTokenProvider = new DataProtectorTokenProvider<ApplicationUser,int>(dataProtector);
            }

            return manager;
        }
    }
}
