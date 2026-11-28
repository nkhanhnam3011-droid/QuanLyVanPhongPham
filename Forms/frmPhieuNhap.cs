using QuanLyVanPhongPham.Class;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace QuanLyVanPhongPham.Forms
{
    public partial class frmPhieuNhap : Form
    {
        private DataTable tblDanhSach;
        private DataTable tblChiTiet;
        private DataTable tblHangHoa;

        private string maPhieuDangChon = "";
        private string trangThaiHienTai = "";
        private bool laPhieuMoi = false;
        private bool daThayDoi = false;
        private bool dangNap = false;

        public frmPhieuNhap()
        {
            InitializeComponent();
        }

        private void frmPhieuNhap_Load(object sender, EventArgs e)
        {
            try
            {
                FunctionsDTung.KhoiTaoPhienMacDinh();

                if (!FunctionsDTung.YeuCauQuyen("mở Quản lý Phiếu nhập kho", "ADMIN", "KHO", "KETOAN"))
                {
                    this.Close();
                    return;
                }

                dangNap = true;
                cboLocTrangThai.Items.Clear();
                cboLocTrangThai.Items.Add("-- Tất cả --");
                cboLocTrangThai.Items.Add("Nháp");
                cboLocTrangThai.Items.Add("Đã xác nhận");
                cboLocTrangThai.Items.Add("Đã hủy");
                cboLocTrangThai.SelectedIndex = 0;

                dtpLocTuNgay.Value = DateTime.Today.AddDays(-30);
                dtpLocDenNgay.Value = DateTime.Today;
                chkLocNgay.Checked = false;
                dtpLocTuNgay.Enabled = false;
                dtpLocDenNgay.Enabled = false;

                tblChiTiet = TaoBangChiTiet();
                dgvChiTietNhap.DataSource = tblChiTiet;
                DinhDangLuoiChiTiet();

                Load_ComboHangHoa();
                Load_ComboNhaCungCap("");
                dangNap = false;

                Load_DanhSachPhieuNhap();
            }
            catch (Exception ex)
            {
                dangNap = false;
                MessageBox.Show(
                    "Lỗi khi khởi tạo màn hình Phiếu nhập kho:\n" + ex.Message,
                    "Lỗi",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        // 1. Nạp dữ liệu và định dạng lưới

        private DataTable TaoBangChiTiet()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("MaHang", typeof(string));
            dt.Columns.Add("TenHang", typeof(string));
            dt.Columns.Add("TenDVT", typeof(string));
            dt.Columns.Add("SoLuong", typeof(int));
            dt.Columns.Add("DonGiaNhap", typeof(decimal));
            dt.Columns.Add("ThanhTien", typeof(decimal));
            return dt;
        }

        private void DinhDangLuoiDanhSach()
        {
            if (dgvDanhSachPhieuNhap.Columns.Count == 0)
                return;

            dgvDanhSachPhieuNhap.Columns["MaPhieuNhap"].HeaderText = "Mã phiếu";
            dgvDanhSachPhieuNhap.Columns["MaPhieuNhap"].FillWeight = 16;

            dgvDanhSachPhieuNhap.Columns["NgayLap"].HeaderText = "Ngày lập";
            dgvDanhSachPhieuNhap.Columns["NgayLap"].FillWeight = 18;
            dgvDanhSachPhieuNhap.Columns["NgayLap"].DefaultCellStyle.Format = "dd/MM/yyyy";
            dgvDanhSachPhieuNhap.Columns["NgayLap"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            dgvDanhSachPhieuNhap.Columns["TenNCC"].HeaderText = "Nhà cung cấp";
            dgvDanhSachPhieuNhap.Columns["TenNCC"].FillWeight = 32;

            dgvDanhSachPhieuNhap.Columns["TongTien"].HeaderText = "Tổng tiền";
            dgvDanhSachPhieuNhap.Columns["TongTien"].FillWeight = 17;
            dgvDanhSachPhieuNhap.Columns["TongTien"].DefaultCellStyle.Format = "#,##0 đ";
            dgvDanhSachPhieuNhap.Columns["TongTien"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            dgvDanhSachPhieuNhap.Columns["TrangThai"].HeaderText = "Trạng thái";
            dgvDanhSachPhieuNhap.Columns["TrangThai"].FillWeight = 17;
        }

        private void DinhDangLuoiChiTiet()
        {
            if (dgvChiTietNhap.Columns.Count == 0)
                return;

            dgvChiTietNhap.Columns["MaHang"].HeaderText = "Mã hàng";
            dgvChiTietNhap.Columns["MaHang"].FillWeight = 13;

            dgvChiTietNhap.Columns["TenHang"].HeaderText = "Tên hàng";
            dgvChiTietNhap.Columns["TenHang"].FillWeight = 33;

            dgvChiTietNhap.Columns["TenDVT"].HeaderText = "ĐVT";
            dgvChiTietNhap.Columns["TenDVT"].FillWeight = 10;
            dgvChiTietNhap.Columns["TenDVT"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            dgvChiTietNhap.Columns["SoLuong"].HeaderText = "Số lượng";
            dgvChiTietNhap.Columns["SoLuong"].FillWeight = 12;
            dgvChiTietNhap.Columns["SoLuong"].DefaultCellStyle.Format = "#,##0";
            dgvChiTietNhap.Columns["SoLuong"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            dgvChiTietNhap.Columns["DonGiaNhap"].HeaderText = "Đơn giá nhập";
            dgvChiTietNhap.Columns["DonGiaNhap"].FillWeight = 16;
            dgvChiTietNhap.Columns["DonGiaNhap"].DefaultCellStyle.Format = "#,##0 đ";
            dgvChiTietNhap.Columns["DonGiaNhap"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            dgvChiTietNhap.Columns["ThanhTien"].HeaderText = "Thành tiền";
            dgvChiTietNhap.Columns["ThanhTien"].FillWeight = 16;
            dgvChiTietNhap.Columns["ThanhTien"].DefaultCellStyle.Format = "#,##0 đ";
            dgvChiTietNhap.Columns["ThanhTien"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
        }

        private void Load_ComboNhaCungCap(string maNCCDangChon)
        {
            string sql =
                "SELECT MaNCC, (MaNCC + ' - ' + TenNCC) AS HienThi " +
                "FROM NhaCungCap " +
                "WHERE TrangThai = 1 OR MaNCC = @MaNCC " +
                "ORDER BY MaNCC";

            bool cu = dangNap;
            dangNap = true;
            Functions.FillCombo(sql, cboNhaCungCap, "MaNCC", "HienThi", new SqlParameter("@MaNCC", maNCCDangChon ?? ""));
            if (!string.IsNullOrEmpty(maNCCDangChon))
                cboNhaCungCap.SelectedValue = maNCCDangChon;
            else
                cboNhaCungCap.SelectedIndex = -1;
            dangNap = cu;
        }

        private void Load_ComboHangHoa()
        {
            string sql =
                "SELECT h.MaHang, (h.MaHang + ' - ' + h.TenHang) AS HienThi, h.TenHang, d.TenDVT " +
                "FROM HangHoa h " +
                "INNER JOIN DonViTinh d ON h.MaDVT = d.MaDVT " +
                "WHERE h.TrangThai = 1 " +
                "ORDER BY h.MaHang";

            tblHangHoa = Functions.GetDataToTable(sql);

            bool cu = dangNap;
            dangNap = true;
            cboHangHoa.ValueMember = "MaHang";
            cboHangHoa.DisplayMember = "HienThi";
            cboHangHoa.DataSource = tblHangHoa;
            cboHangHoa.SelectedIndex = -1;
            dangNap = cu;
        }

        private void Load_DanhSachPhieuNhap()
        {
            List<SqlParameter> dsThamSo = new List<SqlParameter>();
            string sql =
                "SELECT pn.MaPhieuNhap, pn.NgayLap, ncc.TenNCC, pn.TongTien, pn.TrangThai " +
                "FROM PhieuNhap pn " +
                "INNER JOIN NhaCungCap ncc ON pn.MaNCC = ncc.MaNCC " +
                "WHERE 1 = 1 ";

            string tuKhoa = txtTuKhoa.Text.Trim();
            if (!string.IsNullOrEmpty(tuKhoa))
            {
                sql += "AND (pn.MaPhieuNhap LIKE @TuKhoa OR pn.MaNCC LIKE @TuKhoa OR ncc.TenNCC LIKE @TuKhoa) ";
                dsThamSo.Add(new SqlParameter("@TuKhoa", "%" + tuKhoa + "%"));
            }

            if (cboLocTrangThai.SelectedIndex > 0)
            {
                sql += "AND pn.TrangThai = @TrangThai ";
                dsThamSo.Add(new SqlParameter("@TrangThai", cboLocTrangThai.Text));
            }

            if (chkLocNgay.Checked)
            {
                sql += "AND pn.NgayLap >= @TuNgay AND pn.NgayLap < DATEADD(DAY, 1, @DenNgay) ";
                dsThamSo.Add(new SqlParameter("@TuNgay", dtpLocTuNgay.Value.Date));
                dsThamSo.Add(new SqlParameter("@DenNgay", dtpLocDenNgay.Value.Date));
            }

            sql += "ORDER BY pn.NgayLap DESC, pn.MaPhieuNhap DESC";

            tblDanhSach = Functions.GetDataToTable(sql, dsThamSo.ToArray());
            dgvDanhSachPhieuNhap.DataSource = tblDanhSach;
            DinhDangLuoiDanhSach();

            if (laPhieuMoi)
            {
                CapNhatNut();
                return;
            }

            if (tblDanhSach.Rows.Count == 0)
            {
                ResetValues();
                return;
            }

            int dongChon = 0;
            if (!string.IsNullOrEmpty(maPhieuDangChon))
            {
                for (int i = 0; i < tblDanhSach.Rows.Count; i++)
                {
                    if (Convert.ToString(tblDanhSach.Rows[i]["MaPhieuNhap"]) == maPhieuDangChon)
                    {
                        dongChon = i;
                        break;
                    }
                }
            }

            dgvDanhSachPhieuNhap.ClearSelection();
            dgvDanhSachPhieuNhap.Rows[dongChon].Selected = true;
            dgvDanhSachPhieuNhap.CurrentCell = dgvDanhSachPhieuNhap.Rows[dongChon].Cells[0];

            HienThiPhieuNhap(Convert.ToString(tblDanhSach.Rows[dongChon]["MaPhieuNhap"]));
        }

        private void HienThiPhieuNhap(string maPN)
        {
            string sqlMaster =
                "SELECT pn.MaPhieuNhap, pn.NgayLap, pn.MaNCC, pn.TrangThai, pn.LyDoHuy, " +
                "       (tkLap.TenDangNhap + ' - ' + tkLap.HoTen) AS TenNguoiLap, " +
                "       ISNULL(tkXN.TenDangNhap + ' - ' + tkXN.HoTen, N'') AS TenNguoiXacNhan " +
                "FROM PhieuNhap pn " +
                "INNER JOIN TaiKhoan tkLap ON pn.NguoiLap = tkLap.MaTaiKhoan " +
                "LEFT JOIN TaiKhoan tkXN ON pn.NguoiXacNhan = tkXN.MaTaiKhoan " +
                "WHERE pn.MaPhieuNhap = @MaPN";

            DataTable dtMaster = Functions.GetDataToTable(sqlMaster, new SqlParameter("@MaPN", maPN));
            if (dtMaster.Rows.Count == 0)
            {
                ResetValues();
                return;
            }

            DataRow r = dtMaster.Rows[0];
            string maNCC = Convert.ToString(r["MaNCC"]);

            dangNap = true;
            maPhieuDangChon = Convert.ToString(r["MaPhieuNhap"]);
            trangThaiHienTai = Convert.ToString(r["TrangThai"]);
            laPhieuMoi = false;
            daThayDoi = false;

            txtMaPN.Text = maPhieuDangChon;
            dtpNgayLap.Value = Convert.ToDateTime(r["NgayLap"]);
            Load_ComboNhaCungCap(maNCC);
            txtTrangThai.Text = trangThaiHienTai;
            txtNguoiLap.Text = Convert.ToString(r["TenNguoiLap"]);
            txtNguoiXacNhan.Text = r["TenNguoiXacNhan"] == DBNull.Value ? "" : Convert.ToString(r["TenNguoiXacNhan"]);
            txtLyDoHuy.Text = r["LyDoHuy"] == DBNull.Value ? "" : Convert.ToString(r["LyDoHuy"]);

            string sqlDetail =
                "SELECT ct.MaHang, h.TenHang, d.TenDVT, ct.SoLuong, ct.DonGiaNhap, ct.ThanhTien " +
                "FROM ChiTietNhap ct " +
                "INNER JOIN HangHoa h ON ct.MaHang = h.MaHang " +
                "INNER JOIN DonViTinh d ON h.MaDVT = d.MaDVT " +
                "WHERE ct.MaPhieuNhap = @MaPN " +
                "ORDER BY ct.MaHang";

            DataTable dtDetail = Functions.GetDataToTable(sqlDetail, new SqlParameter("@MaPN", maPN));
            tblChiTiet.Rows.Clear();
            foreach (DataRow dr in dtDetail.Rows)
            {
                tblChiTiet.Rows.Add(
                    Convert.ToString(dr["MaHang"]),
                    Convert.ToString(dr["TenHang"]),
                    Convert.ToString(dr["TenDVT"]),
                    Convert.ToInt32(dr["SoLuong"]),
                    Convert.ToDecimal(dr["DonGiaNhap"]),
                    Convert.ToDecimal(dr["ThanhTien"]));
            }

            ResetValuesHang();
            dangNap = false;

            TinhTongTien();
            CapNhatNut();
        }

        // 2. Hàm phụ trợ trạng thái và tính tiền

        private bool DuocSuaPhieu()
        {
            return FunctionsDTung.CoQuyen("ADMIN", "KHO")
                   && trangThaiHienTai == "Nháp"
                   && !string.IsNullOrEmpty(maPhieuDangChon);
        }

        private void ResetValues()
        {
            dangNap = true;
            maPhieuDangChon = "";
            trangThaiHienTai = "";
            laPhieuMoi = false;
            daThayDoi = false;

            txtMaPN.Text = "";
            dtpNgayLap.Value = DateTime.Now;
            cboNhaCungCap.SelectedIndex = -1;
            txtTrangThai.Text = "";
            txtNguoiLap.Text = "";
            txtNguoiXacNhan.Text = "";
            txtLyDoHuy.Text = "";

            tblChiTiet.Rows.Clear();
            ResetValuesHang();
            dangNap = false;

            TinhTongTien();
            CapNhatNut();
        }

        private void ResetValuesHang()
        {
            bool cu = dangNap;
            dangNap = true;
            cboHangHoa.SelectedIndex = -1;
            txtSoLuong.Text = "";
            txtDonGiaNhap.Text = "";
            dangNap = cu;
        }

        private void TinhTongTien()
        {
            decimal tong = 0;
            foreach (DataRow row in tblChiTiet.Rows)
            {
                tong += Convert.ToDecimal(row["ThanhTien"]);
            }
            txtTongTien.Text = FunctionsDTung.DinhDangTien(tong);
        }

        private void CapNhatNut()
        {
            bool coQuyenSua = FunctionsDTung.CoQuyen("ADMIN", "KHO");
            bool duocSua = DuocSuaPhieu();
            bool duocChot = coQuyenSua && !laPhieuMoi && !daThayDoi && trangThaiHienTai == "Nháp" && !string.IsNullOrEmpty(maPhieuDangChon);

            cboNhaCungCap.Enabled = duocSua;
            cboHangHoa.Enabled = duocSua;
            txtSoLuong.ReadOnly = !duocSua;
            txtDonGiaNhap.ReadOnly = !duocSua;
            btnThemDong.Enabled = duocSua;
            btnXoaDong.Enabled = duocSua && dgvChiTietNhap.CurrentRow != null && tblChiTiet.Rows.Count > 0;

            btnLapMoi.Enabled = coQuyenSua && !daThayDoi;
            btnLuu.Enabled = duocSua && daThayDoi;
            btnBoQua.Enabled = coQuyenSua && daThayDoi;
            btnXacNhan.Enabled = duocChot && tblChiTiet.Rows.Count > 0;
            btnHuy.Enabled = duocChot;
        }

        private DataRow TimHangHoa(string maHang)
        {
            if (tblHangHoa == null || string.IsNullOrEmpty(maHang))
                return null;

            foreach (DataRow row in tblHangHoa.Rows)
            {
                if (Convert.ToString(row["MaHang"]) == maHang)
                    return row;
            }
            return null;
        }

        private DataRow TimDongChiTiet(string maHang)
        {
            foreach (DataRow row in tblChiTiet.Rows)
            {
                if (Convert.ToString(row["MaHang"]) == maHang)
                    return row;
            }
            return null;
        }

        // 3. Lọc và chọn trên danh sách

        private void chkLocNgay_CheckedChanged(object sender, EventArgs e)
        {
            dtpLocTuNgay.Enabled = chkLocNgay.Checked;
            dtpLocDenNgay.Enabled = chkLocNgay.Checked;
        }

        private void btnLoc_Click(object sender, EventArgs e)
        {
            if (chkLocNgay.Checked && dtpLocTuNgay.Value.Date > dtpLocDenNgay.Value.Date)
            {
                MessageBox.Show("Từ ngày phải nhỏ hơn hoặc bằng Đến ngày!", "Sai điều kiện lọc", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                Load_DanhSachPhieuNhap();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi lọc danh sách phiếu nhập:\n" + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            txtTuKhoa.Text = "";
            cboLocTrangThai.SelectedIndex = 0;
            chkLocNgay.Checked = false;
            dtpLocTuNgay.Value = DateTime.Today.AddDays(-30);
            dtpLocDenNgay.Value = DateTime.Today;

            try
            {
                Load_DanhSachPhieuNhap();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi làm mới danh sách:\n" + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgvDanhSachPhieuNhap_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
                return;

            string maDuocChon = Convert.ToString(dgvDanhSachPhieuNhap.Rows[e.RowIndex].Cells["MaPhieuNhap"].Value);
            if (string.IsNullOrEmpty(maDuocChon) || maDuocChon == maPhieuDangChon)
                return;

            if (daThayDoi)
            {
                DialogResult hoi = MessageBox.Show(
                    "Phiếu nhập đang mở có thay đổi chưa lưu. Bạn có muốn bỏ qua để xem phiếu khác?",
                    "Xác nhận",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning);
                if (hoi != DialogResult.Yes)
                    return;
            }

            try
            {
                HienThiPhieuNhap(maDuocChon);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi đọc chi tiết phiếu nhập:\n" + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // 4. Nhập thông tin và chi tiết mặt hàng

        private void cboNhaCungCap_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (dangNap || !DuocSuaPhieu())
                return;

            daThayDoi = true;
            CapNhatNut();
        }

        private void txtSoLuong_KeyPress(object sender, KeyPressEventArgs e)
        {
            FunctionsDTung.ChiChoNhapSoNguyen(sender, e);
        }

        private void txtDonGiaNhap_KeyPress(object sender, KeyPressEventArgs e)
        {
            FunctionsDTung.ChiChoNhapSoNguyen(sender, e);
        }

        private void dgvChiTietNhap_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            CapNhatNut();
        }

        private void dgvChiTietNhap_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && DuocSuaPhieu())
            {
                btnXoaDong_Click(sender, EventArgs.Empty);
            }
        }

        private void btnThemDong_Click(object sender, EventArgs e)
        {
            if (!FunctionsDTung.YeuCauQuyen("thêm mặt hàng vào phiếu nhập", "ADMIN", "KHO"))
                return;

            if (!DuocSuaPhieu())
            {
                MessageBox.Show("Phiếu nhập hiện không ở trạng thái Nháp cho phép chỉnh sửa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (cboHangHoa.SelectedIndex == -1 || cboHangHoa.SelectedValue == null)
            {
                MessageBox.Show("Vui lòng chọn mặt hàng!", "Thiếu dữ liệu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cboHangHoa.Focus();
                return;
            }

            int soLuong;
            if (!int.TryParse(txtSoLuong.Text.Trim(), out soLuong) || soLuong <= 0)
            {
                MessageBox.Show("Số lượng nhập phải lớn hơn 0 (QT04)!", "Sai dữ liệu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtSoLuong.Focus();
                return;
            }

            decimal donGiaNhap;
            if (!decimal.TryParse(txtDonGiaNhap.Text.Trim(), out donGiaNhap) || donGiaNhap <= 0)
            {
                MessageBox.Show("Đơn giá nhập phải lớn hơn 0 (QT04)!", "Sai dữ liệu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtDonGiaNhap.Focus();
                return;
            }

            string maHang = Convert.ToString(cboHangHoa.SelectedValue);
            DataRow hang = TimHangHoa(maHang);
            if (hang == null)
                return;

            DataRow dongCu = TimDongChiTiet(maHang);
            if (dongCu != null)
            {
                decimal giaCu = Convert.ToDecimal(dongCu["DonGiaNhap"]);
                decimal giaChon = giaCu;

                // QT03: Trùng mặt hàng nhưng khác đơn giá nhập thì hỏi cập nhật giá mới hay giữ giá cũ
                if (giaCu != donGiaNhap)
                {
                    DialogResult hoi = MessageBox.Show(
                        "Mặt hàng \"" + Convert.ToString(hang["TenHang"]) + "\" đã có trong phiếu với đơn giá cũ " +
                        FunctionsDTung.DinhDangTien(giaCu) + ".\n" +
                        "Chọn 'Yes' để cập nhật toàn bộ dòng theo giá mới " +
                        FunctionsDTung.DinhDangTien(donGiaNhap) + ", hoặc 'No' để giữ giá cũ.",
                        "Trùng mặt hàng khác đơn giá nhập",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Question);

                    if (hoi == DialogResult.Yes)
                        giaChon = donGiaNhap;
                }

                int soLuongMoi = Convert.ToInt32(dongCu["SoLuong"]) + soLuong;
                dongCu["SoLuong"] = soLuongMoi;
                dongCu["DonGiaNhap"] = giaChon;
                dongCu["ThanhTien"] = soLuongMoi * giaChon;
            }
            else
            {
                tblChiTiet.Rows.Add(
                    maHang,
                    Convert.ToString(hang["TenHang"]),
                    Convert.ToString(hang["TenDVT"]),
                    soLuong,
                    donGiaNhap,
                    soLuong * donGiaNhap);
            }

            daThayDoi = true;
            ResetValuesHang();
            TinhTongTien();
            CapNhatNut();
        }

        private void btnXoaDong_Click(object sender, EventArgs e)
        {
            if (!FunctionsDTung.YeuCauQuyen("xóa dòng chi tiết phiếu nhập", "ADMIN", "KHO"))
                return;

            if (!DuocSuaPhieu())
                return;

            if (dgvChiTietNhap.CurrentRow == null || tblChiTiet.Rows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn dòng mặt hàng cần xóa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            string maHang = Convert.ToString(dgvChiTietNhap.CurrentRow.Cells["MaHang"].Value);
            string tenHang = Convert.ToString(dgvChiTietNhap.CurrentRow.Cells["TenHang"].Value);

            DialogResult hoi = MessageBox.Show(
                "Bạn có chắc chắn muốn xóa mặt hàng \"" + maHang + " - " + tenHang + "\" khỏi phiếu nhập?",
                "Xác nhận xóa dòng",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);
            if (hoi != DialogResult.Yes)
                return;

            DataRow dongCanXoa = TimDongChiTiet(maHang);
            if (dongCanXoa != null)
            {
                tblChiTiet.Rows.Remove(dongCanXoa);
                daThayDoi = true;
                TinhTongTien();
                CapNhatNut();
            }
        }

        // 5. Các nút nghiệp vụ (Lập mới, Lưu, Bỏ qua, Xác nhận, Hủy, Đóng)

        private void btnLapMoi_Click(object sender, EventArgs e)
        {
            if (!FunctionsDTung.YeuCauQuyen("lập mới phiếu nhập kho", "ADMIN", "KHO"))
                return;

            if (daThayDoi)
            {
                MessageBox.Show("Vui lòng Lưu hoặc Bỏ qua phiếu đang sửa trước khi lập phiếu mới!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                string maMoi = FunctionsDTung.TaoMaChungTu("PhieuNhap", "MaPhieuNhap", "PN");

                dangNap = true;
                maPhieuDangChon = maMoi;
                trangThaiHienTai = "Nháp";
                laPhieuMoi = true;
                daThayDoi = true;

                txtMaPN.Text = maMoi;
                dtpNgayLap.Value = DateTime.Now;
                Load_ComboNhaCungCap("");
                txtTrangThai.Text = "Nháp";
                txtNguoiLap.Text = FunctionsDTung.TenDangNhapHienTai + " - " + FunctionsDTung.HoTenHienTai;
                txtNguoiXacNhan.Text = "";
                txtLyDoHuy.Text = "";

                tblChiTiet.Rows.Clear();
                ResetValuesHang();
                dangNap = false;

                TinhTongTien();
                CapNhatNut();
                cboNhaCungCap.Focus();
            }
            catch (Exception ex)
            {
                dangNap = false;
                MessageBox.Show("Lỗi khi khởi tạo mã phiếu nhập mới:\n" + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (!FunctionsDTung.YeuCauQuyen("lưu phiếu nhập kho", "ADMIN", "KHO"))
                return;

            if (!DuocSuaPhieu())
            {
                MessageBox.Show("Chỉ được lưu phiếu nhập đang ở trạng thái Nháp!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (cboNhaCungCap.SelectedIndex == -1 || cboNhaCungCap.SelectedValue == null)
            {
                MessageBox.Show("Vui lòng chọn Nhà cung cấp!", "Thiếu dữ liệu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cboNhaCungCap.Focus();
                return;
            }

            if (tblChiTiet.Rows.Count == 0)
            {
                MessageBox.Show("Phiếu nhập phải có ít nhất một dòng mặt hàng (QT04)!", "Thiếu chi tiết", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cboHangHoa.Focus();
                return;
            }

            try
            {
                string maPN = maPhieuDangChon;
                string maNCC = Convert.ToString(cboNhaCungCap.SelectedValue);
                DateTime ngayLap = dtpNgayLap.Value;

                FunctionsDTung.LuuPhieuNhap(maPN, ngayLap, maNCC, FunctionsDTung.MaTaiKhoanHienTai, tblChiTiet, laPhieuMoi);

                laPhieuMoi = false;
                daThayDoi = false;

                Load_DanhSachPhieuNhap();

                MessageBox.Show(
                    "Đã lưu phiếu nhập " + maPN + " ở trạng thái Nháp thành công!\nTồn kho chưa thay đổi cho đến khi bấm Xác nhận nhập kho.",
                    "Lưu thành công",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi lưu phiếu nhập:\n" + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnBoQua_Click(object sender, EventArgs e)
        {
            if (!daThayDoi)
                return;

            DialogResult hoi = MessageBox.Show("Hủy bỏ các thay đổi chưa lưu của phiếu nhập này?", "Xác nhận bỏ qua", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (hoi != DialogResult.Yes)
                return;

            try
            {
                if (laPhieuMoi)
                {
                    laPhieuMoi = false;
                    daThayDoi = false;
                    maPhieuDangChon = "";
                    Load_DanhSachPhieuNhap();
                }
                else
                {
                    HienThiPhieuNhap(maPhieuDangChon);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi khôi phục dữ liệu phiếu nhập:\n" + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnXacNhan_Click(object sender, EventArgs e)
        {
            if (!FunctionsDTung.YeuCauQuyen("xác nhận nhập kho", "ADMIN", "KHO"))
                return;

            if (laPhieuMoi || daThayDoi)
            {
                MessageBox.Show("Vui lòng bấm \"Lưu\" phiếu nhập trước khi xác nhận nhập kho!", "Chưa lưu phiếu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (trangThaiHienTai != "Nháp")
            {
                MessageBox.Show("Chỉ phiếu nhập đang ở trạng thái Nháp mới được xác nhận (QT11)!", "Sai trạng thái", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (tblChiTiet.Rows.Count == 0)
            {
                MessageBox.Show("Phiếu nhập không có dòng mặt hàng nào (QT04)!", "Thiếu dữ liệu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult hoi = MessageBox.Show(
                "Xác nhận nhập kho phiếu " + maPhieuDangChon + "?\nHệ thống sẽ cộng số lượng nhập vào tồn kho và khóa chỉnh sửa phiếu.",
                "Xác nhận nhập kho",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);
            if (hoi != DialogResult.Yes)
                return;

            try
            {
                string maPN = maPhieuDangChon;
                FunctionsDTung.XacNhanPhieuNhap(maPN, FunctionsDTung.MaTaiKhoanHienTai);

                Load_DanhSachPhieuNhap();

                MessageBox.Show(
                    "Đã xác nhận phiếu nhập " + maPN + " và cộng tồn kho thành công!",
                    "Thành công",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Không thể xác nhận phiếu nhập:\n" + ex.Message, "Lỗi giao dịch", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            if (!FunctionsDTung.YeuCauQuyen("hủy phiếu nhập kho", "ADMIN", "KHO"))
                return;

            if (laPhieuMoi || daThayDoi)
            {
                MessageBox.Show("Vui lòng bấm \"Lưu\" phiếu nhập trước khi thực hiện hủy phiếu!", "Chưa lưu phiếu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (trangThaiHienTai != "Nháp")
            {
                MessageBox.Show("Không thể hủy phiếu nhập đã xác nhận hoặc đã hủy trước đó (QT11)!", "Không được phép hủy", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string lyDo = FunctionsDTung.NhapLyDoHuy("Lý do hủy phiếu nhập", "Nhập lý do hủy phiếu nhập " + maPhieuDangChon + ":");
            if (string.IsNullOrEmpty(lyDo))
                return;

            try
            {
                string maPN = maPhieuDangChon;
                FunctionsDTung.HuyPhieuNhap(maPN, lyDo);

                Load_DanhSachPhieuNhap();

                MessageBox.Show(
                    "Đã hủy phiếu nhập " + maPN + ". Tồn kho không thay đổi.",
                    "Đã hủy phiếu",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi hủy phiếu nhập:\n" + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnDong_Click(object sender, EventArgs e)
        {
            if (daThayDoi)
            {
                DialogResult hoi = MessageBox.Show(
                    "Phiếu nhập đang có thay đổi chưa lưu. Bạn có chắc muốn đóng?",
                    "Xác nhận đóng",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning);
                if (hoi != DialogResult.Yes)
                    return;
            }
            this.Close();
        }

        private void txtNguoiXacNhan_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
