using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
// 
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
namespace Nukutaka.Models
{
    [MetadataTypeAttribute(typeof(ProductMetadata))]
    public partial class PRODUCT
    {
        internal sealed class ProductMetadata
        {
            public int ID { get; set; }

            [Required(ErrorMessage = "Mã sản phẩm không được bỏ trống")]
            [DisplayName("MÃ SẢN PHẨM")]
            public string CODE { get; set; }

            [Required(ErrorMessage = "Tên sản phẩm không được bỏ trống")]
            [DisplayName("TÊN SẢN PHẨM")]
            public string NAME { get; set; }

            [DisplayName("ĐƠN GIÁ")]
            [Range(0, Int32.MaxValue)]
            public Nullable<int> PRICE { get; set; }

            [DisplayName("HÌNH ẢNH")]
            public string IMAGEURL { get; set; }

            [DisplayName("DANH MỤC THỜI TRANG")]
            public string CODECATEGORY { get; set; }

            [DisplayName("THƯƠNG HIỆU")]
            public string CODEBRAND { get; set; }

            [DisplayName("LOẠI")]
            public string CODEKIND { get; set; }

            [DisplayName("TRẠNG THÁI")]
            public Nullable<int> STATUS { get; set; }

            [DisplayName("TỒN KHO")]
            public Nullable<int> INSTOCK { get; set; }
        }
    }
}