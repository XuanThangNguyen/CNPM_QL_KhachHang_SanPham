using QL_Customers_Products.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QL_Customers_Products
{
    public partial class frmThanhToan : Form
    {
        public static string idNguoiDungHienTai { get; set; } // Biến tĩnh để lưu thông tin đăng nhập

        SQLConfig config = new SQLConfig();
        string sql;

        private Timer timer = new Timer();
        public frmThanhToan()
        {
            InitializeComponent();
            LoadListIdSanPham();
            LoadChiNhanh();

            dtpNgayBan.Format = DateTimePickerFormat.Custom;
            dtpNgayBan.CustomFormat = "dd/MM/yyyy HH:mm:ss";

            // Đặt Interval của Timer thành 1000 milliseconds (1 giây)
            timer.Interval = 1000;
            timer.Tick += Timer_Tick;
            timer.Start();
        }
        private void Timer_Tick(object sender, EventArgs e)
        {
            dtpNgayBan.Value = DateTime.Now;
        }
        private void btnCloseForm_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Bạn có chắc muốn thoát?", "Thoát chương trình", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                Application.Exit();
            }
        }
        public void LoadListIdSanPham()
        {
            try
            {
                sql = "SELECT IdSanPham FROM SanPham";
                DataTable dataTable = config.ExecuteTableQuery(sql);

                List<string> listIdSanPham = new List<string>();
                if (dataTable.Rows.Count > 0)
                {
                    //foreach (DataRow row in dataTable.Rows)
                    //{
                    //    SanPham sp = new SanPham();
                    //    sp.IdSanPham = row["IdSanPham"].ToString();
                    //    sp.TenSanPham = row["TenSanPham"].ToString();
                    //    sp.IdLoaiSP = row["IdLoaiSP"].ToString();
                    //    sp.AnhSP = row["AnhSP"].ToString();
                    //    sp.GiaGoc = long.Parse( row["GiaGoc"].ToString());
                    //    sp.GiaDaGiam = long.Parse(row["GiaDaGiam"].ToString());
                    //    sp.GiamGia = int.Parse(row["GiamGia"].ToString());
                    //    listSanPham.Add(sp);
                    //}

                    foreach (DataRow row in dataTable.Rows)
                    {
                        listIdSanPham.Add(row["IdSanPham"].ToString());
                    }
                }
                else
                    listIdSanPham.Add("Chưa có sản phẩm");
                cbo_IdSanPham.DataSource = listIdSanPham;
                cbo_IdSanPham.SelectedIndex = -1;
            }
            catch (SqlException ex)
            {
                Console.WriteLine($"Lỗi SQL: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi: {ex.Message}");
            }
        }
        public void LoadChiNhanh()
        {
            try
            {
                sql = "SELECT * FROM ChiNhanh";
                DataTable dataTable = config.ExecuteTableQuery(sql);

                List<string> listChiNhanh = new List<string>();
                if (dataTable.Rows.Count > 0)
                {
                    foreach (DataRow row in dataTable.Rows)
                    {
                        listChiNhanh.Add(row["TenChiNhanh"].ToString());
                    }
                }
                else
                    listChiNhanh.Add("Chưa có chi nhánh");
                cboChiNhanh.DataSource = listChiNhanh;
                cboChiNhanh.SelectedIndex = -1;
            }
            catch (SqlException ex)
            {
                Console.WriteLine($"Lỗi SQL: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi: {ex.Message}");
            }
        }
        public void FormThanhToan_Load(object sender, EventArgs e)
        {
            txt_IdHoaDon.Text = SharedMethods.TaoIdHoaDonDuyNhat();
            nud_SoLuong.Value = 1;
            txt_TenSanPham.Text = null;
            txt_DonGia.Text = null;
            nud_GiamGia.Value = 0;
            txt_ThanhTien.Text = null;
            try
            {
                sql = "SELECT * FROM NhanVien WHERE IdNguoiDung = '" + idNguoiDungHienTai + "'";
                DataTable dataTable = config.ExecuteTableQuery(sql);

                if (dataTable.Rows.Count > 0)
                {

                    foreach (DataRow row in dataTable.Rows)
                    {
                        txt_IdNhanVien.Text = row["IdNhanVien"].ToString();
                        txt_TenNhanVien.Text = row["TenNhanVien"].ToString();
                    }
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine($"Lỗi SQL: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi: {ex.Message}");
            }
        }

        public void txt_SoDienThoai_Leave(object sender, EventArgs e)
        {
            string soDienThoai = txt_SoDienThoai.Text;

            try
            {
                if (txt_SoDienThoai.Text != null)
                {
                    sql = "SELECT IdKhachHang, TenKhachHang, DiaChi FROM KhachHang WHERE SoDienThoai = '" + soDienThoai + "'";
                    DataTable dataTable = config.ExecuteTableQuery(sql);

                    if (dataTable.Rows.Count > 0)
                    {
                        foreach (DataRow row in dataTable.Rows)
                        {
                            txt_IdKhachHang.Text = row["IdKhachHang"].ToString();
                            txt_TenKhachHang.Text = row["TenKhachHang"].ToString();
                            txt_DiaChi.Text = row["DiaChi"].ToString();
                        }
                    }
                    else
                    {
                        // Nếu không tìm thấy thông tin, xóa các TextBox
                        txt_TenKhachHang.Clear();
                        txt_DiaChi.Clear();
                        txt_SoDienThoai.Clear();
                    }
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine($"Lỗi SQL: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi: {ex.Message}");
            }
        }
        private void txt_SoDienThoai_TextChanged(object sender, EventArgs e)
        {
            if (txt_SoDienThoai.Text.Length == 0)
            {
                txt_IdKhachHang.Text = string.Empty;
                txt_TenKhachHang.Text = string.Empty;
                txt_DiaChi.Text = string.Empty;
            }
        }
        public void cmb_IdSanPham_SelectedValueChanged(object sender, EventArgs e)
        {
            try
            {
                ComboBox cmb = sender as ComboBox;
                if (cmb.SelectedItem != null)
                {
                    sql = "SELECT * FROM SanPham WHERE IdSanPham = '" + cbo_IdSanPham.Text + "'";
                    DataTable dataTable = config.ExecuteTableQuery(sql);

                    if (dataTable.Rows.Count > 0)
                    {
                        foreach (DataRow row in dataTable.Rows)
                        {
                            txt_TenSanPham.Text = row[1].ToString();
                            long giaGoc = int.Parse(row[4].ToString());
                            txt_DonGia.Text = giaGoc.ToString();
                            long giamGia = int.Parse(row[5].ToString());
                            nud_GiamGia.Value = giamGia;
                            txt_ThanhTien.Text = (giaGoc*(100-giamGia)/100).ToString();

                        }
                    }
                    SanPham sp = new SanPham();

                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine($"Lỗi SQL: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi: {ex.Message}");
            }
        }



        private void btnThemSanPham_Click(object sender, EventArgs e)
        {
            if (cbo_IdSanPham.Text != "" && txt_TenSanPham.Text != "")
            {
                string idSanPham = cbo_IdSanPham.Text;
                string tenSanPham = txt_TenSanPham.Text;
                string donGia = txt_DonGia.Text;
                int soLuong = (int)nud_SoLuong.Value;
                int giamGia = (int)nud_GiamGia.Value;
                string thanhTien = txt_ThanhTien.Text;

                bool daTonTai = false;

                foreach (ListViewItem item in lsvSanPham.Items)
                {
                    if (item.SubItems[0].Text == idSanPham)
                    {
                        // Tăng số lượng lên 1
                        item.SubItems[3].Text = (int.Parse(item.SubItems[3].Text) + soLuong).ToString();

                        item.SubItems[5].Text = (int.Parse(item.SubItems[3].Text) * (int.Parse(item.SubItems[2].Text) - int.Parse(item.SubItems[2].Text) * int.Parse(item.SubItems[4].Text) / 100)).ToString();
                        daTonTai = true;
                        break;
                    }
                }

                if (!daTonTai)
                {
                    ListViewItem item = lsvSanPham.Items.Add(idSanPham);
                    item.SubItems.Add(tenSanPham);
                    item.SubItems.Add(donGia);
                    item.SubItems.Add(soLuong.ToString());
                    item.SubItems.Add(giamGia.ToString());
                    item.SubItems.Add(thanhTien);
                }
                int tongTien = 0;
                foreach (ListViewItem item in lsvSanPham.Items)
                {
                    tongTien += int.Parse(item.SubItems[5].Text);
                }
                txt_TongTien.Text = tongTien.ToString("#,##0đ");
            }
        }

        private void txt_TienKhachDua_Leave(object sender, EventArgs e)
        {
            if (txt_TongTien.Text != "" && txt_TienKhachDua.Text != "")
            {
                string tongTienStr = txt_TongTien.Text.Replace("đ", "").Replace(".", "");
                int tongTienInt = int.Parse(tongTienStr);
                txt_TienThoi.Text = (int.Parse(txt_TienKhachDua.Text) - tongTienInt).ToString("#,##0đ");
            }
        }

        private void resetHoaDon_Click(object sender, EventArgs e)
        {
            ResetHoaDon();
        }
        private void ResetHoaDon()
        {
            txt_SoDienThoai.Text = string.Empty;
            cbo_IdSanPham.SelectedIndex = -1;
            txt_TenSanPham.Text = string.Empty;
            txt_DonGia.Text = string.Empty;
            nud_SoLuong.Value = 1;
            nud_GiamGia.Value = 0;
            txt_ThanhTien.Text = string.Empty;
            lsvSanPham.Items.Clear();
            txt_TongTien.Text = string.Empty;
            txt_TienKhachDua.Text = string.Empty;
            txt_TienThoi.Text = string.Empty;

        }

        private void taoHoaDon_Click(object sender, EventArgs e)
        {
            try
            {
                if(cboChiNhanh.SelectedIndex == -1)
                {
                    MessageBox.Show("Vui lòng chọn chi nhánh!!!","Thông báo",MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else if(cboQuayThanhToan.SelectedIndex == -1)
                {
                    MessageBox.Show("Vui lòng chọn quầy thanh toán!!!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    string tongTienStr = txt_TongTien.Text.Replace("đ", "").Replace(".", "");
                    if (lsvSanPham.Items.Count > 0)
                    {
                        if (txt_IdKhachHang.Text != string.Empty)
                        {
                            sql = "INSERT INTO HoaDon VALUES ('" + txt_IdHoaDon.Text + "','" + txt_IdKhachHang.Text + "',N'Tiền mặt','" + txt_IdNhanVien.Text + "','" + GetIdChiNhanh(cboChiNhanh.Text, cboQuayThanhToan.Text) + "','" + dtpNgayBan.Text + "');";
                            CapNhatDiem(txt_IdKhachHang.Text, long.Parse(tongTienStr) * 0.01);
                        }
                        else
                            sql = "INSERT INTO HoaDon VALUES ('" + txt_IdHoaDon.Text + "',null,N'Tiền mặt','" + txt_IdNhanVien.Text + "','" + GetIdChiNhanh(cboChiNhanh.Text, cboQuayThanhToan.Text) + "','" + dtpNgayBan.Text + "');";
                        if (config.ExecuteNonQuery(sql) == true)
                        {
                            foreach (ListViewItem item in lsvSanPham.Items)
                            {
                                string sql2 = "INSERT INTO ChiTietHoaDon VALUES ('" + txt_IdHoaDon.Text + "','" + item.SubItems[0].Text + "'," + item.SubItems[3].Text + "," + item.SubItems[5].Text + ");";
                                config.ExecuteNonQuery(sql2);
                            }
                        }

                    }
                    InHoaDon();
                    MessageBox.Show("Đã xuất hoá đơn thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ResetHoaDon();
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine($"Lỗi SQL: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi: {ex.Message}");
            }
        }

        private void CapNhatDiem(string idKhachHang,double diemTichLuy)
        {
            try
            {
                sql = "UPDATE TheThanhVien SET DiemTichLuy = DiemTichLuy - " + diemTichLuy + " WHERE IdKhachHang = '" + idKhachHang + "'";
                config.ExecuteNonQuery(sql);
            }
            catch
            { 

            }
        }
        private string GetIdChiNhanh(string tenChiNhanh,string tenQuay)
        {
            string idQuay = null;
            try
            {
                sql = "SELECT IdQuayThanhToan FROM ChiNhanh CN INNER JOIN QuayThanhToan QTT ON CN.IdChiNhanh = QTT.IdChiNhanh WHERE TenQuay = N'" + tenQuay + "' AND TenChiNhanh = N'" + tenChiNhanh + "'";
                DataTable dataTable = config.ExecuteTableQuery(sql);
                if (dataTable.Rows.Count > 0)
                {
                    foreach (DataRow row in dataTable.Rows)
                    {
                        idQuay = row["IdQuayThanhToan"].ToString();
                    }
                }
                return idQuay;
            }
            catch (SqlException ex)
            {
                Console.WriteLine($"Lỗi SQL: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi: {ex.Message}");
            }
            return idQuay;
        }
        private void cboChiNhanh_SelectedValueChanged(object sender, EventArgs e)
        {
            if(cboChiNhanh.SelectedIndex != -1)
            {
                try
                {
                    sql = "SELECT QTT.TenQuay FROM QuayThanhToan QTT INNER JOIN ChiNhanh CN ON QTT.IdChiNhanh = CN.IdChiNhanh WHERE TenChiNhanh = N'" + cboChiNhanh.Text + "'";
                    DataTable dataTable = config.ExecuteTableQuery(sql);

                    List<string> listQuayThanhToan = new List<string>();
                    if (dataTable.Rows.Count > 0)
                    {

                        foreach (DataRow row in dataTable.Rows)
                        {
                            listQuayThanhToan.Add(row["TenQuay"].ToString());
                        }
                    }
                    else
                        listQuayThanhToan.Add("Chưa có quầy thanh toán");
                    cboQuayThanhToan.DataSource = listQuayThanhToan;
                    cboQuayThanhToan.SelectedIndex = -1;
                }
                catch (SqlException ex)
                {
                    Console.WriteLine($"Lỗi SQL: {ex.Message}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Lỗi: {ex.Message}");
                }
            }    
        }

        private void nud_SoLuong_ValueChanged(object sender, EventArgs e)
        {
            if (txt_DonGia.Text != "")
            {
                int giaDaGiam = int.Parse(txt_DonGia.Text) - int.Parse(txt_DonGia.Text) * int.Parse(nud_GiamGia.Text) / 100;
                txt_ThanhTien.Text = (nud_SoLuong.Value * giaDaGiam).ToString();
            }
        }

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            Brush brush = Brushes.Black;

            //Header
            string txtHeader = "HÓA ĐƠN THANH TOÁN";
            Font frontHeader = new Font("Arial", 24, FontStyle.Bold);
            e.Graphics.DrawString(txtHeader, frontHeader, brush, new Point(centerRow(e, txtHeader, frontHeader), 20));

            //Chi Nhánh
            string txtChiNhanh = cboChiNhanh.Text;
            Font frontChiNhanh = new Font("Arial", 18, FontStyle.Bold);
            e.Graphics.DrawString(txtChiNhanh, frontChiNhanh, brush, new Point(centerRow(e, txtChiNhanh, frontChiNhanh), 65));

            //Tên Chi Nhánh
            string txtTenChiNhanh = GetDiaChiChiNhanh(cboChiNhanh.Text);

            Font frontTenChiNhanh = new Font("Arial", 14, FontStyle.Regular);
            e.Graphics.DrawString(txtTenChiNhanh, frontTenChiNhanh, brush, new Point(centerRow(e, txtTenChiNhanh, frontTenChiNhanh), 100));

            //Số điện thoại
            string txtSDT = "Hotline: 111 222 3333";
            Font frontSDT = new Font("Arial", 14, FontStyle.Bold);
            e.Graphics.DrawString(txtSDT, frontSDT, brush, new Point(centerRow(e, txtSDT, frontSDT), 130));

            //Nhân viên     
            string txtNhanVien = "Nhân viên: " + txt_TenNhanVien.Text;
            Font frontNhanVien = new Font("Arial", 12, FontStyle.Regular);
            e.Graphics.DrawString(txtNhanVien, frontNhanVien, brush, new Point(10, 190));

            //Thời gian xuất hóa đơn
            DateTime gioXuatHD = dtpNgayBan.Value;
            string txtGioXuatHD = "Thời gian xuất hóa đơn: " + gioXuatHD.ToString("HH:mm:ss dd/MM/yyyy");
            Font frontGioXuatHD = new Font("Arial", 12, FontStyle.Regular);
            e.Graphics.DrawString(txtGioXuatHD, frontGioXuatHD, brush, new Point(10, 210));

            //Chuỗi ------------
            string breakString = string.Empty;
            for (int i = 10; i <= 150; i++)
            {
                breakString += "-";
            }
            Font frontBreakString = new Font("Arial", 12, FontStyle.Bold);
            e.Graphics.DrawString(breakString, frontBreakString, brush, new Point(10, 240));

            int y = 260;

            foreach (ListViewItem item in lsvSanPham.Items)
            {

                //Tên sản phẩm
                string txtTenSP = item.SubItems[1].Text;
                Font frontTenSP = new Font("Arial", 12, FontStyle.Regular);
                e.Graphics.DrawString(txtTenSP, frontTenSP, brush, new Point(10, y));

                //Giảm giá
                int giaGiam = int.Parse(item.SubItems[2].Text) - (int.Parse(item.SubItems[2].Text) * int.Parse(item.SubItems[4].Text) / 100);
                string txtGiaGiam = string.Format("{0:n0}", giaGiam);
                Font frontGiaGiam = new Font("Arial", 12, FontStyle.Regular);
                e.Graphics.DrawString(txtGiaGiam, frontGiaGiam, brush, new Point(10, y + 20));
                int giaGiamSize = (int)((SizeF)e.Graphics.MeasureString(txtGiaGiam, frontGiaGiam)).Width;

                //Giá
                string txtGia = string.Format("{0:n0}", int.Parse(item.SubItems[2].Text));
                Font frontGia = new Font("Arial", 12, FontStyle.Strikeout);
                e.Graphics.DrawString(txtGia, frontGia, brush, new Point(giaGiamSize + 20, y + 20));
                int giaSize = (int)((SizeF)e.Graphics.MeasureString(txtGia, frontGia)).Width;

                //Số lượng
                string txtSoLuong = item.SubItems[3].Text;
                Font frontSoLuong = new Font("Arial", 12, FontStyle.Regular);
                e.Graphics.DrawString(txtSoLuong, frontSoLuong, brush, new Point(450, y + 20));

                //ThanhTien
                string txtThanhTien = string.Format("{0:n0}", int.Parse(item.SubItems[5].Text));
                Font frontThanhTien = new Font("Arial", 12, FontStyle.Regular);
                int thanhTienSize = (int)((SizeF)e.Graphics.MeasureString(txtThanhTien, frontThanhTien)).Width;
                e.Graphics.DrawString(txtThanhTien, frontThanhTien, brush, new Point(e.PageBounds.Width - thanhTienSize - 15, y + 20));

                y += 40;
            }

            //Chuỗi ------------
            string breakString2 = string.Empty;
            for (int i = 10; i <= 150; i++)
            {
                breakString2 += "-";
            }
            Font frontBreakString2 = new Font("Arial", 12, FontStyle.Bold);
            e.Graphics.DrawString(breakString, frontBreakString, brush, new Point(10, y));



            string txtTongCong = "TỔNG CỘNG";
            Font frontTongCong = new Font("Arial", 18, FontStyle.Bold);
            e.Graphics.DrawString(txtTongCong, frontTongCong, brush, new Point(10, y + 30));



            //Số lượng
            string txtDVT = "VNĐ";
            Font frontDVT = new Font("Arial", 18, FontStyle.Bold);
            e.Graphics.DrawString(txtDVT, frontDVT, brush, new Point(450, y + 30));

            //ThanhTien
            string txtTongThanhTien = txt_TongTien.Text;
            Font frontTongThanhTien = new Font("Arial", 18, FontStyle.Bold);
            int tongThanhTienSize = (int)((SizeF)e.Graphics.MeasureString(txtTongThanhTien, frontTongThanhTien)).Width;
            e.Graphics.DrawString(txtTongThanhTien, frontTongThanhTien, brush, new Point(e.PageBounds.Width - tongThanhTienSize - 15, y + 30));
        }
        private int centerRow(System.Drawing.Printing.PrintPageEventArgs e, string text, Font font)
        {
            int pageWidth = e.PageBounds.Width;
            SizeF textSize = e.Graphics.MeasureString(text, font);
            int x = (int)((pageWidth - textSize.Width) / 2);
            return x;
        }
        private string GetDiaChiChiNhanh(string tenChiNhanh)
        {
            string diaChi = string.Empty;
            sql = string.Format("SELECT DiaChi FROM ChiNhanh WHERE TenChiNhanh = N'{0}'", tenChiNhanh);
            DataTable dataTable = config.ExecuteTableQuery(sql);
            if (dataTable.Rows.Count > 0)
            {
                foreach (DataRow row in dataTable.Rows)
                {
                    diaChi = row[0].ToString();
                }
            }
            return diaChi;
        }
        private void InHoaDon()
        {
            try
            {
                string downloadFolderPath = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + "\\Downloads";
                string filePath = Path.Combine(downloadFolderPath, "HoaDon.pdf");

                PrintDocument printDocument = new PrintDocument();
                printDocument.PrintPage += new PrintPageEventHandler(printDocument1_PrintPage);
                printDocument.PrinterSettings.PrintToFile = true;
                printDocument.PrinterSettings.PrintFileName = filePath;
                printDocument.Print();

                if (File.Exists(filePath))
                {
                    using (FileStream fs = new FileStream(filePath, FileMode.Create))
                    {
                    }
                }
            }
            catch (Exception ex)
            {

                Console.WriteLine($"Lỗi: {ex.Message}");
            }

        }
    }
}
