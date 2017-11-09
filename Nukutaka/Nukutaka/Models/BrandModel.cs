using Nukutaka.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Nukutaka.Controllers
{
    public class BrandModel
    {
        NukutakaEntities db = new NukutakaEntities();

        public List<BRAND> ListBrand()
        {
            var listBrand = db.BRANDS.ToList();
            return listBrand;
        }

        public List<PRODUCT> ListProductByBrand(string codeBrand)
        {
            var list = db.PRODUCTS.Where(n => n.CODEBRAND == codeBrand).ToList();
            return list;
        }

        public List<PRODUCT> countProduct(string code)
        {
            var countProduct = db.PRODUCTS.Where(n=>n.CODEBRAND == code).ToList();
            return countProduct;
        }
    }
}