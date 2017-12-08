using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Nukutaka.Models;
using Newtonsoft.Json.Linq;
using System.Data.SqlClient;

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
            List<CartModel> listCart = getCart();
            ViewBag.TotalQuantity = TotalQuantity().ToString();
            ViewBag.TotalPrice = TotalPrice().ToString();
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
            if (txtName.Trim() == null || txtName.Trim() == "" || txtName.Trim() == " " || txtPhone.Trim() == null || txtPhone.Trim() == "" || txtPhone.Trim() == " " || txtSonha.Trim() == null || txtSonha.Trim() == "" || txtSonha.Trim() == " " || txtXaphuong.Trim() == null || txtXaphuong.Trim() == "" || txtXaphuong.Trim() == " " || txtThixa.Trim() == null || txtThixa.Trim() == "" || txtThixa.Trim() == " " || txtTinh.Trim() == null || txtTinh.Trim() == "" || txtTinh.Trim() == " ")
            {
                return RedirectToAction("Cart", "Cart");
            }
            var address = txtSonha + ", " + txtXaphuong + ", " + txtThixa + ", " + txtTinh;
            if (Session["Cart"] == null)
            {
                RedirectToAction("Index", "Home");
            }

            // JSON CART
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

            //INSERT INTO DATABASE
            INVOICE inv = new INVOICE();
            inv.CODE = CodeInvoice();
            inv.NAMECUSTOMER = txtName;
            inv.PHONECUSTOMER = txtPhone;
            inv.ADDRESS = address;
            inv.ORDERDATE = DateTime.Now;
            inv.PRODUCT = arr.ToString();
            inv.STATUS = 1;
            inv.TYPE = 0;
            db.INVOICEs.Add(inv);
            db.SaveChanges();

            Session["Cart"] = null;
            return RedirectToAction("Success");
        }

        public ActionResult OnlinePayment()
        {
            //CHECK SESSION EXIST
            if (Session["Cart"] == null)
            {
                RedirectToAction("Index", "Home");
            }
            //GET CODE INVOICE
            var codeInvoice = CodeInvoice();
            var totalPrice = TotalPrice();

            // JSON CART
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

            // INSERT DATABASE
            INVOICE inv = new INVOICE();
            inv.CODE = codeInvoice;
            inv.PRODUCT = arr.ToString();
            inv.ORDERDATE = DateTime.Now;
            inv.STATUS = 0;
            inv.TYPE = 1;
            db.INVOICEs.Add(inv);
            db.SaveChanges();
            Session["Cart"] = null;
            return Redirect("https://www.baokim.vn/payment/product/version11?business=phucnguyen090895%40gmail.com&id=&order_description=&product_name=" + codeInvoice + "&product_price=" + totalPrice + "&product_quantity=1&total_amount=" + totalPrice + "&url_cancel=http://nukutaka.azurewebsites.net/Cart/CancelPayment?code=" + codeInvoice + "&url_detail=&url_success=http://nukutaka.azurewebsites.net/Cart/SuccessPayment?code=" + codeInvoice + "");
        }

        public ActionResult Success()
        {
            return View();
        }

        [HttpGet]
        public ActionResult SuccessPayment(string code)
        {
            INVOICE inv = db.INVOICEs.SingleOrDefault(n => n.CODE == code);
            return View(inv);
        }

        [HttpPost]
        public ActionResult SuccessPayment(INVOICE inv, FormCollection f)
        {
            var name = f.Get("txtName");
            var phone = f.Get("txtPhone");
            var soNha = f.Get("txtSonha");
            var xaPhuong = f.Get("txtXaphuong");
            var thiXa = f.Get("txtThixa");
            var tinh = f.Get("txtTinh");
            var address = soNha + ", " + xaPhuong + ", " + thiXa + ", " + tinh;     

            if (name.Trim() == null || name.Trim() == "" || name.Trim() == " " || phone.Trim() == null || phone.Trim() == "" || phone.Trim() == " ")
            {
                ViewBag.Error = "Hãy nhập đầy đủ thông tin cá nhân để được liên hệ sớm nhất";
                return View(inv);
            }

            if (soNha.Trim() == null || soNha.Trim() == "" || soNha.Trim() == " " || xaPhuong.Trim() == null || xaPhuong.Trim() == "" || xaPhuong.Trim() == " " || thiXa.Trim() == null || thiXa.Trim() == "" || thiXa.Trim() == " " || tinh.Trim() == null || tinh.Trim() == "" || tinh.Trim() == " ")
            {
                ViewBag.Error = "Hãy nhập đầy đủ địa chỉ để được giao hàng nhanh nhất";
                return View(inv);
            }
            int res = UpdateInfoInvoice(inv.CODE, name, phone, address);
            if(res > 0)
            {
                return View("Success");
            }
            else
            {
                return View(inv);
            }
            
        }

        public ActionResult CancelPayment(string code)
        {
            INVOICE inv = db.INVOICEs.SingleOrDefault(n => n.CODE == code);
            if (inv == null)
            {
                return RedirectToAction("Page404", "Home");
            }
            db.INVOICEs.Remove(inv);
            db.SaveChanges();
            return RedirectToAction("Index", "Home");
        }

        public string CodeInvoice()
        {
            var codeInvoice = "HD" + DateTime.Now.Day.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Year.ToString() + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString();
            return codeInvoice;
        }

        public int UpdateInfoInvoice(string code, string name, string phone, string address)
        {
            object[] obj =
            {
                new SqlParameter("@P_CODE", code),
                new SqlParameter("@P_NAMECUSTOMER", name),
                new SqlParameter("@P_PHONECUSTOMER", phone),
                new SqlParameter("@P_ADDRESS", address)
            };
            int res = db.Database.ExecuteSqlCommand("UPDATE_INFO_INVOICE @P_CODE, @P_NAMECUSTOMER, @P_PHONECUSTOMER, @P_ADDRESS", obj);
            return res;

        }
    }
}