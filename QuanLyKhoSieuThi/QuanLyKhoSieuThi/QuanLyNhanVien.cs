using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Linq;

namespace QuanLyKhoSieuThi
{
    public partial class QuanLyNhanVien : Form
    {

        SqlConnection connection;
        string sqlStatement;
        SqlDataAdapter dataAdapter;
        string nhanVienPath = "../../nhanvien.xml";
        bool saveFlag = true;
        public QuanLyNhanVien()
        {
            InitializeComponent();
            Connect();
            InitComponent();
            GetDataAndBindToDataGridView();
        }

        private void InitComponent()
        {
            // DisabledAll();
            dgv_sanpham.Enabled = true;
            // GetExportVoucher();
            Setcombobox();
            //GetAllSanPham();
            //SetExportEmployee();
            // ResetMenuBackColor();
            dgv_sanpham.CellClick += dgv_sanpham_CellContentClick;
            xemToolStripMenuItem.Checked = true;
            xemToolStripMenuItem.BackColor = Color.LightGray;
            menuStrip1.Cursor = Cursors.Hand;
            // cb_NhanVienXuatHang.DropDownStyle = ComboBoxStyle.DropDownList;
        }
        private void Connect()
        {
            string connectionString = "Data Source=NGOVANTUYEN;Initial Catalog=QuanLyKhoSieuThi;Integrated Security=True";
            connection = new SqlConnection(connectionString);
        }
        private void Setcombobox()
        {
            sqlStatement = "select maNV from NhanVien";
            dataAdapter = new SqlDataAdapter(sqlStatement, connection);
            DataTable dataTable = new DataTable();
            dataAdapter.Fill(dataTable);
            for (int i = 0; i < dataTable.Rows.Count; i++)
            {
                cb_maNV.Items.Add(dataTable.Rows[i][0].ToString());
            }
            cb_maNV.Items.Add("Tất cả");
            cb_maNV.SelectedItem = "Tất cả";
            cb_maNV.DropDownStyle = ComboBoxStyle.DropDownList;
        }

        private void GetDataAndBindToDataGridView()
        {
            try
            {
                // Re-query the data and update the DataGridView
                DataTable dataTable = new DataTable();
                dataAdapter = new SqlDataAdapter("SELECT * FROM NhanVien", connection);
                dataAdapter.Fill(dataTable);
                dgv_sanpham.DataSource = dataTable;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error getting data: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                connection.Close();
            }
        }

        private void label10_Click(object sender, EventArgs e)
        {

        }

        private void btn_them_Click(object sender, EventArgs e)
        {
            try
            {
                connection.Open();
                String maNV = tb_maNV.Text.Trim();
                String tenNV = tb_tenNV.Text.Trim();
                String gioiTinh = tb_gioiTinh.Text.Trim();
                String sdt = tb_sdt.Text.Trim();
                String diaChi = tb_diaChi.Text.Trim();

                sqlStatement = "INSERT INTO NhanVien VALUES(@maNV, @tenNV, @gioiTinh, @sdt, @diaChi)";
                SqlCommand command = new SqlCommand(sqlStatement, connection);
                command.Parameters.AddWithValue("@maNV", maNV);
                command.Parameters.AddWithValue("@tenNV", tenNV);
                command.Parameters.AddWithValue("@gioiTinh", gioiTinh);
                command.Parameters.AddWithValue("@sdt", sdt);
                command.Parameters.AddWithValue("@diaChi", diaChi);

                command.ExecuteNonQuery();

                MessageBox.Show("Data inserted successfully.");
                GetDataAndBindToDataGridView();
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }
        }

