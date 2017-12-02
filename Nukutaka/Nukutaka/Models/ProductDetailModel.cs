using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Nukutaka.Models
{
    public class ProductDetailModel
    {
        NukutakaEntities db = new NukutakaEntities();
        public PRODUCT Detail(int id)
        {
            return db.PRODUCTS.SingleOrDefault(n => n.ID == id);
        }      
    }
}