using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Nukutaka.Models;

namespace Nukutaka.Areas.Admin.Controllers
{
    public class MANInvoiceController : BaseController
    {
        NukutakaEntities db = new NukutakaEntities();
        // GET: Admin/MANInvoice
        public ActionResult Index()
        {
            var list = db.INVOICEs.ToList();
            return View(list);
        }

        [HttpGet]
        public ActionResult UpdateInvoice(int id)
        {
            INVOICE inv = db.INVOICEs.SingleOrDefault(n => n.ID == id);
            ddlShipper(inv);
            return View(inv);
        }

        [HttpPost]
        public ActionResult UpdateInvoice(INVOICE inv)
        {
            ddlShipper(inv);
            if (ModelState.IsValid)
            {
                db.Entry(inv).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index"); 
            }
            return View(inv);
        }

        public ActionResult DeleteInvoice(int id)
        {
            INVOICE inv = db.INVOICEs.SingleOrDefault(n => n.ID == id);
            if (inv == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            db.INVOICEs.Remove(inv);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public void ddlShipper(INVOICE inv)
        {
            ViewBag.SHIPPER = new SelectList(db.STAFFs.ToList().OrderBy(n=>n.NAME), "CODE", "NAME", inv.SHIPPER );
        }
    }
}