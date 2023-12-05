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
    public partial class frm_ChiTietYKien : Form
    {
        public frm_ChiTietYKien()
        {
            InitializeComponent();
        }
        DBConnect db = new DBConnect();

        private void Form2_Load(object sender, EventArgs e)
        {
            //try
            //{
            //    string str_connection = "Data Source = DESKTOP-422FLDV\\SQLEXPRESS; Initial Catalog = YKIENKH; Integrated Security = true;";
            //    conn = new SqlConnection(str_connection);
            //    if (conn.State == ConnectionState.Closed)
            //        conn.Open();
            //    if (conn.State == ConnectionState.Open)
            //        MessageBox.Show("Kết nối thành công!!");
            //}
            //catch
            //{
            //    MessageBox.Show("Kết nối thất bại");
            //}
            //if (conn.State == ConnectionState.Open)
            //    conn.Close();

            load_data();
        }
        void load_data()
        {
            lst_tableinfor.Items.Clear();
            string sql_select = "select * from YkienKH";
            SqlDataReader rd = db.getDataReader(sql_select);
            while (rd.Read())
            {
                string IdFB = rd["IdFeedback"].ToString();
                string IdKH = rd["IdKhachHang"].ToString();
                string LoaiYK = rd["LoaiYkien"].ToString();
                string NDYK = rd["NDYkien"].ToString();
                ListViewItem item = new ListViewItem(new[] { IdFB, IdKH, LoaiYK, NDYK });
                lst_tableinfor.Items.Add(item);
            }
            db.close();
            rd.Close();
        }
    }
}
