using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
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
    public partial class QuanLySanPham : Form
    {

        SqlConnection connection;
        string sqlStatement;
        SqlDataAdapter dataAdapter;
        string sanPhamPath = "../../SanPham.xml";
        bool saveFlag = true;
        public QuanLySanPham()
        {
            InitializeComponent();
            Connect();
            InitComponent();
            GetDataAndBindToDataGridView();
            dgv_sanpham.CellClick += dgv_sanpham_CellContentClick;
            cb_category.SelectedIndexChanged += cb_category_SelectedIndexChanged;
            tb_search.TextChanged += tb_search_TextChanged;
        }
        private void InitComponent()
        {
           // DisabledAll();
            dgv_sanpham.Enabled = true;
           // GetExportVoucher();
            SetDanhMuc();
            //GetAllSanPham();
            //SetExportEmployee();
           // ResetMenuBackColor();
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


        private void SetDanhMuc()
        {
            sqlStatement = "select maDM from DanhMuc";
            dataAdapter = new SqlDataAdapter(sqlStatement, connection);
            DataTable dataTable = new DataTable();
            dataAdapter.Fill(dataTable);
            for (int i = 0; i < dataTable.Rows.Count; i++)
            {
                cb_DM.Items.Add(dataTable.Rows[i][0].ToString());
                cb_category.Items.Add(dataTable.Rows[i][0].ToString());
            }
            cb_DM.Items.Add("Tất cả");
            cb_DM.SelectedItem = "Tất cả";
            cb_DM.DropDownStyle = ComboBoxStyle.DropDownList;
            cb_category.Items.Add("Tất cả");
            cb_category.SelectedItem = "Tất cả";
            cb_category.DropDownStyle = ComboBoxStyle.DropDownList;
        }

        private void GetDataAndBindToDataGridView()
        {
            try
            {
                // Re-query the data and update the DataGridView
                DataTable dataTable = new DataTable();
                dataAdapter = new SqlDataAdapter("SELECT * FROM SanPham", connection);
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

        private void btn_save_Click(object sender, EventArgs e)
        {
            try
            {
                connection.Open();
                String maDM = cb_DM.SelectedItem.ToString();
                String tenSP = tb_tenSP.Text.Trim();
                String maSP = tb_maSP.Text.Trim();
                int soLuong = Int16.Parse(tb_soLuong.Text.Trim());
                Double gia = Double.Parse(tb_gia.Text.Trim());

                sqlStatement = "INSERT INTO SanPham VALUES(@maSP, @tenSP, @gia, @soLuong, @maDM)";
                SqlCommand command = new SqlCommand(sqlStatement, connection);
                command.Parameters.AddWithValue("@maSP", maSP);
                command.Parameters.AddWithValue("@tenSP", tenSP);
                command.Parameters.AddWithValue("@gia", gia);
                command.Parameters.AddWithValue("@soLuong", soLuong);
                command.Parameters.AddWithValue("@maDM", maDM);

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

                // Ensure the selected item in cb_DM is not null
                if (cb_DM.SelectedItem == null)
                {
                    MessageBox.Show("Please select a category.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                String maDM = cb_DM.SelectedItem.ToString();
                String tenSP = tb_tenSP.Text.Trim();
                String maSP = tb_maSP.Text.Trim();

                // Handle potential parsing errors for integers and doubles
                if (!int.TryParse(tb_soLuong.Text.Trim(), out int soLuong) || !double.TryParse(tb_gia.Text.Trim(), out double gia))
                {
                    MessageBox.Show("Invalid input for quantity or price.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Use parameterized query for UPDATE statement
                string sqlStatement = "UPDATE SanPham SET tenSP = @tenSP, gia = @gia, soLuongTonKho = @soLuong, maDM = @maDM WHERE maSP = @maSP";
                SqlCommand command = new SqlCommand(sqlStatement, connection);
                command.Parameters.AddWithValue("@maSP", maSP);
                command.Parameters.AddWithValue("@tenSP", tenSP);
                command.Parameters.AddWithValue("@gia", gia);
                command.Parameters.AddWithValue("@soLuong", soLuong);
                command.Parameters.AddWithValue("@maDM", maDM);

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


        private void DeleteSanPham(string maSP)
        {
            try
            {
                connection.Open();
                // Use parameterized query for DELETE statement
                string sqlStatement = "DELETE FROM SanPham WHERE maSP = @maSP";
                SqlCommand command = new SqlCommand(sqlStatement, connection);
                command.Parameters.AddWithValue("@maSP", maSP);
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

        private void btn_xoa_Click(object sender, EventArgs e)
        {
            String maSP = tb_maSP.Text.Trim();
            DeleteSanPham(maSP);
            GetDataAndBindToDataGridView();
        }

        private void dgv_sanpham_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0) // Ensure a valid row is clicked
            {
                DataGridViewRow selectedRow = dgv_sanpham.Rows[e.RowIndex];

                // Assuming your columns in the DataGridView are in order: maSP, tenSP, soLuong, gia, maDM
                tb_maSP.Text = selectedRow.Cells["maSP"].Value.ToString();
                tb_tenSP.Text = selectedRow.Cells["tenSP"].Value.ToString();
                tb_soLuong.Text = selectedRow.Cells["soLuongTonKho"].Value.ToString();
                tb_gia.Text = selectedRow.Cells["gia"].Value.ToString();

                // Assuming that your maDM values in the DataGridView are strings (if not, modify accordingly)
                cb_DM.SelectedItem = selectedRow.Cells["maDM"].Value.ToString();
            }
        }

        private void cb_category_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Get the selected category from the ComboBox
            string selectedCategory = cb_category.SelectedItem.ToString();

            try
            {
                connection.Open();

                // Build the SQL query based on the selected category
                string sqlStatement;
                if (selectedCategory == "Tất cả")
                {
                    // If "Tất cả" is selected, show all products
                    sqlStatement = "SELECT * FROM SanPham";
                }
                else
                {
                    // Show products for the selected category
                    sqlStatement = "SELECT * FROM SanPham WHERE maDM = @maDM";
                }

                SqlDataAdapter dataAdapter = new SqlDataAdapter(sqlStatement, connection);

                if (selectedCategory != "Tất cả")
                {
                    // If a specific category is selected, add the category parameter
                    dataAdapter.SelectCommand.Parameters.AddWithValue("@maDM", selectedCategory);
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
                string sqlStatement = "SELECT * FROM SanPham WHERE tenSP LIKE @searchTerm";
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

        private void sanPhamSqlToXML()
        {
            sqlStatement = "select * from SanPham for xml auto";
            dataAdapter = new SqlDataAdapter(sqlStatement, connection);
            DataTable dataTable = new DataTable();
            dataAdapter.Fill(dataTable);
            string xml = "<?xml version='1.0' encoding='utf-8' ?><danhSach>";
            xml += dataTable.Rows[0].ItemArray[0].ToString() + "</danhSach>";
            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.LoadXml(xml);
            xmlDocument.Save(sanPhamPath);
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
                    string maSP = row["maSP"].ToString();
                    string tenSP = row["tenSP"].ToString();
                   // double gia = Convert.ToDouble(row["gia"]);
                    //int soLuong = Convert.ToInt32(row["soLuongTonKho"]);
                    //string maDM = row["maDM"].ToString();


                    double gia = Convert.ToDouble(row["gia"]);
                    int soLuong = Convert.ToInt32(row["soLuongTonKho"]);
                    string maDM = row["maDM"].ToString();


                    // Check if the record exists in the database
                    string checkExistenceQuery = "SELECT COUNT(*) FROM SanPham WHERE maSP = @maSP";
                    SqlCommand checkExistenceCommand = new SqlCommand(checkExistenceQuery, connection);
                    checkExistenceCommand.Parameters.AddWithValue("@maSP", maSP);

                    int count = Convert.ToInt32(checkExistenceCommand.ExecuteScalar());

                    if (count > 0)
                    {
                        // If the record exists, update it
                        string updateQuery = "UPDATE SanPham SET tenSP = @tenSP, gia = @gia, soLuongTonKho = @soLuong, maDM = @maDM WHERE maSP = @maSP";
                        SqlCommand updateCommand = new SqlCommand(updateQuery, connection);
                        updateCommand.Parameters.AddWithValue("@maSP", maSP);
                        updateCommand.Parameters.AddWithValue("@tenSP", tenSP);
                        updateCommand.Parameters.AddWithValue("@gia", gia);
                        updateCommand.Parameters.AddWithValue("@soLuong", soLuong);
                        updateCommand.Parameters.AddWithValue("@maDM", maDM);

                        updateCommand.ExecuteNonQuery();
                    }
                    else
                    {
                        // If the record doesn't exist, insert a new record
                        string insertQuery = "INSERT INTO SanPham (maSP, tenSP, gia, soLuongTonKho, maDM) VALUES (@maSP, @tenSP, @gia, @soLuong, @maDM)";
                        SqlCommand insertCommand = new SqlCommand(insertQuery, connection);
                        insertCommand.Parameters.AddWithValue("@maSP", maSP);
                        insertCommand.Parameters.AddWithValue("@tenSP", tenSP);
                        insertCommand.Parameters.AddWithValue("@gia", gia);
                        insertCommand.Parameters.AddWithValue("@soLuong", soLuong);
                        insertCommand.Parameters.AddWithValue("@maDM", maDM);

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


        private void saoLưuToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                sanPhamSqlToXML();
                MessageBox.Show("Đã sao lưu dữ liệu!");
            }catch(Exception ex)
            {
                MessageBox.Show("Sao lưu dữ liệu thất bại do: "+ ex.Message);
            }
        }

        private void tảiDữLiệuToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                UpdateDatabaseFromXml(sanPhamPath);
                MessageBox.Show("Khôi phục dữ liệu thành công!");
                GetDataAndBindToDataGridView();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Khôi phục dữ liệu thất bại do: " + ex.Message);
            }
            
        }

        private void QuanLySanPham_Load(object sender, EventArgs e)
        {

        }

        private void đóngToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
