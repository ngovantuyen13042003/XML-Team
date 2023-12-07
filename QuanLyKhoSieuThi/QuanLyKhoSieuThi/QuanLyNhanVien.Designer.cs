
namespace QuanLyKhoSieuThi
{
    partial class QuanLyNhanVien
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
            this.xemToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saoLưuToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tảiDữLiệuToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.đóngToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.btn_sua = new System.Windows.Forms.Button();
            this.btn_xoa = new System.Windows.Forms.Button();
            this.btn_them = new System.Windows.Forms.Button();
            this.tb_sdt = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.tb_diaChi = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.tb_tenNV = new System.Windows.Forms.TextBox();
            this.tb_gioiTinh = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.tb_search = new System.Windows.Forms.TextBox();
            this.cb_maNV = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.dgv_sanpham = new System.Windows.Forms.DataGridView();
            this.label15 = new System.Windows.Forms.Label();
            this.tb_maNV = new System.Windows.Forms.TextBox();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_sanpham)).BeginInit();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(180)))), ((int)(((byte)(209)))));
            this.menuStrip1.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.menuStrip1.GripMargin = new System.Windows.Forms.Padding(2, 2, 0, 2);
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(30, 30);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.xemToolStripMenuItem,
            this.saoLưuToolStripMenuItem,
            this.tảiDữLiệuToolStripMenuItem,
            this.đóngToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(0);
            this.menuStrip1.Size = new System.Drawing.Size(1179, 64);
            this.menuStrip1.TabIndex = 58;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // xemToolStripMenuItem
            // 
            this.xemToolStripMenuItem.AutoSize = false;
            this.xemToolStripMenuItem.Image = global::QuanLyKhoSieuThi.Properties.Resources.eye;
            this.xemToolStripMenuItem.Name = "xemToolStripMenuItem";
            this.xemToolStripMenuItem.Size = new System.Drawing.Size(70, 57);
            this.xemToolStripMenuItem.Text = "Xem";
            this.xemToolStripMenuItem.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            // 
            // saoLưuToolStripMenuItem
            // 
            this.saoLưuToolStripMenuItem.Image = global::QuanLyKhoSieuThi.Properties.Resources.clone;
            this.saoLưuToolStripMenuItem.Name = "saoLưuToolStripMenuItem";
            this.saoLưuToolStripMenuItem.Size = new System.Drawing.Size(98, 64);
            this.saoLưuToolStripMenuItem.Text = "Sao lưu";
            this.saoLưuToolStripMenuItem.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.saoLưuToolStripMenuItem.Click += new System.EventHandler(this.saoLưuToolStripMenuItem_Click);
            // 
            // tảiDữLiệuToolStripMenuItem
            // 
            this.tảiDữLiệuToolStripMenuItem.Image = global::QuanLyKhoSieuThi.Properties.Resources.database;
            this.tảiDữLiệuToolStripMenuItem.Name = "tảiDữLiệuToolStripMenuItem";
            this.tảiDữLiệuToolStripMenuItem.Size = new System.Drawing.Size(122, 64);
            this.tảiDữLiệuToolStripMenuItem.Text = "Khôi phục";
            this.tảiDữLiệuToolStripMenuItem.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tảiDữLiệuToolStripMenuItem.Click += new System.EventHandler(this.tảiDữLiệuToolStripMenuItem_Click);
            // 
            // đóngToolStripMenuItem
            // 
            this.đóngToolStripMenuItem.AutoSize = false;
            this.đóngToolStripMenuItem.Image = global::QuanLyKhoSieuThi.Properties.Resources.close;
            this.đóngToolStripMenuItem.Name = "đóngToolStripMenuItem";
            this.đóngToolStripMenuItem.Size = new System.Drawing.Size(70, 57);
            this.đóngToolStripMenuItem.Text = "Đóng";
            this.đóngToolStripMenuItem.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.đóngToolStripMenuItem.Click += new System.EventHandler(this.đóngToolStripMenuItem_Click);
            // 
            // btn_sua
            // 
            this.btn_sua.BackColor = System.Drawing.Color.SteelBlue;
            this.btn_sua.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_sua.Location = new System.Drawing.Point(919, 273);
            this.btn_sua.Name = "btn_sua";
            this.btn_sua.Size = new System.Drawing.Size(177, 53);
            this.btn_sua.TabIndex = 110;
            this.btn_sua.Text = "SỬA";
            this.btn_sua.UseVisualStyleBackColor = false;
            this.btn_sua.Click += new System.EventHandler(this.btn_sua_Click);
            // 
            // btn_xoa
            // 
            this.btn_xoa.BackColor = System.Drawing.Color.LightCoral;
            this.btn_xoa.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_xoa.Location = new System.Drawing.Point(919, 349);
            this.btn_xoa.Name = "btn_xoa";
            this.btn_xoa.Size = new System.Drawing.Size(177, 53);
            this.btn_xoa.TabIndex = 109;
            this.btn_xoa.Text = "XÓA";
            this.btn_xoa.UseVisualStyleBackColor = false;
            this.btn_xoa.Click += new System.EventHandler(this.btn_xoa_Click);
            // 
            // btn_them
            // 
            this.btn_them.BackColor = System.Drawing.Color.DarkSeaGreen;
            this.btn_them.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_them.Location = new System.Drawing.Point(919, 196);
            this.btn_them.Name = "btn_them";
            this.btn_them.Size = new System.Drawing.Size(177, 53);
            this.btn_them.TabIndex = 108;
            this.btn_them.Text = "THÊM";
            this.btn_them.UseVisualStyleBackColor = false;
            this.btn_them.Click += new System.EventHandler(this.btn_them_Click);
            // 
            // tb_sdt
            // 
            this.tb_sdt.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tb_sdt.Location = new System.Drawing.Point(402, 343);
            this.tb_sdt.Margin = new System.Windows.Forms.Padding(4);
            this.tb_sdt.Name = "tb_sdt";
            this.tb_sdt.Size = new System.Drawing.Size(414, 31);
            this.tb_sdt.TabIndex = 106;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label14.Location = new System.Drawing.Point(216, 349);
            this.label14.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(138, 25);
            this.label14.TabIndex = 105;
            this.label14.Text = "Số điện thoại";
            // 
            // tb_diaChi
            // 
            this.tb_diaChi.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tb_diaChi.Location = new System.Drawing.Point(402, 402);
            this.tb_diaChi.Margin = new System.Windows.Forms.Padding(4);
            this.tb_diaChi.Name = "tb_diaChi";
            this.tb_diaChi.Size = new System.Drawing.Size(414, 31);
            this.tb_diaChi.TabIndex = 104;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.Location = new System.Drawing.Point(216, 408);
            this.label13.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(78, 25);
            this.label13.TabIndex = 103;
            this.label13.Text = "Địa chỉ";
            // 
            // tb_tenNV
            // 
            this.tb_tenNV.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tb_tenNV.Location = new System.Drawing.Point(402, 223);
            this.tb_tenNV.Margin = new System.Windows.Forms.Padding(4);
            this.tb_tenNV.Name = "tb_tenNV";
            this.tb_tenNV.Size = new System.Drawing.Size(414, 31);
            this.tb_tenNV.TabIndex = 101;
            // 
            // tb_gioiTinh
            // 
            this.tb_gioiTinh.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tb_gioiTinh.Location = new System.Drawing.Point(402, 287);
            this.tb_gioiTinh.Margin = new System.Windows.Forms.Padding(4);
            this.tb_gioiTinh.Name = "tb_gioiTinh";
            this.tb_gioiTinh.Size = new System.Drawing.Size(414, 31);
            this.tb_gioiTinh.TabIndex = 102;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(216, 169);
            this.label10.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(142, 25);
            this.label10.TabIndex = 100;
            this.label10.Text = "Mã nhân viên";
            this.label10.Click += new System.EventHandler(this.label10_Click);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.Location = new System.Drawing.Point(216, 229);
            this.label11.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(149, 25);
            this.label11.TabIndex = 98;
            this.label11.Text = "Tên nhân viên";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.Location = new System.Drawing.Point(216, 293);
            this.label12.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(91, 25);
            this.label12.TabIndex = 99;
            this.label12.Text = "Giới tính";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(882, 494);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(80, 25);
            this.label4.TabIndex = 97;
            this.label4.Text = "Search";
            // 
            // tb_search
            // 
            this.tb_search.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tb_search.Location = new System.Drawing.Point(501, 489);
            this.tb_search.Margin = new System.Windows.Forms.Padding(4);
            this.tb_search.Name = "tb_search";
            this.tb_search.Size = new System.Drawing.Size(358, 30);
            this.tb_search.TabIndex = 96;
            this.tb_search.TextChanged += new System.EventHandler(this.tb_search_TextChanged);
            // 
            // cb_maNV
            // 
            this.cb_maNV.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cb_maNV.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cb_maNV.FormattingEnabled = true;
            this.cb_maNV.Location = new System.Drawing.Point(211, 491);
            this.cb_maNV.Margin = new System.Windows.Forms.Padding(4);
            this.cb_maNV.Name = "cb_maNV";
            this.cb_maNV.Size = new System.Drawing.Size(237, 33);
            this.cb_maNV.TabIndex = 95;
            this.cb_maNV.SelectedIndexChanged += new System.EventHandler(this.cb_maNV_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(81, 490);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(109, 25);
            this.label2.TabIndex = 94;
            this.label2.Text = "Nhân viên";
            // 
            // dgv_sanpham
            // 
            this.dgv_sanpham.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgv_sanpham.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dgv_sanpham.BackgroundColor = System.Drawing.Color.White;
            this.dgv_sanpham.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_sanpham.Location = new System.Drawing.Point(52, 538);
            this.dgv_sanpham.Margin = new System.Windows.Forms.Padding(4);
            this.dgv_sanpham.Name = "dgv_sanpham";
            this.dgv_sanpham.RowHeadersWidth = 51;
            this.dgv_sanpham.RowTemplate.Height = 24;
            this.dgv_sanpham.Size = new System.Drawing.Size(1064, 262);
            this.dgv_sanpham.TabIndex = 93;
            this.dgv_sanpham.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgv_sanpham_CellContentClick);
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label15.Location = new System.Drawing.Point(394, 99);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(424, 46);
            this.label15.TabIndex = 111;
            this.label15.Text = "QUẢN LÝ NHÂN VIÊN";
            // 
            // tb_maNV
            // 
            this.tb_maNV.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tb_maNV.Location = new System.Drawing.Point(402, 169);
            this.tb_maNV.Margin = new System.Windows.Forms.Padding(4);
            this.tb_maNV.Name = "tb_maNV";
            this.tb_maNV.Size = new System.Drawing.Size(414, 31);
            this.tb_maNV.TabIndex = 112;
            // 
            // QuanLyNhanVien
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1179, 813);
            this.Controls.Add(this.tb_maNV);
            this.Controls.Add(this.label15);
            this.Controls.Add(this.btn_sua);
            this.Controls.Add(this.btn_xoa);
            this.Controls.Add(this.btn_them);
            this.Controls.Add(this.tb_sdt);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.tb_diaChi);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.tb_tenNV);
            this.Controls.Add(this.tb_gioiTinh);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.tb_search);
            this.Controls.Add(this.cb_maNV);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.dgv_sanpham);
            this.Controls.Add(this.menuStrip1);
            this.Name = "QuanLyNhanVien";
            this.Text = "QuanLyNhanVien";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_sanpham)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem xemToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saoLưuToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem tảiDữLiệuToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem đóngToolStripMenuItem;
        private System.Windows.Forms.Button btn_sua;
        private System.Windows.Forms.Button btn_xoa;
        private System.Windows.Forms.Button btn_them;
        private System.Windows.Forms.TextBox tb_sdt;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.TextBox tb_diaChi;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TextBox tb_tenNV;
        private System.Windows.Forms.TextBox tb_gioiTinh;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox tb_search;
        private System.Windows.Forms.ComboBox cb_maNV;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DataGridView dgv_sanpham;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.TextBox tb_maNV;
    }
}