        private void btn_sua_Click(object sender, EventArgs e)
        {
            try
            {
                connection.Open();

                String maNV = tb_maNV.Text.Trim();
                String tenNV = tb_tenNV.Text.Trim();
                String gioiTinh = tb_gioiTinh.Text.Trim();
                String sdt = tb_sdt.Text.Trim();
                String diaChi = tb_diaChi.Text.Trim();

                // Use parameterized query for UPDATE statement
                string sqlStatement = "UPDATE NhanVien SET tenNV = @tenNV, gioiTinh = @gioiTinh, SDT = @sdt, diaChi = @diaChi WHERE maNV = @maNV";
                SqlCommand command = new SqlCommand(sqlStatement, connection);
                command.Parameters.AddWithValue("@maNV", maNV);
                command.Parameters.AddWithValue("@tenNV", tenNV);
                command.Parameters.AddWithValue("@gioiTinh", gioiTinh);
                command.Parameters.AddWithValue("@sdt", sdt);
                command.Parameters.AddWithValue("@diaChi", diaChi);

                command.ExecuteNonQuery();

                MessageBox.Show("Data updated successfully.");
                GetDataAndBindToDataGridView();
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Error updating data: " + ex.Message + "\n\nStack Trace:\n" + ex.StackTrace, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                connection.Close();
            }
        }

