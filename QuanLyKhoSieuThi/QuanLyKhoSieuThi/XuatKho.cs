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
    public partial class XuatKho : Form
    {
        SqlConnection connection;
        string sqlStatement;
        SqlDataAdapter dataAdapter;
        string phieuXuatPath = "../../PhieuXuat.xml";
        string ctPhieuXuatPath = "../../ChiTietPhieuXuat.xml";
        bool saveFlag = true;
        public XuatKho()
        {
            InitializeComponent();
            Connect();
            InitComponent();
        }

        private void Connect()
        {
            string server = Environment.MachineName + "\\SQLExpress";
            string username = "sa";
            string password = "123456";
            string database = "QuanLyKhoSieuThi";
            string connectionString = "Data Source=" + server + ";Initial Catalog=" + database + ";User ID=" + username + ";Password=" + password + ";Persist Security Info=true;";
            connection = new SqlConnection(connectionString);
        }

        private void FormatGrid(DataGridView dgv)
        {
            dgv.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.ColumnHeadersDefaultCellStyle.Font = new Font(DataGridView.DefaultFont, FontStyle.Bold);
            dgv.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            dgv.RowHeadersVisible = false;
            dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgv.AllowUserToAddRows = false;
            dgv.AllowUserToDeleteRows = false;
            dgv.AllowUserToOrderColumns = false;
            dgv.AllowUserToResizeColumns = false;
            dgv.AllowUserToResizeRows = false;
            dgv.EditMode = DataGridViewEditMode.EditProgrammatically;
            dgv.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgv.MultiSelect = false;
        }

        private void InitGrid(DataGridView dgv)
        {
            if (dgv.Name.Equals("dgv_DanhSachSanPhamPX"))
            {
                dgv.DataSource = null;
                dgv.Columns.Clear();
                dgv.ColumnCount = 3;
                dgv.Columns[0].HeaderText = "Mã sản phẩm";
                dgv.Columns[1].HeaderText = "Tên sản phẩm";
                dgv.Columns[2].HeaderText = "Số lượng";
            }
            FormatGrid(dgv);
        }

        private void DisabledAll()
        {
            dtp_From.Format = DateTimePickerFormat.Custom;
            dtp_To.Format = DateTimePickerFormat.Custom;
            dtp_From.CustomFormat = " ";
            dtp_To.CustomFormat = " ";
            dtp_From.Enabled = false;
            dtp_To.Enabled = false;
            tb_MaPhieuXuat.Enabled = false;
            cb_NhanVienXuatHang.Enabled = false;
            tb_NgayXuatHang.Enabled = false;
            tb_MaSanPham.Enabled = false;
            tb_TenSanPham.Enabled = false;
            nud_SoLuongXuat.Enabled = false;
            btn_Them.Enabled = false;
            btn_Them.BackColor = Color.LightGray;
            btn_ToiDa.Enabled = false;
            btn_ToiDa.BackColor = Color.LightGray;
            dgv_SanPham.Enabled = false;
            dgv_DanhSachSanPhamPX.Enabled = false;
            dgv_XuatKho.Enabled = false;
            cb_DanhMuc.Enabled = false;
            tb_TimTheoTenSP.Enabled = false;
            lưuToolStripMenuItem.Enabled = false;
        }

        private void InitComponent()
        {
            DisabledAll();
            dgv_XuatKho.Enabled = true;
            GetExportVoucher();
            SetDanhMuc();
            GetAllSanPham();
            SetExportEmployee();
            ResetMenuBackColor();
            xemToolStripMenuItem.Checked = true;
            xemToolStripMenuItem.BackColor = Color.LightGray;
            menuStrip1.Cursor = Cursors.Hand;
            cb_NhanVienXuatHang.DropDownStyle = ComboBoxStyle.DropDownList;
        }

        private void SetExportEmployee()
        {
            sqlStatement = "select maNV, tenNV from NhanVien";
            dataAdapter = new SqlDataAdapter(sqlStatement, connection);
            DataTable dataTable = new DataTable();
            dataAdapter.Fill(dataTable);
            Dictionary<string, string> nhanVienXuatHang = new Dictionary<string, string>();
            for (int i = 0; i < dataTable.Rows.Count; i++)
            {
                nhanVienXuatHang.Add(dataTable.Rows[i][0].ToString(), dataTable.Rows[i][1].ToString());
            }
            cb_NhanVienXuatHang.DataSource = new BindingSource(nhanVienXuatHang, null);
            cb_NhanVienXuatHang.DisplayMember = "Value";
            cb_NhanVienXuatHang.ValueMember = "Key";
        }

        private void GetExportVoucher()
        {
            sqlStatement = "select maPX as N'Mã phiếu xuất', format(ngayXuatHang, 'dd/MM/yyyy') as N'Ngày xuất hàng', tenNV as N'Tên nhân viên'\r\n" +
                "from PhieuXuat, NhanVien\r\n" +
                "where PhieuXuat.maNV = NhanVien.maNV";
            dataAdapter = new SqlDataAdapter(sqlStatement, connection);
            DataTable dataTable = new DataTable();
            dataAdapter.Fill(dataTable);
            dgv_XuatKho.DataSource = dataTable;
            FormatGrid(dgv_XuatKho);
        }

        private void GetAllSanPham()
        {
            sqlStatement = "select maSP as N'Mã sản phẩm', tenSP as N'Tên sản phẩm', FORMAT(gia, 'C0', 'vi-VN') as N'Đơn giá'\r\n" +
                "from SanPham";
            dataAdapter = new SqlDataAdapter(sqlStatement, connection);
            DataTable dataTable = new DataTable();
            dataAdapter.Fill(dataTable);
            dgv_SanPham.DataSource = dataTable;
            InitGrid(dgv_SanPham);
        }

        private void ResetMenuBackColor()
        {
            thêmToolStripMenuItem.BackColor = Color.Transparent;
            lưuToolStripMenuItem.BackColor = Color.Transparent;
            tìmToolStripMenuItem.BackColor = Color.Transparent;
            đóngToolStripMenuItem.BackColor = Color.Transparent;
            xemToolStripMenuItem.BackColor = Color.Transparent;
            saoLưuToolStripMenuItem.BackColor = Color.Transparent;
            tảiDữLiệuToolStripMenuItem.BackColor = Color.Transparent;
        }

        private DataTable ReadXMLFile(string path)
        {
            DataTable dataTable = new DataTable();
            if (File.Exists(path))
            {
                DataSet dataSet = new DataSet();
                FileStream fsReadXML = new FileStream(path, FileMode.Open);
                dataSet.ReadXml(fsReadXML);
                DataView dataView = new DataView(dataSet.Tables[0]);
                dataTable = dataView.Table;
                fsReadXML.Close();
            }
            else
            {
                MessageBox.Show("File path: '" + path + "' does not exist!");
            }
            return dataTable;
        }

        private void SetExportVoucherCode()
        {
            sqlStatement = "select count(maPX) from PhieuXuat";
            dataAdapter = new SqlDataAdapter(sqlStatement, connection);
            DataTable dataTable = new DataTable();
            dataAdapter.Fill(dataTable);
            tb_MaPhieuXuat.Text = "PN";
            if (int.Parse(dataTable.Rows[0][0].ToString()) < 10)
            {
                tb_MaPhieuXuat.Text += "0" + (int.Parse(dataTable.Rows[0][0].ToString()) + 1);
            }
            else
            {
                tb_MaPhieuXuat.Text += (int.Parse(dataTable.Rows[0][0].ToString()) + 1);
            }
        }
        private void SetDanhMuc()
        {
            sqlStatement = "select tenDM from DanhMuc";
            dataAdapter = new SqlDataAdapter(sqlStatement, connection);
            DataTable dataTable = new DataTable();
            dataAdapter.Fill(dataTable);
            for (int i = 0; i < dataTable.Rows.Count; i++)
            {
                cb_DanhMuc.Items.Add(dataTable.Rows[i][0].ToString());
            }
            cb_DanhMuc.Items.Add("Tất cả");
            cb_DanhMuc.SelectedItem = "Tất cả";
            cb_DanhMuc.DropDownStyle = ComboBoxStyle.DropDownList;
        }

        private void SaveProductToXML()
        {
            string maPX = tb_MaPhieuXuat.Text;
            string ngayXuatHang = tb_NgayXuatHang.Text;
            string maNV = ((KeyValuePair<string, string>)cb_NhanVienXuatHang.SelectedItem).Key;
            XDocument xDocumentPN = XDocument.Load(phieuXuatPath);
            xDocumentPN.Element("danhSach").Add(
                new XElement("phieuXuat",
                    new XAttribute("maPX", maPX),
                    new XAttribute("ngayXuatHang", ngayXuatHang),
                    new XAttribute("maNV", maNV)
                )
            );
            xDocumentPN.Save(phieuXuatPath);
            XDocument xDocumentCTPN = XDocument.Load(ctPhieuXuatPath);
            for (int i = 0; i < dgv_DanhSachSanPhamPX.Rows.Count; i++)
            {
                xDocumentCTPN.Element("danhSach").Add(
                    new XElement("chiTietPhieuXuat",
                        new XAttribute("maPX", maPX),
                        new XAttribute("maSP", dgv_DanhSachSanPhamPX.Rows[i].Cells[0].Value.ToString()),
                        new XAttribute("soLuongXuat", dgv_DanhSachSanPhamPX.Rows[i].Cells[2].Value.ToString())
                    )
                );
            }
            xDocumentCTPN.Save(ctPhieuXuatPath);
        }

        private void DeleteAllExportVoucher()
        {
            connection.Open();
            sqlStatement = "delete from PhieuXuat";
            SqlCommand cmd = new SqlCommand(sqlStatement, connection);
            cmd.ExecuteNonQuery();
            connection.Close();
        }

        private void SaveProductToDB()
        {
            DeleteAllExportVoucher();
            connection.Open();
            DataTable dataTablePX = ReadXMLFile(phieuXuatPath);
            for (int i = 0; i < dataTablePX.Rows.Count; i++)
            {
                sqlStatement = "set dateformat dmy insert into PhieuXuat values (";
                for (int j = 0; j < dataTablePX.Columns.Count - 1; j++)
                {
                    sqlStatement += "'" + dataTablePX.Rows[i][j].ToString().Trim() + "', ";
                }
                sqlStatement += "'" + dataTablePX.Rows[i][dataTablePX.Columns.Count - 1].ToString().Trim() + "')";
                SqlCommand sqlCommandPX = new SqlCommand(sqlStatement, connection);
                sqlCommandPX.ExecuteNonQuery();
            }
            DataTable dataTableCTPX = ReadXMLFile(ctPhieuXuatPath);
            for (int i = 0; i < dataTableCTPX.Rows.Count; i++)
            {
                sqlStatement = "insert into ChiTietPhieuXuat values (";
                for (int j = 0; j < dataTableCTPX.Columns.Count - 1; j++)
                {
                    sqlStatement += "'" + dataTableCTPX.Rows[i][j].ToString().Trim() + "', ";
                }
                sqlStatement += "'" + dataTableCTPX.Rows[i][dataTableCTPX.Columns.Count - 1].ToString().Trim() + "')";
                SqlCommand sqlCommandCTPX = new SqlCommand(sqlStatement, connection);
                sqlCommandCTPX.ExecuteNonQuery();
            }
            connection.Close();
        }

        private void UpdateInventoryNumber()
        {
            connection.Open();
            for (int i = 0; i < dgv_DanhSachSanPhamPX.Rows.Count; i++)
            {
                sqlStatement = "update SanPham set soLuongTonKho = soLuongTonKho - " + dgv_DanhSachSanPhamPX.Rows[i].Cells[2].Value + "where maSP = '" + dgv_DanhSachSanPhamPX.Rows[i].Cells[0].Value.ToString().Trim() + "'";
                SqlCommand sqlCommand = new SqlCommand(sqlStatement, connection);
                sqlCommand.ExecuteNonQuery();
            }
            connection.Close();
        }

        private void PhieuXuatSqlToXML()
        {
            sqlStatement = "select maPX, format(ngayXuatHang, 'dd/MM/yyyy') as ngayXuatHang, maNV from PhieuXuat as phieuXuat for xml auto";
            dataAdapter = new SqlDataAdapter(sqlStatement, connection);
            DataTable dataTable = new DataTable();
            dataAdapter.Fill(dataTable);
            string xml = "<?xml version='1.0' encoding='utf-8' ?><danhSach>";
            xml += dataTable.Rows[0].ItemArray[0].ToString() + "</danhSach>";
            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.LoadXml(xml);
            xmlDocument.Save(phieuXuatPath);
        }

        private void CtPhieuXuatSqlToXML()
        {
            sqlStatement = "select * from ChiTietPhieuXuat as chiTietPhieuXuat for xml auto";
            dataAdapter = new SqlDataAdapter(sqlStatement, connection);
            DataTable dataTable = new DataTable();
            dataAdapter.Fill(dataTable);
            string xml = "<?xml version='1.0' encoding='utf-8' ?><danhSach>";
            xml += dataTable.Rows[0].ItemArray[0].ToString() + "</danhSach>";
            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.LoadXml(xml);
            xmlDocument.Save(ctPhieuXuatPath);
        }

        private void FindByDay()
        {
            if (dtp_From.Text != "" && dtp_To.Text == "")
            {
                sqlStatement = "set dateformat dmy\r\n" +
                    "select maPX as N'Mã phiếu xuất', format(ngayXuatHang, 'dd/MM/yyyy') as N'Ngày xuất hàng', tenNV as N'Tên nhân viên'\r\n" +
                    "from PhieuXuat, NhanVien\r\n" +
                    "where PhieuXuat.maNV = NhanVien.maNV and ngayXuatHang = '" + dtp_From.Value.ToShortDateString().ToString().Trim() + "'";
                dataAdapter = new SqlDataAdapter(sqlStatement, connection);
                DataTable dataTable = new DataTable();
                dataAdapter.Fill(dataTable);
                dgv_XuatKho.DataSource = dataTable;
                FormatGrid(dgv_XuatKho);
            }
            else if (dtp_From.Text == "" && dtp_To.Text != "")
            {
                sqlStatement = "set dateformat dmy\r\n" +
                   "select maPX as N'Mã phiếu xuất', format(ngayXuatHang, 'dd/MM/yyyy') as N'Ngày xuất hàng', tenNV as N'Tên nhân viên'\r\n" +
                    "from PhieuXuat, NhanVien\r\n" +
                    "where PhieuXuat.maNV = NhanVien.maNV and ngayXuatHang = '" + dtp_To.Value.ToShortDateString().ToString().Trim() + "'";
                dataAdapter = new SqlDataAdapter(sqlStatement, connection);
                DataTable dataTable = new DataTable();
                dataAdapter.Fill(dataTable);
                dgv_XuatKho.DataSource = dataTable;
                FormatGrid(dgv_XuatKho);
            }
            else
            {
                sqlStatement = "set dateformat dmy\r\n" +
                    "select maPX as N'Mã phiếu xuất', format(ngayXuatHang, 'dd/MM/yyyy') as N'Ngày xuất hàng', tenNV as N'Tên nhân viên'\r\n" +
                    "from PhieuXuat, NhanVien\r\n" +
                    "where PhieuXuat.maNV = NhanVien.maNV and ngayXuatHang between '" + dtp_From.Value.ToShortDateString().ToString().Trim() + "' and '" + dtp_To.Value.ToShortDateString().ToString().Trim() + "'";
                dataAdapter = new SqlDataAdapter(sqlStatement, connection);
                DataTable dataTable = new DataTable();
                dataAdapter.Fill(dataTable);
                dgv_XuatKho.DataSource = dataTable;
                InitGrid(dgv_XuatKho);
            }
        }

        private Decimal GetMaxProductQuantity()
        {
            sqlStatement = "select soLuongTonKho\r\n" +
                "from SanPham\r\n" +
                "where maSP = '" + tb_MaSanPham.Text.ToString().Trim() + "'";
            dataAdapter = new SqlDataAdapter(sqlStatement, connection);
            DataTable dataTable = new DataTable();
            dataAdapter.Fill(dataTable);
            Decimal quantity = Decimal.Parse(dataTable.Rows[0][0].ToString());
            return quantity;
        }

        private void đóngToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (saveFlag || dgv_DanhSachSanPhamPX.Rows.Count == 0 || MessageBox.Show("Phiếu xuất chưa được lưu, bạn có muốn hủy dữ liệu đã nhập không?", "Thông báo hệ thống", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                Close();
            }
        }

        private void thêmToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DisabledAll();
            cb_DanhMuc.Enabled = true;
            tb_TimTheoTenSP.Enabled = true;
            dgv_SanPham.Enabled = true;
            cb_NhanVienXuatHang.Enabled = true;
            nud_SoLuongXuat.Enabled = true;
            btn_Them.Enabled = true;
            btn_Them.BackColor = Color.Teal;
            btn_Them.ForeColor = Color.White;
            btn_ToiDa.Enabled = true;
            btn_ToiDa.BackColor = Color.Teal;
            btn_ToiDa.ForeColor = Color.White;
            ResetMenuBackColor();
            thêmToolStripMenuItem.BackColor = Color.LightGray;
            SetExportVoucherCode();
            tb_NgayXuatHang.Text = DateTime.Today.ToShortDateString();
            InitGrid(dgv_DanhSachSanPhamPX);
            saveFlag = false;
            lưuToolStripMenuItem.Enabled = true;
        }

        private void cb_DanhMuc_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cb_DanhMuc.SelectedItem.Equals("Tất cả"))
            {
                sqlStatement = "select maSP as N'Mã sản phẩm', tenSP as N'Tên sản phẩm', FORMAT(gia, 'C0', 'vi-VN') as N'Đơn giá'\r\n" +
                "from SanPham, DanhMuc\r\n" +
                "where SanPham.maDM = DanhMuc.maDM";
            }
            else
            {
                sqlStatement = "select maSP as N'Mã sản phẩm', tenSP as N'Tên sản phẩm', FORMAT(gia, 'C0', 'vi-VN') as N'Đơn giá'\r\n" +
                "from SanPham, DanhMuc\r\n" +
                "where SanPham.maDM = DanhMuc.maDM and tenDM = N'" + cb_DanhMuc.Text + "'";
            }
            dataAdapter = new SqlDataAdapter(sqlStatement, connection);
            DataTable dataTable = new DataTable();
            dataAdapter.Fill(dataTable);
            dgv_SanPham.DataSource = dataTable;
        }

        private void tb_TimTheoTenSP_TextChanged(object sender, EventArgs e)
        {
            sqlStatement = "select maSP as N'Mã sản phẩm', tenSP as N'Tên sản phẩm', FORMAT(gia, 'C0', 'vi-VN') as N'Đơn giá'\r\n" +
                "from SanPham\r\n" +
                "where tenSP like N'%" + tb_TimTheoTenSP.Text + "%'";
            dataAdapter = new SqlDataAdapter(sqlStatement, connection);
            DataTable dataTable = new DataTable();
            dataAdapter.Fill(dataTable);
            dgv_SanPham.DataSource = dataTable;
        }

        private void btn_Them_Click(object sender, EventArgs e)
        {
            if (tb_MaSanPham.Text.Equals(""))
            {
                MessageBox.Show("Vui lòng chọn sản phẩm", "Thông báo hệ thống", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else if (nud_SoLuongXuat.Value == 0)
            {
                MessageBox.Show("Vui lòng nhập số lượng sản phẩm nhập vào kho", "Thông báo hệ thống", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                bool flag = true;
                if (dgv_DanhSachSanPhamPX.Rows.Count > 0)
                {
                    for (int i = 0; i < dgv_DanhSachSanPhamPX.Rows.Count; i++)
                    {
                        if (tb_MaSanPham.Text.Equals(dgv_DanhSachSanPhamPX.Rows[i].Cells[0].Value))
                        {
                            MessageBox.Show("Đã tồn tại mã sản phẩm!", "Thông báo hệ thống!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            flag = false;
                            break;
                        }
                    }
                    if (flag)
                    {
                        dgv_DanhSachSanPhamPX.Rows.Add(tb_MaSanPham.Text, tb_TenSanPham.Text, nud_SoLuongXuat.Value);
                    }
                }
                else
                {
                    dgv_DanhSachSanPhamPX.Rows.Add(tb_MaSanPham.Text, tb_TenSanPham.Text, nud_SoLuongXuat.Value);
                }
            }
        }

        private void dgv_SanPham_CellClick(object sender, DataGridViewCellEventArgs e)
        {            
            tb_MaSanPham.Text = dgv_SanPham.SelectedCells[0].Value.ToString().Trim();
            tb_TenSanPham.Text = dgv_SanPham.SelectedCells[1].Value.ToString().Trim();
            nud_SoLuongXuat.Value = 0;
            nud_SoLuongXuat.Maximum = GetMaxProductQuantity();
        }

        private void lưuToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgv_DanhSachSanPhamPX.Rows.Count == 0)
                {
                    MessageBox.Show("Chưa thêm hàng hóa xuất kho!", "Thông báo hệ thống", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    if (MessageBox.Show("Khi lưu phiếu xuất sẽ không thể chỉnh sửa. Bạn có muốn lưu phiếu xuất không?", "Thông báo hệ thống", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        SaveProductToXML();
                        SaveProductToDB();
                        UpdateInventoryNumber();
                        MessageBox.Show("Lưu thành công!", "Thông báo hệ thống", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        saveFlag = true;
                        xemToolStripMenuItem.Checked = true;
                        DisabledAll();
                        ResetMenuBackColor();
                        xemToolStripMenuItem.BackColor = Color.LightGray;
                        dgv_XuatKho.Enabled = true;
                        GetExportVoucher();
                        tb_MaPhieuXuat.Text = "";
                        tb_NgayXuatHang.Text = "";
                        nud_SoLuongXuat.Value = 0;
                        tb_MaSanPham.Text = "";
                        tb_TenSanPham.Text = "";
                        dgv_DanhSachSanPhamPX.Columns.Clear();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.StackTrace);
            }
        }

        private void xemToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (saveFlag || dgv_DanhSachSanPhamPX.Rows.Count == 0 || MessageBox.Show("Phiếu xuất chưa được lưu, bạn có muốn hủy dữ liệu đã nhập không?", "Thông báo hệ thống", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                DisabledAll();
                ResetMenuBackColor();
                xemToolStripMenuItem.BackColor = Color.LightGray;
                dgv_XuatKho.Enabled = true;
                GetExportVoucher();
                dgv_DanhSachSanPhamPX.Columns.Clear();
                saveFlag = true;
                tb_MaPhieuXuat.Text = "";
                tb_NgayXuatHang.Text = "";
                nud_SoLuongXuat.Value = 0;
                tb_MaSanPham.Text = "";
                tb_TenSanPham.Text = "";
            }
        }

        private void tìmToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (saveFlag || dgv_DanhSachSanPhamPX.Rows.Count == 0 || MessageBox.Show("Phiếu xuất chưa được lưu, bạn có muốn hủy dữ liệu đã nhập không?", "Thông báo hệ thống", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                DisabledAll();
                ResetMenuBackColor();
                tìmToolStripMenuItem.BackColor = Color.LightGray;
                dgv_XuatKho.Enabled = true;
                dtp_From.Enabled = true;
                dtp_From.Format = DateTimePickerFormat.Short;
                dtp_To.Enabled = true;
                dtp_To.Format = DateTimePickerFormat.Short;
                dgv_DanhSachSanPhamPX.Columns.Clear();
                saveFlag = true;
                tb_MaPhieuXuat.Text = "";
                tb_NgayXuatHang.Text = "";
                nud_SoLuongXuat.Value = 0;
                tb_MaSanPham.Text = "";
                tb_TenSanPham.Text = "";
            }
        }

        private void saoLưuToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (saveFlag || dgv_DanhSachSanPhamPX.Rows.Count == 0 || MessageBox.Show("Phiếu xuất chưa được lưu, bạn có muốn hủy dữ liệu đã nhập không?", "Thông báo hệ thống", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                {
                    DisabledAll();
                    GetExportVoucher();
                    dgv_DanhSachSanPhamPX.Columns.Clear();
                    if (MessageBox.Show("Bạn có muốn sao lưu dữ liệu phiếu xuất không?", "Thông báo hệ thống", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        PhieuXuatSqlToXML();
                        CtPhieuXuatSqlToXML();
                        MessageBox.Show("Sao lưu thành công!", "Thông báo hệ thống", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void tảiDữLiệuToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (saveFlag || dgv_DanhSachSanPhamPX.Rows.Count == 0 || MessageBox.Show("Phiếu xuất chưa được lưu, bạn có muốn hủy dữ liệu đã nhập không?", "Thông báo hệ thống", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                {
                    DisabledAll();
                    GetExportVoucher();
                    dgv_DanhSachSanPhamPX.Columns.Clear();
                    if (MessageBox.Show("Bạn có muốn lưu dữ liệu phiếu xuất vào database không?", "Thông báo hệ thống", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        DeleteAllExportVoucher();
                        SaveProductToDB();
                        MessageBox.Show("Lưu dữ liệu thành công!", "Thông báo hệ thống", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void dtp_From_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                FindByDay();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void dtp_To_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                FindByDay();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void btn_ToiDa_Click(object sender, EventArgs e)
        {
            if (tb_MaSanPham.Text.Equals(""))
            {
                MessageBox.Show("Vui lòng chọn sản phẩm", "Thông báo hệ thống", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                nud_SoLuongXuat.Value = GetMaxProductQuantity();
            }
        }

        private void dgv_XuatKho_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            dgv_DanhSachSanPhamPX.Columns.Clear();
            string maPX = dgv_XuatKho.SelectedCells[0].Value.ToString().Trim();
            sqlStatement = "select ChiTietPhieuXuat.maSP as N'Mã sản phẩm', tenSP as N'Tên sản phẩm', soLuongXuat as N'Số lượng xuất'\r\n" +
                "from ChiTietPhieuXuat, SanPham\r\n" +
                "where ChiTietPhieuXuat.maSP = SanPham.maSP and maPX = '" + maPX + "'";
            dataAdapter = new SqlDataAdapter(sqlStatement, connection);
            DataTable dataTable = new DataTable();
            dataAdapter.Fill(dataTable);
            dgv_DanhSachSanPhamPX.DataSource = dataTable;
            FormatGrid(dgv_DanhSachSanPhamPX);
            tb_MaPhieuXuat.Text = maPX;
            tb_NgayXuatHang.Text = dgv_XuatKho.SelectedCells[1].Value.ToString().Trim();
        }
    }
}
