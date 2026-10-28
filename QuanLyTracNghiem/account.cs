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
    public partial class account : Form
    {
         
        SqlConnection con=new SqlConnection();
        cons scon= new cons();
        
        public account()
        {
            InitializeComponent();
        }

        private void enter_Click(object sender, EventArgs e)
        {
            try
            {
                con = new SqlConnection(scon.stron);
                con.Open();

                string tk = accounting.Text;
                string mk = passwork.Text;

                // Sử dụng tham số hóa để tránh lỗ hổng bảo mật SQL Injection
                string sql = "SELECT * FROM tblDangNhap WHERE TaiKhoan=@TaiKhoan AND MatKhau=@MatKhau";
                SqlCommand cmd = new SqlCommand(sql, con);
                cmd.Parameters.AddWithValue("@TaiKhoan", tk);
                cmd.Parameters.AddWithValue("@MatKhau", HashPassword(mk));

                SqlDataReader dta = cmd.ExecuteReader();

                if (dta.Read())
                {
                    this.Hide();
                    TrangChu frm = new TrangChu();
                    frm.ShowDialog();
                    this.Close();
                   
                }
                else
                {
                    MessageBox.Show("Mật khẩu hoặc tài khoản sai, vui lòng nhập lại.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi kết nối: " + ex.Message);
            }
            finally
            {
                con.Close();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void label4_Click(object sender, EventArgs e)
        {
            this.Hide();
            creataccont frm = new creataccont();
            frm.ShowDialog();
            this.Close();
        }

        private void accounting_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) { 
                enter.PerformClick();
            }
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

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
    }
}
