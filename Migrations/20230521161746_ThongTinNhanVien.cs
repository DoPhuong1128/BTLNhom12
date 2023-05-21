using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BTLNhom12.Migrations
{
    /// <inheritdoc />
    public partial class ThongTinNhanVien : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Khachhang",
                columns: table => new
                {
                    Makhachhang = table.Column<string>(type: "TEXT", nullable: false),
                    Tenkhachhang = table.Column<string>(type: "TEXT", nullable: false),
                    Diachi = table.Column<string>(type: "TEXT", nullable: true),
                    Sodienthoai = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Khachhang", x => x.Makhachhang);
                });

            migrationBuilder.CreateTable(
                name: "Nhacungcap",
                columns: table => new
                {
                    MaNCC = table.Column<string>(type: "TEXT", nullable: false),
                    TenNCC = table.Column<string>(type: "TEXT", nullable: false),
                    SDTNCC = table.Column<string>(type: "TEXT", nullable: true),
                    DiaChi = table.Column<string>(type: "TEXT", nullable: false),
                    Email = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Nhacungcap", x => x.MaNCC);
                });

            migrationBuilder.CreateTable(
                name: "Sanpham",
                columns: table => new
                {
                    MaSanPham = table.Column<string>(type: "TEXT", nullable: false),
                    TenSanPham = table.Column<string>(type: "TEXT", nullable: false),
                    DVT = table.Column<string>(type: "TEXT", nullable: true),
                    NSX = table.Column<string>(type: "TEXT", nullable: true),
                    Kichco = table.Column<string>(type: "TEXT", nullable: true),
                    Color = table.Column<string>(type: "TEXT", nullable: true),
                    GiaTien = table.Column<int>(type: "INTEGER", nullable: false),
                    Soluongton = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sanpham", x => x.MaSanPham);
                });

            migrationBuilder.CreateTable(
                name: "ThongTinNhanVien",
                columns: table => new
                {
                    MaNhanVien = table.Column<string>(type: "TEXT", nullable: false),
                    TenNhanVien = table.Column<string>(type: "TEXT", nullable: false),
                    GioiTinhNhanVien = table.Column<string>(type: "TEXT", nullable: false),
                    DiaChi = table.Column<string>(type: "TEXT", nullable: false),
                    Sdt = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ThongTinNhanVien", x => x.MaNhanVien);
                });

            migrationBuilder.CreateTable(
                name: "Donhang",
                columns: table => new
                {
                    Madonhang = table.Column<string>(type: "TEXT", nullable: false),
                    NgayBan = table.Column<DateTime>(type: "TEXT", nullable: false),
                    MaSanPham = table.Column<string>(type: "TEXT", nullable: true),
                    Sanpham = table.Column<string>(type: "TEXT", nullable: true),
                    Makhachhang = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Donhang", x => x.Madonhang);
                    table.ForeignKey(
                        name: "FK_Donhang_Khachhang_Makhachhang",
                        column: x => x.Makhachhang,
                        principalTable: "Khachhang",
                        principalColumn: "Makhachhang");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Donhang_Makhachhang",
                table: "Donhang",
                column: "Makhachhang");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Donhang");

            migrationBuilder.DropTable(
                name: "Nhacungcap");

            migrationBuilder.DropTable(
                name: "Sanpham");

            migrationBuilder.DropTable(
                name: "ThongTinNhanVien");

            migrationBuilder.DropTable(
                name: "Khachhang");
        }
    }
}
