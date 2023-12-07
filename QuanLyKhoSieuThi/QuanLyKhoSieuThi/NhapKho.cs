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
using System.IO;
using System.Xml;
using System.Globalization;
using System.Xml.Linq;

namespace QuanLyKhoSieuThi
{
    public partial class NhapKho : Form
    {
        SqlConnection connection;
        string sqlStatement;
        SqlDataAdapter dataAdapter;
        string phieuNhapPath = "../../PhieuNhap.xml";
        string ctPhieuNhapPath = "../../ChiTietPhieuNhap.xml";
        bool saveFlag = true;
        public NhapKho()
        {
            InitializeComponent();
            Connect();
            InitComponent();
        }

        private void Connect()
        {
            string server = Environment.MachineName + "\\NGOVANTUYEN";
            string username = "sa";
            string password = "13042003";
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
            if (dgv.Name.Equals("dgv_DanhSachSanPhamPN"))
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
            tb_MaPhieuNhap.Enabled = false;
            cb_NhanVienNhapHang.Enabled = false;
            tb_NgayNhapHang.Enabled = false;
            cb_NhaCungCap.Enabled = false;
            tb_MaSanPham.Enabled = false;
            tb_TenSanPham.Enabled = false;
            nud_SoLuongNhap.Enabled = false;
            btn_Them.Enabled = false;
            btn_Them.BackColor = Color.LightGray;
            dgv_SanPham.Enabled = false;
            dgv_DanhSachSanPhamPN.Enabled = false;
            dgv_NhapKho.Enabled = false;
            cb_DanhMuc.Enabled = false;
            tb_TimTheoTenSP.Enabled = false;
            lưuToolStripMenuItem.Enabled = false;
        }

