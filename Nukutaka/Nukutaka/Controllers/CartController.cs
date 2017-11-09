using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Nukutaka.Models;

namespace Nukutaka.Controllers
{
    public class CartController : Controller
    {
        NukutakaEntities db = new NukutakaEntities();
        // GET CART
        public List<CartModel> getCart()
        {
            List<CartModel> listCart = Session["Cart"] as List<CartModel>;
            if(listCart == null)
            {
                listCart = new List<CartModel>();
                Session["Cart"] = listCart;

            }
            return listCart;
        }
        // ADD ITEM INTO CART
        public ActionResult AddCart(string code, string urlCurrent, int iQuantity )
        {           
            if (iQuantity == 0)
            {
                iQuantity = 1;
            }
            PRODUCT product = db.PRODUCTS.SingleOrDefault(n => n.CODE == code);
            if(product == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            List<CartModel> listCart = getCart();
            // Check Item Product Exists
            CartModel item = listCart.Find(n => n.productCode == code);
            if(item == null)
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
            if(item != null)
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
            if(item != null)
            {
                listCart.RemoveAll(n => n.productCode == code);
            }
            if (listCart.Count == 0)
            {
                return RedirectToAction("Index", "Home");
            }
            return RedirectToAction("Cart");
        }
        // RETURN VIEW CART
        public ActionResult Cart()
        {
            if(Session["Cart"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            List<CartModel> listCart = getCart();
            ViewBag.TotalQuantity = TotalQuantity().ToString();
            ViewBag.TotalPrice = TotalPrice().ToString();
            return View(listCart);
        }
        
        public int TotalQuantity()
        {
            int total = 0;
            List <CartModel> listCart = Session["Cart"] as List<CartModel>;
            if(listCart != null)
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
        //ADD INVOICE INTO DATABASE
        public ActionResult InsertCart(string txtName, string txtPhone, string txtSonha, string txtXaphuong, string txtThixa, string txtTinh)
        {
            ViewBag.address = "ABC";
            
            //if(Session["Cart"] == null)
            //{
            //    RedirectToAction("Index", "Home");
            //}
            ////INSERT
            //INVOICE inv = new INVOICE();
            //List<CartModel> cart = new List<CartModel>();

            return View();
        }
    }
}