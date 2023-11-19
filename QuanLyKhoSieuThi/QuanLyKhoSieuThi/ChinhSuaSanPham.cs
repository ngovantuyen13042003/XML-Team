using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.Data;



// Khai báo thêm các namespace sau

using System.Data.SqlClient;// Dùng cho SqlConnect, SqlDataAdapter

using System.Xml;           // Dùng cho XmlDocument

using System.Diagnostics;   // Dùng cho Pricess.Start()

using System.IO;            // Dùng cho lớp Path


namespace QuanLyKhoSieuThi
{
    public partial class ChinhSuaSanPham : Form
    {
        private DataGridView dgvProduccts;

        //SqlConnection con;

        string sql = "";
        public ChinhSuaSanPham() { }

        String maSP, tenSP, maDM;
        decimal gia;
        int soLuongTonKho;
        public ChinhSuaSanPham(String maSP, String tenSP, decimal gia, int soLuongTonKho, String maDM)
        {
            this.maDM = maDM;
            this.maSP = maSP;
            this.tenSP = tenSP;
            this.gia = gia;
            this.soLuongTonKho = soLuongTonKho;
            InitializeComponent();
            LoadDanhMucData();
            DisplayProductInfo();
        }

        private void LoadDanhMucData()
        {
            try
            {
                using (SqlConnection con = new SqlConnection("Data Source=NGOVANTUYEN;Initial Catalog=QuanLyKhoSieuThi;Integrated Security=True"))
                {
                    con.Open();

                    string sql = "SELECT maDM, tenDM FROM DanhMuc";
                    SqlCommand cmd = new SqlCommand(sql, con);
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        // Assuming you have a ComboBox named cboDanhMuc
                        cbCategory.Items.Add(reader["tenDM"].ToString());
                    }
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Error loading DanhMuc data: " + ex.Message);
            }
        }
        private void DisplayProductInfo()
        {
            tb_productId.Text = maSP;
            tb_name.Text = tenSP;
            tb_price.Text = gia.ToString();
            tb_quantity.Text = soLuongTonKho.ToString();

            // Đặt giá trị cho ComboBox, giả sử bạn có một ComboBox tên là cbDanhMuc
            cbCategory.SelectedValue = maDM;
        }
        private void btnEdit_Click(object sender, EventArgs e)
        {
            UpdateProduct(maSP, tenSP, gia, soLuongTonKho, maDM);
            this.Close();
        }

        private void UpdateProduct(string maSP, string tenSP, decimal gia, int soLuongTonKho, string maDM)
        {
            try
                using (SqlConnection con = new SqlConnection("Data Source=NGOVANTUYEN;Initial Catalog=QuanLyKhoSieuThi;Integrated Security=True"))
            {
                con.Open();

                // Viết mã SQL để cập nhật thông tin sản phẩm dựa trên ID
                string sql = "UPDATE SanPham SET tenSP = @tenSP, gia = @gia, soLuongTonKho = @soLuongTonKho, maDM = @maDM WHERE maSP = @maSP";
                SqlCommand cmd = new SqlCommand(sql, con);
                cmd.Parameters.AddWithValue("@maSP", maSP);
                cmd.Parameters.AddWithValue("@tenSP", tenSP);
                cmd.Parameters.AddWithValue("@gia", gia);
                cmd.Parameters.AddWithValue("@soLuongTonKho", soLuongTonKho);
                cmd.Parameters.AddWithValue("@maDM", maDM);
                cmd.ExecuteNonQuery();

                MessageBox.Show("Product updated successfully!");

            }
            catch (SqlException ex)
            {
                MessageBox.Show("Error updating product: " + ex.Message);
            }
            finally
            {
                con.Close();
            }
        }

    }
}
