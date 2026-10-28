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

namespace QuanLyTracNghiem
{
    public partial class QuanLyKeQuaThi : Form
    {
        SqlConnection connection;
        SqlCommand command;
       cons scon=new cons();
        SqlDataAdapter adapter = new SqlDataAdapter();
        DataTable table = new DataTable();
        void loaddata()
        {
            command = connection.CreateCommand();
            command.CommandText = "select * from tblThongTinKetQuaBaiThi";
            adapter.SelectCommand = command;
            table.Clear();
            adapter.Fill(table);
            dataGridView1.DataSource = table;
        }
        void combo_BaiThi()
        {
            SqlDataAdapter adapter1 = new SqlDataAdapter();
            DataTable table1 = new DataTable();
            command = connection.CreateCommand();
            command.CommandText = "select * from tblThongTinBaiThiCuaHocSinh";
            adapter1.SelectCommand = command;
            table1.Clear();
            adapter1.Fill(table1);
            MaBaiThi.DataSource = table1;
            MaBaiThi.ValueMember = "MaBaiThi";
            MaBaiThi.DisplayMember = "MaBaiThi";
        }
        public QuanLyKeQuaThi()
        {
            InitializeComponent();
        }

        private void frm4_Load(object sender, EventArgs e)
        {
            connection = new SqlConnection(scon.stron);
            connection.Open();
            this.FormClosed += (s, ev) => connection?.Close();
            loaddata();
            combo_BaiThi();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            int i = e.RowIndex;
            MaKetQua.Text = dataGridView1.Rows[i].Cells[0].Value.ToString();
            MaBaiThi.Text = dataGridView1.Rows[i].Cells[1].Value.ToString();
            DiemSo.Text = dataGridView1.Rows[i].Cells[2].Value.ToString();
            TrangThai.Text = dataGridView1.Rows[i].Cells[3].Value.ToString();
        }

       

        private void button2_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(MaKetQua.Text) || string.IsNullOrWhiteSpace(DiemSo.Text))
            {
                MessageBox.Show("Vui lòng chọn kết quả và nhập điểm số!");
                return;
            }
            try
            {
                command = connection.CreateCommand();
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.CommandText = "Update_Table_ThongTinKetQuaBaiThi";
                command.Parameters.AddWithValue("@MaKetQua", MaKetQua.Text);
                command.Parameters.AddWithValue("@MaBaiThi", MaBaiThi.Text);
                command.Parameters.AddWithValue("@DiemSo", DiemSo.Text);
                command.Parameters.AddWithValue("@TrangThai", TrangThai.Text);
                command.ExecuteNonQuery();
                loaddata();
                MessageBox.Show("Chỉnh sửa thành công");
                button4_Click(null, null);
            }
            catch(Exception ex)
            {
                MessageBox.Show("Đã xảy ra lỗi: " + ex.Message);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(MaKetQua.Text))
            {
                MessageBox.Show("Vui lòng chọn kết quả cần xóa!");
                return;
            }
            try
            {
                command = connection.CreateCommand();
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.CommandText = "Delete_Table_ThongTinKetQuaBaiThi";
                command.Parameters.AddWithValue("@MaKetQua", MaKetQua.Text);
                int rows = command.ExecuteNonQuery();
                loaddata();
                if (rows > 0)
                {
                    MessageBox.Show("Xóa thành công");
                    button4_Click(null, null);
                }
                else
                {
                    MessageBox.Show("Không tìm thấy kết quả để xóa (Mã: " + MaKetQua.Text + ")");
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show("Đã xảy ra lỗi: " + ex.Message);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            MaKetQua.Text = "";
            MaBaiThi.Text = "";
            DiemSo.Text = "";
            TrangThai.Text = "";
            dataGridView1.ClearSelection();
            MaBaiThi.Focus();
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }
    }
}
