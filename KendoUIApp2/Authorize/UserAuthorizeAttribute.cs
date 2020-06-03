using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace KendoUIApp2.Authorize
{
    public class UserAuthorizeAttribute : AuthorizeAttribute
    {

        public override void OnAuthorization(System.Web.Mvc.AuthorizationContext filterContext)
        {
            bool izinVer = false;

            if (!filterContext.HttpContext.User.Identity.IsAuthenticated)
            {
                filterContext.Result = new RedirectToRouteResult(new
                    RouteValueDictionary(new { controller = "Auth", action = "FormLogin" }));
            }

           
        }
    }
}