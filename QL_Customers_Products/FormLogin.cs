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

namespace QL_Customers_Products
{
    public partial class FormLogin : Form
    {
        private string connectionString = "Server=DESKTOP-MH8F2OA;Database=QLKH_SP;User=sa;Password=123;";
        public FormLogin()
        {
            InitializeComponent();
        }

        private void FormLogin_Load(object sender, EventArgs e)
        {
            txt_username.Focus();
        }
        class User
        {
            public string Username;
            public string Password;
        }
        private void btn_FormLogin_Click(object sender, EventArgs e)
        {
            string username = txt_username.Text;
            string password = txt_password.Text;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {

                    connection.Open();
                    string query = "SELECT COUNT(*) FROM NguoiDung WHERE TenTaiKhoan = @Username AND MatKhau = @Password";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@Username", username);
                    command.Parameters.AddWithValue("@Password", password);

                    int userCount = (int)command.ExecuteScalar();

                    if (userCount > 0)
                    {
                        //FormSanPham formsanpham = new FormSanPham();
                        //formsanpham.ShowDialog();
                        //Thu thu = new Thu();
                        //thu.ShowDialog();
                        Chi chi = new Chi();
                        chi.ShowDialog();
                        //XuLiNhapHang xl = new XuLiNhapHang();
                        //xl.ShowDialog();
                        this.Hide();
                    }
                    else
                    {
                        MessageBox.Show("Đăng nhập không thành công. Vui lòng kiểm tra lại tên đăng nhập và mật khẩu.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi: " + ex.Message);
                }
            }
        }
    }
}
