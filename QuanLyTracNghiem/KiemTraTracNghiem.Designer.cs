namespace QuanLyTracNghiem
{
    partial class KiemTraTracNghiem
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(KiemTraTracNghiem));
            this.label1 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.TenHS = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.Lop = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.MaHocSinh = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.MaDeThi = new System.Windows.Forms.ComboBox();
            this.MonThi = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Times New Roman", 16.125F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.label1.Location = new System.Drawing.Point(133, 140);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(800, 49);
            this.label1.TabIndex = 0;
            this.label1.Text = "KIỂM TRA TRẮC NGHIỆM 5 CÂU HỎI";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(420, 609);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(204, 44);
            this.button1.TabIndex = 1;
            this.button1.Text = "Bắt Đầu";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // TenHS
            // 
            this.TenHS.Location = new System.Drawing.Point(400, 247);
            this.TenHS.Name = "TenHS";
            this.TenHS.Size = new System.Drawing.Size(246, 31);
            this.TenHS.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(279, 247);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(104, 25);
            this.label2.TabIndex = 3;
            this.label2.Text = "Họ và tên";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(251, 318);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(129, 25);
            this.label3.TabIndex = 4;
            this.label3.Text = "Mã học sinh";
            // 
            // Lop
            // 
            this.Lop.Location = new System.Drawing.Point(400, 377);
            this.Lop.Name = "Lop";
            this.Lop.Size = new System.Drawing.Size(246, 31);
            this.Lop.TabIndex = 5;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(320, 383);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(48, 25);
            this.label4.TabIndex = 6;
            this.label4.Text = "Lớp";
            // 
            // MaHocSinh
            // 
            this.MaHocSinh.Location = new System.Drawing.Point(400, 315);
            this.MaHocSinh.Name = "MaHocSinh";
            this.MaHocSinh.Size = new System.Drawing.Size(246, 31);
            this.MaHocSinh.TabIndex = 7;
            this.MaHocSinh.Leave += new System.EventHandler(this.MaHocSinh_Leave);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(285, 523);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(83, 25);
            this.label5.TabIndex = 6;
            this.label5.Text = "Môn thi";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(267, 448);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(101, 25);
            this.label6.TabIndex = 6;
            this.label6.Text = "Mã đề thi";
            // 
            // MaDeThi
            // 
            this.MaDeThi.FormattingEnabled = true;
            this.MaDeThi.Location = new System.Drawing.Point(400, 440);
            this.MaDeThi.Name = "MaDeThi";
            this.MaDeThi.Size = new System.Drawing.Size(246, 33);
            this.MaDeThi.TabIndex = 9;
            this.MaDeThi.SelectedIndexChanged += new System.EventHandler(this.MaDeThi_SelectedIndexChanged);
            // 
            // MonThi
            // 
            this.MonThi.Enabled = false;
            this.MonThi.Location = new System.Drawing.Point(400, 517);
            this.MonThi.Name = "MonThi";
            this.MonThi.Size = new System.Drawing.Size(246, 31);
            this.MonThi.TabIndex = 5;
            // 
            // KiemTraTracNghiem
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1022, 794);
            this.Controls.Add(this.MaDeThi);
            this.Controls.Add(this.MaHocSinh);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.MonThi);
            this.Controls.Add(this.Lop);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.TenHS);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "KiemTraTracNghiem";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Kiểm tra trắc nghiệm";
            this.Load += new System.EventHandler(this.KiemTraTracNghiem_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox TenHS;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox Lop;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox MaHocSinh;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox MaDeThi;
        private System.Windows.Forms.TextBox MonThi;
    }
}
