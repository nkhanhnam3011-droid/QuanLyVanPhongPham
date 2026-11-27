USE master;
GO

SET NOEXEC OFF;
GO

-- Bảo vệ dữ liệu: tuyệt đối không DROP DATABASE trong script dùng chung
IF DB_ID(N'QL_VanPhongPham') IS NOT NULL
BEGIN
    PRINT N'CẢNH BÁO: CSDL QL_VanPhongPham đã tồn tại! Đã dừng script khởi tạo để bảo vệ dữ liệu hiện có.';
    PRINT N'Nếu muốn làm mới dữ liệu mẫu cho môi trường thử nghiệm, hãy chạy file 02_Reset_DuLieuMau.sql.';
    SET NOEXEC ON;
END
GO

CREATE DATABASE QL_VanPhongPham;
GO

USE QL_VanPhongPham;
GO

/* =========================================================================
   PHẦN 1: TẠO 15 BẢNG VÀ CÁC RÀNG BUỘC TOÀN VẸN (PK, FK, UNIQUE, CHECK)
   ========================================================================= */

-- 1. Vai trò (04 vai trò theo Đề 11)
CREATE TABLE VaiTro (
    MaVaiTro    VARCHAR(10)   NOT NULL PRIMARY KEY,
    TenVaiTro   NVARCHAR(50)  NOT NULL UNIQUE
);
GO

-- 2. Tài khoản
CREATE TABLE TaiKhoan (
    MaTaiKhoan  INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
    TenDangNhap VARCHAR(50)       NOT NULL UNIQUE,
    MatKhauHash NVARCHAR(256)     NOT NULL, -- Định dạng: Iterations:SaltBase64:HashBase64
    HoTen       NVARCHAR(100)     NOT NULL,
    MaVaiTro    VARCHAR(10)       NOT NULL,
    TrangThai   BIT               NOT NULL DEFAULT 1, -- 1: Hoạt động, 0: Bị khóa
    CONSTRAINT FK_TaiKhoan_VaiTro FOREIGN KEY (MaVaiTro) REFERENCES VaiTro(MaVaiTro)
);
GO

-- 3. Loại hàng
CREATE TABLE LoaiHang (
    MaLoai      VARCHAR(20)   NOT NULL PRIMARY KEY,
    TenLoai     NVARCHAR(100) NOT NULL,
    TrangThai   BIT           NOT NULL DEFAULT 1 -- 1: Đang sử dụng, 0: Ngừng sử dụng
);
GO

-- 4. Đơn vị tính
CREATE TABLE DonViTinh (
    MaDVT       VARCHAR(20)  NOT NULL PRIMARY KEY,
    TenDVT      NVARCHAR(50) NOT NULL,
    TrangThai   BIT          NOT NULL DEFAULT 1
);
GO
-- 5. Hàng hóa (Văn phòng phẩm)
CREATE TABLE HangHoa (
    MaHang          VARCHAR(20)    NOT NULL PRIMARY KEY,
    TenHang         NVARCHAR(150)  NOT NULL,
    MaLoai          VARCHAR(20)    NOT NULL,
    MaDVT           VARCHAR(20)    NOT NULL,
    GiaBan          DECIMAL(18, 0) NOT NULL CHECK (GiaBan > 0),
    SoLuongTon      INT            NOT NULL DEFAULT 0 CHECK (SoLuongTon >= 0),
    MucTonToiThieu  INT            NOT NULL DEFAULT 0 CHECK (MucTonToiThieu >= 0),
    TrangThai       BIT            NOT NULL DEFAULT 1,
    CONSTRAINT FK_HangHoa_LoaiHang  FOREIGN KEY (MaLoai) REFERENCES LoaiHang(MaLoai),
    CONSTRAINT FK_HangHoa_DonViTinh FOREIGN KEY (MaDVT)  REFERENCES DonViTinh(MaDVT)
);
GO

