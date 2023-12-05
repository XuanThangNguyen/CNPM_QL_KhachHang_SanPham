//using CrystalDecisions.Windows.Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;


namespace QL_Customers_Products
{
    public partial class Thu : Form
    {
        SQLConfig conn = new SQLConfig();
        
        public Thu()
        {
            InitializeComponent();
        }
        private string connectionString = "Server=DESKTOP-UE7V70U\\SQLEXPRESS;Database=QLKH_SP;User=sa;Password=123;"; // Thay thế bằng chuỗi kết nối của bạn
        private SqlConnection connection;
        private SqlCommand command;
        private SqlDataAdapter adapter;
        private DataTable dataTable;
        private void ThuChi_Load(object sender, EventArgs e)
        {
            int s = 0;
            connection = new SqlConnection(connectionString);
            command = new SqlCommand("SELECT * FROM BangThu", connection);
           // Khởi tạo adapter và dataTable
            adapter = new SqlDataAdapter(command);
            dataTable = new DataTable();
            // Mở kết nối và đổ dữ liệu vào dataTable
            connection.Open();
            adapter.Fill(dataTable);
            connection.Close();
            // Gán dữ liệu từ dataTable vào ListView
            foreach (DataRow row in dataTable.Rows)
            {
                ListViewItem item = new ListViewItem(row["idHoaDon"].ToString());
                item.SubItems.Add(row["TongTien"].ToString());
                item.SubItems.Add(row["ThoiGianTao"].ToString());
                item.SubItems.Add(row["TenNhanVien"].ToString());
                item.SubItems.Add(row["TenKhachHang"].ToString());
                item.SubItems.Add(row["IdQuayThanhToan"].ToString());
                item.SubItems.Add(row["SoLuong"].ToString());
                listView1.Items.Add(item);
                int tongTien;
                if (int.TryParse(row["TongTien"].ToString(), out tongTien))
                {
                    s += tongTien;
                }
            }
            textBox1.Text = s.ToString();
        }
        private void UpdateTotal()
        {
            int s = 0; // Khởi tạo biến tổng s
            foreach (DataRow row in dataTable.Rows)
            {
                int tongTien;
                if (int.TryParse(row["TongTien"].ToString(), out tongTien))
                {
                    s += tongTien;
                }
            }
            // Đưa giá trị tổng s vào TextBox
            textBox1.Text = s.ToString();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            DateTime selectedTime = dateTimePicker1.Value.Date;
            using (connection = new SqlConnection(connectionString))
            {
                using (command = new SqlCommand("SELECT * FROM BangThu WHERE ThoiGianTao = @ThoiGianTao", connection))
                {
                    // Thêm tham số cho thời gian đã chọn
                    command.Parameters.AddWithValue("@ThoiGianTao", selectedTime);
                    // Khởi tạo adapter và dataTable
                    adapter = new SqlDataAdapter(command);
                    dataTable = new DataTable();
                    try
                    {
                        connection.Open();
                        adapter.Fill(dataTable);
                        connection.Close();
                        listView1.Items.Clear();
                        foreach (DataRow row in dataTable.Rows)
                        {
                            ListViewItem item = new ListViewItem(row["idHoaDon"].ToString());
                            item.SubItems.Add(row["TongTien"].ToString());
                            item.SubItems.Add(row["ThoiGianTao"].ToString());
                            item.SubItems.Add(row["TenNhanVien"].ToString());
                            item.SubItems.Add(row["TenKhachHang"].ToString());
                            item.SubItems.Add(row["IdQuayThanhToan"].ToString());
                            item.SubItems.Add(row["SoLuong"].ToString());
                            listView1.Items.Add(item);
                        }
                        UpdateTotal();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Lỗi: " + ex.Message);
                    }
                }
            }
        }
        private void button2_Click(object sender, EventArgs e)
        {
            ThuChi_Load(sender, e);
            textBox1.Clear();
        }
        private void button4_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void button3_Click(object sender, EventArgs e)
        {
            //saveFileDialog1.Filter = "Text Files|*.txt"; // Đã chỉnh sửa filter đúng cú pháp
            //if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            //{
            //    // Tạo stream để ghi vào file
            //    using (Stream myStream = saveFileDialog1.OpenFile())
            //    {
            //        // Tạo đối tượng StreamWriter để viết vào file
            //        using (StreamWriter writer = new StreamWriter(myStream))
            //        {
            //            // Duyệt qua từng dòng trong ListView và ghi vào file
            //            foreach (ListViewItem item in listView1.Items)
            //            {
            //                // Ghi nội dung từ các cột của mỗi dòng, cách nhau bằng dấu tab ("\t")
            //                string line = string.Join("\t", item.SubItems.Cast<ListViewItem.ListViewSubItem>().Select(subItem => subItem.Text));
            //                writer.WriteLine(line);
            //            }
            //        }
            //    }
            //}
            //BaoCaoThu dc = new BaoCaoThu();
            //dc.ShowDialog();
            
            
        }
        private void button5_Click(object sender, EventArgs e)
        {
            // Kiểm tra xem có một mục HoaDon nào được chọn trong ListView của form chính không
            if (listView1.SelectedItems.Count > 0)
            {
                // Lấy IdHoaDon của mục HoaDon được chọn
                string idHoaDon = listView1.SelectedItems[0].Text;
                // Tạo form mới để hiển thị danh sách sản phẩm
                ChiTietHDBH chiTietForm = new ChiTietHDBH(idHoaDon);
                chiTietForm.ShowDialog();
            }
        }
    }
}
