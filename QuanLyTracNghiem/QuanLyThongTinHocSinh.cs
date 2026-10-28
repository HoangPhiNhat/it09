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
    public partial class QuanLyThongTinHocSinh : Form
    {
        SqlConnection connection;
        SqlCommand command;
        cons scon=new cons();
        SqlDataAdapter adapter = new SqlDataAdapter();
        DataTable table = new DataTable();
        void loaddata()
        {
            command = connection.CreateCommand();
            command.CommandText = "select MaHocSinh as N'Mã học sinh', TenHocSinh as N'Tên học sinh', GioiTinh as N'Giới tính', NgaySinh as N'Ngày sinh', LopHoc as N'Lớp học' from tblThongTinHocSinh";
            adapter.SelectCommand = command;
            table.Clear();
            adapter.Fill(table);
            dataGridView1.DataSource = table;
        }
        public QuanLyThongTinHocSinh()
        {
            InitializeComponent();
        }

        private void frm1_Load(object sender, EventArgs e)
        {
            connection = new SqlConnection(scon.stron);
            connection.Open();
            this.FormClosed += (s, ev) => connection?.Close();
            loaddata();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
                return;

            int i = e.RowIndex;
            MaHocSinh.Text = dataGridView1.Rows[i].Cells[0].Value.ToString();
            TenHocSinh.Text = dataGridView1.Rows[i].Cells[1].Value.ToString();
            GioiTinh.Text = dataGridView1.Rows[i].Cells[2].Value.ToString();
            DateTime ngaySinh;
            if (DateTime.TryParse(dataGridView1.Rows[i].Cells[3].Value.ToString(), out ngaySinh))
                NgaySinh.Value = ngaySinh;
            LopHoc.Text = dataGridView1.Rows[i].Cells[4].Value.ToString();

            // Khi đã chọn một dòng thì chỉ cho phép sửa/xóa, không chèn lại mã cũ.
            MaHocSinh.ReadOnly = true;
            button1.Enabled = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string maHocSinh = MaHocSinh.Text.Trim();
            if (string.IsNullOrWhiteSpace(maHocSinh) || string.IsNullOrWhiteSpace(TenHocSinh.Text))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ Mã học sinh và Tên học sinh!");
                return;
            }
            try
            {
                using (SqlCommand checkCommand = connection.CreateCommand())
                {
                    checkCommand.CommandText = "SELECT COUNT(1) FROM tblThongTinHocSinh WHERE MaHocSinh = @MaHocSinh";
                    checkCommand.Parameters.Add("@MaHocSinh", SqlDbType.VarChar, 15).Value = maHocSinh;
                    if ((int)checkCommand.ExecuteScalar() > 0)
                    {
                        MessageBox.Show("Mã học sinh " + maHocSinh + " đã tồn tại. Hãy chọn Sửa hoặc nhập mã khác.",
                            "Trùng mã học sinh", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }

                command = connection.CreateCommand();
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.CommandText = "Inser_Table_ThongTinHocSinh";
                command.Parameters.AddWithValue("@MaHocSinh", maHocSinh);
                command.Parameters.AddWithValue("@TenHocSinh", TenHocSinh.Text.Trim());
                command.Parameters.AddWithValue("@GioiTinh", GioiTinh.Text);
                command.Parameters.Add("@NgaySinh", SqlDbType.Date).Value = NgaySinh.Value.Date;
                command.Parameters.AddWithValue("@LopHoc", LopHoc.Text.Trim());
                command.ExecuteNonQuery();
                loaddata();
                MessageBox.Show("Thêm thành công");
                button4_Click(null, null);
            }
            catch(Exception ex)
            {
                MessageBox.Show("Đã xảy ra lỗi: " + ex.Message);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(MaHocSinh.Text))
            {
                MessageBox.Show("Vui lòng chọn học sinh cần sửa!");
                return;
            }
            try
            {
                command = connection.CreateCommand();
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.CommandText = "Update_Table_ThongTinHocSinh";
                command.Parameters.AddWithValue("@MaHocSinh", MaHocSinh.Text);
                command.Parameters.AddWithValue("@TenHocSinh", TenHocSinh.Text);
                command.Parameters.AddWithValue("@GioiTinh", GioiTinh.Text);
                command.Parameters.Add("@NgaySinh", SqlDbType.Date).Value = NgaySinh.Value.Date;
                command.Parameters.AddWithValue("@LopHoc", LopHoc.Text);
                command.ExecuteNonQuery();
                loaddata();
                MessageBox.Show("Chỉnh sửa thành công");
                button4_Click(null, null);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Đã xảy ra lỗi: " + ex.Message);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(MaHocSinh.Text))
            {
                MessageBox.Show("Vui lòng chọn học sinh cần xóa!");
                return;
            }

            string maHocSinh = MaHocSinh.Text.Trim();
            try
            {
                int soBaiThi;
                using (SqlCommand countCommand = connection.CreateCommand())
                {
                    countCommand.CommandText =
                        "SELECT COUNT(1) FROM tblThongTinBaiThiCuaHocSinh WHERE MaHocSinh = @MaHocSinh";
                    countCommand.Parameters.Add("@MaHocSinh", SqlDbType.VarChar, 15).Value = maHocSinh;
                    soBaiThi = (int)countCommand.ExecuteScalar();
                }

                string noiDungXacNhan = soBaiThi > 0
                    ? "Học sinh " + maHocSinh + " có " + soBaiThi +
                      " bài thi. Xóa học sinh sẽ xóa cả bài thi và kết quả liên quan.\nBạn có chắc chắn muốn xóa?"
                    : "Bạn có chắc chắn muốn xóa học sinh " + maHocSinh + "?";

                if (MessageBox.Show(noiDungXacNhan, "Xác nhận xóa",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != DialogResult.Yes)
                    return;

                using (SqlTransaction transaction = connection.BeginTransaction())
                {
                    try
                    {
                        using (SqlCommand deleteCommand = connection.CreateCommand())
                        {
                            deleteCommand.Transaction = transaction;
                            deleteCommand.CommandText = @"
DELETE FROM tblThongTinKetQuaBaiThi
WHERE MaBaiThi IN (
    SELECT MaBaiThi FROM tblThongTinBaiThiCuaHocSinh WHERE MaHocSinh = @MaHocSinh
);
DELETE FROM tblThongTinBaiThiCuaHocSinh WHERE MaHocSinh = @MaHocSinh;
DELETE FROM tblThongTinHocSinh WHERE MaHocSinh = @MaHocSinh;";
                            deleteCommand.Parameters.Add("@MaHocSinh", SqlDbType.VarChar, 15).Value = maHocSinh;
                            deleteCommand.ExecuteNonQuery();
                        }

                        transaction.Commit();
                    }
                    catch
                    {
                        transaction.Rollback();
                        throw;
                    }
                }

                loaddata();
                MessageBox.Show("Xóa học sinh thành công.");
                button4_Click(null, null);
            }
            catch(Exception ex)
            {
                MessageBox.Show("Đã xảy ra lỗi: " + ex.Message);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            MaHocSinh.Text = "";
            TenHocSinh.Text = "";
            GioiTinh.Text = "";
            NgaySinh.Value = DateTime.Today;
            LopHoc.Text = "";
            MaHocSinh.ReadOnly = false;
            button1.Enabled = true;
            dataGridView1.ClearSelection();
            MaHocSinh.Focus();
        }
    }
}
