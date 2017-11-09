using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Nukutaka.Controllers
{
    public class BrandController : Controller
    {
        BrandModel model = new BrandModel();
        // GET: Brand
        public ActionResult ProductByBrand(string code)
        {
            return View(model.ListProductByBrand(code));
        }

        public ActionResult Brands()
        {
            return View(model.ListBrand());
        }

        public ActionResult CountProduct(string code)
        {
            var countProduct = model.countProduct(code).Count();
            ViewBag.Count = countProduct;
            return View();
        }
    }
}