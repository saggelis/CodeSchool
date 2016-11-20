using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace Flipween.Controllers
{
    public abstract class BaseController : ApiController
    {
        //protected void AddResponseErrorsToModelState(ApiResponse response)
        //{
        //    var errors = response.ErrorState.ModelState;
        //    if (errors == null)
        //    {
        //        return;
        //    }

        //    foreach (var error in errors)
        //    {
        //        foreach (var entry in
        //            from entry in ModelState
        //            let matchSuffix = string.Concat(".", entry.Key)
        //            where error.Key.EndsWith(matchSuffix)
        //            select entry)
        //        {
        //            ModelState.AddModelError(entry.Key, error.Value[0]);
        //        }
        //    }
        //}
    }
}