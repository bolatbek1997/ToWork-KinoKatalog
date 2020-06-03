using KendoUIApp2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;

namespace KendoUIApp2.Controllers
{
    public class BaseController : Controller
    {
       
        public BaseController()
        {

        }
        public UserModel user
        {
            get
            {
                if (Session["user"] != null)
                {
                    return (UserModel)Session["user"];
                }
                else
                {
                   new RedirectToRouteResult(new RouteValueDictionary(new
                    {
                        controller = "Auth",
                        action = "FormLogin"
                    }));
                    return null;
                }
            }
            set { Session["user"] = value; }
        }

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);
            //var c = Request.Cookies["lang"];
            //var k = c == null ? LANG_SETTINGS.GetDefaultLang().value : c.Value;
            //ViewBag.lang = langs[k];

           

        }

        /// <summary>
        /// Проверка существования подключенного юзера
        /// </summary>
        private void CheckUser()
        {
            if (User != null && User.Identity.IsAuthenticated)
            {
                UserModel u = (UserModel)Session["user"];

                if (u == null)
                {
                    //Данных пользователя нет в сессии (или они неверны), поэтому мы их обновляем
                    //Отсутствие данных в сессии возможно из-за перезагрузки приложения (сессия почистилась)
                    user = UserModel.GetUserByLogin(User.Identity.Name);
                    Session["user"] = user;
                }
                else user = u;
            }
            else
            {
                if (User != null)
                { FormsAuthentication.SignOut(); }
            }
        }


        /// <summary>
        /// Проверка существования пользователя
        /// </summary>
        /// <param name="filterContext"></param>
        //protected override void OnAuthorization(AuthorizationContext filterContext)
        //{
        //    base.OnAuthorization(filterContext);
        //    CheckUser();
        //}
    }
}