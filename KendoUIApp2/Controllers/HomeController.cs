using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Kendo.Mvc.UI;
using Kendo.Mvc.Extensions;
using KendoUIApp2.Models;
using System.Threading.Tasks;
using System.Data.Entity;

namespace KendoUIApp2.Controllers
{
    public class HomeController : Controller
    {

        private TaskContext _db = new TaskContext();

        public ActionResult Tasks()

        {
            ViewBag.Message = "Welcome to ASP.NET MVC!";

            return View();
        }
        public async Task<ActionResult> GridAsync([DataSourceRequest] DataSourceRequest request)
        {
            var list = new List<Tasks>();
            try
            {
                list = await _db.Tasks.ToListAsync();
                return Json(await list.ToDataSourceResultAsync(request));
            }
            catch (Exception e)
            {
                return Json(await list.ToDataSourceResultAsync(request));
                throw e;
            }
           
        }
        [AcceptVerbs(HttpVerbs.Post)]
        public async Task<ActionResult> Create([DataSourceRequest] DataSourceRequest request, Tasks model)
        {
            if (model != null && ModelState.IsValid)
            {               
                
                try
                {
                    _db.Tasks.Add(model);
                    await _db.SaveChangesAsync();
                }
                catch (Exception e)
                {
                    return Json("error");
                    throw e;
                }
                           
            }

            return Json(new[] { model }.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public async Task<ActionResult> Update([DataSourceRequest] DataSourceRequest request, Tasks model)
        {
          
            if (model != null && ModelState.IsValid)
            {
                _db.Entry(model).State = EntityState.Modified;
                try
                {
                    await _db.SaveChangesAsync();
                }
                catch (Exception e)
                {
                    return Json("error");
                    throw e;
                }
            }
            return Json(new[] { model }.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public async Task<ActionResult> Delete([DataSourceRequest] DataSourceRequest request, Tasks model)
        {
            if (model != null)
            {
                var _model =await _db.Tasks.FindAsync(model.Id);
                _db.Tasks.Remove(_model);
                try
                {
                    await _db.SaveChangesAsync();
                }
                catch (Exception e)
                {
                    return Json("error");
                    throw e;
                }
            }

            return Json(new[] { model }.ToDataSourceResult(request, ModelState)); ;
        }
                                    
    }
}
