using Nukutaka.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Nukutaka.Controllers
{
    public class ProductDetailController : Controller
    {
        NukutakaEntities db = new NukutakaEntities();
        // GET: ProductDetail
        public ActionResult Detail(int id)
        {
            PRODUCT pr = db.PRODUCTS.SingleOrDefault(p => p.ID == id);
            if(pr == null)
            {
                return RedirectToAction("Page404", "Home");

            }

            ViewBag.CategoryName = db.CATEGORies.Single(n => n.CODE == pr.CODECATEGORY).NAME;
            ViewBag.KindName = db.KINDs.Single(n => n.CODE == pr.CODEKIND).NAME;
            ViewBag.BrandName = db.BRANDS.Single(n => n.CODE == pr.CODEBRAND).NAME;
            return View(pr);
            
        }
    }
}