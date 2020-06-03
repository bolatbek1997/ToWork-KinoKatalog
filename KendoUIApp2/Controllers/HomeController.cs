using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Kendo.Mvc.UI;
using Kendo.Mvc.Extensions;
using KendoUIApp2.Models;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Web;
using System.IO;
using KendoUIApp2.Authorize;

namespace KendoUIApp2.Controllers
{
    [UserAuthorize]
    public class HomeController : BaseController
    {

        private FilmContext _db = new FilmContext();

        public ActionResult Tasks()
        {
            var model = user;
            ViewBag.Message = "Welcome to ASP.NET MVC!";

            return View();
        }
         
        public async Task<ActionResult> AddFilmAsync(int? id)
        { 
            ViewBag.Message = "Welcome to ASP.NET MVC!";
            if (id != null) {
                var model = await _db.Films.FirstOrDefaultAsync(x => x.Id == id);
                View(model);
            }
            return View();
        }
        [HttpPost]
        public async Task<ActionResult> AddFilmAsync(Film model)
        {
            HttpPostedFileBase file = Request.Files["ImageData"];
            model.Poster = ConvertToBytes(file);
            if (model != null && ModelState.IsValid)
            {
                try
                {
                    var film = await _db.Films.FirstOrDefaultAsync(x => x.Id == model.Id);
                    if (film != null) {
                        film.Name = model.Name;
                        film.Poster = model.Poster;
                        film.Producer = model.Producer;
                        film.Year = model.Year;
                        film.UserId = user.Id;
                        film.User = user;
                    }
                    _db.Films.Add(model);
                    _db.SaveChanges();
                }
                catch (Exception e)
                {

                    throw e;
                }
            }
                ViewBag.Message = "Welcome to ASP.NET MVC!";
           

            return View();
        }

        public async Task<ActionResult> FilmGridAsync([DataSourceRequest] DataSourceRequest request)
        {
            var list = new List<Film>();
            try
            {
                list = await _db.Films.ToListAsync();
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

        public async Task<ActionResult> RenderImage(Guid id)
        {
            var item = await _db.Films.FindAsync(id);

            byte[] photoBack = item.Poster;

            return File(photoBack, "image/png");
        }
        private byte[] ConvertToBytes(HttpPostedFileBase image)
        {
            byte[] imageBytes = null;
            BinaryReader reader = new BinaryReader(image.InputStream);
            imageBytes = reader.ReadBytes((int)image.ContentLength);
            return imageBytes;
        }

    }
}
