using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyTracNghiem
{
    internal class cons
    {
        private static string _workingConnectionString = null;
        public static string WorkingServerName = @".\SQLEXPRESS";

        public string stron;

        public cons()
        {
            if (_workingConnectionString != null)
            {
                this.stron = _workingConnectionString;
                return;
            }

            // 1. Thử chuỗi kết nối từ cấu hình (App.config / Settings)
            string settingConn = Properties.Settings.Default.QuanLyTracNghiemConnectionString;
            if (TryConnect(settingConn, out string serverFromSetting))
            {
                _workingConnectionString = settingConn;
                WorkingServerName = serverFromSetting;
                this.stron = settingConn;
                return;
            }

            // 2. Thử các Data Source phổ biến (phòng máy trường .\SQLEXPRESS, LocalDB máy cá nhân, localhost, .)
            string[] candidateSources = new string[]
            {
                @".\SQLEXPRESS",
                @"(localdb)\MSSQLLocalDB",
                @".",
                @"localhost"
            };

            foreach (var src in candidateSources)
            {
                string testConn = $"Data Source={src};Initial Catalog=QuanLyTracNghiem;Integrated Security=True;Encrypt=False;Connect Timeout=2";
                if (TryConnect(testConn, out _))
                {
                    _workingConnectionString = testConn;
                    WorkingServerName = src;
                    this.stron = testConn;
                    return;
                }
            }

            // 3. Fallback mặc định
            this.stron = @"Data Source=.\SQLEXPRESS;Initial Catalog=QuanLyTracNghiem;Integrated Security=True;Encrypt=False";
        }

        private static bool TryConnect(string connectionString, out string serverName)
        {
            serverName = @".\SQLEXPRESS";
            if (string.IsNullOrWhiteSpace(connectionString)) return false;

            try
            {
                var builder = new SqlConnectionStringBuilder(connectionString);
                serverName = builder.DataSource;
                builder.ConnectTimeout = 2;

                using (var conn = new SqlConnection(builder.ConnectionString))
                {
                    conn.Open();
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }
    }
}
