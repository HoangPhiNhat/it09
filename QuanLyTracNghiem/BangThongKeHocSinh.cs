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
    public partial class BangThongKeHocSinh : Form
    {
        SqlConnection connection;
        SqlCommand command;
       cons scon= new cons();
        SqlDataAdapter adapter = new SqlDataAdapter();
        DataTable table = new DataTable();
        void loaddata()
        {
            command = connection.CreateCommand();
            command.CommandText = "select * from vw_ThongKeDiem";
            adapter.SelectCommand = command;
            table.Clear();
            adapter.Fill(table);
            dataGridView1.DataSource = table;
        }
        public BangThongKeHocSinh()
        {
            InitializeComponent();
        }

        private void BangThongKeHocSinh_Load(object sender, EventArgs e)
        {
            connection = new SqlConnection(scon.stron);
            connection.Open();
            this.FormClosed += (s, ev) => connection?.Close();
            loaddata();
        }

        private void button4_Click(object sender, EventArgs e)
        {
          
            this.Close();
        }
    }
}
