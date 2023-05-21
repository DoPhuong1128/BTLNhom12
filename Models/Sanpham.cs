using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace BTL_Nhom12.Models
{
    [Table("Sanpham")]
    public class Sanpham
    {
        [Key]
        [Required(ErrorMessage = "Mã sản phẩm không được để trống !!!")]
        public string? MaSanPham { get; set; }
        [Required(ErrorMessage = "Tên sản phẩm không được để trống !!!")]
        public string? TenSanPham{ get; set; }
        public string? DVT { get; set; }
        public string? NSX { get; set; }
        public string? Kichco { get; set; }
        public string? Color { get; set; }
        public int GiaTien { get; set; }
        public int Soluongton { get; set; }
        
    }
}