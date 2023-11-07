using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLyKhoSieuThi
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void xuatKhoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new XuatKhoForm().Show();
        }

        private void nhapKhoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new NhapKhoForm().Show();
        }

        private void quanLyKhoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new QuanLyKhoForm().Show();
        }
    }
}
