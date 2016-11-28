using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace CodeSchool.Attributes
{
   
    public class TokenAuthorize : AuthorizeAttribute
    {
        public TokenAuthorize()
        { }

        public string AccessLevel { get; set; }
        //public override void OnAuthorization(AuthorizationContext filterContext)
        //{
        //    base.OnAuthorization(filterContext);
        //    CheckToken(filterContext);
        //}

        //protected override bool AuthorizeCore(HttpContextBase httpContext)
        //{
        //    bool authorize = false;
        //    foreach (var role in allowedroles)
        //    {
        //        var user = httpContext.AppUser.Where(m => m.UserID == GetUser.CurrentUser/* getting user form current context */ && m.Role == role &&
        //        m.IsActive == true); // checking active users with allowed roles.  
        //        if (user.Count() > 0)
        //        {
        //            authorize = true; /* return true if Entity has current user(active) with specific role */
        //        }
        //    }
        //    return authorize;
        //}

      
        //protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        //{
        //    filterContext.Result = new RedirectToRouteResult(
        //                new RouteValueDictionary(
        //                    new
        //                    {
        //                        controller = "Account",
        //                        action = "Login"
        //                    })
        //                );
        //}
    }
}