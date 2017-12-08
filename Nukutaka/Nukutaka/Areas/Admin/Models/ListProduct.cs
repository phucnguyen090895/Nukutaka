using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Nukutaka.Areas.Admin.Models
{
    public class ListProduct
    {
        public string code { get; set; }
        public int quantity { get; set; }
        public double price { get; set; }
    }
}