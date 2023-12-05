//using System;
//using System.Collections.Generic;
//using System.ComponentModel;
//using System.Data;
//using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;
//using System.Data.SqlClient;
//using System.Drawing;
//using System.Globalization;
//using System.Linq;
//using System.Runtime.Remoting.Contexts;
//using System.Security.Cryptography;
//using System.Text;
//using System.Threading.Tasks;
//using System.Windows.Forms;
//using static System.Windows.Forms.VisualStyles.VisualStyleElement;
//using static System.Windows.Forms.VisualStyles.VisualStyleElement.ProgressBar;
//using static System.Windows.Forms.VisualStyles.VisualStyleElement.TrackBar;
//using System.Linq.Expressions;
//using QL_Customers_Products;
//using System.Data.Entity.Infrastructure;
//using System.Data.Entity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Windows.Forms;


namespace QL_Customers_Products
{
    public partial class FNhapHang : Form
    {
        private QLKH_SPEntities db;
        public FNhapHang()
        {
            db = new QLKH_SPEntities();
            InitializeComponent();
        }
        private void FNhapHang_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult r;
            r = MessageBox.Show("Bạn có muốn thoát?", "Thoát khỏi đây", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (r == DialogResult.No)
            {
                e.Cancel = true;
            }
        }

        private void FNhapHang_Load(object sender, EventArgs e)
        {
            QLKH_SPEntities db = new QLKH_SPEntities();
            List<string> tenSanPhamList = db.SanPham.Select(s => s.TenSanPham).ToList();
            // Lấy cột ComboBox có tên là "Column2" từ DataGridView
            DataGridViewComboBoxColumn comboBoxColumn = (DataGridViewComboBoxColumn)dataGridView1.Columns["Column2"];
            // Đặt các mục là các tên sản phẩm vào cột ComboBox
            comboBoxColumn.Items.AddRange(tenSanPhamList.ToArray());
            List<string> nhacc = db.NhaCungCap.Select(s => s.TenNhaCungCap).ToList();
            comboNhaCC.Items.AddRange(nhacc.ToArray());
            List<string> tenNhanVien = db.NhanVien
    .Where(nv => nv.IdNguoiDung == db.NguoiDung.FirstOrDefault(nd => nd.IdNguoiDung == nv.IdNguoiDung).IdNguoiDung)
    .Select(nv => nv.TenNhanVien)
    .ToList();
            comboBox2.Items.AddRange(tenNhanVien.ToArray());
            List<string> Kho = db.KhoHang.Select(s => s.TenKhoHang).ToList();
            comboKhoHang.Items.AddRange(Kho.ToArray());

            dataGridView1.EditingControlShowing += dataGridView1_EditingControlShowing;
            dataGridView1.CellEndEdit += dataGridView1_CellEndEdit;
            dataGridView1.CellValueChanged += dataGridView1_CellValueChanged;
        }
        private void dataGridView1_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            DataGridViewComboBoxEditingControl comboBox = e.Control as DataGridViewComboBoxEditingControl;
            if (comboBox != null)
            {
                comboBox.SelectedIndexChanged -= ComboBox_SelectedIndexChanged; // Remove the previous event handler
                comboBox.SelectedIndexChanged += ComboBox_SelectedIndexChanged; // Add the event handler
            }
            string columnName = "Column4";
            // Kiểm tra xem cột đang được chỉnh sửa có phải là cột "Column4" không
            if (dataGridView1.CurrentCell.OwningColumn.Name == columnName)
            {
                // Cho phép chỉnh sửa cột số lượng
                e.Control.Enabled = true;
            }
        }

        private void dataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            // Kiểm tra xem sự thay đổi xảy ra ở cột "Column7"
            string columnName = "Column7"; // Thay thế bằng tên cột "Column7"
            string giaTienColumnName = "Column1"; // Thay thế bằng tên cột "Column1"

