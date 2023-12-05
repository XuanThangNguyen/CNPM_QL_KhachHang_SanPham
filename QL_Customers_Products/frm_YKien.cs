using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;


namespace QL_Customers_Products
{
    public partial class frm_YKien : Form
    {
        public frm_YKien()
        {
            InitializeComponent();
        }
        DBConnect conn = new DBConnect();
        private void Form1_Load(object sender, EventArgs e)
        {

        }
        private void btn_theminf_Click(object sender, EventArgs e)
        {
            string sql_insert = "INSERT INTO YkienKH VALUES('" + txt_idfb.Text + "','" + txt_idkh.Text + "',N'" + txt_loaiykien.Text + "',N'" + txt_noidung.Text + "' )";
            int kq = conn.getNonQuery(sql_insert);
            if (kq > 0)
                MessageBox.Show("Thêm thành công");
            else
                MessageBox.Show("Thêm thất bại");
        }

        private void btn_xembang_Click(object sender, EventArgs e)
        {
            frm_ChiTietYKien form2 = new frm_ChiTietYKien();
            form2.Show();
        }

        private void txt_loaiykien_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
