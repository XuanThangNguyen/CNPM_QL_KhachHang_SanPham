//------------------------------------------------------------------------------
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
    using System.Collections.Generic;
    
    public partial class NhapHang
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public NhapHang()
        {
            this.ChiTietHoaDonNhapHang = new HashSet<ChiTietHoaDonNhapHang>();
        }
    
        public string IdNhapHang { get; set; }
        public string IdNhanVien { get; set; }
        public Nullable<long> TongTien { get; set; }
        public string PhuongThucThanhToan { get; set; }
        public System.DateTime ThoiGianTao { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ChiTietHoaDonNhapHang> ChiTietHoaDonNhapHang { get; set; }
        public virtual NhanVien NhanVien { get; set; }
    }
}