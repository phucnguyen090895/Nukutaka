using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Nukutaka.Models
{
    public class CartModel
    {
        NukutakaEntities db = new NukutakaEntities();
        public int id { get; set; }
        public string productCode { get; set; }
        public string productName { get; set; }
        public string imageURL { get; set; }
        public double price { get; set; }
        public int quantity { get; set; }
        public double total
        {
            get { return quantity * price; }
        }

        public CartModel(string code, int iQuantity)
        {
            productCode = code;
            PRODUCT product = db.PRODUCTS.Single(n => n.CODE == productCode);
            id = product.ID;
            productName = product.NAME;
            imageURL = product.IMAGEURL;
            price = double.Parse(product.PRICE.ToString());
            quantity = iQuantity;
            
        }

    }
}