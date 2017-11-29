using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Nukutaka.Models;
namespace Nukutaka.Areas.Admin.Controllers
{
    public class LoginController : Controller
    {
        NukutakaEntities db = new NukutakaEntities();
        // GET: Admin/Login
        [HttpGet]
        public ActionResult Index()
        {
            Session["Admin"] = null;
            return View();
        }

        [HttpPost]
        public ActionResult Index(string phone, string password)
        {
            if(phone == "&nbsp" || password == "")
            {
                ViewBag.Error = "Tài khoản hoặc mật khẩu không được bỏ trống";
                return View();
            }
            ADMIN ad = db.ADMINs.SingleOrDefault(n => n.PHONE == phone && n.PASSWORD == password);
            if (ad != null)
            {
                Session["Admin"] = ad.CODE;
                return RedirectToAction("Index", "MANProduct");
            }
            else
            {
                ViewBag.Error = "Sai tài khoản hoặc mật khẩu";
                return View();
            }
        }

        [ChildActionOnly]
        public ActionResult NameSession()
        {
            if (Session["Admin"] != null)
            {
                var ss = Session["Admin"].ToString();
                var ad = db.ADMINs.SingleOrDefault(n => n.CODE == ss);
                // ADMIN ad = db.ADMINs.SingleOrDefault(n => n.CODE == ss);
                ViewBag.NameAdmin = ad.NAME;
                return View();

            }
            else
            {
                return RedirectToAction("Index", "Login");
            }


        }
    }
}