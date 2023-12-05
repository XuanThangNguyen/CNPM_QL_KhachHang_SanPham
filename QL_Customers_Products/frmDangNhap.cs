using System;
using System.Collections;
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
    public partial class frmDangNhap : Form
    {
        SQLConfig config = new SQLConfig();
        string sql;
        public frmDangNhap()
        {
            InitializeComponent();
        }

        public void FormLogin_Load(object sender, EventArgs e)
        {
            txt_Username.Focus();
        }

        private void btnCloseForm_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Bạn có chắc muốn thoát?", "Thoát chương trình", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        public void btn_DangNhap_Click(object sender, EventArgs e)
        {
            try
            {
                sql = "SELECT COUNT(*) FROM NguoiDung WHERE TenTaiKhoan = '" + txt_Username.Text + "' AND MatKhau = '" + txt_Password.Text + "'";
                Object result = config.ExecuteScalar(sql);
                if (int.Parse(result.ToString()) > 0)
                {
                    string query2 = " SELECT * FROM NguoiDung WHERE TenTaiKhoan = '" + txt_Username.Text + "' AND MatKhau = '" + txt_Password.Text + "'";
                    DataTable dataTable = config.ExecuteTableQuery(query2);
                    if (dataTable.Rows.Count > 0)
                        foreach (DataRow row in dataTable.Rows)
                            frmThanhToan.idNguoiDungHienTai = row["IdNguoiDung"].ToString();
                  frmMain main = new frmMain();
                    main.ShowDialog();
                  
                    this.Hide();
                    
                }
                else
                {
                    MessageBox.Show("Tài khoản không tồn tại! Vui lòng liên hệ quản trị viên.", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
    }
}
