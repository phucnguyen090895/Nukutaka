using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Nukutaka.Models;
using System.IO;
using System.Data.SqlClient;

namespace Nukutaka.Areas.Admin.Controllers
{
    public class MANProductController : Controller
    {
        NukutakaEntities db = new NukutakaEntities();
        // GET: Admin/MANProduct

        public ActionResult Index()
        {
            var list = db.PRODUCTS.ToList();
            return View(list);
        }
        [HttpGet]
        public ActionResult AddProduct()
        {
            string c = "1";
            //ddlKind(c);
            Ddl();
            return View();
        }
        [HttpPost]
        public ActionResult AddProduct(PRODUCT pr, HttpPostedFileBase fileUpLoadImg)
        {
            if (fileUpLoadImg == null)
            {
                ViewBag.Error = "Chưa chọn hình ảnh";
                Ddl();
                return View();
            }
            // file Name     
            var fileName = Path.GetFileName(fileUpLoadImg.FileName);
            // file Path
            var filePath = Path.Combine(Server.MapPath("~/images/products"), fileName);
            if (System.IO.File.Exists(filePath))
            {
                ViewBag.Error = "Hình ảnh đã tồn tại";
                Ddl();
                return View();
            }

            try
            {
                if (ModelState.IsValid)
                {
                    string ddlCategory = Request.Form["CATEGORY"];
                    string ddlKind = Request.Form["KIND"];
                    string ddlBrand = Request.Form["BRAND"];
                    int res = Add(0, pr.CODE, pr.NAME, pr.PRICE, fileUpLoadImg.FileName, ddlCategory, ddlKind, ddlBrand, pr.STATUS, pr.INSTOCK);
                    if(res > 0)
                    {
                        fileUpLoadImg.SaveAs(filePath);
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        ModelState.AddModelError("", "Thêm mới không thành công");
                    }
                    
                }
            }
            catch
            {

            }
            Ddl();
            return View();
        }

        public int Add(int id, string code, string name, int? price, string imgurl, string codeCategory, string codeKind, string codeBrand, int? status, int? instock)
        {
            object[] obj =
            {
                new SqlParameter("@P_ID", id),
                new SqlParameter("@P_CODE", code),
                new SqlParameter("@P_NAME", name),
                new SqlParameter("@P_PRICE", price),
                new SqlParameter("@P_IMAGEURL", imgurl),
                new SqlParameter("@P_CODECATEGORY", codeCategory),
                new SqlParameter("@P_CODEKIND", codeKind),
                new SqlParameter("@P_CODEBRAND", codeBrand),
                new SqlParameter("@P_STATUS", status),
                new SqlParameter("@P_INSTOCK", instock)
            };
            var res = db.Database.ExecuteSqlCommand("SP_INSERT_UPDATE_PRODUCT @P_ID, @P_CODE, @P_NAME, @P_PRICE, @P_IMAGEURL, @P_CODECATEGORY, @P_CODEKIND, @P_CODEBRAND, @P_STATUS, @P_INSTOCK",obj);
            return res;
        }

        public void Ddl()
        {
            ViewBag.CATEGORY = new SelectList(db.CATEGORies.ToList().OrderBy(n => n.NAME), "CODE", "NAME");
            ViewBag.BRAND = new SelectList(db.BRANDS.ToList().OrderBy(n => n.NAME), "CODE", "NAME");
            ViewBag.KIND = new SelectList(db.KINDs.ToList().OrderBy(n => n.NAME), "CODE", "NAME");
        }

        public void ddlKind(string code)
        {
            ViewBag.Kind = new SelectList(db.KINDs.ToList().Where(n => n.CODECATEGORY == code).OrderBy(n => n.NAME), "CODE", "NAME");
        }
    }
}