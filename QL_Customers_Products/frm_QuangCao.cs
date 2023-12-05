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
    public partial class frm_QuangCao : Form
    {
        DBConnect dbConnect = new DBConnect();

        public frm_QuangCao()
        {
            InitializeComponent();
            LoadDataToDataGridView();
            dgv_quangcao.SelectionChanged += dgv_quangcao_SelectionChanged;
        }

        private void LoadDataToDataGridView()
        {
            // Tạo câu truy vấn SQL để lấy dữ liệu từ bảng QuangCao
            string sql = "SELECT * FROM QuangCao";

            // Gọi phương thức getDataTable từ đối tượng dbConnect để lấy dữ liệu vào DataTable
            DataTable dt = dbConnect.getDataTable(sql);

            // Đổ dữ liệu từ DataTable vào DataGridView
            dgv_quangcao.DataSource = dt;
        }

        private void dgv_quangcao_SelectionChanged(object sender, EventArgs e)
        {
            // Kiểm tra xem có dòng nào được chọn hay không
            if (dgv_quangcao.SelectedRows.Count > 0)
            {
                // Lấy dòng được chọn
                DataGridViewRow selectedRow = dgv_quangcao.SelectedRows[0];

                // Lấy giá trị từ các cột của dòng được chọn
                string idQuangCao = selectedRow.Cells["IdQuangCao"].Value.ToString();
                string idSanPham = selectedRow.Cells["IdSanPham"].Value.ToString();
                string tenQuangCao = selectedRow.Cells["TenQuangCao"].Value.ToString();
                string moTaQuangCao = selectedRow.Cells["MoTaQuangCao"].Value.ToString();
                DateTime ngayBatDau = Convert.ToDateTime(selectedRow.Cells["NgayBatDau"].Value);
                DateTime ngayKetThuc = Convert.ToDateTime(selectedRow.Cells["NgayKetThuc"].Value);
                string kenhQuangCao = selectedRow.Cells["KenhQuangCao"].Value.ToString();

                // Hiển thị giá trị lên các TextBox
                txt_idquangcao.Text = idQuangCao;
                txt_idsanpham.Text = idSanPham;
                txt_tenquangcao.Text = tenQuangCao;
                txt_mota.Text = moTaQuangCao;
                dateTimePicker1.Text = ngayBatDau.ToString("yyyy-MM-dd");
                dateTimePicker2.Text = ngayKetThuc.ToString("yyyy-MM-dd");
                txt_kenhquangcao.Text = kenhQuangCao;
            }
        }

        private void btn_them_Click(object sender, EventArgs e)
        {
            if (dgv_quangcao.SelectedRows.Count > 0)
            {
                string idQuangCao = txt_idquangcao.Text;
                string idSanPham = txt_idsanpham.Text;
                string tenQuangCao = txt_tenquangcao.Text;
                string moTaQuangCao = txt_mota.Text;
                DateTime ngayBatDau = dateTimePicker1.Value;
                DateTime ngayKetThuc = dateTimePicker2.Value;
                string kenhQuangCao = txt_kenhquangcao.Text;

                string sql = $"INSERT INTO QuangCao (IdQuangCao, TenQuangCao, MoTaQuangCao, IdSanPham, NgayBatDau, NgayKetThuc, KenhQuangCao) VALUES ('{idQuangCao}', N'{tenQuangCao}', N'{moTaQuangCao}', '{idSanPham}', '{ngayBatDau.ToString("yyyy-MM-dd")}', '{ngayKetThuc.ToString("yyyy-MM-dd")}', '{kenhQuangCao}')";

                dbConnect.getNonQuery(sql);

                LoadDataToDataGridView();
            }
        }

        private void btn_xoa_Click(object sender, EventArgs e)
        {
            // Kiểm tra xem đã chọn dòng để xóa hay chưa
            if (dgv_quangcao.SelectedRows.Count > 0)
            {
                string idQuangCao = txt_idquangcao.Text;

                string sql = $"DELETE FROM QuangCao WHERE IdQuangCao = '{idQuangCao}'";

                dbConnect.getNonQuery(sql);

                LoadDataToDataGridView();
            }
        }

        private void btn_sua_Click(object sender, EventArgs e)
        {
            // Kiểm tra xem đã chọn dòng để sửa hay chưa
            if (dgv_quangcao.SelectedRows.Count > 0)
            {
                string idQuangCao = txt_idquangcao.Text;
                string idSanPham = txt_idsanpham.Text;
                string tenQuangCao = txt_tenquangcao.Text;
                string moTaQuangCao = txt_mota.Text;
                DateTime ngayBatDau = dateTimePicker1.Value;
                DateTime ngayKetThuc = dateTimePicker2.Value;
                string kenhQuangCao = txt_kenhquangcao.Text;

                string sql = $"UPDATE QuangCao SET TenQuangCao = N'{tenQuangCao}', MoTaQuangCao = N'{moTaQuangCao}', IdSanPham = '{idSanPham}', NgayBatDau = '{ngayBatDau.ToString("yyyy-MM-dd")}', NgayKetThuc = '{ngayKetThuc.ToString("yyyy-MM-dd")}', KenhQuangCao = '{kenhQuangCao}' WHERE IdQuangCao = '{idQuangCao}'";

                dbConnect.getNonQuery(sql);

                LoadDataToDataGridView();
            }
        }
    }
}
