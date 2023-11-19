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
    public partial class ThemSanPham : Form
    {
        private DataGridView dgvProduccts;
        public event EventHandler DataUpdatedEvent;


        SqlConnection con;

        string sql = "";

        SqlDataAdapter da;
        string strCon;

        string path_xmlSP = "../../SanPham.xml";
        string path_xmlDM = "../../DanhMuc.xml";


        public ThemSanPham(DataGridView dgvProduccts)
        {
            this.dgvProduccts = dgvProduccts;
            InitializeComponent();
            strCon = "Data Source=NGOVANTUYEN;Initial Catalog=QuanLyKhoSieuThi;Integrated Security=True";
            con = new SqlConnection(strCon);
            LoadDanhMucData();
        }



        private void InsertProductIntoDatabase(string maSP, string tenSP, decimal gia, int soLuongTonKho, string maDM)
        {
            SqlConnection con = new SqlConnection("Data Source=NGOVANTUYEN;Initial Catalog=QuanLyKhoSieuThi;Integrated Security=True");

            try
            {
                con.Open(); // Mở kết nối
                string sql = "INSERT INTO SanPham (maSP, tenSP, gia, soLuongTonKho, maDM) VALUES (@maSP, @tenSP, @gia, @soLuongTonKho, @maDM)";
                SqlCommand cmd = new SqlCommand(sql, con);
                cmd.Parameters.AddWithValue("@maSP", maSP);
                cmd.Parameters.AddWithValue("@tenSP", tenSP);
                cmd.Parameters.AddWithValue("@gia", gia);
                cmd.Parameters.AddWithValue("@soLuongTonKho", soLuongTonKho);
                cmd.Parameters.AddWithValue("@maDM", maDM);
                cmd.ExecuteNonQuery();
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Error inserting product into database: " + ex.Message);
            }
            finally
            {
                con.Close(); // Đóng kết nối
            }
        }


        private void LoadDanhMucData()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(strCon))
                {
                    connection.Open();

                    // Thay đổi câu truy vấn phù hợp với cấu trúc của bạn
                    string query = "SELECT maDM, tenDM FROM DanhMuc";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                // Đổ dữ liệu vào ComboBox, hiển thị tên danh mục và lưu giá trị là mã danh mục
                                cbCategory.Items.Add(new ComboboxItem(reader["tenDM"].ToString(), reader["maDM"].ToString()));
                            }
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Error loading DanhMuc data: " + ex.Message);
            }
        }


        private void comboBoxDanhMuc_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Lấy giá trị mã danh mục khi người dùng chọn
            if (cbCategory.SelectedItem != null)
            {
                ComboboxItem selectedItem = (ComboboxItem)cbCategory.SelectedItem;
                string maDanhMuc = selectedItem.Value;
                MessageBox.Show("Đã chọn mã danh mục: " + maDanhMuc, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }


        private void btnAdd_Click(object sender, EventArgs e)
        {
            // Lấy thông tin từ TextBox
            string maSP = tb_productId.Text;
            string tenSP = tb_name.Text;

            ComboboxItem selectedItem = (ComboboxItem)cbCategory.SelectedItem;
            string maDanhMuc = selectedItem.Value;

            decimal gia = Decimal.Parse(tb_price.Text);
            int soLuongTonKho = Int32.Parse(tb_quantity.Text);

            // Lấy giá trị của ComboBox
            string maDM = maDanhMuc;

            // Kiểm tra xem danh mục có trong cơ sở dữ liệu hay không
            if (CheckCategoryInDatabase(maDM))
            {
                // Thêm sản phẩm vào cơ sở dữ liệu
                InsertProductIntoDatabase(maSP, tenSP, gia, soLuongTonKho, maDM);

                // Đóng form ThemSanPham
                this.Close();
                OnDataUpdatedEvent();
            }
            else
            {
                MessageBox.Show("Danh mục không tồn tại.");
            }
        }


        private bool CheckCategoryInDatabase(string maDM)
        {
            using (SqlConnection con = new SqlConnection("Data Source=NGOVANTUYEN;Initial Catalog=QuanLyKhoSieuThi;Integrated Security=True"))
            {
                con.Open();

                string sql = "SELECT COUNT(*) AS count FROM DanhMuc WHERE maDM = @maDM";
                SqlCommand cmd = new SqlCommand(sql, con);
                cmd.Parameters.AddWithValue("@maDM", maDM);

                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    return reader["count"].ToString() == "1";
                }

                return false;
            }
        }

        protected virtual void OnDataUpdatedEvent()
        {
            DataUpdatedEvent?.Invoke(this, EventArgs.Empty);
        }
    }
}
