using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
//
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
namespace Nukutaka.Models
{
    [MetadataTypeAttribute(typeof(InvoiceMetadata))]
    public partial class INVOICE
    {
        internal sealed class InvoiceMetadata
        {
            [DisplayName("MÃ ĐƠN HÀNG")]
            public string CODE { get; set; }

            [DisplayName("TÊN KHÁCH HÀNG")]
            public string NAMECUSTOMER { get; set; }

            [DisplayName("SỐ ĐIỆN THOẠI")]
            public string PHONECUSTOMER { get; set; }

            [DisplayName("ĐỊA CHỈ")]
            public string ADDRESS { get; set; }

            [DisplayName("NGÀY ĐẶT HÀNG")]
            [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
            public Nullable<System.DateTime> ORDERDATE { get; set; }

            [DisplayName("NGÀY CHUYỂN HÀNG")]
            public Nullable<System.DateTime> SHIPPINGDATE { get; set; }

            [DisplayName("NGÀY NHẬN HÀNG")]
            public Nullable<System.DateTime> DELIVERYDATE { get; set; }

            [DisplayName("NGƯỜI VẬN CHUYỂN")]
            public string SHIPPER { get; set; }

            [DisplayName("TRẠNG THÁI")]
            public Nullable<int> STATUS { get; set; }

            [DisplayName("SẢN PHẨM")]
            public string PRODUCT { get; set; }

            [DisplayName("LOẠI")]
            public Nullable<int> TYPE { get; set; }
        }
    }
}