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
    
    public partial class TheThanhVien
    {
        public string IdThe { get; set; }
        public string IdKhachHang { get; set; }
        public System.DateTime NgayDangKy { get; set; }
        public string SoCCCD { get; set; }
        public Nullable<long> DiemTichLuy { get; set; }
    
        public virtual KhachHang KhachHang { get; set; }
    }
}
