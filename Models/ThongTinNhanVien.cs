using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace BTL_Nhom12.Models
{
    [Table("ThongTinNhanVien")]
    public class ThongTinNhanVien
    {
        [Key]
        [Required(ErrorMessage ="Mã nhân viên không được bỏ trống")]
        [Display( Name = "Mã NV")]
        public string MaNhanVien { get; set; }
        [Required(ErrorMessage ="Tên nhân viên không được bỏ trống")]
        [Display( Name = "Tên NV")]
        public string TenNhanVien { get; set; }

        [Display( Name = "Giới tính NV")]
        public string GioiTinhNhanVien { get; set; }

        [Required(ErrorMessage ="Địa chỉ không được bỏ trống")]
        [Display( Name = "Địa chỉ")]
        public string DiaChi { get; set; }

        [Display( Name = "Số điện thoại")]
        public string Sdt { get; set; }

    }
}