        private void InitComponent()
        {
            DisabledAll();
            dgv_NhapKho.Enabled = true;
            GetImportVoucher();
            SetDanhMuc();
            SetProvider();
            SetImportEmployee();
            ResetMenuBackColor();
            xemToolStripMenuItem.Checked = true;
            xemToolStripMenuItem.BackColor = Color.LightGray;
            cb_NhanVienNhapHang.DropDownStyle = ComboBoxStyle.DropDownList;
            cb_NhaCungCap.DropDownStyle = ComboBoxStyle.DropDownList;
            nud_SoLuongNhap.Maximum = Decimal.MaxValue;
            menuStrip1.Cursor = Cursors.Hand;
            GetAllSanPham();
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

        private void GetImportVoucher()
        {
            sqlStatement = "select maPN as N'Mã phiếu nhập', format(ngayNhapHang, 'dd/MM/yyyy') as N'Ngày nhập hàng', tenNCC as N'Tên nhà cung cấp', tenNV as N'Tên nhân viên'\r\n" +
                "from PhieuNhap, NhaCungCap, NhanVien\r\n" +
                "where PhieuNhap.maNCC = NhaCungCap.maNCC and PhieuNhap.maNV = NhanVien.maNV";
            dataAdapter = new SqlDataAdapter(sqlStatement, connection);
            DataTable dataTable = new DataTable();
            dataAdapter.Fill(dataTable);
            dgv_NhapKho.DataSource = dataTable;
            FormatGrid(dgv_NhapKho);
        }

        private void PhieuNhapSqlToXML()
        {
            sqlStatement = "select maPN, format(ngayNhapHang, 'dd/MM/yyyy') as ngayNhapHang, maNV, maNCC from PhieuNhap as phieuNhap for xml auto";
            dataAdapter = new SqlDataAdapter(sqlStatement, connection);
            DataTable dataTable = new DataTable();
            dataAdapter.Fill(dataTable);
            string xml = "<?xml version='1.0' encoding='utf-8' ?><danhSach>";
            xml += dataTable.Rows[0].ItemArray[0].ToString() + "</danhSach>";
            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.LoadXml(xml);
            xmlDocument.Save(phieuNhapPath);
        }

        private void CtPhieuNhapSqlToXML()
        {
            sqlStatement = "select * from ChiTietPhieuNhap as chiTietPhieuNhap for xml auto";
            dataAdapter = new SqlDataAdapter(sqlStatement, connection);
            DataTable dataTable = new DataTable();
            dataAdapter.Fill(dataTable);
            string xml = "<?xml version='1.0' encoding='utf-8' ?><danhSach>";
            xml += dataTable.Rows[0].ItemArray[0].ToString() + "</danhSach>";
            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.LoadXml(xml);
            xmlDocument.Save(ctPhieuNhapPath);
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

        private void GetAllSanPham()
        {
            sqlStatement = "select maSP as N'Mã sản phẩm', tenSP as N'Tên sản phẩm', FORMAT(gia, 'C0', 'vi-VN') as N'Đơn giá'\r\n" +
                "from SanPham";
            dataAdapter = new SqlDataAdapter(sqlStatement, connection);
            DataTable dataTable = new DataTable();
            dataAdapter.Fill(dataTable);
            dgv_SanPham.DataSource = dataTable;
            FormatGrid(dgv_SanPham);
        }

        private void SetImportEmployee()
        {
            sqlStatement = "select maNV, tenNV from NhanVien";
            dataAdapter = new SqlDataAdapter(sqlStatement, connection);
            DataTable dataTable = new DataTable();
            dataAdapter.Fill(dataTable);
            Dictionary<string, string> nhanVienNhapHang = new Dictionary<string, string>();
            for (int i = 0; i < dataTable.Rows.Count; i++)
            {
                nhanVienNhapHang.Add(dataTable.Rows[i][0].ToString(), dataTable.Rows[i][1].ToString());
            }
            cb_NhanVienNhapHang.DataSource = new BindingSource(nhanVienNhapHang, null);
            cb_NhanVienNhapHang.DisplayMember = "Value";
            cb_NhanVienNhapHang.ValueMember = "Key";
        }

        private void SetProvider()
        {
            sqlStatement = "select maNCC, tenNCC from NhaCungCap";
            dataAdapter = new SqlDataAdapter(sqlStatement, connection);
            DataTable dataTable = new DataTable();
            dataAdapter.Fill(dataTable);
            Dictionary<string, string> nhaCungCap = new Dictionary<string, string>();
            for (int i = 0; i < dataTable.Rows.Count; i++)
            {
                nhaCungCap.Add(dataTable.Rows[i][0].ToString(), dataTable.Rows[i][1].ToString());
            }
            cb_NhaCungCap.DataSource = new BindingSource(nhaCungCap, null);
            cb_NhaCungCap.DisplayMember = "Value";
            cb_NhaCungCap.ValueMember = "Key";
        }

        private void SetImportVoucherCode()
        {
            sqlStatement = "select count(maPN) from PhieuNhap";
            dataAdapter = new SqlDataAdapter(sqlStatement, connection);
            DataTable dataTable = new DataTable();
            dataAdapter.Fill(dataTable);
            tb_MaPhieuNhap.Text = "PN";
            if (int.Parse(dataTable.Rows[0][0].ToString()) < 10)
            {
                tb_MaPhieuNhap.Text += "0" + (int.Parse(dataTable.Rows[0][0].ToString()) + 1);
            }
            else
            {
                tb_MaPhieuNhap.Text += (int.Parse(dataTable.Rows[0][0].ToString()) + 1);
            }
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

        private void SaveProductToXML()
        {
            string maPN = tb_MaPhieuNhap.Text;
            string ngayNhapHang = tb_NgayNhapHang.Text;
            string maNV = ((KeyValuePair<string, string>)cb_NhanVienNhapHang.SelectedItem).Key;
            string maNCC = ((KeyValuePair<string, string>)cb_NhaCungCap.SelectedItem).Key;
            XDocument xDocumentPN = XDocument.Load(phieuNhapPath);
            xDocumentPN.Element("danhSach").Add(
                new XElement("phieuNhap",
                    new XAttribute("maPN", maPN),
                    new XAttribute("ngayNhapHang", ngayNhapHang),
                    new XAttribute("maNV", maNV),
                    new XAttribute("maNCC", maNCC)
                )
            );
            xDocumentPN.Save(phieuNhapPath);
            XDocument xDocumentCTPN = XDocument.Load(ctPhieuNhapPath);
            for (int i = 0; i < dgv_DanhSachSanPhamPN.Rows.Count; i++)
            {
                xDocumentCTPN.Element("danhSach").Add(
                    new XElement("chiTietPhieuNhap",
                        new XAttribute("maPN", maPN),
                        new XAttribute("maSP", dgv_DanhSachSanPhamPN.Rows[i].Cells[0].Value.ToString()),
                        new XAttribute("soLuongNhap", dgv_DanhSachSanPhamPN.Rows[i].Cells[2].Value.ToString())
                    )
                );
            }
            xDocumentCTPN.Save(ctPhieuNhapPath);
        }

        private void DeleteAllImportVoucher()
        {
            connection.Open();
            sqlStatement = "delete from PhieuNhap";
            SqlCommand cmd = new SqlCommand(sqlStatement, connection);
            cmd.ExecuteNonQuery();
            connection.Close();
        }

        private void SaveProductToDB()
        {
            DeleteAllImportVoucher();
            connection.Open();
            DataTable dataTablePN = ReadXMLFile(phieuNhapPath);
            for (int i = 0; i < dataTablePN.Rows.Count; i++)
            {
                sqlStatement = "set dateformat dmy insert into PhieuNhap values (";
                for (int j = 0; j < dataTablePN.Columns.Count - 1; j++)
                {
                    sqlStatement += "'" + dataTablePN.Rows[i][j].ToString().Trim() + "', ";
                }
                sqlStatement += "'" + dataTablePN.Rows[i][dataTablePN.Columns.Count - 1].ToString().Trim() + "')";
                SqlCommand sqlCommandPN = new SqlCommand(sqlStatement, connection);
                sqlCommandPN.ExecuteNonQuery();
            }
            DataTable dataTableCTPN = ReadXMLFile(ctPhieuNhapPath);
            for (int i = 0; i < dataTableCTPN.Rows.Count; i++)
            {
                sqlStatement = "insert into ChiTietPhieuNhap values (";
                for (int j = 0; j < dataTableCTPN.Columns.Count - 1; j++)
                {
                    sqlStatement += "'" + dataTableCTPN.Rows[i][j].ToString().Trim() + "', ";
                }
                sqlStatement += "'" + dataTableCTPN.Rows[i][dataTableCTPN.Columns.Count - 1].ToString().Trim() + "')";
                SqlCommand sqlCommandCTPN = new SqlCommand(sqlStatement, connection);
                sqlCommandCTPN.ExecuteNonQuery();
            }
            connection.Close();
        }

        private void UpdateInventoryNumber()
        {
            connection.Open();
            for (int i = 0; i < dgv_DanhSachSanPhamPN.Rows.Count; i++)
            {
                sqlStatement = "update SanPham set soLuongTonKho = soLuongTonKho + " + dgv_DanhSachSanPhamPN.Rows[i].Cells[2].Value + "where maSP = '" + dgv_DanhSachSanPhamPN.Rows[i].Cells[0].Value.ToString().Trim() + "'";
                SqlCommand sqlCommand = new SqlCommand(sqlStatement, connection);
                sqlCommand.ExecuteNonQuery();
            }
            connection.Close();
        }

        private void FindByDay()
        {
            if (dtp_From.Text != "" && dtp_To.Text == "")
            {
                sqlStatement = "set dateformat dmy\r\n" +
                    "select maPN as N'Mã phiếu nhập', format(ngayNhapHang, 'dd/MM/yyyy') as N'Ngày nhập hàng',  tenNCC as N'Tên nhà cung cấp', tenNV as N'Tên nhân viên'\r\n" +
                    "from PhieuNhap, NhanVien, NhaCungCap\r\n" +
                    "where PhieuNhap.maNCC = NhaCungCap.maNCC and PhieuNhap.maNV = NhanVien.maNV and ngayNhapHang = '" + dtp_From.Value.ToShortDateString().ToString().Trim() + "'";
                dataAdapter = new SqlDataAdapter(sqlStatement, connection);
                DataTable dataTable = new DataTable();
                dataAdapter.Fill(dataTable);
                dgv_NhapKho.DataSource = dataTable;
                FormatGrid(dgv_NhapKho);
            }
            else if (dtp_From.Text == "" && dtp_To.Text != "")
            {
                sqlStatement = "set dateformat dmy\r\n" +
                   "select maPN as N'Mã phiếu nhập', format(ngayNhapHang, 'dd/MM/yyyy') as N'Ngày nhập hàng',  tenNCC as N'Tên nhà cung cấp', tenNV as N'Tên nhân viên'\r\n" +
                    "from PhieuNhap, NhanVien, NhaCungCap\r\n" +
                    "where PhieuNhap.maNCC = NhaCungCap.maNCC and PhieuNhap.maNV = NhanVien.maNV and ngayNhapHang = '" + dtp_To.Value.ToShortDateString().ToString().Trim() + "'";
                dataAdapter = new SqlDataAdapter(sqlStatement, connection);
                DataTable dataTable = new DataTable();
                dataAdapter.Fill(dataTable);
                dgv_NhapKho.DataSource = dataTable;
                FormatGrid(dgv_NhapKho);
            }
            else
            {
                sqlStatement = "set dateformat dmy\r\n" +
                    "select maPN as N'Mã phiếu nhập', format(ngayNhapHang, 'dd/MM/yyyy') as N'Ngày nhập hàng',  tenNCC as N'Tên nhà cung cấp', tenNV as N'Tên nhân viên'\r\n" +
                    "from PhieuNhap, NhanVien, NhaCungCap\r\n" +
                    "where PhieuNhap.maNCC = NhaCungCap.maNCC and PhieuNhap.maNV = NhanVien.maNV and ngayNhapHang between '" + dtp_From.Value.ToShortDateString().ToString().Trim() + "' and '" + dtp_To.Value.ToShortDateString().ToString().Trim() + "'";
                dataAdapter = new SqlDataAdapter(sqlStatement, connection);
                DataTable dataTable = new DataTable();
                dataAdapter.Fill(dataTable);
                dgv_NhapKho.DataSource = dataTable;
                FormatGrid(dgv_NhapKho);
            }
        }

        private void btn_XuatFileXML_Click(object sender, EventArgs e)
        {
            try
            {
                PhieuNhapSqlToXML();
                CtPhieuNhapSqlToXML();
                MessageBox.Show("Export XML complete!");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void đóngToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (saveFlag || dgv_DanhSachSanPhamPN.Rows.Count == 0 || MessageBox.Show("Phiếu nhập chưa được lưu, bạn có muốn hủy dữ liệu đã nhập không?", "Thông báo hệ thống", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
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
            cb_NhanVienNhapHang.Enabled = true;
            cb_NhaCungCap.Enabled = true;
            nud_SoLuongNhap.Enabled = true;
            btn_Them.Enabled = true;
            btn_Them.BackColor = Color.Teal;
            btn_Them.ForeColor = Color.White;
            ResetMenuBackColor();
            thêmToolStripMenuItem.BackColor = Color.LightGray;
            SetImportVoucherCode();
            tb_NgayNhapHang.Text = DateTime.Today.ToShortDateString();
            InitGrid(dgv_DanhSachSanPhamPN);
            saveFlag = false;
            lưuToolStripMenuItem.Enabled = true;
        }

        private void btn_Them_Click(object sender, EventArgs e)
        {
            if (tb_MaSanPham.Text.Equals(""))
            {
                MessageBox.Show("Vui lòng chọn sản phẩm", "Thông báo hệ thống", MessageBoxButtons.OK, MessageBoxIcon.Information); 
            }
            else if (nud_SoLuongNhap.Value == 0)
            {
                MessageBox.Show("Vui lòng nhập số lượng sản phẩm nhập vào kho", "Thông báo hệ thống", MessageBoxButtons.OK, MessageBoxIcon.Information);
            } else
            {
                bool flag = true;
                if (dgv_DanhSachSanPhamPN.Rows.Count > 0)
                {
                    for (int i = 0; i < dgv_DanhSachSanPhamPN.Rows.Count; i++)
                    {
                        if (tb_MaSanPham.Text.Equals(dgv_DanhSachSanPhamPN.Rows[i].Cells[0].Value))
                        {
                            MessageBox.Show("Đã tồn tại mã sản phẩm!", "Thông báo hệ thống!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            flag = false;
                            break;
                        }
                    }
                    if (flag)
                    {
                        dgv_DanhSachSanPhamPN.Rows.Add(tb_MaSanPham.Text, tb_TenSanPham.Text, nud_SoLuongNhap.Value);
                    }
                }
                else
                {
                    dgv_DanhSachSanPhamPN.Rows.Add(tb_MaSanPham.Text, tb_TenSanPham.Text, nud_SoLuongNhap.Value);
                }
            }
        }

        private void dgv_SanPham_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            tb_MaSanPham.Text = dgv_SanPham.SelectedCells[0].Value.ToString().Trim();
            tb_TenSanPham.Text = dgv_SanPham.SelectedCells[1].Value.ToString().Trim();
            nud_SoLuongNhap.Value = 0;
        }

        private void dgv_NhapKho_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            dgv_DanhSachSanPhamPN.Columns.Clear();
            string maPN = dgv_NhapKho.SelectedCells[0].Value.ToString().Trim();
            sqlStatement = "select ChiTietPhieuNhap.maSP as N'Mã sản phẩm', tenSP as N'Tên sản phẩm', soLuongNhap as N'Số lượng nhập'\r\n" +
                "from ChiTietPhieuNhap, SanPham\r\n" +
                "where ChiTietPhieuNhap.maSP = SanPham.maSP and maPN = '" + maPN + "'";
            dataAdapter = new SqlDataAdapter(sqlStatement, connection);
            DataTable dataTable = new DataTable();
            dataAdapter.Fill(dataTable);
            dgv_DanhSachSanPhamPN.DataSource = dataTable;
            FormatGrid(dgv_DanhSachSanPhamPN);
            tb_MaPhieuNhap.Text = maPN;
            tb_NgayNhapHang.Text = dgv_NhapKho.SelectedCells[1].Value.ToString().Trim();
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
            dataAdapter = new SqlDataAdapter( sqlStatement, connection);
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

        private void xemToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (saveFlag || dgv_DanhSachSanPhamPN.Rows.Count == 0 || MessageBox.Show("Phiếu nhập chưa được lưu, bạn có muốn hủy dữ liệu đã nhập không?", "Thông báo hệ thống", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                DisabledAll();
                ResetMenuBackColor();
                xemToolStripMenuItem.BackColor = Color.LightGray;
                dgv_NhapKho.Enabled = true;
                GetImportVoucher();
                dgv_DanhSachSanPhamPN.Columns.Clear();
                saveFlag = true;
                tb_MaPhieuNhap.Text = "";
                tb_NgayNhapHang.Text = "";
                nud_SoLuongNhap.Value = 0;
                tb_MaSanPham.Text = "";
                tb_TenSanPham.Text = "";
            }
        }

        private void lưuToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgv_DanhSachSanPhamPN.Rows.Count == 0)
                {                    
                    MessageBox.Show("Chưa thêm hàng hóa nhập kho!", "Thông báo hệ thống", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    if (MessageBox.Show("Khi lưu phiếu nhập sẽ không thể chỉnh sửa. Bạn có muốn lưu phiếu nhập không?", "Thông báo hệ thống", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
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
                        dgv_NhapKho.Enabled = true;
                        GetImportVoucher();
                        tb_MaPhieuNhap.Text = "";
                        tb_NgayNhapHang.Text = "";
                        nud_SoLuongNhap.Value = 0;
                        tb_MaSanPham.Text = "";
                        tb_TenSanPham.Text = "";
                        dgv_DanhSachSanPhamPN.Columns.Clear();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void saoLưuToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (saveFlag || dgv_DanhSachSanPhamPN.Rows.Count == 0 || MessageBox.Show("Phiếu nhập chưa được lưu, bạn có muốn hủy dữ liệu đã nhập không?", "Thông báo hệ thống", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                {
                    DisabledAll();
                    GetImportVoucher();
                    dgv_DanhSachSanPhamPN.Columns.Clear();
                    if (MessageBox.Show("Bạn có muốn sao lưu dữ liệu phiếu nhập không?", "Thông báo hệ thống", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        PhieuNhapSqlToXML();
                        CtPhieuNhapSqlToXML();
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
                if (saveFlag || dgv_DanhSachSanPhamPN.Rows.Count == 0 || MessageBox.Show("Phiếu nhập chưa được lưu, bạn có muốn hủy dữ liệu đã nhập không?", "Thông báo hệ thống", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                {
                    DisabledAll();
                    GetImportVoucher();
                    dgv_DanhSachSanPhamPN.Columns.Clear();
                    if (MessageBox.Show("Bạn có muốn lưu dữ liệu phiếu nhập vào database không?", "Thông báo hệ thống", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        DeleteAllImportVoucher();
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

        private void tìmToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (saveFlag || dgv_DanhSachSanPhamPN.Rows.Count == 0 || MessageBox.Show("Phiếu nhập chưa được lưu, bạn có muốn hủy dữ liệu đã nhập không?", "Thông báo hệ thống", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                DisabledAll();
                ResetMenuBackColor();
                tìmToolStripMenuItem.BackColor = Color.LightGray;
                dgv_NhapKho.Enabled = true;
                dtp_From.Enabled = true;
                dtp_From.Format = DateTimePickerFormat.Short;
                dtp_To.Enabled = true;
                dtp_To.Format = DateTimePickerFormat.Short;
                dgv_DanhSachSanPhamPN.Columns.Clear();
                saveFlag = true;
                tb_MaPhieuNhap.Text = "";
                tb_NgayNhapHang.Text = "";
                nud_SoLuongNhap.Value = 0;
                tb_MaSanPham.Text = "";
                tb_TenSanPham.Text = "";
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

        private void dgv_NhapKho_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }
    }
}