-- 6. Khách hàng
CREATE TABLE KhachHang (
    MaKH        VARCHAR(20)   NOT NULL PRIMARY KEY,
    TenKH       NVARCHAR(150) NOT NULL,
    DiaChi      NVARCHAR(250) NULL,
    DienThoai   VARCHAR(20)   NULL,
    TrangThai   BIT           NOT NULL DEFAULT 1
);
GO

-- 7. Nhà cung cấp
CREATE TABLE NhaCungCap (
    MaNCC       VARCHAR(20)   NOT NULL PRIMARY KEY,
    TenNCC      NVARCHAR(150) NOT NULL,
    DiaChi      NVARCHAR(250) NULL,
    DienThoai   VARCHAR(20)   NULL,
    TrangThai   BIT           NOT NULL DEFAULT 1
);
GO

-- 8. Báo giá
CREATE TABLE BaoGia (
    MaBaoGia    VARCHAR(30)    NOT NULL PRIMARY KEY,
    NgayLap     DATETIME       NOT NULL DEFAULT GETDATE(),
    MaKH        VARCHAR(20)    NOT NULL,
    NguoiLap    INT            NOT NULL,
    TrangThai   NVARCHAR(30)   NOT NULL DEFAULT N'Mới lập'
                CHECK (TrangThai IN (N'Mới lập', N'Khách đặt hàng', N'Đã chuyển đơn bán', N'Đã hủy')),
    LyDoHuy     NVARCHAR(250)  NULL,
    TongTien    DECIMAL(18, 0) NOT NULL DEFAULT 0 CHECK (TongTien >= 0),
    CONSTRAINT FK_BaoGia_KhachHang FOREIGN KEY (MaKH)     REFERENCES KhachHang(MaKH),
    CONSTRAINT FK_BaoGia_NguoiLap  FOREIGN KEY (NguoiLap) REFERENCES TaiKhoan(MaTaiKhoan)
);
GO

-- 9. Chi tiết báo giá
CREATE TABLE ChiTietBaoGia (
    MaBaoGia      VARCHAR(30)    NOT NULL,
    MaHang        VARCHAR(20)    NOT NULL,
    SoLuong       INT            NOT NULL CHECK (SoLuong > 0),
    DonGia        DECIMAL(18, 0) NOT NULL CHECK (DonGia > 0),
    TyLeChietKhau INT            NOT NULL DEFAULT 0 CHECK (TyLeChietKhau BETWEEN 0 AND 100),
    ThanhTien     DECIMAL(18, 0) NOT NULL CHECK (ThanhTien >= 0),
    PRIMARY KEY (MaBaoGia, MaHang),
    CONSTRAINT FK_CTBaoGia_BaoGia  FOREIGN KEY (MaBaoGia) REFERENCES BaoGia(MaBaoGia),
    CONSTRAINT FK_CTBaoGia_HangHoa FOREIGN KEY (MaHang)   REFERENCES HangHoa(MaHang)
);
GO

