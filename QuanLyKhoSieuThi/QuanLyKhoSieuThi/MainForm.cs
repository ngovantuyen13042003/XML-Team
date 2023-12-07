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

namespace QuanLyKhoSieuThi
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void btn_NavNhapKho_Click(object sender, EventArgs e)
        {
            new NhapKho().Show();
        }

        private void btn_NavXuatKho_Click(object sender, EventArgs e)
        {
            new XuatKho().Show();
        }

        private void btn_Close_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btn_sanpham_Click(object sender, EventArgs e)
        {
            new QuanLySanPham().Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            new QuanLyNhanVien().Show();
        }
    }
}
