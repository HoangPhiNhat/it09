using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;

namespace QuanLyTracNghiem
{
    public partial class TracNghiem : Form
    {
        public string TenHocSinh { get; set; }
        public string LopHocSinh { get; set; }
        cons scon = new cons();
        SqlConnection con = new SqlConnection();
        decimal correctAnswers = 0;
        private void LoadRandomQuestions()
        {
            try
            {
                con = new SqlConnection(scon.stron);
                con.Open();
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandText = "DELETE FROM tblCauHoi_new; INSERT INTO tblCauHoi_new EXEC GetRandomQuestions @MaDeThi=@MaDeThi";
                cmd.Parameters.AddWithValue("@MaDeThi", MaDT);
                cmd.ExecuteNonQuery();
                cmd =con.CreateCommand();
                cmd.CommandText = "select * from tblCauHoi_new";
                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = cmd;
                DataTable dt = new DataTable();
                dt.Clear();
                da.Fill(dt);

                // Điền dữ liệu vào các label và các checkbox tương ứng
                if (dt.Rows.Count >= 1)
                {
                    label1.Text = dt.Rows[0]["cauHoi"].ToString();
                    checkBox1.Text = dt.Rows[0]["CauA"].ToString();
                    checkBox2.Text = dt.Rows[0]["CauB"].ToString();
                    checkBox3.Text = dt.Rows[0]["CauC"].ToString();
                    checkBox4.Text = dt.Rows[0]["CauD"].ToString();
                }

                if (dt.Rows.Count >= 2)
                {
                    label2.Text = dt.Rows[1]["cauHoi"].ToString();
                    checkBox5.Text = dt.Rows[1]["CauA"].ToString();
                    checkBox6.Text = dt.Rows[1]["CauB"].ToString();
                    checkBox7.Text = dt.Rows[1]["CauC"].ToString();
                    checkBox8.Text = dt.Rows[1]["CauD"].ToString();
                }
                if (dt.Rows.Count >= 3)
                {
                    label3.Text = dt.Rows[2]["cauHoi"].ToString();
                    checkBox9.Text = dt.Rows[2]["CauA"].ToString();
                    checkBox10.Text = dt.Rows[2]["CauB"].ToString();
                    checkBox11.Text = dt.Rows[2]["CauC"].ToString();
                    checkBox12.Text = dt.Rows[2]["CauD"].ToString();
                }
                if (dt.Rows.Count >= 4)
                {
                    label4.Text = dt.Rows[3]["cauHoi"].ToString();
                    checkBox13.Text = dt.Rows[3]["CauA"].ToString();
                    checkBox14.Text = dt.Rows[3]["CauB"].ToString();
                    checkBox15.Text = dt.Rows[3]["CauC"].ToString();
                    checkBox16.Text = dt.Rows[3]["CauD"].ToString();
                }
                if (dt.Rows.Count >= 5)
                {
                    label5.Text = dt.Rows[4]["cauHoi"].ToString();
                    checkBox17.Text = dt.Rows[4]["CauA"].ToString();
                    checkBox18.Text = dt.Rows[4]["CauB"].ToString();
                    checkBox19.Text = dt.Rows[4]["CauC"].ToString();
                    checkBox20.Text = dt.Rows[4]["CauD"].ToString();
                }
                
                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải câu hỏi: " + ex.Message);
            }
        }
        private string MaHS;
        private string MaDT;
        public TracNghiem(string MaHS, string MaDeThi)
        {
            this.MaHS = MaHS;
            this.MaDT = MaDeThi;
            InitializeComponent();
        }

        private void TracNghiem_Load(object sender, EventArgs e)
        {
            LoadRandomQuestions();
        }