-- 10. Đơn bán
CREATE TABLE DonBan (
    MaDonBan     VARCHAR(30)    NOT NULL PRIMARY KEY,
    NgayLap      DATETIME       NOT NULL DEFAULT GETDATE(),
    NgayXacNhan  DATETIME       NULL,
    MaKH         VARCHAR(20)    NOT NULL,
    MaBaoGia     VARCHAR(30)    NULL, -- NULL nếu bán trực tiếp không qua báo giá
    LoaiDon      NVARCHAR(20)   NOT NULL CHECK (LoaiDon IN (N'Bán lẻ', N'Bán sỉ')),
    NguoiLap     INT            NOT NULL,
    NguoiXacNhan INT            NULL,
    TrangThai    NVARCHAR(30)   NOT NULL DEFAULT N'Nháp'
                 CHECK (TrangThai IN (N'Nháp', N'Đã xác nhận', N'Đã hủy')),
    LyDoHuy      NVARCHAR(250)  NULL,
    TongTien     DECIMAL(18, 0) NOT NULL DEFAULT 0 CHECK (TongTien >= 0),
    CONSTRAINT CK_DonBan_XacNhan CHECK (
        (TrangThai = N'Đã xác nhận' AND NgayXacNhan IS NOT NULL AND NguoiXacNhan IS NOT NULL)
        OR (TrangThai <> N'Đã xác nhận')
    ),
    CONSTRAINT FK_DonBan_KhachHang    FOREIGN KEY (MaKH)         REFERENCES KhachHang(MaKH),
    CONSTRAINT FK_DonBan_BaoGia       FOREIGN KEY (MaBaoGia)     REFERENCES BaoGia(MaBaoGia),
    CONSTRAINT FK_DonBan_NguoiLap     FOREIGN KEY (NguoiLap)     REFERENCES TaiKhoan(MaTaiKhoan),
    CONSTRAINT FK_DonBan_NguoiXacNhan FOREIGN KEY (NguoiXacNhan) REFERENCES TaiKhoan(MaTaiKhoan)
);
GO

-- Chống chuyển 1 báo giá thành 2 đơn bán (chỉ áp dụng khi MaBaoGia khác NULL)
CREATE UNIQUE INDEX UX_DonBan_MaBaoGia
ON DonBan(MaBaoGia)
WHERE MaBaoGia IS NOT NULL;
GO

-- 11. Chi tiết bán
CREATE TABLE ChiTietBan (
    MaChiTietBan  INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
    MaDonBan      VARCHAR(30)       NOT NULL,
    MaHang        VARCHAR(20)       NOT NULL,
    SoLuong       INT               NOT NULL CHECK (SoLuong > 0),
    DonGiaBan     DECIMAL(18, 0)    NOT NULL CHECK (DonGiaBan > 0),
    TyLeChietKhau INT               NOT NULL DEFAULT 0 CHECK (TyLeChietKhau BETWEEN 0 AND 100),
    ThanhTien     DECIMAL(18, 0)    NOT NULL CHECK (ThanhTien >= 0),
    CONSTRAINT UQ_ChiTietBan_Don_Hang UNIQUE (MaDonBan, MaHang),
    -- Ràng buộc phụ phục vụ khóa ngoại ghép từ ChiTietTra để chống trả nhầm đơn
    CONSTRAINT UQ_ChiTietBan_ID_Don   UNIQUE (MaChiTietBan, MaDonBan),
    CONSTRAINT FK_CTBan_DonBan  FOREIGN KEY (MaDonBan) REFERENCES DonBan(MaDonBan),
    CONSTRAINT FK_CTBan_HangHoa FOREIGN KEY (MaHang)   REFERENCES HangHoa(MaHang)
);
GO

-- 12. Phiếu nhập kho
CREATE TABLE PhieuNhap (
    MaPhieuNhap  VARCHAR(30)    NOT NULL PRIMARY KEY,
    NgayLap      DATETIME       NOT NULL DEFAULT GETDATE(),
    NgayXacNhan  DATETIME       NULL,
    MaNCC        VARCHAR(20)    NOT NULL,
    NguoiLap     INT            NOT NULL,
    NguoiXacNhan INT            NULL,
    TrangThai    NVARCHAR(30)   NOT NULL DEFAULT N'Nháp'
                 CHECK (TrangThai IN (N'Nháp', N'Đã xác nhận', N'Đã hủy')),
    LyDoHuy      NVARCHAR(250)  NULL,
    TongTien     DECIMAL(18, 0) NOT NULL DEFAULT 0 CHECK (TongTien >= 0),
    CONSTRAINT CK_PhieuNhap_XacNhan CHECK (
        (TrangThai = N'Đã xác nhận' AND NgayXacNhan IS NOT NULL AND NguoiXacNhan IS NOT NULL)
        OR (TrangThai <> N'Đã xác nhận')
    ),
    CONSTRAINT FK_PhieuNhap_NCC          FOREIGN KEY (MaNCC)        REFERENCES NhaCungCap(MaNCC),
    CONSTRAINT FK_PhieuNhap_NguoiLap     FOREIGN KEY (NguoiLap)     REFERENCES TaiKhoan(MaTaiKhoan),
    CONSTRAINT FK_PhieuNhap_NguoiXacNhan FOREIGN KEY (NguoiXacNhan) REFERENCES TaiKhoan(MaTaiKhoan)
);
GO

