﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace QL_Customers_Products
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class QLKH_SPEntities : DbContext
    {
        public QLKH_SPEntities()
            : base("name=QLKH_SPEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<ChienDichQuangCao> ChienDichQuangCao { get; set; }
        public virtual DbSet<ChiNhanh> ChiNhanh { get; set; }
        public virtual DbSet<ChiTietChienDichQuangCao> ChiTietChienDichQuangCao { get; set; }
        public virtual DbSet<ChiTietHoaDon> ChiTietHoaDon { get; set; }
        public virtual DbSet<ChiTietHoaDonNhapHang> ChiTietHoaDonNhapHang { get; set; }
        public virtual DbSet<ChucVu> ChucVu { get; set; }
        public virtual DbSet<HoaDon> HoaDon { get; set; }
        public virtual DbSet<KhachHang> KhachHang { get; set; }
        public virtual DbSet<KhoHang> KhoHang { get; set; }
        public virtual DbSet<LoaiSanPham> LoaiSanPham { get; set; }
        public virtual DbSet<LoaiSanPhamCha> LoaiSanPhamCha { get; set; }
        public virtual DbSet<NguoiDung> NguoiDung { get; set; }
        public virtual DbSet<NhaCungCap> NhaCungCap { get; set; }
        public virtual DbSet<NhanVien> NhanVien { get; set; }
        public virtual DbSet<NhapHang> NhapHang { get; set; }
        public virtual DbSet<QuangCao> QuangCao { get; set; }
        public virtual DbSet<QuayThanhToan> QuayThanhToan { get; set; }
        public virtual DbSet<SanPham> SanPham { get; set; }
        public virtual DbSet<TheThanhVien> TheThanhVien { get; set; }
        public virtual DbSet<YkienKH> YkienKH { get; set; }
    }
}