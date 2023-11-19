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
    public partial class QuanLySanPham : Form
    {
        SqlConnection con;

        string sql = "";

        SqlDataAdapter da;

        string path_xmlSP = "../../SanPham.xml";
        string path_xmlDM = "../../DanhMuc.xml";

        public QuanLySanPham()
        {
            InitializeComponent();
            Connect();
            InitGrid(dgvProduccts);
        }

        private void Connect()
        {
            string strCon = "Data Source=NGOVANTUYEN;Initial Catalog=QuanLyKhoSieuThi;Integrated Security=True";
            con = new SqlConnection(strCon);
            LoadDanhMucData();
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

                    DataTable dt = new DataTable("DanhMuc");

                    dt.Columns.Add("maDM", typeof(string));
                    dt.Columns.Add("tenDM", typeof(string));

                    while (reader.Read())
                    {
                        cbCategory.Items.Add(reader["tenDM"].ToString());

                        // Add data to DataTable for XML
                        dt.Rows.Add(reader["maDM"].ToString(), reader["tenDM"].ToString());
                    }

                    // Create a new DataSet
                    DataSet ds = new DataSet("danhsach");

                    // Add DataTable to DataSet
                    ds.Tables.Add(dt);

                    // Save DataSet to XML
                    ds.WriteXml(path_xmlDM);
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Error loading DanhMuc data: " + ex.Message);
            }
        }


        private void InitGrid(DataGridView dgv)
        {
            try
            {
                sql = "Select * from SanPham";
                da = new SqlDataAdapter(sql, con);
                DataTable dt = new DataTable("SanPham");
                da.Fill(dt);

                // Thêm hai cột mới
                DataColumn updateColumn = new DataColumn("Update", typeof(string));
                DataColumn removeColumn = new DataColumn("Remove", typeof(string));

                // Thêm cột vào DataTable
                dt.Columns.Add(updateColumn);
                dt.Columns.Add(removeColumn);

                // Gán giá trị mặc định cho cột Update và Remove
                foreach (DataRow row in dt.Rows)
                {
                    row["Update"] = "Update";
                    row["Remove"] = "Remove";
                }

                // Create a new DataSet
                DataSet ds = new DataSet("danhsach");

                // Add DataTable to DataSet
                ds.Tables.Add(dt);

                // Bind the DataTable to the DataGridView
                dgv.DataSource = dt;

                // Save DataSet to XML
                ds.WriteXml(path_xmlSP);
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                con.Close(); // Make sure to close the connection
            }
        }



        public void UpdateDataInGridView(DataTable newData)
        {
            dgvProduccts.DataSource = newData;
        }

        private void UploadDataToDatabaseAndXml(string maSP, string tenSP, decimal gia, int soLuongTonKho, string maDM)
        {
            // Insert new data into the database
            InsertProductIntoDatabase(maSP, tenSP, gia, soLuongTonKho, maDM);

            // Retrieve the current data from the DataGridView
            DataTable dt = (DataTable)dgvProduccts.DataSource;

            // Add the new data to the DataTable
            dt.Rows.Add(maSP, tenSP, gia, soLuongTonKho, maDM);

            // Save the updated DataTable to XML
            SaveDataTableToXml(dt, path_xmlSP);

            // Refresh the DataGridView
            UpdateDataInGridView(dt);
        }

        private void InsertProductIntoDatabase(string maSP, string tenSP, decimal gia, int soLuongTonKho, string maDM)
        {
            try
            {
                using (SqlConnection con = new SqlConnection("Data Source=NGOVANTUYEN;Initial Catalog=QuanLyKhoSieuThi;Integrated Security=True"))
                    con.Open();
                string sql = "INSERT INTO SanPham (maSP, tenSP, gia, soLuongTonKho, maDM) VALUES (@maSP, @tenSP, @gia, @soLuongTonKho, @maDM)";
                SqlCommand cmd = new SqlCommand(sql, con);
                cmd.Parameters.AddWithValue("@maSP", maSP);
                cmd.Parameters.AddWithValue("@tenSP", tenSP);
                cmd.Parameters.AddWithValue("@gia", gia);
                cmd.Parameters.AddWithValue("@soLuongTonKho", soLuongTonKho);
                cmd.Parameters.AddWithValue("@maDM", maDM);
                cmd.ExecuteNonQuery();

                UploadDataToDatabaseAndXml( maSP,  tenSP,  gia,  soLuongTonKho,  maDM);
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Error inserting product into database: " + ex.Message);
            }
            finally
            {
                con.Close();
            }
        }

        private void SaveDataTableToXml(DataTable dt, string xmlFilePath)
        {
            try
            {
                DataSet ds = new DataSet("danhsach");
                ds.Tables.Add(dt);
                ds.WriteXml(xmlFilePath);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error saving DataTable to XML: " + ex.Message);
            }
        }



        private void btnAdd_Click(object sender, EventArgs e)
        {
            ThemSanPham formThemSanPham = new ThemSanPham(dgvProduccts);
            formThemSanPham.DataUpdatedEvent += FormThemSanPham_DataUpdatedEvent;
            formThemSanPham.Show();
        }

        private void FormThemSanPham_DataUpdatedEvent(object sender, EventArgs e)
        {
            dgvProduccts.DataSource = GetDataFromDatabase();
        }


        private DataTable GetDataFromDatabase()
        {
            DataTable dt = new DataTable();
            try
            {
                string sql = "SELECT * FROM SanPham";
                using (SqlDataAdapter da = new SqlDataAdapter(sql, con))
                {
                    da.Fill(dt);
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Error retrieving data from database: " + ex.Message);
            }
            finally
            {
                con.Close();
            }
            return dt;
        }


        private void btn_edit_Click(object sender, EventArgs e)
        {
            ChinhSuaSanPham chinhSuaSanPham = new ChinhSuaSanPham();
            chinhSuaSanPham.Show();
            InitGrid(dgvProduccts);
        }

        private void dgvProduccts_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvProduccts.Rows[e.RowIndex];

                // Kiểm tra nếu cột "Update" được click
                if (e.ColumnIndex == dgvProduccts.Columns[5].Index)
                {
                    // Lấy thông tin sản phẩm từ DataGridViewRow
                    string maSP = row.Cells[0].Value.ToString();
                    string tenSP = row.Cells[1].Value.ToString();
                    decimal gia = Convert.ToDecimal(row.Cells[2].Value);
                    int soLuongTonKho = Convert.ToInt32(row.Cells[3].Value);
                    string maDM = row.Cells[4].Value.ToString();

                    // Gọi hàm cập nhật sản phẩm
                    UpdateProduct(maSP, tenSP, gia, soLuongTonKho, maDM);
                }
                // Kiểm tra nếu cột "Remove" được click
                else if (e.ColumnIndex == dgvProduccts.Columns[6].Index)
                {
                    // Lấy thông tin sản phẩm từ DataGridViewRow
                    string maSP = row.Cells[0].Value.ToString();

                    // Gọi hàm xóa sản phẩm
                    RemoveProduct(maSP);
                }
            }
        }


        private void UpdateProduct(string maSP, string tenSP, decimal gia, int soLuongTonKho, string maDM)
        {
            ChinhSuaSanPham chinhSuaSanPham = new ChinhSuaSanPham(maSP, tenSP, gia, soLuongTonKho, maDM);
            chinhSuaSanPham.Show();
        }

        private void RemoveProduct(string maSP)
        {
            try
            {
                con.Open();

                // Viết mã SQL để xóa sản phẩm dựa trên ID
                string sql = "DELETE FROM SanPham WHERE maSP = @maSP";
                SqlCommand cmd = new SqlCommand(sql, con);
                cmd.Parameters.AddWithValue("@maSP", maSP);
                cmd.ExecuteNonQuery();

                MessageBox.Show("Product removed successfully!");
                // Sau khi xóa, bạn có thể làm mới DataGridView nếu cần
               
                InitGrid(dgvProduccts);
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Error removing product: " + ex.Message);
            }
            finally
            {
                con.Close();
            }
        }   

    }
}
