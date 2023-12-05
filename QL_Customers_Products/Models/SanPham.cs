using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QL_Customers_Products.Models
{
    class SanPham
    {
        public string IdSanPham { get; set; }
        public string TenSanPham { get; set; }
        public string IdLoaiSP { get; set; }
        public string AnhSP { get; set; }
        public long GiaGoc { get; set; }
        public long GiaDaGiam { get; set; }
        public int GiamGia { get; set; }

    }
}
