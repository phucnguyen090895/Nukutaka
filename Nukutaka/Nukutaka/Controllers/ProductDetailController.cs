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
        ProductDetailModel model = new ProductDetailModel();
        // GET: ProductDetail
        public ActionResult Detail(int id)
        {
            var detail = model.Detail(id);
            if(detail == null)
            {
                return RedirectToAction("Page404", "Home");

            }
            ViewBag.CategoryName = db.CATEGORies.Single(n => n.CODE == detail.CODECATEGORY).NAME;
            ViewBag.KindName = db.KINDs.Single(n => n.CODE == detail.CODEKIND).NAME;
            ViewBag.BrandName = db.BRANDS.Single(n => n.CODE == detail.CODEBRAND).NAME;
            return View(detail);
            
        }
    }
}