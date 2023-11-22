
namespace QuanLyKhoSieuThi
{
    partial class MainForm
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
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.phiếuNhậpXuấtKhoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.btn_NavNhapKho = new System.Windows.Forms.Button();
            this.btn_NavXuatKho = new System.Windows.Forms.Button();
            this.btn_Close = new System.Windows.Forms.Button();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.phiếuNhậpXuấtKhoToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(956, 30);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // phiếuNhậpXuấtKhoToolStripMenuItem
            // 
            this.phiếuNhậpXuấtKhoToolStripMenuItem.Name = "phiếuNhậpXuấtKhoToolStripMenuItem";
            this.phiếuNhậpXuấtKhoToolStripMenuItem.Size = new System.Drawing.Size(14, 20);
            // 
            // btn_NavNhapKho
            // 
            this.btn_NavNhapKho.BackColor = System.Drawing.Color.Teal;
            this.btn_NavNhapKho.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_NavNhapKho.FlatAppearance.BorderSize = 0;
            this.btn_NavNhapKho.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_NavNhapKho.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_NavNhapKho.ForeColor = System.Drawing.Color.White;
            this.btn_NavNhapKho.Location = new System.Drawing.Point(125, 182);
            this.btn_NavNhapKho.Name = "btn_NavNhapKho";
            this.btn_NavNhapKho.Size = new System.Drawing.Size(251, 68);
            this.btn_NavNhapKho.TabIndex = 1;
            this.btn_NavNhapKho.Text = "Quản lý nhập kho";
            this.btn_NavNhapKho.UseVisualStyleBackColor = false;
            this.btn_NavNhapKho.Click += new System.EventHandler(this.btn_NavNhapKho_Click);
            // 
            // btn_NavXuatKho
            // 
            this.btn_NavXuatKho.BackColor = System.Drawing.Color.Teal;
            this.btn_NavXuatKho.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_NavXuatKho.FlatAppearance.BorderSize = 0;
            this.btn_NavXuatKho.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_NavXuatKho.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_NavXuatKho.ForeColor = System.Drawing.Color.White;
            this.btn_NavXuatKho.Location = new System.Drawing.Point(553, 182);
            this.btn_NavXuatKho.Name = "btn_NavXuatKho";
            this.btn_NavXuatKho.Size = new System.Drawing.Size(251, 68);
            this.btn_NavXuatKho.TabIndex = 1;
            this.btn_NavXuatKho.Text = "Quản lý xuất kho";
            this.btn_NavXuatKho.UseVisualStyleBackColor = false;
            this.btn_NavXuatKho.Click += new System.EventHandler(this.btn_NavXuatKho_Click);
            // 
            // btn_Close
            // 
            this.btn_Close.BackColor = System.Drawing.Color.Red;
            this.btn_Close.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_Close.FlatAppearance.BorderSize = 0;
            this.btn_Close.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_Close.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Close.ForeColor = System.Drawing.Color.White;
            this.btn_Close.Location = new System.Drawing.Point(396, 392);
            this.btn_Close.Name = "btn_Close";
            this.btn_Close.Size = new System.Drawing.Size(126, 47);
            this.btn_Close.TabIndex = 2;
            this.btn_Close.Text = "Đóng";
            this.btn_Close.UseVisualStyleBackColor = false;
            this.btn_Close.Click += new System.EventHandler(this.btn_Close_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(120F, 120F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(956, 451);
            this.Controls.Add(this.btn_Close);
            this.Controls.Add(this.btn_NavXuatKho);
            this.Controls.Add(this.btn_NavNhapKho);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "MainForm";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem phiếuNhậpXuấtKhoToolStripMenuItem;
        private System.Windows.Forms.Button btn_NavNhapKho;
        private System.Windows.Forms.Button btn_NavXuatKho;
        private System.Windows.Forms.Button btn_Close;
    }
}

