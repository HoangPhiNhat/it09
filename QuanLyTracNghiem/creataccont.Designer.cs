namespace QuanLyTracNghiem
{
    partial class creataccont
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(creataccont));
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.accounting = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.passwork = new System.Windows.Forms.TextBox();
            this.df = new System.Windows.Forms.Label();
            this.passworkcomf = new System.Windows.Forms.TextBox();
            this.DongY = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Times New Roman", 13.875F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.Navy;
            this.label3.Location = new System.Drawing.Point(337, 53);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(259, 42);
            this.label3.TabIndex = 7;
            this.label3.Text = "Tạo Tài Khoản";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(100, 125);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(147, 36);
            this.label1.TabIndex = 8;
            this.label1.Text = "Tài Khoản";
            // 
            // accounting
            // 
            this.accounting.Location = new System.Drawing.Point(331, 130);
            this.accounting.Name = "accounting";
            this.accounting.Size = new System.Drawing.Size(290, 31);
            this.accounting.TabIndex = 9;
            this.accounting.KeyDown += new System.Windows.Forms.KeyEventHandler(this.accounting_KeyDown);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(107, 206);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(140, 36);
            this.label2.TabIndex = 10;
            this.label2.Text = "Mật Khẩu";
            // 
            // passwork
            // 
            this.passwork.Location = new System.Drawing.Point(331, 206);
            this.passwork.Name = "passwork";
            this.passwork.Size = new System.Drawing.Size(290, 31);
            this.passwork.TabIndex = 11;
            this.passwork.UseSystemPasswordChar = true;
            this.passwork.KeyDown += new System.Windows.Forms.KeyEventHandler(this.accounting_KeyDown);
            // 
            // df
            // 
            this.df.AutoSize = true;
            this.df.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.df.Location = new System.Drawing.Point(31, 265);
            this.df.Name = "df";
            this.df.Size = new System.Drawing.Size(269, 36);
            this.df.TabIndex = 12;
            this.df.Text = "Xét Nhận Mật Khẩu";
            // 
            // passworkcomf
            // 
            this.passworkcomf.Location = new System.Drawing.Point(331, 265);
            this.passworkcomf.Name = "passworkcomf";
            this.passworkcomf.Size = new System.Drawing.Size(290, 31);
            this.passworkcomf.TabIndex = 13;
            this.passworkcomf.UseSystemPasswordChar = true;
            this.passworkcomf.KeyDown += new System.Windows.Forms.KeyEventHandler(this.accounting_KeyDown);
            // 
            // DongY
            // 
            this.DongY.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.DongY.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DongY.Location = new System.Drawing.Point(143, 359);
            this.DongY.Name = "DongY";
            this.DongY.Size = new System.Drawing.Size(198, 62);
            this.DongY.TabIndex = 5;
            this.DongY.Text = "Đồng ý";
            this.DongY.UseVisualStyleBackColor = false;
            this.DongY.Click += new System.EventHandler(this.DongY_Click);
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.button1.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.Location = new System.Drawing.Point(458, 359);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(195, 62);
            this.button1.TabIndex = 4;
            this.button1.Text = "Thoát";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.groupBox1.Controls.Add(this.button1);
            this.groupBox1.Controls.Add(this.DongY);
            this.groupBox1.Controls.Add(this.passworkcomf);
            this.groupBox1.Controls.Add(this.df);
            this.groupBox1.Controls.Add(this.passwork);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.accounting);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Location = new System.Drawing.Point(0, 2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(754, 452);
            this.groupBox1.TabIndex = 14;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Tạo Tài Khoản";
            this.groupBox1.Enter += new System.EventHandler(this.groupBox1_Enter);
            // 
            // creataccont
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(756, 462);
            this.Controls.Add(this.groupBox1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "creataccont";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Tạo tài khoản";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        protected internal System.Windows.Forms.Label label3;
        protected internal System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox accounting;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox passwork;
        private System.Windows.Forms.Label df;
        private System.Windows.Forms.TextBox passworkcomf;
        private System.Windows.Forms.Button DongY;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.GroupBox groupBox1;
    }
}