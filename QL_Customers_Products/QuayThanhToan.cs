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
    
    public partial class QuayThanhToan
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public QuayThanhToan()
        {
            this.HoaDon = new HashSet<HoaDon>();
        }
    
        public string IdQuayThanhToan { get; set; }
        public string IdChiNhanh { get; set; }
        public string TenQuay { get; set; }
    
        public virtual ChiNhanh ChiNhanh { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<HoaDon> HoaDon { get; set; }
    }
}