-- 13. Chi tiết nhập kho
CREATE TABLE ChiTietNhap (
    MaPhieuNhap VARCHAR(30)    NOT NULL,
    MaHang      VARCHAR(20)    NOT NULL,
    SoLuong     INT            NOT NULL CHECK (SoLuong > 0),
    DonGiaNhap  DECIMAL(18, 0) NOT NULL CHECK (DonGiaNhap > 0),
    ThanhTien   DECIMAL(18, 0) NOT NULL CHECK (ThanhTien >= 0),
    PRIMARY KEY (MaPhieuNhap, MaHang),
    CONSTRAINT FK_CTNhap_PhieuNhap FOREIGN KEY (MaPhieuNhap) REFERENCES PhieuNhap(MaPhieuNhap),
    CONSTRAINT FK_CTNhap_HangHoa   FOREIGN KEY (MaHang)      REFERENCES HangHoa(MaHang)
);
GO

-- 14. Phiếu trả hàng
CREATE TABLE PhieuTra (
    MaPhieuTra   VARCHAR(30)    NOT NULL PRIMARY KEY,
    NgayLap      DATETIME       NOT NULL DEFAULT GETDATE(),
    NgayXacNhan  DATETIME       NULL,
    MaDonBan     VARCHAR(30)    NOT NULL,
    NguoiLap     INT            NOT NULL,
    NguoiXacNhan INT            NULL,
    TrangThai    NVARCHAR(30)   NOT NULL DEFAULT N'Nháp'
                 CHECK (TrangThai IN (N'Nháp', N'Đã xác nhận', N'Đã hủy')),
    LyDoTra      NVARCHAR(250)  NOT NULL,
    LyDoHuy      NVARCHAR(250)  NULL,
    TongTienHoan DECIMAL(18, 0) NOT NULL DEFAULT 0 CHECK (TongTienHoan >= 0),
    -- Ràng buộc phụ phục vụ khóa ngoại ghép từ ChiTietTra để chống trả nhầm đơn
    CONSTRAINT UQ_PhieuTra_Ma_Don UNIQUE (MaPhieuTra, MaDonBan),
    CONSTRAINT CK_PhieuTra_XacNhan CHECK (
        (TrangThai = N'Đã xác nhận' AND NgayXacNhan IS NOT NULL AND NguoiXacNhan IS NOT NULL)
        OR (TrangThai <> N'Đã xác nhận')
    ),
    CONSTRAINT FK_PhieuTra_DonBan       FOREIGN KEY (MaDonBan)     REFERENCES DonBan(MaDonBan),
    CONSTRAINT FK_PhieuTra_NguoiLap     FOREIGN KEY (NguoiLap)     REFERENCES TaiKhoan(MaTaiKhoan),
    CONSTRAINT FK_PhieuTra_NguoiXacNhan FOREIGN KEY (NguoiXacNhan) REFERENCES TaiKhoan(MaTaiKhoan)
);
GO