        private void dgv_sanpham_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0) // Ensure a valid row is clicked
            {
                DataGridViewRow selectedRow = dgv_sanpham.Rows[e.RowIndex];

                // Assuming your columns in the DataGridView are in order: maSP, tenSP, soLuong, gia, maDM
                tb_maNV.Text = selectedRow.Cells["maNV"].Value.ToString();
                tb_tenNV.Text = selectedRow.Cells["tenNV"].Value.ToString();
                tb_sdt.Text = selectedRow.Cells["sdt"].Value.ToString();
                tb_gioiTinh.Text = selectedRow.Cells["gioiTinh"].Value.ToString();
                tb_diaChi.Text = selectedRow.Cells["diaChi"].Value.ToString();


         
            }
        }

        private void btn_xoa_Click(object sender, EventArgs e)
        {
            String maNV = tb_maNV.Text.Trim();
            DeleteNhanVien(maNV);
            GetDataAndBindToDataGridView();
        }
        private void DeleteNhanVien(string maNV)
        {
            try
            {
                connection.Open();
                // Use parameterized query for DELETE statement
                string sqlStatement = "DELETE FROM NhanVien WHERE maNV = @maNV";
                SqlCommand command = new SqlCommand(sqlStatement, connection);
                command.Parameters.AddWithValue("@maNV", maNV);
                // Execute the DELETE query
                command.ExecuteNonQuery();
                MessageBox.Show("Data deleted successfully.");
                GetDataAndBindToDataGridView();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error deleting data: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                connection.Close(); // Ensure the connection is closed even if an exception occurs
            }
        }

        private void saoLưuToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                nhanVienSqlToXML();
                MessageBox.Show("Đã sao lưu dữ liệu!");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Sao lưu dữ liệu thất bại do: " + ex.Message);
            }
        }
        private void nhanVienSqlToXML()
        {
            sqlStatement = "select * from NhanVien for xml auto";
            dataAdapter = new SqlDataAdapter(sqlStatement, connection);
            DataTable dataTable = new DataTable();
            dataAdapter.Fill(dataTable);
            string xml = "<?xml version='1.0' encoding='utf-8' ?><danhSach>";
            xml += dataTable.Rows[0].ItemArray[0].ToString() + "</danhSach>";
            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.LoadXml(xml);
            xmlDocument.Save(nhanVienPath);
        }

        private void tảiDữLiệuToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                UpdateDatabaseFromXml(nhanVienPath);
                MessageBox.Show("Khôi phục dữ liệu thành công!");
                GetDataAndBindToDataGridView();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Khôi phục dữ liệu thất bại do: " + ex.Message);
            }
        }

        private void UpdateDatabaseFromXml(string xmlFilePath)
        {
            try
            {
                // Create a new DataSet
                DataSet dataSet = new DataSet();

                // Read data from the XML file into the DataSet
                dataSet.ReadXml(xmlFilePath);

                // Assuming that your XML structure matches the DataTable structure in your database
                DataTable dataTable = dataSet.Tables[0];

                // Open the database connection
                connection.Open();

                foreach (DataRow row in dataTable.Rows)
                {
                    // Assuming your columns are named maSP, tenSP, gia, soLuong, maDM
                    string maNV = row["maNV"].ToString();
                    string tenNV = row["tenNV"].ToString();
                    // double gia = Convert.ToDouble(row["gia"]);
                    //int soLuong = Convert.ToInt32(row["soLuongTonKho"]);
                    //string maDM = row["maDM"].ToString();


                    String gioiTinh = row["gioiTinh"].ToString();
                    String sdt= row["SDT"].ToString();
                    string diaChi = row["diaChi"].ToString();


                    // Check if the record exists in the database
                    string checkExistenceQuery = "SELECT COUNT(*) FROM NhanVien WHERE maNV = @maNV";
                    SqlCommand checkExistenceCommand = new SqlCommand(checkExistenceQuery, connection);
                    checkExistenceCommand.Parameters.AddWithValue("@maNV", maNV);

                    int count = Convert.ToInt32(checkExistenceCommand.ExecuteScalar());

                    if (count > 0)
                    {
                        // If the record exists, update it
                        string updateQuery = "UPDATE NhanVien SET tenNV = @tenNV, gioiTinh = @gioiTinh, sdt = @sdt, diaChi = @diaChi WHERE maNV = @maNV";
                        SqlCommand updateCommand = new SqlCommand(updateQuery, connection);
                        updateCommand.Parameters.AddWithValue("@maNV", maNV);
                        updateCommand.Parameters.AddWithValue("@tenNV", tenNV);
                        updateCommand.Parameters.AddWithValue("@gioiTinh", gioiTinh);
                        updateCommand.Parameters.AddWithValue("@sdt", sdt);
                        updateCommand.Parameters.AddWithValue("@diaChi", diaChi);

                        updateCommand.ExecuteNonQuery();
                    }
                    else
                    {
                        // If the record doesn't exist, insert a new record
                        string insertQuery = "INSERT INTO NhanVien (maNV, tenNV, gioiTinh, SDT, diaChi) VALUES (@maNV, @tenNV, @gioiTinh, @sdt, @diaChi)";
                        SqlCommand insertCommand = new SqlCommand(insertQuery, connection);
                        insertCommand.Parameters.AddWithValue("@maNV", maNV);
                        insertCommand.Parameters.AddWithValue("@tenNV", tenNV);
                        insertCommand.Parameters.AddWithValue("@gioiTinh", gioiTinh);
                        insertCommand.Parameters.AddWithValue("@sdt", sdt);
                        insertCommand.Parameters.AddWithValue("@diaChi", diaChi);

                        insertCommand.ExecuteNonQuery();
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error updating database from XML: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                // Close the database connection
                connection.Close();
            }
        }

        private void cb_maNV_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedCategory = cb_maNV.SelectedItem.ToString();

            try
            {
                connection.Open();

                // Build the SQL query based on the selected category
                string sqlStatement;
                if (selectedCategory == "Tất cả")
                {
                    // If "Tất cả" is selected, show all products
                    sqlStatement = "SELECT * FROM NhanVien";
                }
                else
                {
                    // Show products for the selected category
                    sqlStatement = "SELECT * FROM NhanVien WHERE maNV = @maNV";
                }

                SqlDataAdapter dataAdapter = new SqlDataAdapter(sqlStatement, connection);

                if (selectedCategory != "Tất cả")
                {
                    // If a specific category is selected, add the category parameter
                    dataAdapter.SelectCommand.Parameters.AddWithValue("@maNV", selectedCategory);
                }

                DataTable dataTable = new DataTable();
                dataAdapter.Fill(dataTable);

                dgv_sanpham.DataSource = dataTable;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error filtering data: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                connection.Close();
            }
        }

        private void tb_search_TextChanged(object sender, EventArgs e)
        {
            string searchTerm = tb_search.Text.Trim();

            try
            {
                connection.Open();

                // Build the SQL query for searching by product name
                string sqlStatement = "SELECT * FROM NhanVien WHERE tenNV LIKE @searchTerm";
                SqlDataAdapter dataAdapter = new SqlDataAdapter(sqlStatement, connection);
                dataAdapter.SelectCommand.Parameters.AddWithValue("@searchTerm", "%" + searchTerm + "%");

                DataTable dataTable = new DataTable();
                dataAdapter.Fill(dataTable);

                dgv_sanpham.DataSource = dataTable;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error searching data: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                connection.Close();
            }
        }

        private void đóngToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
