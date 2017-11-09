using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Nukutaka.Models;

namespace Nukutaka.Models
{
    public class CategoryModel
    {
        NukutakaEntities db = new NukutakaEntities();

        public List<CATEGORY> ListCatagory()
        {
            var listCategory = db.CATEGORies.ToList();
            return listCategory;
        }

        public List<PRODUCT> ListProductByCategory(string codeCategory)
        {
            var list = db.PRODUCTS.Where(n=>n.CODECATEGORY == codeCategory).ToList();
            return list;
        }
        
        public List<KIND> ListKindByCategory(string codeCategory)
        {
            var list = db.KINDs.Where(n => n.CODECATEGORY == codeCategory).ToList();
            return list;
        }

        public List<PRODUCT> ListProductByKindandCate(string codeCategory, string codeKind)
        {
            var list = db.PRODUCTS.Where(n => n.CODECATEGORY == codeCategory && n.CODEKIND == codeKind).ToList();
            return list;
        }
    }
}