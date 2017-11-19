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
    public class HomeController : Controller
    {
        // GET: Home
        NukutakaEntities db = new NukutakaEntities();
        public ActionResult Index()
        {
            var listProducts = db.PRODUCTS.Where(n=>n.STATUS==1).OrderByDescending(n=>n.PRICE).Take(6).ToList();
            return View(listProducts);
        }

        public ActionResult ListProduct(int? page)
        {
            int pageSize = 9;
            int pageNum = (page ?? 1);
            var listProducts = db.PRODUCTS.OrderByDescending(n => n.PRICE).ToPagedList(pageNum, pageSize);
            return View(listProducts);
        }

        public ActionResult Page404()
        {
            return View();
        }
    }
}