using QuanLyVanPhongPham.Class;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Windows.Forms;

namespace QuanLyVanPhongPham.Forms
{
    public partial class frmPhieuNhap : Form
    {
        private DataTable tblDanhSach;
        private DataTable tblChiTiet;

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
                lblNguoiDung.Text = FunctionsDTung.TenDangNhapHienTai;
                if (!FunctionsDTung.YeuCauQuyen("mở Quản lý Phiếu nhập kho", "ADMIN", "KHO", "KETOAN"))
                {
                    Close();
                    return;
                }

                PhanQuyenMenu();

                cboLocTrangThai.Items.AddRange(new object[] { "-- Tất cả --", "Nháp", "Đã xác nhận", "Đã hủy" });
                cboLocTrangThai.SelectedIndex = 0;

                dtpLocTuNgay.Value = DateTime.Today.AddDays(-30);
                dtpLocDenNgay.Value = DateTime.Today;
                chkLocNgay.Checked = false;
                dtpLocTuNgay.Enabled = false;
                dtpLocDenNgay.Enabled = false;

                Load_ComboHangHoa();
                Load_ComboNhaCungCap("");
                Load_DanhSachPhieuNhap();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi mở Phiếu nhập kho:\n" + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // 1. Nạp dữ liệu lên ComboBox và DataGridView

        private void Load_ComboHangHoa()
        {
            string sql = "SELECT h.MaHang, (h.MaHang + ' - ' + h.TenHang) AS HienThi, h.TenHang, d.TenDVT " +
                         "FROM HangHoa h INNER JOIN DonViTinh d ON h.MaDVT = d.MaDVT " +
                         "WHERE h.TrangThai = 1 ORDER BY h.MaHang";
            Functions.FillCombo(sql, cboHangHoa, "MaHang", "HienThi");
        }

        private void Load_ComboNhaCungCap(string maNCCDangChon)
        {
            string sql = "SELECT MaNCC, (MaNCC + ' - ' + TenNCC) AS HienThi " +
                         "FROM NhaCungCap WHERE TrangThai = 1 OR MaNCC = @MaNCC ORDER BY MaNCC";
            dangNap = true;
            Functions.FillCombo(sql, cboNhaCungCap, "MaNCC", "HienThi", new SqlParameter("@MaNCC", maNCCDangChon ?? ""));
            if (!string.IsNullOrEmpty(maNCCDangChon))
                cboNhaCungCap.SelectedValue = maNCCDangChon;
            dangNap = false;
        }

