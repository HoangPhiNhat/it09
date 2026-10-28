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
    public partial class QuanLyBaiThiCuaHocSinh : Form
    {
        SqlConnection connection;
        SqlCommand command;
        cons scon=new cons();
        SqlDataAdapter adapter = new SqlDataAdapter();
        DataTable table = new DataTable();
        void loaddata()
        {
            connection = new SqlConnection(scon.stron);
            connection.Open();
            command = connection.CreateCommand();
            command.CommandText = "select MaBaiThi as N'Mã bài thi', MaHocSinh as N'Mã học sinh', MaDeThi as N'Mã đề thi', ThoiGianThi as N'Thời gian thi' from tblThongTinBaiThiCuaHocSinh";
            adapter.SelectCommand = command;
            table.Clear();
            adapter.Fill(table);
            dataGridView1.DataSource = table;
            connection.Close();
        }
        void Combo_HocSinh()
        {
            connection = new SqlConnection(scon.stron);
            connection.Open();
            DataTable dt1= new DataTable();
            SqlDataAdapter ad1= new SqlDataAdapter();
            command = connection.CreateCommand();
            command.CommandText = "select * from tblThongTinHocSinh";
            ad1.SelectCommand = command;
            dt1.Clear();
            ad1.Fill(dt1);
            MaHocSinh.DataSource = dt1;
            MaHocSinh.ValueMember = "MaHocSinh";
            MaHocSinh.DisplayMember = "MaHocSinh";
            connection.Close();
        }
        void Combo_DeThi()
        {
            connection = new SqlConnection(scon.stron);
            connection.Open();
            DataTable dt2 = new DataTable();
            SqlDataAdapter ad2 = new SqlDataAdapter();
            command = connection.CreateCommand();
            command.CommandText = "select * from tblThongTinKhoDeThi";
            ad2.SelectCommand = command;
            dt2.Clear();
            ad2.Fill(dt2);
            MaDeThi.DataSource = dt2;
            MaDeThi.ValueMember = "MaDeThi";
            MaDeThi.DisplayMember = "MaDeThi";
            connection.Close();
        }
        public QuanLyBaiThiCuaHocSinh()
        {
            InitializeComponent();
        }

        private void frm3_Load(object sender, EventArgs e)
        {
            Combo_DeThi();
            Combo_HocSinh();
            loaddata();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            int i = e.RowIndex;
            MaBaiThi.Text = dataGridView1.Rows[i].Cells[0].Value.ToString();
            MaHocSinh.Text = dataGridView1.Rows[i].Cells[1].Value.ToString();
            MaDeThi.Text = dataGridView1.Rows[i].Cells[2].Value.ToString();
            ThoiGianThi.Text = dataGridView1.Rows[i].Cells[3].Value.ToString();
            MaBaiThi.ReadOnly = true;
            button1.Enabled = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string maBaiThi = MaBaiThi.Text.Trim();
            if (string.IsNullOrWhiteSpace(maBaiThi))
            {
                MessageBox.Show("Vui lòng nhập Mã bài thi!");
                return;
            }
            try
            {
                connection = new SqlConnection(scon.stron);
                connection.Open();
                using (SqlCommand checkCommand = connection.CreateCommand())
                {
                    checkCommand.CommandText = "SELECT COUNT(1) FROM tblThongTinBaiThiCuaHocSinh WHERE MaBaiThi=@MaBaiThi";
                    checkCommand.Parameters.Add("@MaBaiThi", SqlDbType.VarChar, 15).Value = maBaiThi;
                    if ((int)checkCommand.ExecuteScalar() > 0)
                    {
                        MessageBox.Show("Mã bài thi " + maBaiThi + " đã tồn tại. Hãy chọn Sửa hoặc nhập mã khác.",
                            "Trùng mã bài thi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        connection.Close();
                        return;
                    }
                }
                command = connection.CreateCommand();
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.CommandText = "Inser_Table_ThongTinBaiThiCuaHocSinh";
                command.Parameters.AddWithValue("@MaBaiThi", maBaiThi);
                command.Parameters.AddWithValue("@MaHocSinh", MaHocSinh.Text);
                command.Parameters.AddWithValue("@MaDeThi", MaDeThi.Text);
                command.Parameters.Add("@ThoiGianThi", SqlDbType.DateTime).Value = ThoiGianThi.Value;
                command.ExecuteNonQuery();
                connection.Close();
                loaddata();
                MessageBox.Show("Thêm thành công");
                button4_Click(null, null);
            }
            catch(Exception ex)
            {
                MessageBox.Show("Đã xảy ra lỗi: " + ex.Message);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(MaBaiThi.Text))
            {
                MessageBox.Show("Vui lòng chọn bài thi cần xóa!");
                return;
            }
            try
            {
                string maBaiThi = MaBaiThi.Text.Trim();
                connection = new SqlConnection(scon.stron);
                connection.Open();
                int soKetQua;
                using (SqlCommand countCommand = connection.CreateCommand())
                {
                    countCommand.CommandText = "SELECT COUNT(1) FROM tblThongTinKetQuaBaiThi WHERE MaBaiThi=@MaBaiThi";
                    countCommand.Parameters.Add("@MaBaiThi", SqlDbType.VarChar, 15).Value = maBaiThi;
                    soKetQua = (int)countCommand.ExecuteScalar();
                }

                string message = soKetQua > 0
                    ? "Bài thi " + maBaiThi + " có " + soKetQua + " kết quả liên quan. Xóa cả bài thi và kết quả?"
                    : "Bạn có chắc chắn muốn xóa bài thi " + maBaiThi + "?";
                if (MessageBox.Show(message, "Xác nhận xóa", MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning) != DialogResult.Yes)
                {
                    connection.Close();
                    return;
                }

                using (SqlTransaction transaction = connection.BeginTransaction())
                {
                    try
                    {
                        using (SqlCommand deleteCommand = connection.CreateCommand())
                        {
                            deleteCommand.Transaction = transaction;
                            deleteCommand.CommandText = @"
DELETE FROM tblThongTinKetQuaBaiThi WHERE MaBaiThi=@MaBaiThi;
DELETE FROM tblThongTinBaiThiCuaHocSinh WHERE MaBaiThi=@MaBaiThi;";
                            deleteCommand.Parameters.Add("@MaBaiThi", SqlDbType.VarChar, 15).Value = maBaiThi;
                            deleteCommand.ExecuteNonQuery();
                        }
                        transaction.Commit();
                    }
                    catch { transaction.Rollback(); throw; }
                }
                connection.Close();
                loaddata();
                MessageBox.Show("Xóa bài thi thành công.");
                button4_Click(null, null);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Đã xảy ra lỗi: " + ex.Message);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(MaBaiThi.Text))
            {
                MessageBox.Show("Vui lòng chọn bài thi cần sửa!");
                return;
            }
            try
            {
                connection = new SqlConnection(scon.stron);
                connection.Open();
                command = connection.CreateCommand();
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.CommandText = "Update_Table_ThongTinBaiThiCuaHocSinh";
                command.Parameters.AddWithValue("@MaBaiThi", MaBaiThi.Text);
                command.Parameters.AddWithValue("@MaHocSinh", MaHocSinh.Text);
                command.Parameters.AddWithValue("@MaDeThi", MaDeThi.Text);
                command.Parameters.Add("@ThoiGianThi", SqlDbType.DateTime).Value = ThoiGianThi.Value;
                command.ExecuteNonQuery();
                connection.Close();
                loaddata();
                MessageBox.Show("Chỉnh sửa thành công");
                button4_Click(null, null);
            }
            catch(Exception ex)
            {
                MessageBox.Show("Đã xảy ra lỗi: " + ex.Message);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            MaBaiThi.Text = "";
            MaHocSinh.Text = "";
            MaDeThi.Text = "";
            ThoiGianThi.Value = DateTime.Today;
            MaBaiThi.ReadOnly = false;
            button1.Enabled = true;
            dataGridView1.ClearSelection();
            MaHocSinh.Focus();
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }
    }
}
