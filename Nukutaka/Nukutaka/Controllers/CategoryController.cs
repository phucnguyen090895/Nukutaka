using Nukutaka.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using PagedList.Mvc;

namespace Nukutaka.Controllers
{
    public class CategoryController : Controller
    {
        NukutakaEntities db = new NukutakaEntities();
        // GET: Category
        //[ChildActionOnly]
        public PartialViewResult Category()
        {           
            return PartialView(db.CATEGORies.ToList());
        }

        public ActionResult ProductByCategory(string code, int? page)
        {
            int pageSize = 9;
            int pageNum = (page ?? 1);
            var list = db.PRODUCTS.Where(n => n.CODECATEGORY == code).OrderByDescending(n => n.PRICE).ToPagedList(pageNum, pageSize);
            return View(list);
        }

        [ChildActionOnly]
        public ActionResult KindByCategory(string code)
        {

            ViewBag.CodeCategory = code;
            var list = db.KINDs.Where(n => n.CODECATEGORY == code).ToList();
            return View(list);
        }

        public ActionResult ProductByKandC(string codeCategory, string codeKind, int? page)
        {
            int pageSize = 9;
            int pageNum = (page ?? 1);
            var list = db.PRODUCTS.Where(n => n.CODECATEGORY == codeCategory && n.CODEKIND == codeKind).OrderByDescending(n => n.PRICE).ToPagedList(pageNum, pageSize);
            return View(list);
        }
        
    }
}