-- 15. Chi tiết trả hàng (Dùng khóa ngoại ghép ép trùng MaDonBan với cả PhieuTra và ChiTietBan)
CREATE TABLE ChiTietTra (
    MaPhieuTra   VARCHAR(30)    NOT NULL,
    MaChiTietBan INT            NOT NULL,
    MaDonBan     VARCHAR(30)    NOT NULL,
    SoLuongTra   INT            NOT NULL CHECK (SoLuongTra > 0),
    DonGiaHoan   DECIMAL(18, 2) NOT NULL CHECK (DonGiaHoan > 0), -- Đơn giá sau chiết khấu của đơn bán gốc
    TienHoan     DECIMAL(18, 0) NOT NULL CHECK (TienHoan >= 0),
    PRIMARY KEY (MaPhieuTra, MaChiTietBan),
    CONSTRAINT FK_CTTra_PhieuTra_Don   FOREIGN KEY (MaPhieuTra, MaDonBan)   REFERENCES PhieuTra(MaPhieuTra, MaDonBan),
    CONSTRAINT FK_CTTra_ChiTietBan_Don FOREIGN KEY (MaChiTietBan, MaDonBan) REFERENCES ChiTietBan(MaChiTietBan, MaDonBan)
);
GO

/* PHẦN 2: DỮ LIỆU MẪU KIỂM CHỨNG*/


INSERT INTO VaiTro (MaVaiTro, TenVaiTro) VALUES
('ADMIN',   N'Admin'),
('KHO',     N'Nhân viên kho'),
('BANHANG', N'Nhân viên bán hàng'),
('KETOAN',  N'Kế toán');

-- BƯỚC QUAN TRỌNG TRƯỚC KHI CHIA CHO NHÓM:
-- Chạy hàm Functions.TaoScriptHashMau("123") 1 lần ở Visual Studio (xem Mục 4),
-- rồi thay 5 chuỗi N'THAY_BANG_HASH_...' bên dưới bằng 5 chuỗi PBKDF2-SHA256 có Salt thật.
INSERT INTO TaiKhoan (TenDangNhap, MatKhauHash, HoTen, MaVaiTro, TrangThai) VALUES
('admin',   N'THAY_BANG_HASH_ADMIN',   N'Quản trị viên hệ thống', 'ADMIN',   1),
('kho',     N'THAY_BANG_HASH_KHO',     N'Nguyễn Văn Kho',         'KHO',     1),
('banhang', N'THAY_BANG_HASH_BANHANG', N'Trần Thị Bán Hàng',      'BANHANG', 1),
('ketoan',  N'THAY_BANG_HASH_KETOAN',  N'Lê Thị Kế Toán',         'KETOAN',  1),
('bikhoa',  N'THAY_BANG_HASH_BIKHOA',  N'Tài khoản thử bị khóa',  'BANHANG', 0);

DECLARE @TK_Kho     INT = (SELECT MaTaiKhoan FROM TaiKhoan WHERE TenDangNhap = 'kho');
DECLARE @TK_BanHang INT = (SELECT MaTaiKhoan FROM TaiKhoan WHERE TenDangNhap = 'banhang');

INSERT INTO LoaiHang (MaLoai, TenLoai, TrangThai) VALUES
('LH01', N'Bút viết',           1),
('LH02', N'Giấy và vở',         1),
('LH03', N'Dụng cụ văn phòng',  1);

INSERT INTO DonViTinh (MaDVT, TenDVT, TrangThai) VALUES
('DV01', N'Chiếc', 1),
('DV02', N'Quyển', 1),
('DV03', N'Ram',   1),
('DV04', N'Hộp',   1);

-- Tồn kho khớp chính xác tổng phiếu Đã xác nhận:
--   HH01: +20 (PN001) - 10 (DB001) + 2 (PT001) = 12
--   HH02: +105 (PN001) - 50 (DB002) + 5 (PT002) = 60
--   HH03: 0 (PN002 đang Nháp)   -> Tồn thấp
--   HH04: +5 (PN001) = 5 (<=10) -> Tồn thấp
--   HH05: +8 (PN001) = 8 (Ngừng sử dụng -> không cảnh báo tồn thấp)
INSERT INTO HangHoa (MaHang, TenHang, MaLoai, MaDVT, GiaBan, SoLuongTon, MucTonToiThieu, TrangThai) VALUES
('HH01', N'Bút bi Thiên Long TL-027',  'LH01', 'DV01', 10000, 12, 10, 1),
('HH02', N'Vở kẻ ngang A5 120 trang',  'LH02', 'DV02', 15000, 60, 10, 1),
('HH03', N'Giấy in A4 Double A 80gsm', 'LH02', 'DV03', 65000,  0,  5, 1),
('HH04', N'Kẹp file tài liệu A4',      'LH03', 'DV01', 25000,  5, 10, 1),
('HH05', N'Bút xóa nước (ngừng bán)',  'LH01', 'DV01', 18000,  8,  5, 0);