        private void Ketqu_Click(object sender, EventArgs e)
        {
            try
            {
                correctAnswers = 0;
                con.Open();

                SqlCommand cmd = con.CreateCommand();
                cmd.CommandText = "select * from tblCauHoi_new";
                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = cmd;
                DataTable dt = new DataTable();
                dt.Clear();
                da.Fill(dt);

                // Câu đúng khi chọn ĐÚNG TẤT CẢ phương án (hỗ trợ nhiều đáp án đúng)
                // DBNull được coi là false (chưa có đáp án = không đúng)
                bool ToB(object val) => val != DBNull.Value && Convert.ToBoolean(val);

                if (checkBox1.Checked == ToB(dt.Rows[0]["DapAnA"]) &&
                    checkBox2.Checked == ToB(dt.Rows[0]["DapAnB"]) &&
                    checkBox3.Checked == ToB(dt.Rows[0]["DapAnC"]) &&
                    checkBox4.Checked == ToB(dt.Rows[0]["DapAnD"]))
                    correctAnswers++;

                if (checkBox5.Checked == ToB(dt.Rows[1]["DapAnA"]) &&
                    checkBox6.Checked == ToB(dt.Rows[1]["DapAnB"]) &&
                    checkBox7.Checked == ToB(dt.Rows[1]["DapAnC"]) &&
                    checkBox8.Checked == ToB(dt.Rows[1]["DapAnD"]))
                    correctAnswers++;

                if (checkBox9.Checked  == ToB(dt.Rows[2]["DapAnA"]) &&
                    checkBox10.Checked == ToB(dt.Rows[2]["DapAnB"]) &&
                    checkBox11.Checked == ToB(dt.Rows[2]["DapAnC"]) &&
                    checkBox12.Checked == ToB(dt.Rows[2]["DapAnD"]))
                    correctAnswers++;

                if (checkBox13.Checked == ToB(dt.Rows[3]["DapAnA"]) &&
                    checkBox14.Checked == ToB(dt.Rows[3]["DapAnB"]) &&
                    checkBox15.Checked == ToB(dt.Rows[3]["DapAnC"]) &&
                    checkBox16.Checked == ToB(dt.Rows[3]["DapAnD"]))
                    correctAnswers++;

                if (checkBox17.Checked == ToB(dt.Rows[4]["DapAnA"]) &&
                    checkBox18.Checked == ToB(dt.Rows[4]["DapAnB"]) &&
                    checkBox19.Checked == ToB(dt.Rows[4]["DapAnC"]) &&
                    checkBox20.Checked == ToB(dt.Rows[4]["DapAnD"]))
                    correctAnswers++;

                MessageBox.Show($"Đúng {correctAnswers}/5");

                SqlCommand cmd1 = con.CreateCommand();
                cmd1.CommandText = "INSERT INTO tblThongTinBaiThiCuaHocSinh(MaHocSinh, MaDeThi) VALUES (@MaHS, @MaDT)";
                cmd1.Parameters.AddWithValue("@MaHS", MaHS);
                cmd1.Parameters.AddWithValue("@MaDT", MaDT);
                cmd1.ExecuteNonQuery();

                // INSTEAD OF trigger tự sinh MaBaiThi (BT001...) nên SCOPE_IDENTITY() = NULL
                // Lấy MaBaiThi vừa tạo bằng cách query lại
                SqlCommand cmdGetBT = con.CreateCommand();
                cmdGetBT.CommandText = "SELECT TOP 1 MaBaiThi FROM tblThongTinBaiThiCuaHocSinh WHERE MaHocSinh=@MaHS AND MaDeThi=@MaDT ORDER BY MaBaiThi DESC";
                cmdGetBT.Parameters.AddWithValue("@MaHS", MaHS);
                cmdGetBT.Parameters.AddWithValue("@MaDT", MaDT);
                string newMaBaiThi = cmdGetBT.ExecuteScalar().ToString();

                decimal diem = correctAnswers * 20;
                string kq = diem < 50 ? "Rớt" : "Đậu";

                SqlCommand cmd3 = con.CreateCommand();
                cmd3.CommandText = "INSERT INTO tblThongTinKetQuaBaiThi(MaBaiThi, DiemSo, TrangThai) VALUES (@MaBaiThi, @DiemSo, @TrangThai)";
                cmd3.Parameters.AddWithValue("@MaBaiThi", newMaBaiThi);
                cmd3.Parameters.AddWithValue("@DiemSo", diem);
                cmd3.Parameters.AddWithValue("@TrangThai", kq);
                cmd3.ExecuteNonQuery();

                SqlCommand cmd4 = con.CreateCommand();
                cmd4.CommandText = "DELETE FROM tblCauHoi_new";
                cmd4.ExecuteNonQuery();
                con.Close();
                this.Close();


            }
            catch(Exception ex)
            {
                MessageBox.Show("Bị Lỗi"+ex.Message);
               
            }


        }
        
   

        private void label10_Click(object sender, EventArgs e)
        {

        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void checkBox4_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void checkBox1_Click(object sender, EventArgs e) { }
        private void checkBox2_Click(object sender, EventArgs e) { }
        private void checkBox3_Click(object sender, EventArgs e) { }
        private void checkBox4_Click(object sender, EventArgs e) { }
        private void checkBox5_Click(object sender, EventArgs e) { }
        private void checkBox6_Click(object sender, EventArgs e) { }
        private void checkBox7_Click(object sender, EventArgs e) { }
        private void checkBox8_Click(object sender, EventArgs e) { }
        private void checkBox9_Click(object sender, EventArgs e) { }
        private void checkBox10_Click(object sender, EventArgs e) { }
        private void checkBox11_Click(object sender, EventArgs e) { }
        private void checkBox12_Click(object sender, EventArgs e) { }
        private void checkBox13_Click(object sender, EventArgs e) { }
        private void checkBox14_Click(object sender, EventArgs e) { }
        private void checkBox15_Click(object sender, EventArgs e) { }
        private void checkBox16_Click(object sender, EventArgs e) { }
        private void checkBox17_Click(object sender, EventArgs e) { }
        private void checkBox18_Click(object sender, EventArgs e) { }
        private void checkBox19_CheckedChanged(object sender, EventArgs e) { }
        private void checkBox20_CheckedChanged(object sender, EventArgs e) { }
        private void checkBox12_CheckedChanged(object sender, EventArgs e) { }
    }
}
