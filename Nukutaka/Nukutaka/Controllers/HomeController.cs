using Nukutaka.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Nukutaka.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        NukutakaEntities db = new NukutakaEntities();
        public ActionResult Index()
        {
            var listProducts = db.PRODUCTS.Take(6).ToList();
            return View(listProducts);
        }     
    }
}