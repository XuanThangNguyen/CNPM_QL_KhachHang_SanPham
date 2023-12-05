using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Linq.Expressions;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace QL_Customers_Products
{
    public class AppDataContext: DbContext
    {
        public AppDataContext() : base("Server = DESKTOP-UE7V70U\\SQLEXPRESS; Database=QLKH_SP;User=sa;Password=123")
        { }
        public DbSet<NhaCungCap> NhaCungCaps { get; set; }
        public DbSet<KhoHang> KhoHangs { get; set; }
        public DbSet<NhapHang> NhapHangs { get; set; }
        public DbSet<ChiTietHoaDonNhapHang> chiTietHoaDonNhapHangs { get; set; }
        public DbSet<NhanVien> nhanViens { get; set; }
    }
}