            if (dataGridView1.Columns[e.ColumnIndex].Name == columnName)
            {
                // Lấy giá trị từ cột "Column7" trong hàng hiện tại
                object selectedValue = dataGridView1.Rows[e.RowIndex].Cells[columnName].Value;

                if (selectedValue != null)
                {
                    string selectedId = selectedValue.ToString(); // Chuyển đổi giá trị thành kiểu string

                    // Khởi tạo đối tượng DbContext
                    using (QLKH_SPEntities db = new QLKH_SPEntities())
                    {
                        // Thực hiện truy vấn cơ sở dữ liệu để lấy giá sản phẩm dựa trên IdSanPham (kiểu string)
                        var selectedProduct = db.SanPham.FirstOrDefault(sp => sp.IdSanPham == selectedId);

                        if (selectedProduct != null)
                        {
                            // Lấy giá sản phẩm và đặt vào cột "Column1" (Giá Bán)
                            string giaTien = selectedProduct.GiaGoc.ToString();
                            dataGridView1.Rows[e.RowIndex].Cells[giaTienColumnName].Value = giaTien;
                        }
                    }
                }
            }
        }
        private void ComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataGridViewComboBoxEditingControl comboBox = sender as DataGridViewComboBoxEditingControl;
            if (comboBox != null)
            {
                string selectedValue = comboBox.SelectedItem?.ToString();
                if (!string.IsNullOrEmpty(selectedValue))
                {
                    string idSp = string.Empty;
                    // Khởi tạo đối tượng DbContext
                    using (QLKH_SPEntities db = new QLKH_SPEntities())
                    {
                        // Thực hiện so sánh không phân biệt hoa thường
                        var selectedProduct = db.SanPham.FirstOrDefault(sp => sp.TenSanPham == selectedValue);
                        if (selectedProduct != null)
                        {
                            idSp = selectedProduct.IdSanPham.ToString();
                        }
                    }
                    // Lấy chỉ số của cột bạn muốn thay đổi (thay thế bằng tên cột của bạn)
                    string targetColumnName = "Column7"; // Thay thế bằng tên cột của bạn
                    int columnIndex = dataGridView1.Columns[targetColumnName].Index;
                    // Lấy hàng hiện tại
                    int rowIndex = dataGridView1.CurrentCell.RowIndex;
                    // Truy cập ô (cell) tương ứng và đặt giá trị
                    dataGridView1.Rows[rowIndex].Cells[columnIndex].Value = idSp;
                    // Rest of your code to display giaTien
                }
            }
        }
        private void dataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            // Kiểm tra xem sự kiện xảy ra trong cột "Column4" (Số lượng) hoặc "Column1" (Giá bán)
            if (e.RowIndex >= 0 && (e.ColumnIndex == dataGridView1.Columns["Column4"].Index || e.ColumnIndex == dataGridView1.Columns["Column1"].Index))
            {
                // Lấy giá trị từ cột "Column4" (Số lượng)
                object soLuongValue = dataGridView1.Rows[e.RowIndex].Cells["Column4"].Value;
                // Lấy giá trị từ cột "Column1" (Giá bán)
                object giaBanValue = dataGridView1.Rows[e.RowIndex].Cells["Column1"].Value;

                if (soLuongValue != null && giaBanValue != null)
                {
                    int soLuong;
                    decimal giaBan;
                    // Kiểm tra xem giá trị có thể chuyển đổi thành kiểu số nguyên và số thập phân không
                    if (int.TryParse(soLuongValue.ToString(), out soLuong) && decimal.TryParse(giaBanValue.ToString(), out giaBan))
                    {
                        // Tính thành tiền bằng cách nhân giá bán với số lượng
                        decimal thanhTien = giaBan * soLuong;
                        // Đặt giá trị vào cột "Column6" (Thành tiền)
                        dataGridView1.Rows[e.RowIndex].Cells["Column6"].Value = thanhTien;
                    }
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        
        //private void button1_Click(object sender, EventArgs e)
        //{
        //    QLKH_SPEntities db = new QLKH_SPEntities();
        //    using (var context = new AppDataContext())
        //    {
        //        //string tenNhaCc = comboNhaCC.Text;
        //        string tenNhaCc = comboNhaCC.SelectedItem.ToString();
        //        string tenKho = comboKhoHang.Text;
        //        DateTime ngayLap = dateTimePicker1.Value;
        //        string nguoiLap = comboBox2.Text;
        //        string ptthanhtoan = comboBox1.Text;
        //        string ghichu = textBox4.Text;
        //        string tennv = comboBox2.SelectedItem.ToString();
        //        string idnv = "";
        //        string idncc = "";
        //        var ncc = db.NhaCungCap.FirstOrDefault(x => x.TenNhaCungCap == tenNhaCc);
        //        if (ncc != null)
        //        {
        //            idncc = ncc.IdNhaCungCap.ToString();
        //        }
        //        else
        //        {
        //            MessageBox.Show("Không tìm thấy Nhà Cung Cấp!");
        //            return;
        //        }
        //        var nv = db.NhanVien.FirstOrDefault(x => x.TenNhanVien == tennv);
        //        if (nv != null)
        //        {
        //            idnv = nv.IdNhanVien.ToString();

        //        }
        //        else
        //        {
        //            MessageBox.Show("Không tìm thấy Nhà Cung Cấp!");
        //            return;
        //        }
        //        var nhapHang = new NhapHang
        //        {
        //            IdNhapHang = Guid.NewGuid().ToString(),
        //            IdNhanVien = idnv,
        //            TongTien = long.Parse(textBox1.Text),
        //            PhuongThucThanhToan = ptthanhtoan,
        //            ThoiGianTao = ngayLap
        //        };
        //        context.NhapHangs.Add(nhapHang);
        //        foreach (DataGridViewRow row in dataGridView1.Rows)
        //        {
        //            if (row.IsNewRow) // Bỏ qua dòng mới nếu có
        //                continue;
        //            string idSp = row.Cells[0].Value != null ? row.Cells[0].Value.ToString() : string.Empty;
        //            string tenSp = row.Cells[1].Value != null ? row.Cells[1].Value.ToString() : string.Empty;
        //            string donViTinh = row.Cells[2].Value != null ? row.Cells[2].Value.ToString() : string.Empty;
        //            int soLuong;
        //            if (row.Cells[3].Value != null && int.TryParse(row.Cells[3].Value.ToString(), out soLuong))
        //            {
        //            }
        //            else
        //            {
        //                continue; // Bỏ qua dòng có lỗi
        //            }

        //            decimal giaBan;
        //            if (row.Cells[4].Value != null && decimal.TryParse(row.Cells[4].Value.ToString(), out giaBan))
        //            {
        //            }
        //            else
        //            {
        //                continue; // Bỏ qua dòng có lỗi
        //            }

        //            decimal thanhTien;
        //            if (row.Cells[5].Value != null && decimal.TryParse(row.Cells[5].Value.ToString(), out thanhTien))
        //            {
        //            }
        //            else
        //            {
        //                MessageBox.Show("Lỗi rồi!");
        //                continue; // Bỏ qua dòng có lỗi
        //            }

        //            var chiTietHoaDonNhaphang = new ChiTietHoaDonNhapHang
        //            {
        //                IdNhapHang = nhapHang.IdNhapHang,
        //                IdSanPham = idSp,
        //                IdNhaCungCap = textBox2.Text,
        //                SoLuong = soLuong,
        //                GiaNhap = long.Parse(giaBan.ToString())
        //            };

        //            context.chiTietHoaDonNhapHangs.Add(chiTietHoaDonNhaphang);
        //            nhapHang.TongTien += long.Parse(thanhTien.ToString());
        //        }
        //        context.SaveChanges();
        //        MessageBox.Show("Lưu dữ liệu thành công!");
        //    }
        //}
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                // Lấy thông tin từ các điều khiển trên giao diện
                string tenNhaCc = comboNhaCC.SelectedItem?.ToString();
                string tenKho = comboKhoHang.SelectedItem?.ToString();
                DateTime ngayLap = dateTimePicker1.Value;
                string nguoiLap = comboBox2.SelectedItem?.ToString();
                string ptthanhtoan = comboBox1.SelectedItem?.ToString();
                string ghichu = textBox4.Text;
                string uniqueId = "HDNH" + new Random().Next(0, 99999999).ToString("D8");


                string idNhaCungCap = textBox2.Text.Trim();
                // Kiểm tra chiều dài giá trị IdNhaCungCap và cắt nếu cần
                if (idNhaCungCap.Length > 12)
                {
                    idNhaCungCap = idNhaCungCap.Substring(0, 12);
                }
                // Kiểm tra xem các giá trị đã được chọn hoặc nhập đúng chưa
                //if (string.IsNullOrEmpty(tenNhaCc) || string.IsNullOrEmpty(tenKho) || string.IsNullOrEmpty(nguoiLap) || string.IsNullOrEmpty(ptthanhtoan))
                //{
                //    MessageBox.Show("Vui lòng điền đầy đủ thông tin.");
                //    return;
                //}

                // Lấy ID của Nhà Cung Cấp
                var ncc = db.NhaCungCap.FirstOrDefault(x => x.TenNhaCungCap == tenNhaCc);
                if (ncc == null)
                {
                    MessageBox.Show("Không tìm thấy Nhà Cung Cấp!");
                    return;
                }

                // Lấy ID của Nhân Viên
                var nv = db.NhanVien.FirstOrDefault(x => x.TenNhanVien == nguoiLap);
                if (nv == null)
                {
                    MessageBox.Show("Không tìm thấy Nhân Viên!");
                    return;
                }

                // Tạo đối tượng NhapHang
                var nhapHang = new NhapHang
                {
                    
                    IdNhapHang = uniqueId,
                    IdNhanVien = nv.IdNhanVien,
                    TongTien = 0, // Khởi tạo tổng tiền
                    PhuongThucThanhToan = ptthanhtoan,
                    ThoiGianTao = ngayLap
                };

                // Tạo danh sách chi tiết hóa đơn nhập hàng
                List<ChiTietHoaDonNhapHang> chiTietNhapHangs = new List<ChiTietHoaDonNhapHang>();

                // Lặp qua từng hàng của DataGridView để tạo chi tiết hóa đơn
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    if (row.IsNewRow) // Bỏ qua dòng mới nếu có
                        continue;

                    string idSp = row.Cells["Column7"].Value?.ToString();
                    string tenSp = row.Cells["Column2"].Value?.ToString();
                    string donViTinh = row.Cells["Column3"].Value?.ToString();
                    int soLuong = 0;
                    decimal giaBan = 0;

                    if (row.Cells["Column4"].Value != null)
                        int.TryParse(row.Cells["Column4"].Value.ToString(), out soLuong);

                    if (row.Cells["Column1"].Value != null)
                        decimal.TryParse(row.Cells["Column1"].Value.ToString(), out giaBan);

                    if (string.IsNullOrEmpty(idSp) || string.IsNullOrEmpty(tenSp) || string.IsNullOrEmpty(donViTinh) || soLuong <= 0 || giaBan <= 0)
                        continue; // Bỏ qua dòng có lỗi

                    // Tính thành tiền cho chi tiết hóa đơn
                    decimal thanhTien = soLuong * giaBan;

                    // Tạo chi tiết hóa đơn nhập hàng
                    var chiTietHoaDonNhaphang = new ChiTietHoaDonNhapHang
                    {
                        IdNhapHang = uniqueId,
                        IdSanPham = idSp,
                        IdNhaCungCap = idNhaCungCap,
                        SoLuong = soLuong,
                        GiaNhap = long.Parse(giaBan.ToString())
                    };

                    chiTietNhapHangs.Add(chiTietHoaDonNhaphang);

                    // Cập nhật tổng tiền của đơn hàng
                    nhapHang.TongTien += long.Parse(thanhTien.ToString());
                }

                // Thêm đơn hàng và chi tiết hóa đơn vào cơ sở dữ liệu
                db.NhapHang.Add(nhapHang);
                db.ChiTietHoaDonNhapHang.AddRange(chiTietNhapHangs);
                db.SaveChanges();
                MessageBox.Show("Nhập hàng thành công!");
            }
            catch (DbUpdateException ex)
            {
                // Lỗi chính của Entity Framework
                if (ex.InnerException != null)
                {
                    var innerException = ex.InnerException;
                    while (innerException.InnerException != null)
                    {
                        innerException = innerException.InnerException;
                    }

                    // In thông điệp lỗi từ inner exception
                    MessageBox.Show(innerException.Message);
                }
            }
            catch (System.Data.Entity.Validation.DbEntityValidationException ex)
            {
                // Xử lý lỗi kiểm tra dữ liệu
                foreach (var validationErrors in ex.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        MessageBox.Show(string.Format("Lỗi kiểm tra dữ liệu - Property: {0}, Error: {1}",
                            validationError.PropertyName, validationError.ErrorMessage));
                    }
                }
            }
        }
        private void comboNhaCC_SelectedIndexChanged(object sender, EventArgs e)
        {
            QLKH_SPEntities db = new QLKH_SPEntities();
            string tenNhaCc = comboNhaCC.SelectedItem.ToString();
            var ncc = db.NhaCungCap.FirstOrDefault(x => x.TenNhaCungCap == tenNhaCc);
            if (ncc != null)
            {
                textBox2.Text = ncc.IdNhaCungCap.ToString();
            }
            else
            {
                textBox2.Text = "Không tìm thấy Nhà Cung Cấp";
            }

        }

        private void button3_Click(object sender, EventArgs e)
        {
            decimal totalAmount = 0;
            // Lặp qua từng hàng của DataGridView để tính tổng thành tiền
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (!row.IsNewRow) // Đảm bảo không tính hàng mới
                {
                    // Kiểm tra giá trị trong cột "Column6" (Thành tiền)
                    object cellValue = row.Cells["Column6"].Value;
                    if (cellValue != null && decimal.TryParse(cellValue.ToString(), out decimal rowAmount))
                    {
                        totalAmount += rowAmount;
                    }
                }
            }
            textBox1.Text = totalAmount.ToString();
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
