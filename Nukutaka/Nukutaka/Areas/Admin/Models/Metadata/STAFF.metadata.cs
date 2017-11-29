using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
//
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
namespace Nukutaka.Models
{
    [MetadataTypeAttribute(typeof(StaffMetadata))]
    public partial class STAFF
    {
        internal sealed class StaffMetadata
        {
            public int ID { get; set; }

            [Required(ErrorMessage = "Mã nhân viên không được bỏ trống")]
            [DisplayName("MÃ NHÂN VIÊN")]
            public string CODE { get; set; }

            [Required(ErrorMessage = "Tên nhân viên không được bỏ trống")]
            [DisplayName("TÊN NHÂN VIÊN")]
            public string NAME { get; set; }

            [Required(ErrorMessage = "Số điện thoại không được bỏ trống")]
            [DisplayName("SỐ ĐIỆN THOẠI")]
            public string PHONE { get; set; }
        }
    }
}