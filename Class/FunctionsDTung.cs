using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace QuanLyVanPhongPham.Class
{
    // Lớp hàm dùng riêng cho phần Phiếu nhập kho và Phiếu trả hàng (Đ Tùng)
    public static class FunctionsDTung
    {
        // 1. Thông tin phiên đăng nhập tạm & hàm phân quyền (chờ ghép nhóm)
        // Khi ghép với frmDangNhap, chỉ cần gọi SetPhienDangNhap(...) sau khi đăng nhập thành công.
        // Đổi TenDangNhapHienTai thành "admin", "kho", "banhang" hoặc "ketoan" để test.
        public static int MaTaiKhoanHienTai { get; private set; }
        public static string TenDangNhapHienTai { get; private set; } = "admin";
        public static string HoTenHienTai { get; private set; } = "";
        public static string MaVaiTroHienTai { get; private set; } = "ADMIN";

        // Nạp thông tin tài khoản mặc định từ CSDL nếu chưa gắn phiên đăng nhập thật
        public static void KhoiTaoPhienMacDinh()
        {
            if (MaTaiKhoanHienTai > 0)
                return;

            string sql = "SELECT TOP 1 MaTaiKhoan, TenDangNhap, HoTen, MaVaiTro " +
                         "FROM TaiKhoan WHERE TenDangNhap = @TenDN AND TrangThai = 1";
            DataTable dt = Functions.GetDataToTable(sql, new SqlParameter("@TenDN", TenDangNhapHienTai));

            if (dt.Rows.Count == 0)
            {
                sql = "SELECT TOP 1 MaTaiKhoan, TenDangNhap, HoTen, MaVaiTro " +
                      "FROM TaiKhoan WHERE MaVaiTro = 'ADMIN' AND TrangThai = 1 ORDER BY MaTaiKhoan";
                dt = Functions.GetDataToTable(sql);
            }

            if (dt.Rows.Count > 0)
            {
                DataRow r = dt.Rows[0];
                MaTaiKhoanHienTai = Convert.ToInt32(r["MaTaiKhoan"]);
                TenDangNhapHienTai = Convert.ToString(r["TenDangNhap"]);
                HoTenHienTai = Convert.ToString(r["HoTen"]);
                MaVaiTroHienTai = Convert.ToString(r["MaVaiTro"]);
            }
        }

        // Gán phiên đăng nhập hiện tại khi ghép với frmDangNhap của nhóm
        public static void SetPhienDangNhap(int maTaiKhoan, string tenDangNhap, string hoTen, string maVaiTro)
        {
            MaTaiKhoanHienTai = maTaiKhoan;
            TenDangNhapHienTai = tenDangNhap;
            HoTenHienTai = hoTen;
            MaVaiTroHienTai = maVaiTro;
        }

        // Kiểm tra người dùng hiện tại có thuộc một trong các vai trò được phép hay không
        public static bool CoQuyen(params string[] danhSachVaiTro)
        {
            if (danhSachVaiTro == null || danhSachVaiTro.Length == 0)
                return false;
            if (string.IsNullOrWhiteSpace(MaVaiTroHienTai))
                return false;

            string vaiTroHienTai = MaVaiTroHienTai.Trim();
            foreach (string vaiTro in danhSachVaiTro)
            {
                if (!string.IsNullOrWhiteSpace(vaiTro) &&
                    string.Equals(vaiTroHienTai, vaiTro.Trim(), StringComparison.OrdinalIgnoreCase))
                {
                    return true;
                }
            }
            return false;
        }

        // Kiểm tra quyền ngay trong mã lệnh xử lý (QT13); tự hiện thông báo chặn nếu không đủ quyền
        public static bool YeuCauQuyen(string tenThaoTac, params string[] danhSachVaiTro)
        {
            if (!CoQuyen(danhSachVaiTro))
            {
                MessageBox.Show(
                    "Bạn không có quyền thực hiện thao tác: " + tenThaoTac + "!",
                    "Chặn quyền",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Stop);
                return false;
            }
            return true;
        }

        // 2. Hàm tiện ích chung (sinh mã, nhập liệu, định dạng tiền, hộp thoại hủy)

        // Sinh mã chứng từ nối tiếp theo dữ liệu mẫu (VD: PN001 -> PN002 -> PN003)
        public static string TaoMaChungTu(string tenBang, string tenCotMa, string tienTo)
        {
            bool hopLe = (tenBang == "PhieuNhap" && tenCotMa == "MaPhieuNhap") ||
                         (tenBang == "PhieuTra" && tenCotMa == "MaPhieuTra");
            if (!hopLe)
                throw new ArgumentException("Bảng hoặc cột sinh mã không hợp lệ.");

            string sql = "SELECT " + tenCotMa + " FROM " + tenBang + " WHERE " + tenCotMa + " LIKE @TienTo";
            DataTable dt = Functions.GetDataToTable(sql, new SqlParameter("@TienTo", tienTo + "%"));

            int maxSo = 0;
            foreach (DataRow row in dt.Rows)
            {
                string ma = Convert.ToString(row[tenCotMa]);
                if (!string.IsNullOrEmpty(ma) && ma.StartsWith(tienTo, StringComparison.OrdinalIgnoreCase))
                {
                    string phanSo = ma.Substring(tienTo.Length);
                    int so;
                    if (int.TryParse(phanSo, out so) && so > maxSo)
                    {
                        maxSo = so;
                    }
                }
            }

            return tienTo + (maxSo + 1).ToString("000");
        }

        // Chỉ cho phép nhập chữ số nguyên và phím điều khiển (Backspace) vào TextBox
        public static void ChiChoNhapSoNguyen(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        // Định dạng số tiền hiển thị theo chuẩn phân cách hàng nghìn
        public static string DinhDangTien(decimal soTien)
        {
            return soTien.ToString("#,##0") + " đ";
        }

        // Bật hộp thoại nhập lý do hủy chứng từ (bắt buộc nhập nội dung trước khi xác nhận)
        public static string NhapLyDoHuy(string tieuDe, string noiDung, string giaTriMacDinh = "")
        {
            using (Form frm = new Form())
            {
                frm.Text = tieuDe;
                frm.ClientSize = new Size(440, 150);
                frm.StartPosition = FormStartPosition.CenterParent;
                frm.FormBorderStyle = FormBorderStyle.FixedDialog;
                frm.MaximizeBox = false;
                frm.MinimizeBox = false;
                frm.Font = new Font("Segoe UI", 9.75F);

                Label lbl = new Label();
                lbl.Text = noiDung;
                lbl.Location = new Point(14, 12);
                lbl.Size = new Size(412, 36);

                TextBox txt = new TextBox();
                txt.Location = new Point(14, 52);
                txt.Size = new Size(412, 25);
                txt.MaxLength = 250;
                txt.Text = giaTriMacDinh ?? "";

                Button btnOK = new Button();
                btnOK.Text = "Xác nhận";
                btnOK.Location = new Point(242, 98);
                btnOK.Size = new Size(88, 30);
                btnOK.Click += delegate
                {
                    if (string.IsNullOrWhiteSpace(txt.Text))
                    {
                        MessageBox.Show(
                            "Vui lòng nhập lý do hủy phiếu!",
                            "Thiếu thông tin",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Warning);
                        txt.Focus();
                        return;
                    }
                    frm.DialogResult = DialogResult.OK;
                    frm.Close();
                };

                Button btnCancel = new Button();
                btnCancel.Text = "Hủy bỏ";
                btnCancel.Location = new Point(338, 98);
                btnCancel.Size = new Size(88, 30);
                btnCancel.DialogResult = DialogResult.Cancel;

                frm.Controls.Add(lbl);
                frm.Controls.Add(txt);
                frm.Controls.Add(btnOK);
                frm.Controls.Add(btnCancel);
                frm.AcceptButton = btnOK;
                frm.CancelButton = btnCancel;

                if (frm.ShowDialog() == DialogResult.OK)
                    return txt.Text.Trim();

                return null;
            }
        }

        // 3. Nghiệp vụ SqlTransaction cho Phiếu nhập kho

        // Lưu phiếu nhập ở trạng thái Nháp và toàn bộ dòng chi tiết trong 1 SqlTransaction
        public static void LuuPhieuNhap(string maPN, DateTime ngayLap, string maNCC, int nguoiLap, DataTable tblChiTiet, bool laPhieuMoi)
        {
            if (tblChiTiet == null || tblChiTiet.Rows.Count == 0)
                throw new Exception("Phiếu nhập phải có ít nhất một dòng mặt hàng (QT04)!");

            decimal tongTien = 0;
            foreach (DataRow row in tblChiTiet.Rows)
            {
                tongTien += Convert.ToDecimal(row["ThanhTien"]);
            }

            Functions.Connect();
            SqlTransaction tran = Functions.Conn.BeginTransaction();
            try
            {
                if (laPhieuMoi)
                {
                    string sqlInsertMaster =
                        "INSERT INTO PhieuNhap (MaPhieuNhap, NgayLap, NgayXacNhan, MaNCC, NguoiLap, NguoiXacNhan, TrangThai, LyDoHuy, TongTien) " +
                        "VALUES (@MaPN, @NgayLap, NULL, @MaNCC, @NguoiLap, NULL, N'Nháp', NULL, @TongTien)";
                    using (SqlCommand cmd = new SqlCommand(sqlInsertMaster, Functions.Conn, tran))
                    {
                        cmd.Parameters.AddWithValue("@MaPN", maPN);
                        cmd.Parameters.AddWithValue("@NgayLap", ngayLap);
                        cmd.Parameters.AddWithValue("@MaNCC", maNCC);
                        cmd.Parameters.AddWithValue("@NguoiLap", nguoiLap);
                        cmd.Parameters.AddWithValue("@TongTien", tongTien);
                        cmd.ExecuteNonQuery();
                    }
                }
                else
                {
                    string sqlUpdateMaster =
                        "UPDATE PhieuNhap SET MaNCC = @MaNCC, TongTien = @TongTien " +
                        "WHERE MaPhieuNhap = @MaPN AND TrangThai = N'Nháp'";
                    int rows;
                    using (SqlCommand cmd = new SqlCommand(sqlUpdateMaster, Functions.Conn, tran))
                    {
                        cmd.Parameters.AddWithValue("@MaNCC", maNCC);
                        cmd.Parameters.AddWithValue("@TongTien", tongTien);
                        cmd.Parameters.AddWithValue("@MaPN", maPN);
                        rows = cmd.ExecuteNonQuery();
                    }

                    if (rows == 0)
                        throw new Exception("Phiếu nhập không còn ở trạng thái Nháp nên không thể chỉnh sửa!");

                    string sqlDeleteDetail = "DELETE FROM ChiTietNhap WHERE MaPhieuNhap = @MaPN";
                    using (SqlCommand cmd = new SqlCommand(sqlDeleteDetail, Functions.Conn, tran))
                    {
                        cmd.Parameters.AddWithValue("@MaPN", maPN);
                        cmd.ExecuteNonQuery();
                    }
                }

                string sqlInsertDetail =
                    "INSERT INTO ChiTietNhap (MaPhieuNhap, MaHang, SoLuong, DonGiaNhap, ThanhTien) " +
                    "VALUES (@MaPN, @MaHang, @SoLuong, @DonGiaNhap, @ThanhTien)";
                foreach (DataRow row in tblChiTiet.Rows)
                {
                    using (SqlCommand cmd = new SqlCommand(sqlInsertDetail, Functions.Conn, tran))
                    {
                        cmd.Parameters.AddWithValue("@MaPN", maPN);
                        cmd.Parameters.AddWithValue("@MaHang", Convert.ToString(row["MaHang"]));
                        cmd.Parameters.AddWithValue("@SoLuong", Convert.ToInt32(row["SoLuong"]));
                        cmd.Parameters.AddWithValue("@DonGiaNhap", Convert.ToDecimal(row["DonGiaNhap"]));
                        cmd.Parameters.AddWithValue("@ThanhTien", Convert.ToDecimal(row["ThanhTien"]));
                        cmd.ExecuteNonQuery();
                    }
                }

                tran.Commit();
            }
            catch
            {
                if (tran.Connection != null)
                    tran.Rollback();
                throw;
            }
        }

        // Xác nhận nhập kho và cộng tồn kho cho từng mặt hàng trong 1 SqlTransaction (QT08)
        public static void XacNhanPhieuNhap(string maPN, int nguoiXacNhan)
        {
            Functions.Connect();
            SqlTransaction tran = Functions.Conn.BeginTransaction();
            try
            {
                string sqlConfirm =
                    "UPDATE PhieuNhap " +
                    "SET TrangThai = N'Đã xác nhận', NgayXacNhan = GETDATE(), NguoiXacNhan = @NguoiXacNhan " +
                    "WHERE MaPhieuNhap = @MaPN AND TrangThai = N'Nháp'";
                int rows;
                using (SqlCommand cmd = new SqlCommand(sqlConfirm, Functions.Conn, tran))
                {
                    cmd.Parameters.AddWithValue("@NguoiXacNhan", nguoiXacNhan);
                    cmd.Parameters.AddWithValue("@MaPN", maPN);
                    rows = cmd.ExecuteNonQuery();
                }

                if (rows == 0)
                    throw new Exception("Chỉ phiếu nhập đang ở trạng thái Nháp mới được xác nhận!");

                DataTable dtChiTiet = new DataTable();
                string sqlGetDetail = "SELECT MaHang, SoLuong FROM ChiTietNhap WHERE MaPhieuNhap = @MaPN";
                using (SqlCommand cmd = new SqlCommand(sqlGetDetail, Functions.Conn, tran))
                {
                    cmd.Parameters.AddWithValue("@MaPN", maPN);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        dtChiTiet.Load(reader);
                    }
                }

                if (dtChiTiet.Rows.Count == 0)
                    throw new Exception("Phiếu nhập phải có ít nhất 1 dòng mặt hàng trước khi xác nhận!");

                string sqlCongTon = "UPDATE HangHoa SET SoLuongTon = SoLuongTon + @SoLuong WHERE MaHang = @MaHang";
                foreach (DataRow row in dtChiTiet.Rows)
                {
                    string maHang = Convert.ToString(row["MaHang"]);
                    int soLuong = Convert.ToInt32(row["SoLuong"]);
                    using (SqlCommand cmd = new SqlCommand(sqlCongTon, Functions.Conn, tran))
                    {
                        cmd.Parameters.AddWithValue("@SoLuong", soLuong);
                        cmd.Parameters.AddWithValue("@MaHang", maHang);
                        int updated = cmd.ExecuteNonQuery();
                        if (updated == 0)
                            throw new Exception("Không tìm thấy mặt hàng " + maHang + " để cộng tồn kho!");
                    }
                }

                tran.Commit();
            }
            catch
            {
                if (tran.Connection != null)
                    tran.Rollback();
                throw;
            }
        }

        // Hủy phiếu nhập đang ở trạng thái Nháp và lưu lý do hủy
        public static void HuyPhieuNhap(string maPN, string lyDoHuy)
        {
            string sql =
                "UPDATE PhieuNhap SET TrangThai = N'Đã hủy', LyDoHuy = @LyDoHuy " +
                "WHERE MaPhieuNhap = @MaPN AND TrangThai = N'Nháp'";
            int rows = Functions.RunSql(
                sql,
                new SqlParameter("@LyDoHuy", lyDoHuy),
                new SqlParameter("@MaPN", maPN));

            if (rows == 0)
                throw new Exception("Chỉ được hủy phiếu nhập đang ở trạng thái Nháp!");
        }
    }
}
