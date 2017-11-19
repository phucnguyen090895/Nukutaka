using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Nukutaka.Models;
using Newtonsoft.Json.Linq;

namespace Nukutaka.Controllers
{
    public class CartController : Controller
    {
        NukutakaEntities db = new NukutakaEntities();
        // GET CART
        public List<CartModel> getCart()
        {
            List<CartModel> listCart = Session["Cart"] as List<CartModel>;
            if (listCart == null)
            {
                listCart = new List<CartModel>();
                Session["Cart"] = listCart;

            }
            return listCart;
        }
        // ADD ITEM INTO CART
        public ActionResult AddCart(string code, string urlCurrent, int iQuantity)
        {
            if (iQuantity == 0)
            {
                iQuantity = 1;
            }
            PRODUCT product = db.PRODUCTS.SingleOrDefault(n => n.CODE == code);
            if (product == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            List<CartModel> listCart = getCart();
            // Check Item Product Exists
            CartModel item = listCart.Find(n => n.productCode == code);
            if (item == null)
            {
                item = new CartModel(code, iQuantity);
                listCart.Add(item);
                return Redirect(urlCurrent);
            }
            else
            {
                item.quantity += iQuantity;
                return Redirect(urlCurrent);
            }
        }
        //UPDATE ITEM CART
        public ActionResult UpdateCart(string code, FormCollection f)
        {
            PRODUCT product = db.PRODUCTS.SingleOrDefault(n => n.CODE == code);
            if (product == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            List<CartModel> listCart = getCart();
            // Check Item Product Exists
            CartModel item = listCart.Find(n => n.productCode == code);
            if (item != null)
            {
                item.quantity = int.Parse(f["txtQuantity"].ToString());
            }
            return View("Cart");
        }
        //DELETE ITEM INTO CARD
        public ActionResult DeleteCart(string code)
        {
            PRODUCT product = db.PRODUCTS.SingleOrDefault(n => n.CODE == code);
            if (product == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            List<CartModel> listCart = getCart();
            // Check Item Product Exists
            CartModel item = listCart.Find(n => n.productCode == code);
            if (item != null)
            {
                listCart.RemoveAll(n => n.productCode == code);
            }
            if (listCart.Count == 0)
            {
                Session["Cart"] = null;
                return RedirectToAction("Index", "Home");
            }
            return RedirectToAction("Cart");
        }
        // RETURN VIEW CART
        public ActionResult Cart()
        {
            if (Session["Cart"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            var codeInvoice = "HD" + DateTime.Now.Day.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Year.ToString() + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString();
            List<CartModel> listCart = getCart();
            ViewBag.TotalQuantity = TotalQuantity().ToString();
            ViewBag.TotalPrice = TotalPrice().ToString();
            ViewBag.TT = "https://www.baokim.vn/payment/product/version11?business=phucnguyen090895%40gmail.com&id=&order_description=&product_name="+ codeInvoice + "&product_price=" + TotalPrice() + "&product_quantity=1&total_amount=" + TotalPrice() + "&url_cancel=&url_detail=&url_success=";
            return View(listCart);
        }

        public int TotalQuantity()
        {
            int total = 0;
            List<CartModel> listCart = Session["Cart"] as List<CartModel>;
            if (listCart != null)
            {
                total = listCart.Sum(n => n.quantity);
            }
            return total;

        }

        public double TotalPrice()
        {
            double total = 0;
            List<CartModel> listCart = Session["Cart"] as List<CartModel>;
            if (listCart != null)
            {
                total = listCart.Sum(n => n.total);
            }
            return total;
        }

        //COUNT QUANTITY
        public ActionResult CountProduct()
        {
            ViewBag.CountProduct = TotalQuantity();
            return View();
        }

        //ADD INVOICE INTO DATABASE
        public ActionResult InsertCart(string txtName, string txtPhone, string txtSonha, string txtXaphuong, string txtThixa, string txtTinh)
        {
            var address = txtSonha + ", " + txtXaphuong + ", " + txtThixa + ", " + txtTinh;

            var codeInvoice = "HD" + DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Day.ToString() + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString();

            if (Session["Cart"] == null)
            {
                RedirectToAction("Index", "Home");
            }
            //INSERT
            INVOICE inv = new INVOICE();
            inv.CODE = codeInvoice;
            inv.NAMECUSTOMER = txtName;
            inv.PHONECUSTOMER = txtPhone;
            inv.ADDRESS = address;
            inv.ORDERDATE = DateTime.Now;
            inv.STATUS = 0;
            db.INVOICEs.Add(inv);
            db.SaveChanges();
            List<CartModel> cart = getCart();

            JArray arr = new JArray();
            foreach (var item in cart)
            {
                JObject obj = new JObject(
                    new JProperty("code", item.productCode),
                    new JProperty("quantity", item.quantity),
                    new JProperty("price", item.price)
                    );
                arr.Add(obj);

            }

            INVOICE_DETAILS invDetail = new INVOICE_DETAILS();
            invDetail.CODE = codeInvoice;
            invDetail.PRODUCT = arr.ToString();
            db.INVOICE_DETAILS.Add(invDetail);
            db.SaveChanges();
            Session["Cart"] = null;
            return RedirectToAction("Index", "Home");
            // return View();
        }
    }
}