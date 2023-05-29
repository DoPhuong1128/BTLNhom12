using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace BTL_Nhom12.Models
{
    [Table("Khachhang")]
    public class Khachhang
    {
        [Key]
        [Required(ErrorMessage = "Mã  khách  hàng không được để trống !!!")]
        public string? Makhachhang { get; set; }
        [Required(ErrorMessage = "Tên khách hàng không được để trống !!!")]
        public string? Tenkhachhang{ get; set; }
    
        public string? Diachi { get; set; }
        public string Sodienthoai { get; set; }
        
    }
}