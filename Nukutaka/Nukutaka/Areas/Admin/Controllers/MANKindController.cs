using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Nukutaka.Models;
using System.Data.SqlClient;

namespace Nukutaka.Areas.Admin.Controllers
{
    public class MANKindController : BaseController
    {
        NukutakaEntities db = new NukutakaEntities();
        // GET: Admin/MANKind
        public ActionResult Index()
        {
            var list = db.KINDs.ToList();
            return View(list);
        }

        [HttpGet]
        public ActionResult AddKind()
        {
            DDLAdd();
            return View();
        }

        [HttpPost]
        public ActionResult AddKind(KIND k)
        {
            DDLAdd();
            var checkExist = db.KINDs.Where(n => n.CODE == k.CODE).Count();
            if (ModelState.IsValid)
            {
                if (checkExist > 0)
                {
                    ViewBag.Exist = "Mã loại đã tồn tại";
                    return View();
                }
                int res = Add(0, k.CODE, k.NAME, k.CODECATEGORY);
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
        public ActionResult UpdateKind(int id)
        {
            KIND k = db.KINDs.SingleOrDefault(n => n.ID == id);
            if (k == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            DDLUpdate(k);
            return View(k);
        }

        [HttpPost]
        public ActionResult UpdateKind(KIND k)
        {
            DDLUpdate(k);
            if (ModelState.IsValid)
            {
                int res = Add(k.ID, k.CODE, k.NAME, k.CODECATEGORY);
                if (res > 0)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("", "Cập nhật không thành công");
                }
            }
            return View(k);
        }

        public ActionResult DeleteKind(int id)
        {
            KIND k = db.KINDs.SingleOrDefault(n => n.ID == id);
            if (k == null)
            {
                Response.StatusCode = 404;
            }
            db.KINDs.Remove(k);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public void DDLAdd()
        {
            ViewBag.CODECATEGORY = new SelectList(db.CATEGORies.ToList().OrderBy(n => n.NAME), "CODE", "NAME");
        }

        public void DDLUpdate(KIND k)
        {
            ViewBag.CODECATEGORY = new SelectList(db.CATEGORies.ToList().OrderBy(n => n.NAME), "CODE", "NAME", k.CODECATEGORY);
        }

        public int Add(int id, string code, string name, string codeCategory)
        {
            object[] obj =
            {
                new SqlParameter("@P_ID", id),
                new SqlParameter("@P_CODE", code),
                new SqlParameter("@P_NAME", name),
                new SqlParameter("@P_CODECATE", codeCategory)
            };
            var res = db.Database.ExecuteSqlCommand("SP_INSERT_UPDATE_KIND @P_ID, @P_CODE, @P_NAME, @P_CODECATE", obj);
            return res;
        }
    }
}