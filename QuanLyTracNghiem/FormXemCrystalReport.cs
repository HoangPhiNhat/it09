using System;
using System.IO;
using System.Windows.Forms;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;

namespace QuanLyTracNghiem
{
    /// <summary>
    /// Xem bÃ¡o cÃ¡o báº±ng Crystal Report tháº­t (CrystalReportViewer).
    /// File .rpt náº±m trong thÆ° má»¥c Report\ (Ä‘Æ°á»£c copy ra thÆ° má»¥c build).
    /// </summary>
    public class FormXemCrystalReport : Form
    {
        private CrystalDecisions.Windows.Forms.CrystalReportViewer crystalReportViewer1;

        public FormXemCrystalReport(string tenFileRpt, string tieuDe)
        {
            this.Text = tieuDe;
            this.Size = new System.Drawing.Size(1000, 700);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.WindowState = FormWindowState.Maximized;

            crystalReportViewer1 = new CrystalDecisions.Windows.Forms.CrystalReportViewer();
            crystalReportViewer1.Dock = DockStyle.Fill;
            crystalReportViewer1.ShowGroupTreeButton = false;
            this.Controls.Add(crystalReportViewer1);

            this.Load += (s, e) => TaiBaoCao(tenFileRpt);
        }

        private void TaiBaoCao(string tenFileRpt)
        {
            try
            {
                string rptPath = Path.Combine(Application.StartupPath, "Report", tenFileRpt);
                if (!File.Exists(rptPath))
                {
                    MessageBox.Show("KhÃ´ng tÃ¬m tháº¥y file bÃ¡o cÃ¡o: " + rptPath, "Lá»—i",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                var report = new ReportDocument();
                report.Load(rptPath);

                var connInfo = new ConnectionInfo
                {
                    ServerName = cons.WorkingServerName ?? @".\SQLEXPRESS",
                    DatabaseName = "QuanLyTracNghiem",
                    IntegratedSecurity = true
                };

                foreach (CrystalDecisions.CrystalReports.Engine.Table t in report.Database.Tables)
                {
                    TableLogOnInfo logon = t.LogOnInfo;
                    logon.ConnectionInfo = connInfo;
                    t.ApplyLogOnInfo(logon);
                }

                crystalReportViewer1.ReportSource = report;
            }
            catch (Exception ex)
            {
                MessageBox.Show("KhÃ´ng má»Ÿ Ä‘Æ°á»£c bÃ¡o cÃ¡o Crystal Report: " + ex.Message, "Lá»—i",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
