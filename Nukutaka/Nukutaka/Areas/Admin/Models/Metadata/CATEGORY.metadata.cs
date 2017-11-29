using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
// 
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
namespace Nukutaka.Models
{
    [MetadataTypeAttribute(typeof(CategoryMetadata))]
    public partial class CATEGORY
    {
        internal sealed class CategoryMetadata
        {
            public int ID { get; set; }

            [Required(ErrorMessage = "Mã danh mục không được bỏ trống")]
            [DisplayName("MÃ DANH MỤC")]
            public string CODE { get; set; }

            [Required(ErrorMessage = "Tên danh mục không được bỏ trống")]
            [DisplayName("TÊN DANH MỤC")]
            public string NAME { get; set; }
        }
    }
}