using BTL_Nhom12.Models;
using Microsoft.EntityFrameworkCore;

namespace BTL_Nhom12.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext (DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<ThongTinNhanVien> ThongTinNhanVien { get; set; }
        public DbSet<Khachhang> Khachhang {get; set;}
        public DbSet<Nhacungcap> Nhacungcap {get; set;}
        public DbSet<Sanpham> Sanpham {get; set;}
        public DbSet<Donhang> Donhang{get; set;}
       
    }
}