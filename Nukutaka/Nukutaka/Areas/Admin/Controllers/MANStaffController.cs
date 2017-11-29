using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Nukutaka.Models;
using System.Data.SqlClient;

namespace Nukutaka.Areas.Admin.Controllers
{
    public class MANStaffController : BaseController
    {
        NukutakaEntities db = new NukutakaEntities();
        // GET: Admin/MANStaff
        public ActionResult Index()
        {
            var list = db.STAFFs.ToList();
            return View(list);
        }

        [HttpGet]
        public ActionResult AddStaff()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddStaff(STAFF s)
        {
            var checkExist = db.STAFFs.Where(n => n.CODE == s.CODE).Count();
            if (ModelState.IsValid)
            {
                if (checkExist > 0)
                {
                    ViewBag.Exist = "Mã nhân viên đã tồn tại";
                    return View();
                }
                int res = Add(0, s.CODE, s.NAME, s.PHONE);
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
        public ActionResult UpdateStaff(int id)
        {
            STAFF s = db.STAFFs.SingleOrDefault(n => n.ID == id);
            if (s == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(s);
        }

        [HttpPost]
        public ActionResult UpdateStaff(STAFF s)
        {
            if (ModelState.IsValid)
            {
                int res = Add(s.ID, s.CODE, s.NAME, s.PHONE);
                if (res > 0)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("", "Cập nhật không thành công");
                }
            }
            return View(s);
        }

        public ActionResult DeleteStaff(int id)
        {
            STAFF s = db.STAFFs.SingleOrDefault(n => n.ID == id);
            if (s == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            db.STAFFs.Remove(s);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public int Add(int id, string code, string name, string phone)
        {
            object[] obj =
            {
                new SqlParameter("@P_ID", id),
                new SqlParameter("@P_CODE", code),
                new SqlParameter("@P_NAME", name),
                new SqlParameter("@P_PHONE", phone)
            };
            int res = db.Database.ExecuteSqlCommand("SP_INSERT_UPDATE_STAFF @P_ID, @P_CODE, @P_NAME, @P_PHONE", obj);
            return res;
        }
    }
}