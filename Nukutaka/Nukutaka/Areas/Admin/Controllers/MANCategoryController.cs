using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Nukutaka.Models;
using System.Data.SqlClient;

namespace Nukutaka.Areas.Admin.Controllers
{
    public class MANCategoryController : BaseController
    {
        NukutakaEntities db = new NukutakaEntities();
        // GET: Admin/MANCategory
        public ActionResult Index()
        {
            var list = db.CATEGORies.ToList();
            return View(list);
        }

        [HttpGet]
        public ActionResult AddCategory()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddCategory(CATEGORY cate)
        {
            var checkExist = db.CATEGORies.Where(n => n.CODE == cate.CODE).Count();
            if (ModelState.IsValid)
            {
                if (checkExist > 0)
                {
                    ViewBag.Exist = "Mã danh mục đã tồn tại";
                    return View();
                }
                int res = Add(0, cate.CODE, cate.NAME);
                if (res > 0)
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
        public ActionResult UpdateCategory(int id)
        {
            CATEGORY cate = db.CATEGORies.SingleOrDefault(n => n.ID == id);
            if (cate == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(cate);
        }

        [HttpPost]
        public ActionResult UpdateCategory(CATEGORY cate)
        {
            if (ModelState.IsValid)
            {
                int res = Add(cate.ID, cate.CODE, cate.NAME);
                if (res > 0)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("", "Cập nhật không thành công");
                }
            }
            return View(cate);
        }

        public ActionResult DeleteCategory(int id)
        {
            CATEGORY cate = db.CATEGORies.SingleOrDefault(n => n.ID == id);
            if(cate == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            db.CATEGORies.Remove(cate);
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
            var res = db.Database.ExecuteSqlCommand("SP_INSERT_UPDATE_CATEGORY @P_ID, @P_CODE, @P_NAME", obj);
            return res;
        }
    }
}