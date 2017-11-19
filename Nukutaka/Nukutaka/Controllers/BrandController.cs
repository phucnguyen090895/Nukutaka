using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Nukutaka.Models;
using PagedList;
using PagedList.Mvc;

namespace Nukutaka.Controllers
{
    public class BrandController : Controller
    {
        NukutakaEntities db = new NukutakaEntities();
        // GET: Brand
        public ActionResult ProductByBrand(string code, int? page)
        {
            int pageSize = 9;
            int pageNum = (page ?? 1);
            var listProducts = db.PRODUCTS.Where(n => n.CODEBRAND == code).OrderByDescending(n => n.PRICE).ToPagedList(pageNum, pageSize);
            return View(listProducts);
        }

        public ActionResult Brands()
        {
            return View(db.BRANDS.ToList());
        }

        //[ChildActionOnly]
        public ActionResult CountProduct(string code)
        {
            var countProduct = db.PRODUCTS.Where(n => n.CODEBRAND == code).Count();
            ViewBag.Count = countProduct;
            return View();
        }
    }
}