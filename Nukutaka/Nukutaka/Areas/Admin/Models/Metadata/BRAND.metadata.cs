using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
//
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
namespace Nukutaka.Models
{
    [MetadataTypeAttribute(typeof(BrandMetadata))]
    public partial class BRAND
    {
        internal sealed class BrandMetadata
        {
            public int ID { get; set; }

            [Required(ErrorMessage = "Mã thương hiệu không được bỏ trống")]
            [DisplayName("MÃ THƯƠNG HIỆU")]
            public string CODE { get; set; }

            [Required(ErrorMessage = "Tên thương hiệu không được bỏ trống")]
            [DisplayName("TÊN THƯƠNG HIỆU")]
            public string NAME { get; set; }
        }
    }
}