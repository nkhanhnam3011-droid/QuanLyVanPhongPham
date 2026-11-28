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
        // 1. Phiên đăng nhập tạm & phân quyền (chờ ghép form Đăng nhập của nhóm)
        // Đổi TenDangNhapHienTai sang "admin", "kho", "banhang", "ketoan" để test quyền
        public static int MaTaiKhoanHienTai { get; private set; }
        public static string TenDangNhapHienTai { get; private set; } = "admin";
        public static string HoTenHienTai { get; private set; } = "";
        public static string MaVaiTroHienTai { get; private set; } = "ADMIN";

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
                SetPhienDangNhap(
                    Convert.ToInt32(r["MaTaiKhoan"]),
                    Convert.ToString(r["TenDangNhap"]),
                    Convert.ToString(r["HoTen"]),
                    Convert.ToString(r["MaVaiTro"]));
            }
        }

        public static void SetPhienDangNhap(int maTaiKhoan, string tenDangNhap, string hoTen, string maVaiTro)
        {
            MaTaiKhoanHienTai = maTaiKhoan;
            TenDangNhapHienTai = tenDangNhap;
            HoTenHienTai = hoTen;
            MaVaiTroHienTai = maVaiTro;
        }

        public static bool CoQuyen(params string[] danhSachVaiTro)
        {
            if (danhSachVaiTro == null || string.IsNullOrWhiteSpace(MaVaiTroHienTai))
                return false;

            foreach (string vaiTro in danhSachVaiTro)
            {
                if (!string.IsNullOrWhiteSpace(vaiTro) &&
                    string.Equals(MaVaiTroHienTai.Trim(), vaiTro.Trim(), StringComparison.OrdinalIgnoreCase))
                    return true;
            }
            return false;
        }

        // Kiểm tra quyền ngay trong mã lệnh xử lý (QT13)
        public static bool YeuCauQuyen(string tenThaoTac, params string[] danhSachVaiTro)
        {
            if (CoQuyen(danhSachVaiTro))
                return true;

            MessageBox.Show("Bạn không có quyền thực hiện thao tác: " + tenThaoTac + "!", "Chặn quyền", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            return false;
        }

        // 2. Hàm tiện ích chung (sinh mã nối tiếp, nhập số, tiền, lý do hủy)
        public static string TaoMaChungTu(string tenBang, string tenCotMa, string tienTo)
        {
            if ((tenBang != "PhieuNhap" || tenCotMa != "MaPhieuNhap") &&
                (tenBang != "PhieuTra" || tenCotMa != "MaPhieuTra"))
                throw new ArgumentException("Bảng hoặc cột sinh mã không hợp lệ.");

            string sql = "SELECT " + tenCotMa + " FROM " + tenBang + " WHERE " + tenCotMa + " LIKE @TienTo";
            DataTable dt = Functions.GetDataToTable(sql, new SqlParameter("@TienTo", tienTo + "%"));

            int maxSo = 0;
            foreach (DataRow row in dt.Rows)
            {
                string ma = Convert.ToString(row[tenCotMa]);
                int so;
                if (ma.StartsWith(tienTo, StringComparison.OrdinalIgnoreCase) &&
                    int.TryParse(ma.Substring(tienTo.Length), out so) && so > maxSo)
                {
                    maxSo = so;
                }
            }
            return tienTo + (maxSo + 1).ToString("000");
        }

        public static void ChiChoNhapSoNguyen(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
                e.Handled = true;
        }

        public static string DinhDangTien(decimal soTien)
        {
            return soTien.ToString("#,##0") + " đ";
        }

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

                Label lbl = new Label { Text = noiDung, Location = new Point(14, 12), Size = new Size(412, 36) };
                TextBox txt = new TextBox { Location = new Point(14, 52), Size = new Size(412, 25), MaxLength = 250, Text = giaTriMacDinh ?? "" };
                Button btnOK = new Button { Text = "Xác nhận", Location = new Point(242, 98), Size = new Size(88, 30) };
                Button btnCancel = new Button { Text = "Hủy bỏ", Location = new Point(338, 98), Size = new Size(88, 30), DialogResult = DialogResult.Cancel };

                btnOK.Click += delegate
                {
                    if (string.IsNullOrWhiteSpace(txt.Text))
                    {
                        MessageBox.Show("Vui lòng nhập lý do hủy phiếu!", "Thiếu thông tin", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        txt.Focus();
                        return;
                    }
                    frm.DialogResult = DialogResult.OK;
                    frm.Close();
                };

                frm.Controls.AddRange(new Control[] { lbl, txt, btnOK, btnCancel });
                frm.AcceptButton = btnOK;
                frm.CancelButton = btnCancel;
                return frm.ShowDialog() == DialogResult.OK ? txt.Text.Trim() : null;
            }
        }

        // 3. Nghiệp vụ Phiếu nhập kho (dùng SqlTransaction đảm bảo đồng bộ)
        private static int RunSqlTran(string sql, SqlTransaction tran, params SqlParameter[] thamSo)
        {
            using (SqlCommand cmd = new SqlCommand(sql, Functions.Conn, tran))
            {
                if (thamSo != null)
                    cmd.Parameters.AddRange(thamSo);
                return cmd.ExecuteNonQuery();
            }
        }

        public static void LuuPhieuNhap(string maPN, DateTime ngayLap, string maNCC, int nguoiLap, DataTable tblChiTiet, bool laPhieuMoi)
        {
            if (tblChiTiet == null || tblChiTiet.Rows.Count == 0)
                throw new Exception("Phiếu nhập phải có ít nhất một dòng mặt hàng (QT04)!");

            decimal tongTien = 0;
            foreach (DataRow row in tblChiTiet.Rows)
                tongTien += Convert.ToDecimal(row["ThanhTien"]);

            Functions.Connect();
            SqlTransaction tran = Functions.Conn.BeginTransaction();
            try
            {
                if (laPhieuMoi)
                {
                    string sql = "INSERT INTO PhieuNhap (MaPhieuNhap, NgayLap, MaNCC, NguoiLap, TrangThai, TongTien) " +
                                 "VALUES (@MaPN, @NgayLap, @MaNCC, @NguoiLap, N'Nháp', @TongTien)";
                    RunSqlTran(sql, tran,
                        new SqlParameter("@MaPN", maPN),
                        new SqlParameter("@NgayLap", ngayLap),
                        new SqlParameter("@MaNCC", maNCC),
                        new SqlParameter("@NguoiLap", nguoiLap),
                        new SqlParameter("@TongTien", tongTien));
                }
                else
                {
                    string sqlUp = "UPDATE PhieuNhap SET MaNCC = @MaNCC, TongTien = @TongTien " +
                                   "WHERE MaPhieuNhap = @MaPN AND TrangThai = N'Nháp'";
                    if (RunSqlTran(sqlUp, tran, new SqlParameter("@MaNCC", maNCC), new SqlParameter("@TongTien", tongTien), new SqlParameter("@MaPN", maPN)) == 0)
                        throw new Exception("Phiếu nhập không còn ở trạng thái Nháp nên không thể chỉnh sửa!");

                    RunSqlTran("DELETE FROM ChiTietNhap WHERE MaPhieuNhap = @MaPN", tran, new SqlParameter("@MaPN", maPN));
                }

                string sqlCT = "INSERT INTO ChiTietNhap (MaPhieuNhap, MaHang, SoLuong, DonGiaNhap, ThanhTien) " +
                               "VALUES (@MaPN, @MaHang, @SoLuong, @DonGiaNhap, @ThanhTien)";
                foreach (DataRow row in tblChiTiet.Rows)
                {
                    RunSqlTran(sqlCT, tran,
                        new SqlParameter("@MaPN", maPN),
                        new SqlParameter("@MaHang", Convert.ToString(row["MaHang"])),
                        new SqlParameter("@SoLuong", Convert.ToInt32(row["SoLuong"])),
                        new SqlParameter("@DonGiaNhap", Convert.ToDecimal(row["DonGiaNhap"])),
                        new SqlParameter("@ThanhTien", Convert.ToDecimal(row["ThanhTien"])));
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

        public static void XacNhanPhieuNhap(string maPN, int nguoiXacNhan)
        {
            Functions.Connect();
            SqlTransaction tran = Functions.Conn.BeginTransaction();
            try
            {
                string sqlXN = "UPDATE PhieuNhap SET TrangThai = N'Đã xác nhận', NgayXacNhan = GETDATE(), NguoiXacNhan = @NguoiXN " +
                               "WHERE MaPhieuNhap = @MaPN AND TrangThai = N'Nháp'";
                if (RunSqlTran(sqlXN, tran, new SqlParameter("@NguoiXN", nguoiXacNhan), new SqlParameter("@MaPN", maPN)) == 0)
                    throw new Exception("Chỉ phiếu nhập đang ở trạng thái Nháp mới được xác nhận!");

                DataTable dtChiTiet = new DataTable();
                using (SqlCommand cmd = new SqlCommand("SELECT MaHang, SoLuong FROM ChiTietNhap WHERE MaPhieuNhap = @MaPN", Functions.Conn, tran))
                {
                    cmd.Parameters.AddWithValue("@MaPN", maPN);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                        dtChiTiet.Load(reader);
                }

                if (dtChiTiet.Rows.Count == 0)
                    throw new Exception("Phiếu nhập phải có ít nhất 1 dòng mặt hàng trước khi xác nhận!");

                foreach (DataRow row in dtChiTiet.Rows)
                {
                    string maHang = Convert.ToString(row["MaHang"]);
                    int soLuong = Convert.ToInt32(row["SoLuong"]);
                    if (RunSqlTran("UPDATE HangHoa SET SoLuongTon = SoLuongTon + @SoLuong WHERE MaHang = @MaHang",
                        tran, new SqlParameter("@SoLuong", soLuong), new SqlParameter("@MaHang", maHang)) == 0)
                    {
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

        public static void HuyPhieuNhap(string maPN, string lyDoHuy)
        {
            string sql = "UPDATE PhieuNhap SET TrangThai = N'Đã hủy', LyDoHuy = @LyDoHuy " +
                         "WHERE MaPhieuNhap = @MaPN AND TrangThai = N'Nháp'";
            if (Functions.RunSql(sql, new SqlParameter("@LyDoHuy", lyDoHuy), new SqlParameter("@MaPN", maPN)) == 0)
                throw new Exception("Chỉ được hủy phiếu nhập đang ở trạng thái Nháp!");
        }
    }
}
