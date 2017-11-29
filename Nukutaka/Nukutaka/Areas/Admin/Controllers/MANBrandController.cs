using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Nukutaka.Models;
using System.Data.SqlClient;

namespace Nukutaka.Areas.Admin.Controllers
{
    public class MANBrandController : BaseController
    {
        NukutakaEntities db = new NukutakaEntities();
        // GET: Admin/MANBrand
        public ActionResult Index()
        {
            var list = db.BRANDS.ToList();
            return View(list);
        }

        [HttpGet]
        public ActionResult AddBrand()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddBrand(BRAND br)
        {
            var checkExist = db.BRANDS.Where(n => n.CODE == br.CODE).Count();
            if (ModelState.IsValid)
            {
                if(checkExist > 0)
                {
                    ViewBag.Exist = "Mã thương hiệu đã tồn tại";
                    return View();
                }
                int res = Add(0, br.CODE, br.NAME);
                if(res > 0)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("", "Thêm mới không thành công");
                }
               
            }
            return View();
        }

        [HttpGet]
        public ActionResult UpdateBrand(int id)
        {
            BRAND br = db.BRANDS.SingleOrDefault(n => n.ID == id);
            if(br == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(br);
        }

        [HttpPost]
        public ActionResult UpdateBrand(BRAND br)
        {
            if (ModelState.IsValid)
            {
                int res = Add(br.ID, br.CODE, br.NAME);
                if (res > 0)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("", "Cập nhật không thành công");
                }
            }
            return View(br);
        }

        public ActionResult DeleteBrand(int id)
        {
            BRAND br = db.BRANDS.SingleOrDefault(n => n.ID == id);
            if (br == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            db.BRANDS.Remove(br);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public int Add(int id, string code, string name)
        {
            object[] obj =
            {
                new SqlParameter("@P_ID", id),
                new SqlParameter("@P_CODE", code),
                new SqlParameter("@P_NAME", name)
            };
            int res = db.Database.ExecuteSqlCommand("SP_INSERT_UPDATE_BRAND @P_ID, @P_CODE, @P_NAME", obj);
            return res;
        }
    }
}