using FlipWeen.MVC.Api;

namespace FlipWeen.MVC.Controllers
{
    using System.Linq;
    using System.Web.Mvc;
    

    public abstract class BaseController : Controller
    {
        protected void AddResponseErrorsToModelState(ApiResponse response)
        {
            var errors = response.ErrorState.ModelState;
            if (errors == null)
            {
                return;
            }

            foreach (var error in errors)
            {
                foreach (var entry in 
                    from entry in ModelState
                    let matchSuffix = string.Concat(".", entry.Key)
                    where error.Key.EndsWith(matchSuffix)
                    select entry)
                {
                    ModelState.AddModelError(entry.Key, error.Value[0]);
                }
            }
        }
    }
}