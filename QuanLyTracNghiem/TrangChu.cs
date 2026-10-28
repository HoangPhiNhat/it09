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
    public partial class TrangChu : Form
    {
        public TrangChu()
        {
            InitializeComponent();
        }


        private void tblThongTinHocSinhToolStripMenuItem_Click(object sender, EventArgs e)
        {
            QuanLyThongTinHocSinh frm = new QuanLyThongTinHocSinh();
            frm.ShowDialog();
        }

        private void tblThongTinKhoDeThiToolStripMenuItem_Click(object sender, EventArgs e)
        {
            QuanLyKhoDeThi frm2 = new QuanLyKhoDeThi();
            frm2.ShowDialog();
        }

        private void tblThongTinBaiThiCuaHocSinhToolStripMenuItem_Click(object sender, EventArgs e)
        {
            QuanLyBaiThiCuaHocSinh frm3 = new QuanLyBaiThiCuaHocSinh();
            frm3.ShowDialog();
        }

        private void tblThongTinKetQuaBaiThiToolStripMenuItem_Click(object sender, EventArgs e)
        {
            QuanLyKeQuaThi frm4 = new QuanLyKeQuaThi();
            frm4.ShowDialog();
           
        }


        private void diemMonHọcToolStripMenuItem_Click(object sender, EventArgs e)
        {
            BaoCao frm5 = new BaoCao();
            frm5.ShowDialog();
        }

        
        private void kiểmTraTrắcNghiệmToolStripMenuItem_Click(object sender, EventArgs e)
        {
            QuanLyDapAn frm7 = new QuanLyDapAn();
            frm7.ShowDialog();
        }

        private void trắcNghiệmToolStripMenuItem_Click(object sender, EventArgs e)
        {
            BangThongKeHocSinh frm4 = new BangThongKeHocSinh();
            frm4.ShowDialog();
        }

        private void thiTrắcNghiệmToolStripMenuItem_Click(object sender, EventArgs e)
        {
            KiemTraTracNghiem frm = new KiemTraTracNghiem();
            frm.ShowDialog();
        }

        private void thốngKêĐiểmSỐToolStripMenuItem_Click(object sender, EventArgs e)
        {
            BangThongKeHocSinh frm = new BangThongKeHocSinh();
            frm.ShowDialog();
        }
        SqlConnection connection;
        SqlCommand command;
        cons scon=new cons();
        
        SqlDataAdapter adapter1 = new SqlDataAdapter(); 
        SqlDataAdapter adapter2 = new SqlDataAdapter(); 
        DataTable table1 = new DataTable(); 
        DataTable table2 = new DataTable(); 
        void loaddata1()
        {
            try
            {
                connection = new SqlConnection(scon.stron);
                connection.Open();
                command = connection.CreateCommand();
                command.CommandText = "exec ThongTinDiemHocSinh @TimKiem";
                command.Parameters.AddWithValue("@TimKiem", TkHS.Text);
                adapter1.SelectCommand = command;
                table1.Clear();
                adapter1.Fill(table1);
                dataGridView1.DataSource = table1;
                connection.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Đã xảy ra lỗi: " + ex.Message);
            }
        }

        void loaddata2(string MonDeThi)
        {
            connection = new SqlConnection(scon.stron);
            connection.Open();
            command = connection.CreateCommand();
            command.CommandText = "select MaDeThi as N'Mã đề thi', MonDeThi as N'Môn thi', NoiDungThi as N'Nội dung thi', MucDo as N'Mức độ' from tblThongTinKhoDeThi where MonDeThi like @MonDeThi";
            command.Parameters.AddWithValue("@MonDeThi", "%" + MonDeThi + "%");
            adapter2.SelectCommand = command;
            table2.Clear();
            adapter2.Fill(table2);
            dataGridView2.DataSource = table2;
            connection.Close();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            loaddata1();
            loaddata2(TKDe.Text);

        }

     
        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void TKDe_TextChanged(object sender, EventArgs e)
        {
            loaddata2(TKDe.Text);
        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void TkHS_TextChanged(object sender, EventArgs e)
        {
            loaddata1();
        }
    }
}
