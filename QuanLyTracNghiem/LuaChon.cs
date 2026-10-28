using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLyTracNghiem
{
    public partial class LuaChon : Form
    {
        public LuaChon()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            account frm = new account();
            frm.ShowDialog();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            KiemTraTracNghiem frm= new KiemTraTracNghiem();
            frm.ShowDialog();
        }

        private void LuaChon_Load(object sender, EventArgs e)
        {

        }
    }
}
