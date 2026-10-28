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
using System.Windows.Input;

namespace QuanLyTracNghiem
{
    public partial class QuanLyDapAn : Form
    {

        SqlConnection connection;
        SqlCommand command;
        cons scon= new cons();
        SqlDataAdapter adapter = new SqlDataAdapter();
        DataTable table = new DataTable();
        void loaddata()
        {
            command = connection.CreateCommand();
            command.CommandText = "select * from tblCauHoi";
            adapter.SelectCommand = command;
            table.Clear();
            adapter.Fill(table);
            dataGridView1.DataSource = table;
        }
        public QuanLyDapAn()
        {
            InitializeComponent();
        }
        void combo_DeThi()
        {
            SqlDataAdapter adapter1 = new SqlDataAdapter();
            DataTable table1 = new DataTable();
            command = connection.CreateCommand();
            command.CommandText = "select * from tblThongTinKhoDeThi";
            adapter1.SelectCommand = command;
            table1.Clear();
            adapter1.Fill(table1);
            MaDeThi.DataSource = table1;
            MaDeThi.ValueMember = "MaDeThi";
            MaDeThi.DisplayMember = "MaDeThi";
        }
        private void frm7_Load(object sender, EventArgs e)
        {
            connection = new SqlConnection(scon.stron);
            connection.Open();
            this.FormClosed += (s, ev) => connection?.Close();
            loaddata();
            combo_DeThi();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            int i = e.RowIndex;
            MaCauHoi.Text = dataGridView1.Rows[i].Cells[0].Value.ToString();
            MaDeThi.Text = dataGridView1.Rows[i].Cells[1].Value.ToString();
            CauHoi.Text = dataGridView1.Rows[i].Cells[2].Value.ToString();
            CauA.Text = dataGridView1.Rows[i].Cells[3].Value.ToString();
            CauB.Text = dataGridView1.Rows[i].Cells[4].Value.ToString();
            CauC.Text = dataGridView1.Rows[i].Cells[5].Value.ToString();
            CauD.Text = dataGridView1.Rows[i].Cells[6].Value.ToString();
            A.Text = dataGridView1.Rows[i].Cells[7].Value.ToString();
            B.Text = dataGridView1.Rows[i].Cells[8].Value.ToString();
            C.Text = dataGridView1.Rows[i].Cells[9].Value.ToString();
            D.Text = dataGridView1.Rows[i].Cells[10].Value.ToString();
            KetQua.Text = dataGridView1.Rows[i].Cells[11].Value.ToString();
            MaCauHoi.ReadOnly = true;
            button1.Enabled = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string maCauHoi = MaCauHoi.Text.Trim();
            if (string.IsNullOrWhiteSpace(maCauHoi) || string.IsNullOrWhiteSpace(CauHoi.Text))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ Mã câu hỏi và Nội dung câu hỏi!");
                return;
            }
            if (!CoDapAnDung())
            {
                MessageBox.Show("Phải chọn ít nhất một đáp án đúng (True)!",
                    "Thiếu đáp án đúng", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            try
            {
                using (SqlCommand checkCommand = connection.CreateCommand())
                {
                    checkCommand.CommandText = "SELECT COUNT(1) FROM tblCauHoi WHERE MaCauHoi=@MaCauHoi";
                    checkCommand.Parameters.Add("@MaCauHoi", SqlDbType.VarChar, 15).Value = maCauHoi;
                    if ((int)checkCommand.ExecuteScalar() > 0)
                    {
                        MessageBox.Show("Mã câu hỏi " + maCauHoi + " đã tồn tại. Hãy chọn Sửa hoặc nhập mã khác.",
                            "Trùng mã câu hỏi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }
                command = connection.CreateCommand();
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.CommandText = "Insert_CauHoi";
                command.Parameters.AddWithValue("@MaCauHoi", maCauHoi);
                command.Parameters.AddWithValue("@MaDeThi", MaDeThi.Text);
                command.Parameters.AddWithValue("@cauHoi", CauHoi.Text);
                command.Parameters.AddWithValue("@CauA", CauA.Text);
                command.Parameters.AddWithValue("@CauB", CauB.Text);
                command.Parameters.AddWithValue("@CauC", CauC.Text);
                command.Parameters.AddWithValue("@CauD", CauD.Text);
                command.Parameters.AddWithValue("@DapAnA", A.Text);
                command.Parameters.AddWithValue("@DapAnB", B.Text);
                command.Parameters.AddWithValue("@DapAnC", C.Text);
                command.Parameters.AddWithValue("@DapAnD", D.Text);
                command.Parameters.AddWithValue("@KetQua", KetQua.Text);
                command.ExecuteNonQuery();
                loaddata();
                MessageBox.Show("Thêm thông tin thành công");
                button4_Click(null, null);
            }catch (Exception ex) { MessageBox.Show("Lỗi kết nối: " + ex.Message); }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(MaCauHoi.Text))
            {
                MessageBox.Show("Vui lòng chọn câu hỏi cần sửa!");
                return;
            }
            if (!CoDapAnDung())
            {
                MessageBox.Show("Phải chọn ít nhất một đáp án đúng (True)!",
                    "Thiếu đáp án đúng", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            try
            {
                command = connection.CreateCommand();
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.CommandText = "Update_CauHoi";
                command.Parameters.AddWithValue("@MaCauHoi", MaCauHoi.Text);
                command.Parameters.AddWithValue("@MaDeThi", MaDeThi.Text);
                command.Parameters.AddWithValue("@cauHoi", CauHoi.Text);
                command.Parameters.AddWithValue("@CauA", CauA.Text);
                command.Parameters.AddWithValue("@CauB", CauB.Text);
                command.Parameters.AddWithValue("@CauC", CauC.Text);
                command.Parameters.AddWithValue("@CauD", CauD.Text);
                command.Parameters.AddWithValue("@DapAnA", A.Text);
                command.Parameters.AddWithValue("@DapAnB", B.Text);
                command.Parameters.AddWithValue("@DapAnC", C.Text);
                command.Parameters.AddWithValue("@DapAnD", D.Text);
                command.Parameters.AddWithValue("@KetQua", KetQua.Text);
                command.ExecuteNonQuery();
                loaddata();
                MessageBox.Show("Chỉnh sửa thông tin thành công");
                button4_Click(null, null);
            }
            catch (Exception ex) { MessageBox.Show("Lỗi kết nối: " + ex.Message); }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(MaCauHoi.Text))
            {
                MessageBox.Show("Vui lòng chọn câu hỏi cần xóa!");
                return;
            }

            string maCauHoi = MaCauHoi.Text.Trim();
            try
            {
                using (SqlCommand checkCommand = connection.CreateCommand())
                {
                    checkCommand.CommandText = "SELECT COUNT(1) FROM tblCauHoi WHERE MaCauHoi=@MaCauHoi";
                    checkCommand.Parameters.Add("@MaCauHoi", SqlDbType.VarChar, 15).Value = maCauHoi;
                    if ((int)checkCommand.ExecuteScalar() == 0)
                    {
                        MessageBox.Show("Câu hỏi " + maCauHoi + " không còn tồn tại. Danh sách sẽ được tải lại.",
                            "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        loaddata();
                        button4_Click(null, null);
                        return;
                    }
                }

                if (MessageBox.Show("Bạn có chắc chắn muốn xóa câu hỏi " + maCauHoi + "?",
                    "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != DialogResult.Yes)
                    return;

                using (SqlTransaction transaction = connection.BeginTransaction())
                {
                    try
                    {
                        using (SqlCommand deleteCommand = connection.CreateCommand())
                        {
                            deleteCommand.Transaction = transaction;
                            deleteCommand.CommandText = @"
DELETE FROM tblCauHoi_new WHERE MaCauHoi=@MaCauHoi;
DELETE FROM tblCauHoi WHERE MaCauHoi=@MaCauHoi;";
                            deleteCommand.Parameters.Add("@MaCauHoi", SqlDbType.VarChar, 15).Value = maCauHoi;
                            deleteCommand.ExecuteNonQuery();
                        }
                        transaction.Commit();
                    }
                    catch { transaction.Rollback(); throw; }
                }
                loaddata();
                MessageBox.Show("Xóa câu hỏi thành công.");
                button4_Click(null, null);
            }
            catch (Exception ex) { MessageBox.Show("Lỗi kết nối: " + ex.Message); }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            MaCauHoi.Text = "";
            CauHoi.Text ="";
            CauA.Text = "";
            CauB.Text = "";
            CauC.Text = "";
            CauD.Text = "";
            A.Text = "";
            B.Text ="";
            C.Text = "";
            D.Text ="";
            KetQua.Text = "";
            MaCauHoi.ReadOnly = false;
            button1.Enabled = true;
            dataGridView1.ClearSelection();
            MaDeThi.Focus();
        }

        private void UpdateKetQua()
        {
            var correct = new System.Collections.Generic.List<string>();
            if (A.Text == "True") correct.Add("A");
            if (B.Text == "True") correct.Add("B");
            if (C.Text == "True") correct.Add("C");
            if (D.Text == "True") correct.Add("D");
            KetQua.Text = string.Join(",", correct);
        }

        private bool CoDapAnDung()
        {
            return A.Text == "True" || B.Text == "True" ||
                   C.Text == "True" || D.Text == "True";
        }

        private void A_SelectedIndexChanged(object sender, EventArgs e) => UpdateKetQua();
        private void B_SelectedIndexChanged(object sender, EventArgs e) => UpdateKetQua();
        private void C_SelectedIndexChanged(object sender, EventArgs e) => UpdateKetQua();
        private void D_SelectedIndexChanged(object sender, EventArgs e) => UpdateKetQua();

        private void MaDeThi_SelectedIndexChanged(object sender, EventArgs e)
        {
            command = connection.CreateCommand();
            command.CommandText = "select * from tblThongTinKhoDeThi where MaDeThi=@MaDeThi";
            command.Parameters.AddWithValue("@MaDeThi", MaDeThi.Text);
            SqlDataReader ra1 = command.ExecuteReader();
            if (ra1.Read()) {
                MonDeThi.Text = ra1["MonDeThi"].ToString();
            }
            ra1.Close();
        }
    }
}
