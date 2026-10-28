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
    public partial class creataccont : Form
    {
        cons scon=new cons();
        SqlConnection con = new SqlConnection();
        public creataccont()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            account frm = new account();
            frm.ShowDialog();
            this.Close();
        }

        private void DongY_Click(object sender, EventArgs e)
        {
            string tk = accounting.Text;
            string mk = passwork.Text;
            string mkConfirm = passworkcomf.Text;

            bool accountExists = false;
            try
            {
                con = new SqlConnection(scon.stron);
                con.Open();
                string sqlCheck = "SELECT * FROM tblDangNhap WHERE TaiKhoan=@TaiKhoan";
                SqlCommand cmdCheck = new SqlCommand(sqlCheck, con);
                cmdCheck.Parameters.AddWithValue("@TaiKhoan", tk);
                using (SqlDataReader dtaCheck = cmdCheck.ExecuteReader())
                {
                    accountExists = dtaCheck.Read();
                }
            }
            finally
            {
                con.Close();
            }

            if (accountExists)
            {
                MessageBox.Show("Tài khoản đã tồn tại!");
                return;
            }

            if (mk != mkConfirm)
            {
                MessageBox.Show("Xác Nhận Mật Khẩu Chưa Chính Xác!");
                return;
            }

            try
            {
                string sqlInsert = "INSERT INTO tblDangNhap (TaiKhoan, MatKhau) VALUES (@TaiKhoan, @MatKhau)";
                SqlCommand cmdInsert = new SqlCommand(sqlInsert, con);
                cmdInsert.Parameters.AddWithValue("@TaiKhoan", tk);
                cmdInsert.Parameters.AddWithValue("@MatKhau", HashPassword(mk));
                con.Open();
                int rowsAffected = cmdInsert.ExecuteNonQuery();
                if (rowsAffected > 0)
                {
                    MessageBox.Show("Thêm tài khoản thành công!");
                    accounting.Text = "";
                    passwork.Text = "";
                    passworkcomf.Text = "";
                }
                else
                {
                    MessageBox.Show("Thêm tài khoản không thành công!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Đã xảy ra lỗi: " + ex.Message);
            }
            finally
            {
                con.Close();
            }
        }

        private static string HashPassword(string password)
        {
            using (var sha256 = System.Security.Cryptography.SHA256.Create())
            {
                byte[] bytes = System.Text.Encoding.UTF8.GetBytes(password);
                byte[] hash = sha256.ComputeHash(bytes);
                return Convert.ToBase64String(hash);
            }
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void accounting_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                DongY.PerformClick();
            }
        }
    }
}
