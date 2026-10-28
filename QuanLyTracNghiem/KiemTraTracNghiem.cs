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
    public partial class KiemTraTracNghiem : Form
    {
        
       
        public KiemTraTracNghiem()
        {
            InitializeComponent();
        }
        cons scon = new cons();
        SqlConnection connection = new SqlConnection();
        SqlCommand cmd = new SqlCommand();
        void combo_MaDeThi()
        {
            DataTable dt1 = new DataTable();
            using (SqlConnection conn = new SqlConnection(scon.stron))
            using (SqlDataAdapter adapter = new SqlDataAdapter(@"
SELECT d.MaDeThi, d.MonDeThi, d.MaDeThi + N' - ' + d.MonDeThi AS HienThi
FROM tblThongTinKhoDeThi d
INNER JOIN tblCauHoi c ON d.MaDeThi=c.MaDeThi
GROUP BY d.MaDeThi, d.MonDeThi
HAVING COUNT(c.MaCauHoi) >= 5
ORDER BY d.MaDeThi", conn))
            {
                adapter.Fill(dt1);
            }
            MaDeThi.DataSource = dt1;
            MaDeThi.ValueMember = "MaDeThi";
            MaDeThi.DisplayMember = "HienThi";
        }

        private bool TaiThongTinHocSinh()
        {
            string maHocSinh = MaHocSinh.Text.Trim();
            TenHS.Clear();
            Lop.Clear();
            if (maHocSinh.Length == 0) return false;

            using (SqlConnection conn = new SqlConnection(scon.stron))
            using (SqlCommand command = conn.CreateCommand())
            {
                command.CommandText = "SELECT TenHocSinh, LopHoc FROM tblThongTinHocSinh WHERE MaHocSinh=@MaHocSinh";
                command.Parameters.Add("@MaHocSinh", SqlDbType.VarChar, 15).Value = maHocSinh;
                conn.Open();
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (!reader.Read()) return false;
                    TenHS.Text = reader["TenHocSinh"].ToString();
                    Lop.Text = reader["LopHoc"].ToString();
                    return true;
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(MaHocSinh.Text))
            {
                MessageBox.Show("Vui lòng nhập mã học sinh.");
                MaHocSinh.Focus();
                return;
            }
            if (!TaiThongTinHocSinh())
            {
                MessageBox.Show("Không tìm thấy mã học sinh " + MaHocSinh.Text.Trim() + ".");
                MaHocSinh.Focus();
                return;
            }
            if (MaDeThi.SelectedValue == null)
            {
                MessageBox.Show("Không có đề thi đủ 5 câu hỏi để lựa chọn.");
                return;
            }

            string maHocSinh = MaHocSinh.Text.Trim();
            string maDeThi = MaDeThi.SelectedValue.ToString();
            int soLanThi;
            using (SqlConnection conn = new SqlConnection(scon.stron))
            using (SqlCommand command = conn.CreateCommand())
            {
                command.CommandText = "SELECT COUNT(1) FROM tblThongTinBaiThiCuaHocSinh WHERE MaHocSinh=@MaHocSinh AND MaDeThi=@MaDeThi";
                command.Parameters.Add("@MaHocSinh", SqlDbType.VarChar, 15).Value = maHocSinh;
                command.Parameters.Add("@MaDeThi", SqlDbType.VarChar, 15).Value = maDeThi;
                conn.Open();
                soLanThi = (int)command.ExecuteScalar();
            }

            string xacNhan = "Học sinh: " + TenHS.Text + " (" + maHocSinh + ")\nLớp: " + Lop.Text +
                              "\nĐề thi: " + MaDeThi.Text + "\nSố câu hỏi: 5\n\nBắt đầu làm bài?";
            if (soLanThi > 0)
                xacNhan = "Học sinh đã làm đề này " + soLanThi + " lần.\n\n" + xacNhan;
            if (MessageBox.Show(xacNhan, "Xác nhận bắt đầu", MessageBoxButtons.YesNo,
                MessageBoxIcon.Question) != DialogResult.Yes) return;

            Hide();
            using (TracNghiem frm = new TracNghiem(maHocSinh, maDeThi))
                frm.ShowDialog(this);
            Show();
            Activate();
        }

        private void KiemTraTracNghiem_Load(object sender, EventArgs e)
        {
            TenHS.ReadOnly = true;
            Lop.ReadOnly = true;
            MonThi.ReadOnly = true;
            MaDeThi.DropDownStyle = ComboBoxStyle.DropDownList;
            combo_MaDeThi();
            MaHocSinh.Focus();
        }

        private void MaDeThi_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataRowView row = MaDeThi.SelectedItem as DataRowView;
            MonThi.Text = row == null ? "" : row["MonDeThi"].ToString();
        }

        private void MaHocSinh_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(MaHocSinh.Text) && !TaiThongTinHocSinh())
                MessageBox.Show("Không tìm thấy mã học sinh " + MaHocSinh.Text.Trim() + ".");
        }
    }
}
