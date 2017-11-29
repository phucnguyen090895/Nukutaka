using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
//
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
namespace Nukutaka.Models
{
    [MetadataTypeAttribute(typeof(KindMetadata))]
    public partial class KIND
    {  
        internal sealed class KindMetadata
        {
            public int ID { get; set; }

            [Required(ErrorMessage = "Mã loại không được bỏ trống")]
            [DisplayName("MÃ LOẠI")]
            public string CODE { get; set; }

            [Required(ErrorMessage = "Tên loại không được bỏ trống")]
            [DisplayName("TÊN LOẠI")]
            public string NAME { get; set; }

            [Required(ErrorMessage = "Mã danh mục không được bỏ trống")]
            [DisplayName("MÃ DANH MỤC")]
            public string CODECATEGORY { get; set; }
        }
    }
}