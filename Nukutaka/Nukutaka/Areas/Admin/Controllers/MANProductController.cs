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
    public class MANProductController : BaseController
    {
        NukutakaEntities db = new NukutakaEntities();
        // GET: Admin/MANProduct

        public ActionResult Index()
        {
            var list = db.PRODUCTS.ToList();;
            return View(list);
        }

        [HttpGet]
        public ActionResult AddProduct()
        {
            DdlAdd();
            return View();
        }

        [HttpPost]
        public ActionResult AddProduct(PRODUCT pr, HttpPostedFileBase fileUpLoadImg)
        {
            DdlAdd();
            var checkExist = db.PRODUCTS.Where(n => n.CODE == pr.CODE).Count();      
            if (fileUpLoadImg == null)
            {
                ViewBag.Error = "Chưa chọn hình ảnh";
                return View();
            }
            // file Name     
            var fileName = Path.GetFileName(fileUpLoadImg.FileName);
            // file Path
            var filePath = Path.Combine(Server.MapPath("~/images/products"), fileName);
            if (System.IO.File.Exists(filePath))
            {
                ViewBag.Error = "Hình ảnh đã tồn tại";
                
                return View();
            }

            try
            {
                if (ModelState.IsValid)
                {
                    if (checkExist > 0)
                    {
                        ViewBag.Exist = "Mã sản phẩm đã tồn tại";
                        return View();
                    }
                    //string ddlCategory = Request.Form["CATEGORY"];
                    //string ddlKind = Request.Form["KIND"];
                    //string ddlBrand = Request.Form["BRAND"];
                    int res = Add(0, pr.CODE, pr.NAME, pr.PRICE, fileUpLoadImg.FileName, pr.CODECATEGORY, pr.CODECATEGORY, pr.CODEBRAND, pr.STATUS, pr.INSTOCK);
                    if (res > 0)
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
            return View();
        }

        [HttpGet]
        public ActionResult UpdateProduct(int id)
        {        
            PRODUCT pr = db.PRODUCTS.SingleOrDefault(n => n.ID == id);  
            if (pr == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            DdlUpdate(pr);
            return View(pr);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult UpdateProduct(PRODUCT pr, HttpPostedFileBase fileUpLoadImg)
        {
            DdlUpdate(pr);
            try
            {
                if (ModelState.IsValid)
                {
                    //string ddlCategory = Request.Form["CATEGORY"];
                    //string ddlKind = Request.Form["KIND"];
                    //string ddlBrand = Request.Form["BRAND"];

                    if (fileUpLoadImg != null)
                    {
                        // file Name     
                        var fileName = Path.GetFileName(fileUpLoadImg.FileName);
                        // file Path
                        var filePath = Path.Combine(Server.MapPath("~/images/products"), fileName);
                        if (System.IO.File.Exists(filePath))
                        {
                            ViewBag.Error = "Hình ảnh đã tồn tại";
                            return View(pr);
                        }
                        int res = Add(pr.ID, pr.CODE, pr.NAME, pr.PRICE, fileUpLoadImg.FileName, pr.CODECATEGORY, pr.CODEKIND, pr.CODEBRAND, pr.STATUS, pr.INSTOCK);
                        if (res > 0)
                        {
                            fileUpLoadImg.SaveAs(filePath);
                            return RedirectToAction("Index");
                        }
                        else
                        {
                            ModelState.AddModelError("", "Cập nhật không thành công");
                        }
                    }
                    else
                    {
                        int res = Add(pr.ID, pr.CODE, pr.NAME, pr.PRICE, pr.IMAGEURL, pr.CODECATEGORY, pr.CODEKIND, pr.CODEBRAND, pr.STATUS, pr.INSTOCK);
                        if (res > 0)
                        {
                            return RedirectToAction("Index");
                        }
                        else
                        {
                            ModelState.AddModelError("", "Cập nhật không thành công");
                        }
                    }                                   
                }
                return View(pr);
            }
            catch
            {

            }     
            return View(pr);
        }
       
        public ActionResult DeleteProduct(int id)
        {
            PRODUCT pr = db.PRODUCTS.SingleOrDefault(n => n.ID == id);
            if(pr == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            db.PRODUCTS.Remove(pr);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public void DdlAdd()
        {
            ViewBag.CODECATEGORY = new SelectList(db.CATEGORies.ToList().OrderBy(n => n.NAME), "CODE", "NAME");
            ViewBag.CODEBRAND = new SelectList(db.BRANDS.ToList().OrderBy(n => n.NAME), "CODE", "NAME");
            ViewBag.CODEKIND = new SelectList(db.KINDs.ToList().OrderBy(n => n.NAME), "CODE", "NAME");
        }

        public void DdlUpdate(PRODUCT pr)
        {
            ViewBag.CODECATEGORY = new SelectList(db.CATEGORies.ToList().OrderBy(n => n.NAME), "CODE", "NAME", pr.CODECATEGORY);
            ViewBag.CODEBRAND = new SelectList(db.BRANDS.ToList().OrderBy(n => n.NAME), "CODE", "NAME", pr.CODEBRAND);
            ViewBag.CODEKIND = new SelectList(db.KINDs.ToList().OrderBy(n => n.NAME), "CODE", "NAME", pr.CODEKIND);
        }

        //public void getName(PRODUCT pr)
        //{
        //    CATEGORY category = db.CATEGORies.SingleOrDefault(n => n.CODE == pr.CODE);
        //    ViewBag.CATEGORY = category.NAME;
        //    //ViewBag.BRAND = new SelectList(db.BRANDS.ToList().OrderBy(n => n.NAME), "CODE", "NAME");
        //    //ViewBag.KIND = new SelectList(db.KINDs.ToList().OrderBy(n => n.NAME), "CODE", "NAME");
        //}

        //public void ddlKind(string code)
        //{
        //    ViewBag.Kind = new SelectList(db.KINDs.ToList().Where(n => n.CODECATEGORY == code).OrderBy(n => n.NAME), "CODE", "NAME");
        //}

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
            var res = db.Database.ExecuteSqlCommand("SP_INSERT_UPDATE_PRODUCT @P_ID, @P_CODE, @P_NAME, @P_PRICE, @P_IMAGEURL, @P_CODECATEGORY, @P_CODEKIND, @P_CODEBRAND, @P_STATUS, @P_INSTOCK", obj);
            return res;
        }
    }
}