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
    public partial class BaoCao : Form
    {
        public BaoCao()
        {
            InitializeComponent();
            ThemNutCrystalReport();
        }

        // Thêm nút "Crystal Report" bằng code (không đụng vào Designer.cs) để
        // mở song song bản báo cáo bằng Crystal Report thật, cạnh ReportViewer (RDLC) có sẵn.
        private void ThemNutCrystalReport()
        {
            var btnCrystalReport = new Button
            {
                Text = "Crystal Report",
                Dock = DockStyle.Top,
                Height = 32,
                BackColor = Color.MediumPurple,
                ForeColor = Color.White
            };
            btnCrystalReport.Click += (s, e) =>
            {
                using (var f = new FormXemCrystalReport("BaoCaoHocSinh.rpt", "Báo cáo Học Sinh (Crystal Report)"))
                {
                    f.ShowDialog(this);
                }
            };
            this.Controls.Add(btnCrystalReport);
            btnCrystalReport.BringToFront();
        }

        private void BaoCao_Load(object sender, EventArgs e)
        {

            cons scon = new cons();
            SqlConnection connection = new SqlConnection(scon.stron);
            SqlCommand command = new SqlCommand();
            connection.Open();
            command = connection.CreateCommand();
            command.CommandText = "select * from vw_ThongTinDiemHocSinh";
            SqlDataAdapter adapter = new SqlDataAdapter();
            adapter.SelectCommand = command;
            DataSet ds = new DataSet();
            adapter.Fill(ds, "vw_ThongTinDiemHocSinh");
            reportViewer1.LocalReport.ReportPath = @"Report1.rdlc";
            reportViewer1.LocalReport.DataSources.Clear();
            reportViewer1.LocalReport.DataSources.Add(new Microsoft.Reporting.WinForms.ReportDataSource("DataSet1", ds.Tables["vw_ThongTinDiemHocSinh"]));
            reportViewer1.RefreshReport();
            this.reportViewer1.RefreshReport();
            connection.Close();


        }
    }
}
