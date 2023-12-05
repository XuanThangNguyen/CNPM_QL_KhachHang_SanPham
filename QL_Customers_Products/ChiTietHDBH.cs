using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace QL_Customers_Products
{
    public partial class ChiTietHDBH : Form
    {
        public ChiTietHDBH()
        {
            InitializeComponent();
        }
        // Thêm một biến instance để lưu IdHoaDon
        private string idHoaDon;

        // Tạo constructor để truyền IdHoaDon từ form chính
        public ChiTietHDBH(string idHoaDon)
        {
            InitializeComponent();
            
            this.idHoaDon = idHoaDon;
        }
        private string connectionString = "Server=DESKTOP-UE7V70U\\SQLEXPRESS;Database=QLKH_SP;User=sa;Password=123;"; // Thay thế bằng chuỗi kết nối của bạn

        private SqlConnection connection;
        private SqlCommand command;
        private SqlDataAdapter adapter;
        private DataTable dataTable;

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void ChiTietHDNhap_Load(object sender, EventArgs e)
        {
            // Lấy IdHoaDon từ constructor
            string idHoaDon = this.idHoaDon;
            label1.Text = idHoaDon;
            // Kiểm tra xem IdHoaDon có giá trị không
            if (!string.IsNullOrEmpty(idHoaDon))
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    // Truy vấn danh sách sản phẩm dựa trên IdHoaDon
                    string query = "SELECT CTHD.IdHoaDon, SP.TenSanPham, CTHD.SoLuong, CTHD.GiaBan FROM ChiTietHoaDon CTHD INNER JOIN SanPham SP ON CTHD.IdSanPham = SP.IdSanPham WHERE CTHD.IdHoaDon = @IdHoaDon";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@IdHoaDon", idHoaDon);
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                           // Đọc dữ liệu từ SqlDataReader và thêm vào ListView
                            while (reader.Read())
                            {
                                ListViewItem item = new ListViewItem(reader["IdHoaDon"].ToString());
                                
                                item.SubItems.Add(reader["TenSanPham"].ToString());                              
                                item.SubItems.Add(reader["SoLuong"].ToString());
                                item.SubItems.Add(reader["GiaBan"].ToString());
                           
                                listView1.Items.Add(item);
                            }
                        }
                    }
                    connection.Close();
                }
            }
        }
    }
}