INSERT INTO KhachHang (MaKH, TenKH, DiaChi, DienThoai, TrangThai) VALUES
('KH01', N'Công ty TNHH An Phát',   N'12 Chùa Bộc, Đống Đa, Hà Nội',   '0901000001', 1),
('KH02', N'Nguyễn Minh Anh',        N'45 Cầu Giấy, Hà Nội',            '0901000002', 1),
('KH03', N'Trường THCS Chu Văn An', N'17 Thụy Khuê, Tây Hồ, Hà Nội',   '0901000003', 1);

INSERT INTO NhaCungCap (MaNCC, TenNCC, DiaChi, DienThoai, TrangThai) VALUES
('NCC01', N'Văn phòng phẩm Minh An', N'88 Giải Phóng, Hà Nội', '0912000001', 1),
('NCC02', N'Công ty Giấy Bãi Bằng',  N'Phù Ninh, Phú Thọ',     '0912000002', 1);

INSERT INTO PhieuNhap (MaPhieuNhap, NgayLap, NgayXacNhan, MaNCC, NguoiLap, NguoiXacNhan, TrangThai, TongTien) VALUES
('PN001', '2026-10-01T08:30:00', '2026-10-01T09:00:00', 'NCC01', @TK_Kho, @TK_Kho, N'Đã xác nhận', 1376000),
('PN002', '2026-11-20T09:15:00', NULL,                  'NCC02', @TK_Kho, NULL,    N'Nháp',        1500000);

INSERT INTO ChiTietNhap (MaPhieuNhap, MaHang, SoLuong, DonGiaNhap, ThanhTien) VALUES
('PN001', 'HH01',  20,  7000,  140000),
('PN001', 'HH02', 105, 10000, 1050000),
('PN001', 'HH04',   5, 18000,   90000),
('PN001', 'HH05',   8, 12000,   96000),
('PN002', 'HH03',  30, 50000, 1500000);

INSERT INTO BaoGia (MaBaoGia, NgayLap, MaKH, NguoiLap, TrangThai, LyDoHuy, TongTien) VALUES
('BG001', '2026-11-05T09:00:00', 'KH01', @TK_BanHang, N'Đã chuyển đơn bán', NULL,           95000),
('BG002', '2026-11-18T10:30:00', 'KH03', @TK_BanHang, N'Khách đặt hàng',    NULL,          171000),
('BG003', '2026-11-21T14:00:00', 'KH02', @TK_BanHang, N'Mới lập',           NULL,           50000),
('BG004', '2026-11-02T16:00:00', 'KH02', @TK_BanHang, N'Đã hủy',            N'Khách đổi ý', 50000);

INSERT INTO ChiTietBaoGia (MaBaoGia, MaHang, SoLuong, DonGia, TyLeChietKhau, ThanhTien) VALUES
('BG001', 'HH01', 10, 10000, 5,  95000),
('BG002', 'HH02', 12, 15000, 5, 171000),
('BG003', 'HH01',  5, 10000, 0,  50000),
('BG004', 'HH04',  2, 25000, 0,  50000);

