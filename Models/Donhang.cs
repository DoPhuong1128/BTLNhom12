using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace BTL_Nhom12.Models
{
    [Table("Donhang")]
    public class Donhang

    {
    
    [Key]
        [Required(ErrorMessage = "Mã  sản phẩm không được để trống !!!")]
        public string? Madonhang { get; set; }
        [DataType(DataType.Date)]
        public DateTime NgayBan { get; set; }

        public string? MaSanPham { get; set; }
        [ForeignKey("MaSanPham")]
        public string? Sanpham { get; set; } 
        public string? Makhachhang { get; set; }
        [ForeignKey("Makhachhang")]
        public Khachhang? Khachhang{ get; set; }


    }
}