using KendoUIApp2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace KendoUIApp2.Controllers
{
    public class AuthController : BaseController
    {

        [HttpGet]
        public PartialViewResult FormLogin(string str)
        {
            if (str == "signin")
                ViewBag.SignIn = true;
            else
                ViewBag.SignIn = false;
            UserModel viewMod = new UserModel();
            return PartialView(viewMod);
        }


        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult FormLogin(UserModel model, string returnUrl = "")
        {

            // model.IsSaved = false;
            if (ModelState.IsValid)
            {

                try
                {
                    var db = new FilmContext();
                    var ua = db.Users.FirstOrDefault(x => x.Login == model.Login && x.Password == model.Password);
                    if (ua == null)
                    {
                        ModelState.AddModelError("", "Логин или пароль не верные");
                        return PartialView(model);
                    }
                    ViewBag.SignIn = false;
                    Session.Clear();
                    this.user = ua;
                    FormsAuthentication.SetAuthCookie(model.Login, false);
                }
                catch (Exception e)
                {

                    throw e;
                }
            }
            return PartialView(model);
        }
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult SignIn(UserModel model, string returnUrl = "")
        {

            // model.IsSaved = false;
            if (ModelState.IsValid)
            {

                try
                {
                    var db = new FilmContext();
                    db.Users.Add(model);
                    db.SaveChanges();
                    this.user = model;
                    FormsAuthentication.SetAuthCookie(model.Login, true);
                    var boll = User.Identity.IsAuthenticated;
                }
                catch (Exception e)
                {
                   return RedirectToAction("FormLogin", new { str = "Пользователь с таким логином уже существует" });
                    throw e;
                }
            }
            // return RedirectToAction("Tasks", "Home");
            return View();
        }



        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            Session.Clear();
            Session.Abandon();
            Session.RemoveAll();
            this.Response.Cache.SetExpires(DateTime.UtcNow.AddMinutes(-1));
            this.Response.Cache.SetCacheability(HttpCacheability.NoCache);
            this.Response.Cache.SetNoStore();
            return RedirectToAction("Index", "Home");
        }
    }
}