INSERT INTO DonBan (MaDonBan, NgayLap, NgayXacNhan, MaKH, MaBaoGia, LoaiDon, NguoiLap, NguoiXacNhan, TrangThai, LyDoHuy, TongTien) VALUES
('DB001', '2026-11-05T10:00:00', '2026-11-05T10:15:00', 'KH01', 'BG001', N'Bán lẻ', @TK_BanHang, @TK_BanHang, N'Đã xác nhận', NULL,                    95000),
('DB002', '2026-10-20T14:00:00', '2026-10-20T14:10:00', 'KH03', NULL,    N'Bán sỉ', @TK_BanHang, @TK_BanHang, N'Đã xác nhận', NULL,                   675000),
('DB003', '2026-11-22T09:00:00', NULL,                  'KH02', NULL,    N'Bán lẻ', @TK_BanHang, NULL,        N'Nháp',        NULL,                    50000),
('DB004', '2026-11-12T15:00:00', NULL,                  'KH02', NULL,    N'Bán lẻ', @TK_BanHang, NULL,        N'Đã hủy',      N'Khách hủy trước giao', 50000);

INSERT INTO ChiTietBan (MaDonBan, MaHang, SoLuong, DonGiaBan, TyLeChietKhau, ThanhTien) VALUES
('DB001', 'HH01', 10, 10000,  5,  95000),
('DB002', 'HH02', 50, 15000, 10, 675000),
('DB003', 'HH01',  5, 10000,  0,  50000),
('DB004', 'HH04',  2, 25000,  0,  50000);

DECLARE @CTB_DB001_HH01 INT = (SELECT MaChiTietBan FROM ChiTietBan WHERE MaDonBan = 'DB001' AND MaHang = 'HH01');
DECLARE @CTB_DB002_HH02 INT = (SELECT MaChiTietBan FROM ChiTietBan WHERE MaDonBan = 'DB002' AND MaHang = 'HH02');

INSERT INTO PhieuTra (MaPhieuTra, NgayLap, NgayXacNhan, MaDonBan, NguoiLap, NguoiXacNhan, TrangThai, LyDoTra, TongTienHoan) VALUES
('PT001', '2026-11-08T11:00:00', '2026-11-08T11:30:00', 'DB001', @TK_BanHang, @TK_Kho, N'Đã xác nhận', N'Khách mua dư 2 chiếc',       19000),
('PT002', '2026-11-03T09:00:00', '2026-11-03T09:20:00', 'DB002', @TK_BanHang, @TK_Kho, N'Đã xác nhận', N'Khách trả bớt 5 quyển thừa', 67500),
('PT003', '2026-11-22T10:30:00', NULL,                  'DB001', @TK_BanHang, NULL,    N'Nháp',        N'Đổi sang màu mực khác',       9500);

-- Truyền thêm MaDonBan để thỏa mãn cặp khóa ngoại ghép chống trả nhầm đơn
INSERT INTO ChiTietTra (MaPhieuTra, MaChiTietBan, MaDonBan, SoLuongTra, DonGiaHoan, TienHoan) VALUES
('PT001', @CTB_DB001_HH01, 'DB001', 2,  9500.00, 19000),
('PT002', @CTB_DB002_HH02, 'DB002', 5, 13500.00, 67500),
('PT003', @CTB_DB001_HH01, 'DB001', 1,  9500.00,  9500);
GO
--   PHẦN 3: TRUY VẤN KIỂM CHỨNG NHANH
SELECT MaHang, TenHang, GiaBan, SoLuongTon, MucTonToiThieu, TrangThai FROM HangHoa;

SELECT
    db.MaDonBan,
    db.TongTien AS TienBan,
    ISNULL(SUM(pt.TongTienHoan), 0) AS TienHoanDaXacNhan,
    db.TongTien - ISNULL(SUM(pt.TongTienHoan), 0) AS DoanhThuThuan
FROM DonBan db
LEFT JOIN PhieuTra pt ON pt.MaDonBan = db.MaDonBan AND pt.TrangThai = N'Đã xác nhận'
WHERE db.MaDonBan = 'DB001'
GROUP BY db.MaDonBan, db.TongTien;
GO

SET NOEXEC OFF;
GO