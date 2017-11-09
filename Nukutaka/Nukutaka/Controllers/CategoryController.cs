using Nukutaka.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Nukutaka.Controllers
{
    public class CategoryController : Controller
    {
        NukutakaEntities db = new NukutakaEntities();
        CategoryModel model = new CategoryModel();
        // GET: Category
        public PartialViewResult Category()
        {           
            return PartialView(model.ListCatagory());
        }

        public ActionResult ProductByCategory(string code)
        {
            return View(model.ListProductByCategory(code));
        }
        
        public ActionResult KindByCategory(string code)
        {
            ViewBag.CodeCategory = code;
            return View(model.ListKindByCategory(code));
        }

        public ActionResult ProductByKandC(string codeCategory, string codeKind)
        {
            return View(model.ListProductByKindandCate(codeCategory, codeKind));
        }
        
    }
}