
namespace QuanLyKhoSieuThi
{
    partial class Form1
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
            this.khoHàngToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.xuatKhoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.nhapKhoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.quanLyKhoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.khoHàngToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(956, 28);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // khoHàngToolStripMenuItem
            // 
            this.khoHàngToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.xuatKhoToolStripMenuItem,
            this.nhapKhoToolStripMenuItem,
            this.quanLyKhoToolStripMenuItem});
            this.khoHàngToolStripMenuItem.Name = "khoHàngToolStripMenuItem";
            this.khoHàngToolStripMenuItem.Size = new System.Drawing.Size(86, 24);
            this.khoHàngToolStripMenuItem.Text = "Kho hàng";
            // 
            // xuatKhoToolStripMenuItem
            // 
            this.xuatKhoToolStripMenuItem.Name = "xuatKhoToolStripMenuItem";
            this.xuatKhoToolStripMenuItem.Size = new System.Drawing.Size(224, 26);
            this.xuatKhoToolStripMenuItem.Text = "Xuất kho";
            this.xuatKhoToolStripMenuItem.Click += new System.EventHandler(this.xuatKhoToolStripMenuItem_Click);
            // 
            // nhapKhoToolStripMenuItem
            // 
            this.nhapKhoToolStripMenuItem.Name = "nhapKhoToolStripMenuItem";
            this.nhapKhoToolStripMenuItem.Size = new System.Drawing.Size(224, 26);
            this.nhapKhoToolStripMenuItem.Text = "Nhập kho";
            this.nhapKhoToolStripMenuItem.Click += new System.EventHandler(this.nhapKhoToolStripMenuItem_Click);
            // 
            // quanLyKhoToolStripMenuItem
            // 
            this.quanLyKhoToolStripMenuItem.Name = "quanLyKhoToolStripMenuItem";
            this.quanLyKhoToolStripMenuItem.Size = new System.Drawing.Size(224, 26);
            this.quanLyKhoToolStripMenuItem.Text = "Quản lý kho";
            this.quanLyKhoToolStripMenuItem.Click += new System.EventHandler(this.quanLyKhoToolStripMenuItem_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(956, 451);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "Form1";
            this.Text = "Form1";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem khoHàngToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem xuatKhoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem nhapKhoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem quanLyKhoToolStripMenuItem;
    }
}

