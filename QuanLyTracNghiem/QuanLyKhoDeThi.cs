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
    public partial class QuanLyKhoDeThi : Form
    {
        public QuanLyKhoDeThi()
        {
            InitializeComponent();
        }
        SqlConnection connection;
        SqlCommand command;
        cons cons =new cons();
        SqlDataAdapter adapter = new SqlDataAdapter();
        DataTable table = new DataTable();
        void loaddata()
        {
            command = connection.CreateCommand();
            command.CommandText = "select MaDeThi as N'Mã đề thi', MonDeThi as N'Môn đề thi', NoiDungThi as N'Nội dung thi', MucDo as N'Mức độ' from tblThongTinKhoDeThi";
            adapter.SelectCommand = command;
            table.Clear();
            adapter.Fill(table);
            dataGridView1.DataSource = table;
        }
        private void QuanLyKhoDeThi_Load(object sender, EventArgs e)
        {
            connection = new SqlConnection(cons.stron);
            connection.Open();
            this.FormClosed += (s, ev) => connection?.Close();
            loaddata();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            int i = e.RowIndex;
            MaDeThi.Text = dataGridView1.Rows[i].Cells[0].Value.ToString();
            MoDeThi.Text = dataGridView1.Rows[i].Cells[1].Value.ToString();
            NoiDungThi.Text = dataGridView1.Rows[i].Cells[2].Value.ToString();
            MucDoThi.Text = dataGridView1.Rows[i].Cells[3].Value.ToString();
            MaDeThi.ReadOnly = true;
            button1.Enabled = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string maDeThi = MaDeThi.Text.Trim();
            if (string.IsNullOrWhiteSpace(maDeThi) || string.IsNullOrWhiteSpace(MoDeThi.Text))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ Mã đề thi và Môn đề thi!");
                return;
            }
            try
            {
                using (SqlCommand checkCommand = connection.CreateCommand())
                {
                    checkCommand.CommandText = "SELECT COUNT(1) FROM tblThongTinKhoDeThi WHERE MaDeThi=@MaDeThi";
                    checkCommand.Parameters.Add("@MaDeThi", SqlDbType.VarChar, 15).Value = maDeThi;
                    if ((int)checkCommand.ExecuteScalar() > 0)
                    {
                        MessageBox.Show("Mã đề thi " + maDeThi + " đã tồn tại. Hãy chọn Sửa hoặc nhập mã khác.",
                            "Trùng mã đề thi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }
                command = connection.CreateCommand();
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.CommandText = "Inser_Table_ThongTinKhoDeThi";
                command.Parameters.AddWithValue("@MaDeThi", maDeThi);
                command.Parameters.AddWithValue("@MonDeThi", MoDeThi.Text);
                command.Parameters.AddWithValue("@NoiDungThi", NoiDungThi.Text);
                command.Parameters.AddWithValue("@MucDo", MucDoThi.Text);
                command.ExecuteNonQuery();
                loaddata();
                MessageBox.Show("Thêm dữ liệu thành công");
                button4_Click(null, null);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Thêm dữ liệu thất bại"+ex.Message);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(MaDeThi.Text))
            {
                MessageBox.Show("Vui lòng chọn đề thi cần sửa!");
                return;
            }
            try
            {
                command = connection.CreateCommand();
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.CommandText = "Update_Table_ThongTinKhoDeThi";
                command.Parameters.AddWithValue("@MaDeThi", MaDeThi.Text);
                command.Parameters.AddWithValue("@MonDeThi", MoDeThi.Text);
                command.Parameters.AddWithValue("@NoiDungThi", NoiDungThi.Text);
                command.Parameters.AddWithValue("@MucDo", MucDoThi.Text);
                command.ExecuteNonQuery();
                loaddata();
                MessageBox.Show("Chỉnh sửa thành công");
                button4_Click(null, null);
            }
            catch (Exception ex) { MessageBox.Show("Đã xảy ra lỗi: " + ex.Message); }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(MaDeThi.Text))
            {
                MessageBox.Show("Vui lòng chọn đề thi cần xóa!");
                return;
            }
            try
            {
                string maDeThi = MaDeThi.Text.Trim();
                int soCauHoi, soBaiThi, soKetQua;
                using (SqlCommand countCommand = connection.CreateCommand())
                {
                    countCommand.CommandText = @"SELECT
(SELECT COUNT(1) FROM tblCauHoi WHERE MaDeThi=@MaDeThi),
(SELECT COUNT(1) FROM tblThongTinBaiThiCuaHocSinh WHERE MaDeThi=@MaDeThi),
(SELECT COUNT(1) FROM tblThongTinKetQuaBaiThi k INNER JOIN tblThongTinBaiThiCuaHocSinh b ON k.MaBaiThi=b.MaBaiThi WHERE b.MaDeThi=@MaDeThi)";
                    countCommand.Parameters.Add("@MaDeThi", SqlDbType.VarChar, 15).Value = maDeThi;
                    using (SqlDataReader reader = countCommand.ExecuteReader())
                    {
                        reader.Read();
                        soCauHoi = reader.GetInt32(0);
                        soBaiThi = reader.GetInt32(1);
                        soKetQua = reader.GetInt32(2);
                    }
                }

                string message = "Bạn có chắc chắn muốn xóa đề thi " + maDeThi + "?";
                if (soCauHoi + soBaiThi + soKetQua > 0)
                    message = "Đề thi " + maDeThi + " có " + soCauHoi + " câu hỏi, " + soBaiThi +
                              " bài thi và " + soKetQua + " kết quả.\nXóa đề thi sẽ xóa toàn bộ dữ liệu này. Bạn có chắc chắn?";
                if (MessageBox.Show(message, "Xác nhận xóa", MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning) != DialogResult.Yes) return;

                using (SqlTransaction transaction = connection.BeginTransaction())
                {
                    try
                    {
                        using (SqlCommand deleteCommand = connection.CreateCommand())
                        {
                            deleteCommand.Transaction = transaction;
                            deleteCommand.CommandText = @"
DELETE FROM tblThongTinKetQuaBaiThi WHERE MaBaiThi IN (SELECT MaBaiThi FROM tblThongTinBaiThiCuaHocSinh WHERE MaDeThi=@MaDeThi);
DELETE FROM tblThongTinBaiThiCuaHocSinh WHERE MaDeThi=@MaDeThi;
DELETE FROM tblCauHoi_new WHERE MaDeThi=@MaDeThi;
DELETE FROM tblCauHoi WHERE MaDeThi=@MaDeThi;
DELETE FROM tblThongTinKhoDeThi WHERE MaDeThi=@MaDeThi;";
                            deleteCommand.Parameters.Add("@MaDeThi", SqlDbType.VarChar, 15).Value = maDeThi;
                            deleteCommand.ExecuteNonQuery();
                        }
                        transaction.Commit();
                    }
                    catch { transaction.Rollback(); throw; }
                }
                loaddata();
                MessageBox.Show("Xóa đề thi thành công.");
                button4_Click(null, null);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Đã xảy ra lỗi: " + ex.Message);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            MaDeThi.Text = "";
            MoDeThi.Text = "";
            NoiDungThi.Text = "";
            MucDoThi.Text = "";
            MaDeThi.ReadOnly = false;
            button1.Enabled = true;
            dataGridView1.ClearSelection();
            MaDeThi.Focus();
        }
    }
}