        private void Load_DanhSachPhieuNhap()
        {
            List<SqlParameter> thamSo = new List<SqlParameter>();
            string sql = "SELECT pn.MaPhieuNhap, pn.NgayLap, ncc.TenNCC, pn.TongTien, pn.TrangThai " +
                         "FROM PhieuNhap pn INNER JOIN NhaCungCap ncc ON pn.MaNCC = ncc.MaNCC WHERE 1 = 1 ";

            string tuKhoa = txtTuKhoa.Text.Trim();
            if (!string.IsNullOrEmpty(tuKhoa))
            {
                sql += "AND (pn.MaPhieuNhap LIKE @TuKhoa OR pn.MaNCC LIKE @TuKhoa OR ncc.TenNCC LIKE @TuKhoa) ";
                thamSo.Add(new SqlParameter("@TuKhoa", "%" + tuKhoa + "%"));
            }
            if (cboLocTrangThai.SelectedIndex > 0)
            {
                sql += "AND pn.TrangThai = @TrangThai ";
                thamSo.Add(new SqlParameter("@TrangThai", cboLocTrangThai.Text));
            }
            if (chkLocNgay.Checked)
            {
                sql += "AND pn.NgayLap >= @TuNgay AND pn.NgayLap < DATEADD(DAY, 1, @DenNgay) ";
                thamSo.Add(new SqlParameter("@TuNgay", dtpLocTuNgay.Value.Date));
                thamSo.Add(new SqlParameter("@DenNgay", dtpLocDenNgay.Value.Date));
            }
            sql += "ORDER BY pn.NgayLap DESC, pn.MaPhieuNhap DESC";

            tblDanhSach = Functions.GetDataToTable(sql, thamSo.ToArray());
            dgvDanhSachPhieuNhap.DataSource = tblDanhSach;

            dgvDanhSachPhieuNhap.Columns["MaPhieuNhap"].HeaderText = "Mã phiếu";
            dgvDanhSachPhieuNhap.Columns["NgayLap"].HeaderText = "Ngày lập";
            dgvDanhSachPhieuNhap.Columns["NgayLap"].DefaultCellStyle.Format = "dd/MM/yyyy";
            dgvDanhSachPhieuNhap.Columns["TenNCC"].HeaderText = "Nhà cung cấp";
            dgvDanhSachPhieuNhap.Columns["TongTien"].HeaderText = "Tổng tiền";
            dgvDanhSachPhieuNhap.Columns["TongTien"].DefaultCellStyle.Format = "#,##0 đ";
            dgvDanhSachPhieuNhap.Columns["TongTien"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvDanhSachPhieuNhap.Columns["TrangThai"].HeaderText = "Trạng thái";

            if (tblDanhSach.Rows.Count == 0)
            {
                ResetValues();
                return;
            }

            int dongChon = 0;
            for (int i = 0; i < tblDanhSach.Rows.Count; i++)
            {
                if (Convert.ToString(tblDanhSach.Rows[i]["MaPhieuNhap"]) == maPhieuDangChon)
                {
                    dongChon = i;
                    break;
                }
            }

            dgvDanhSachPhieuNhap.ClearSelection();
            dgvDanhSachPhieuNhap.Rows[dongChon].Selected = true;
            dgvDanhSachPhieuNhap.CurrentCell = dgvDanhSachPhieuNhap.Rows[dongChon].Cells[0];
            HienThiPhieuNhap(Convert.ToString(tblDanhSach.Rows[dongChon]["MaPhieuNhap"]));
        }

        private void Load_ChiTiet(string maPN)
        {
            string sql = "SELECT ct.MaHang, h.TenHang, d.TenDVT, ct.SoLuong, ct.DonGiaNhap, ct.ThanhTien " +
                         "FROM ChiTietNhap ct INNER JOIN HangHoa h ON ct.MaHang = h.MaHang " +
                         "INNER JOIN DonViTinh d ON h.MaDVT = d.MaDVT " +
                         "WHERE ct.MaPhieuNhap = @MaPN ORDER BY ct.MaHang";

            tblChiTiet = Functions.GetDataToTable(sql, new SqlParameter("@MaPN", maPN ?? ""));
            dgvChiTietNhap.DataSource = tblChiTiet;

            dgvChiTietNhap.Columns["MaHang"].HeaderText = "Mã hàng";
            dgvChiTietNhap.Columns["TenHang"].HeaderText = "Tên hàng";
            dgvChiTietNhap.Columns["TenDVT"].HeaderText = "ĐVT";
            dgvChiTietNhap.Columns["SoLuong"].HeaderText = "Số lượng";
            dgvChiTietNhap.Columns["SoLuong"].DefaultCellStyle.Format = "#,##0";
            dgvChiTietNhap.Columns["SoLuong"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvChiTietNhap.Columns["DonGiaNhap"].HeaderText = "Đơn giá nhập";
            dgvChiTietNhap.Columns["DonGiaNhap"].DefaultCellStyle.Format = "#,##0 đ";
            dgvChiTietNhap.Columns["DonGiaNhap"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvChiTietNhap.Columns["ThanhTien"].HeaderText = "Thành tiền";
            dgvChiTietNhap.Columns["ThanhTien"].DefaultCellStyle.Format = "#,##0 đ";
            dgvChiTietNhap.Columns["ThanhTien"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            TinhTongTien();
        }

        private void HienThiPhieuNhap(string maPN)
        {
            string sql = "SELECT pn.MaPhieuNhap, pn.NgayLap, pn.MaNCC, pn.TrangThai, pn.LyDoHuy, " +
                         "(tkLap.TenDangNhap + ' - ' + tkLap.HoTen) AS TenNguoiLap, " +
                         "ISNULL(tkXN.TenDangNhap + ' - ' + tkXN.HoTen, N'') AS TenNguoiXacNhan " +
                         "FROM PhieuNhap pn INNER JOIN TaiKhoan tkLap ON pn.NguoiLap = tkLap.MaTaiKhoan " +
                         "LEFT JOIN TaiKhoan tkXN ON pn.NguoiXacNhan = tkXN.MaTaiKhoan " +
                         "WHERE pn.MaPhieuNhap = @MaPN";

            DataTable dt = Functions.GetDataToTable(sql, new SqlParameter("@MaPN", maPN));
            if (dt.Rows.Count == 0)
            {
                ResetValues();
                return;
            }

            DataRow r = dt.Rows[0];
            dangNap = true;
            maPhieuDangChon = Convert.ToString(r["MaPhieuNhap"]);
            trangThaiHienTai = Convert.ToString(r["TrangThai"]);
            laPhieuMoi = false;
            daThayDoi = false;

            txtMaPN.Text = maPhieuDangChon;
            dtpNgayLap.Value = Convert.ToDateTime(r["NgayLap"]);
            Load_ComboNhaCungCap(Convert.ToString(r["MaNCC"]));
            txtTrangThai.Text = trangThaiHienTai;
            txtNguoiLap.Text = Convert.ToString(r["TenNguoiLap"]);
            txtNguoiXacNhan.Text = r["TenNguoiXacNhan"] == DBNull.Value ? "" : Convert.ToString(r["TenNguoiXacNhan"]);
            txtLyDoHuy.Text = r["LyDoHuy"] == DBNull.Value ? "" : Convert.ToString(r["LyDoHuy"]);
            dangNap = false;

            ResetValuesHang();
            Load_ChiTiet(maPN);
            CapNhatNut();
        }

        // 2. Hàm phụ trợ trạng thái, tính tiền và kiểm tra thay đổi

        private bool DuocSuaPhieu()
        {
            return FunctionsDTung.CoQuyen("ADMIN", "KHO") && trangThaiHienTai == "Nháp" && maPhieuDangChon != "";
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
            dangNap = false;

            ResetValuesHang();
            Load_ChiTiet("");
            CapNhatNut();
        }

        private void ResetValuesHang()
        {
            cboHangHoa.SelectedIndex = -1;
            txtSoLuong.Text = "";
            txtDonGiaNhap.Text = "";
        }

        private void TinhTongTien()
        {
            decimal tong = 0;
            if (tblChiTiet != null)
            {
                foreach (DataRow row in tblChiTiet.Rows)
                    tong += Convert.ToDecimal(row["ThanhTien"]);
            }
            txtTongTien.Text = FunctionsDTung.DinhDangTien(tong);
        }

        private void CapNhatNut()
        {
            bool coQuyenSua = FunctionsDTung.CoQuyen("ADMIN", "KHO");
            bool duocSua = DuocSuaPhieu();
            bool coDong = tblChiTiet != null && tblChiTiet.Rows.Count > 0;
            bool duocChot = coQuyenSua && !laPhieuMoi && !daThayDoi && trangThaiHienTai == "Nháp" && maPhieuDangChon != "";

            // Ẩn phần 1 (Tìm kiếm & Danh sách) khi lập mới để giao diện gọn hơn
            grpDanhSach.Visible = !laPhieuMoi;
            grpThongTinChung.Top = laPhieuMoi ? 58 : 230;
            grpChiTiet.Top = laPhieuMoi ? 200 : 372;
            grpChiTiet.Height = btnLapMoi.Top - 8 - grpChiTiet.Top;

            cboNhaCungCap.Enabled = duocSua;
            cboHangHoa.Enabled = duocSua;
            txtSoLuong.ReadOnly = !duocSua;
            txtDonGiaNhap.ReadOnly = !duocSua;
            btnThemDong.Enabled = duocSua;
            btnXoaDong.Enabled = duocSua && dgvChiTietNhap.CurrentRow != null && coDong;

            btnLapMoi.Enabled = coQuyenSua && !daThayDoi;
            btnLuu.Enabled = duocSua && daThayDoi;
            btnBoQua.Enabled = coQuyenSua && daThayDoi;
            btnXacNhan.Enabled = duocChot && coDong;
            btnHuy.Enabled = duocChot;
        }

        private bool KiemTraChuaLuu(string hanhDong)
        {
            if (!daThayDoi)
                return true;

            return MessageBox.Show(
                "Phiếu nhập đang có thay đổi chưa lưu. Bạn có muốn bỏ qua để " + hanhDong + "?",
                "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes;
        }

        // 3. Tìm kiếm, lọc và chọn phiếu trên danh sách

        private void chkLocNgay_CheckedChanged(object sender, EventArgs e)
        {
            dtpLocTuNgay.Enabled = chkLocNgay.Checked;
            dtpLocDenNgay.Enabled = chkLocNgay.Checked;
        }

        private void btnLoc_Click(object sender, EventArgs e)
        {
            if (!KiemTraChuaLuu("lọc lại danh sách"))
                return;

            if (chkLocNgay.Checked && dtpLocTuNgay.Value.Date > dtpLocDenNgay.Value.Date)
            {
                MessageBox.Show("Từ ngày phải nhỏ hơn hoặc bằng Đến ngày!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                daThayDoi = false;
                Load_DanhSachPhieuNhap();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi lọc danh sách:\n" + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            if (!KiemTraChuaLuu("làm mới danh sách"))
                return;

            txtTuKhoa.Text = "";
            cboLocTrangThai.SelectedIndex = 0;
            chkLocNgay.Checked = false;
            dtpLocTuNgay.Value = DateTime.Today.AddDays(-30);
            dtpLocDenNgay.Value = DateTime.Today;

            try
            {
                daThayDoi = false;
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

            if (!KiemTraChuaLuu("xem phiếu khác"))
                return;

            try
            {
                HienThiPhieuNhap(maDuocChon);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi đọc phiếu nhập:\n" + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // 4. Nhập thông tin chung và chi tiết mặt hàng

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

        private void btnThemDong_Click(object sender, EventArgs e)
        {
            if (!FunctionsDTung.YeuCauQuyen("thêm mặt hàng vào phiếu nhập", "ADMIN", "KHO"))
                return;

            if (!DuocSuaPhieu())
            {
                MessageBox.Show("Phiếu nhập hiện không ở trạng thái Nháp cho phép sửa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (cboHangHoa.SelectedIndex == -1 || cboHangHoa.SelectedItem == null)
            {
                MessageBox.Show("Vui lòng chọn mặt hàng!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cboHangHoa.Focus();
                return;
            }

            int soLuong;
            if (!int.TryParse(txtSoLuong.Text.Trim(), NumberStyles.None, CultureInfo.InvariantCulture, out soLuong) || soLuong <= 0)
            {
                MessageBox.Show("Số lượng nhập phải là số nguyên dương lớn hơn 0", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtSoLuong.Focus();
                return;
            }

            long donGiaNguyen;
            if (!long.TryParse(txtDonGiaNhap.Text.Trim(), NumberStyles.None, CultureInfo.InvariantCulture, out donGiaNguyen) || donGiaNguyen <= 0)
            {
                MessageBox.Show("Đơn giá nhập phải là số nguyên dương lớn hơn 0", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtDonGiaNhap.Focus();
                return;
            }

            DataRowView hang = (DataRowView)cboHangHoa.SelectedItem;
            string maHang = Convert.ToString(hang["MaHang"]);
            string tenHang = Convert.ToString(hang["TenHang"]);
            decimal donGiaNhap = donGiaNguyen;

            DataRow dongCu = null;
            foreach (DataRow row in tblChiTiet.Rows)
            {
                if (Convert.ToString(row["MaHang"]) == maHang)
                {
                    dongCu = row;
                    break;
                }
            }

            if (dongCu != null)
            {
                decimal giaCu = Convert.ToDecimal(dongCu["DonGiaNhap"]);
                decimal giaChon = giaCu;

                // Trùng mặt hàng nhưng khác đơn giá nhập thì hỏi cập nhật giá mới hay giữ giá cũ
                if (giaCu != donGiaNhap)
                {
                    DialogResult hoi = MessageBox.Show(
                        "Mặt hàng \"" + tenHang + "\" đã có với giá cũ " + FunctionsDTung.DinhDangTien(giaCu) +
                        ".\nChọn 'Yes' để cập nhật toàn bộ dòng theo giá mới " + FunctionsDTung.DinhDangTien(donGiaNhap) +
                        ", hoặc 'No' để giữ giá cũ.",
                        "Trùng mặt hàng khác đơn giá nhập", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (hoi == DialogResult.Yes)
                        giaChon = donGiaNhap;
                }

                int slMoi = Convert.ToInt32(dongCu["SoLuong"]) + soLuong;
                dongCu["SoLuong"] = slMoi;
                dongCu["DonGiaNhap"] = giaChon;
                dongCu["ThanhTien"] = (decimal)slMoi * giaChon;
            }
            else
            {
                tblChiTiet.Rows.Add(maHang, tenHang, Convert.ToString(hang["TenDVT"]), soLuong, donGiaNhap, (decimal)soLuong * donGiaNhap);
            }

            daThayDoi = true;
            ResetValuesHang();
            TinhTongTien();
            CapNhatNut();
        }

        private void btnXoaDong_Click(object sender, EventArgs e)
        {
            if (!FunctionsDTung.YeuCauQuyen("xóa dòng chi tiết phiếu nhập", "ADMIN", "KHO") || !DuocSuaPhieu())
                return;

            if (dgvChiTietNhap.CurrentRow == null || tblChiTiet.Rows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn dòng mặt hàng cần xóa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            string maHang = Convert.ToString(dgvChiTietNhap.CurrentRow.Cells["MaHang"].Value);
            string tenHang = Convert.ToString(dgvChiTietNhap.CurrentRow.Cells["TenHang"].Value);
            if (MessageBox.Show("Xóa mặt hàng \"" + maHang + " - " + tenHang + "\" khỏi phiếu nhập?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                return;

            for (int i = 0; i < tblChiTiet.Rows.Count; i++)
            {
                if (Convert.ToString(tblChiTiet.Rows[i]["MaHang"]) == maHang)
                {
                    tblChiTiet.Rows.RemoveAt(i);
                    break;
                }
            }

            daThayDoi = true;
            TinhTongTien();
            CapNhatNut();
        }

        // 5. Các nút nghiệp vụ (Lập mới, Lưu, Bỏ qua, Xác nhận, Hủy, Đóng)

        private void btnLapMoi_Click(object sender, EventArgs e)
        {
            if (!FunctionsDTung.YeuCauQuyen("lập mới phiếu nhập kho", "ADMIN", "KHO"))
                return;
            if (daThayDoi)
            {
                MessageBox.Show("Vui lòng Lưu hoặc Bỏ qua phiếu đang sửa trước!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                maPhieuDangChon = FunctionsDTung.TaoMaChungTu("PhieuNhap", "MaPhieuNhap", "PN");
                trangThaiHienTai = "Nháp";
                laPhieuMoi = true;
                daThayDoi = true;

                txtMaPN.Text = maPhieuDangChon;
                dtpNgayLap.Value = DateTime.Now;
                Load_ComboNhaCungCap("");
                txtTrangThai.Text = "Nháp";
                txtNguoiLap.Text = FunctionsDTung.TenDangNhapHienTai + " - " + FunctionsDTung.HoTenHienTai;
                txtNguoiXacNhan.Text = "";
                txtLyDoHuy.Text = "";

                ResetValuesHang();
                Load_ChiTiet("");
                CapNhatNut();
                cboNhaCungCap.Focus();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi lập phiếu mới:\n" + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                MessageBox.Show("Vui lòng chọn Nhà cung cấp!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cboNhaCungCap.Focus();
                return;
            }
            if (tblChiTiet.Rows.Count == 0)
            {
                MessageBox.Show("Phiếu nhập phải có ít nhất một dòng mặt hàng!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cboHangHoa.Focus();
                return;
            }

            try
            {
                string maPN = maPhieuDangChon;
                string maNCC = Convert.ToString(cboNhaCungCap.SelectedValue);
                FunctionsDTung.LuuPhieuNhap(maPN, dtpNgayLap.Value, maNCC, FunctionsDTung.MaTaiKhoanHienTai, tblChiTiet, laPhieuMoi);

                laPhieuMoi = false;
                daThayDoi = false;
                Load_DanhSachPhieuNhap();

                MessageBox.Show("Đã lưu phiếu nhập " + maPN + " thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

            if (MessageBox.Show("Hủy bỏ các thay đổi chưa lưu của phiếu nhập này?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
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
                MessageBox.Show("Lỗi khi khôi phục dữ liệu:\n" + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnXacNhan_Click(object sender, EventArgs e)
        {
            if (!FunctionsDTung.YeuCauQuyen("xác nhận nhập kho", "ADMIN", "KHO"))
                return;

            if (laPhieuMoi || daThayDoi)
            {
                MessageBox.Show("Vui lòng bấm \"Lưu\" trước khi xác nhận nhập kho!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (trangThaiHienTai != "Nháp" || tblChiTiet.Rows.Count == 0)
            {
                MessageBox.Show("Chỉ phiếu Nháp có chi tiết mặt hàng mới được xác nhận!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (MessageBox.Show("Xác nhận nhập kho phiếu " + maPhieuDangChon + "?\nSố lượng nhập sẽ được cộng vào tồn kho.", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                return;

            try
            {
                string maPN = maPhieuDangChon;
                FunctionsDTung.XacNhanPhieuNhap(maPN, FunctionsDTung.MaTaiKhoanHienTai);
                Load_DanhSachPhieuNhap();
                MessageBox.Show("Đã xác nhận phiếu nhập " + maPN + " và cộng tồn kho thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Không thể xác nhận phiếu nhập:\n" + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            if (!FunctionsDTung.YeuCauQuyen("hủy phiếu nhập kho", "ADMIN", "KHO"))
                return;

            if (laPhieuMoi || daThayDoi)
            {
                MessageBox.Show("Vui lòng bấm \"Lưu\" trước khi hủy phiếu!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (trangThaiHienTai != "Nháp")
            {
                MessageBox.Show("Không thể hủy phiếu đã xác nhận hoặc đã hủy!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                MessageBox.Show("Đã hủy phiếu nhập " + maPN + ".", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi hủy phiếu nhập:\n" + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnDong_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void frmPhieuNhap_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!KiemTraChuaLuu("đóng màn hình"))
                e.Cancel = true;
        }

        // 6. Điều hướng thanh menu

        private void PhanQuyenMenu()
        {
            mnuTaiKhoan.Visible = FunctionsDTung.CoQuyen("ADMIN");
            mnuKhachHang.Visible = FunctionsDTung.CoQuyen("ADMIN", "BANHANG", "KETOAN");
            mnuNhaCungCap.Visible = FunctionsDTung.CoQuyen("ADMIN", "KHO", "KETOAN");
            mnuBaoGia.Visible = FunctionsDTung.CoQuyen("ADMIN", "BANHANG", "KETOAN");
            mnuPhieuNhap.Visible = FunctionsDTung.CoQuyen("ADMIN", "KHO", "KETOAN");
            mnuBCDoanhThu.Visible = FunctionsDTung.CoQuyen("ADMIN", "KETOAN");
        }

        private void MoForm(Form frm)
        {
            if (!KiemTraChuaLuu("chuyển màn hình"))
            {
                frm.Dispose();
                return;
            }
            using (frm)
            {
                frm.ShowDialog(this);
            }
        }

        private void mnuTrangChu_Click(object sender, EventArgs e) { Close(); }
        private void mnuTaiKhoan_Click(object sender, EventArgs e) { MoForm(new frmQuanLyTaiKhoan()); }
        private void mnuSanPham_Click(object sender, EventArgs e) { MoForm(new frmSanPham()); }
        private void mnuKhachHang_Click(object sender, EventArgs e) { MoForm(new frmKhachHang()); }
        private void mnuNhaCungCap_Click(object sender, EventArgs e) { MoForm(new frmNhaCungCap()); }
        private void mnuBaoGia_Click(object sender, EventArgs e) { MoForm(new frmBaoGia()); }
        private void mnuDonBan_Click(object sender, EventArgs e) { MoForm(new frmDonBan()); }
        private void mnuTraHang_Click(object sender, EventArgs e) { MoForm(new frmTraHang()); }
        private void mnuBCDoanhThu_Click(object sender, EventArgs e) { MoForm(new frmBaoCaoDoanhThu()); }
        private void mnuBCTonKho_Click(object sender, EventArgs e) { MoForm(new frmBaoCaoTonKho()); }

        private void mnuThoat_Click(object sender, EventArgs e)
        {
            if (!KiemTraChuaLuu("thoát phần mềm"))
                return;
            if (MessageBox.Show("Bạn có muốn thoát phần mềm?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                daThayDoi = false;
                Application.Exit();
            }
        }
    }
}
