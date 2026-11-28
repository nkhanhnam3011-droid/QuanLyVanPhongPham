namespace QuanLyVanPhongPham.Forms
{
    partial class frmTrangChu
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.mnuChinh = new System.Windows.Forms.MenuStrip();
            this.mnuHeThong = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuTrangChu = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuTaiKhoan = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuDangXuat = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuDanhMuc = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuSanPham = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuKhachHang = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuNhaCungCap = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuNghiepVu = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuBaoGia = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuDonBan = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuPhieuNhap = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuTraHang = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuBaoCao = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuBCDoanhThu = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuBCTonKho = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuThoat = new System.Windows.Forms.ToolStripMenuItem();
            this.lblTaiKhoan = new System.Windows.Forms.Label();
            this.flpPhien = new System.Windows.Forms.FlowLayoutPanel();
            this.lblNguoiDung = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lnkDangXuat = new System.Windows.Forms.LinkLabel();
            this.mnuChinh.SuspendLayout();
            this.flpPhien.SuspendLayout();
            this.SuspendLayout();
            // 
            // mnuChinh
            // 
            this.mnuChinh.GripMargin = new System.Windows.Forms.Padding(2, 2, 0, 2);
            this.mnuChinh.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.mnuChinh.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuHeThong,
            this.mnuDanhMuc,
            this.mnuNghiepVu,
            this.mnuBaoCao,
            this.mnuThoat});
            this.mnuChinh.Location = new System.Drawing.Point(0, 0);
            this.mnuChinh.Name = "mnuChinh";
            this.mnuChinh.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.mnuChinh.Size = new System.Drawing.Size(1200, 35);
            this.mnuChinh.TabIndex = 0;
            this.mnuChinh.Text = "menuStrip1";
            // 
            // mnuHeThong
            // 
            this.mnuHeThong.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuTrangChu,
            this.mnuTaiKhoan,
            this.toolStripSeparator1,
            this.mnuDangXuat});
            this.mnuHeThong.Name = "mnuHeThong";
            this.mnuHeThong.Size = new System.Drawing.Size(103, 29);
            this.mnuHeThong.Text = "Hệ thống";
            // 
            // mnuTrangChu
            // 
            this.mnuTrangChu.Name = "mnuTrangChu";
            this.mnuTrangChu.Size = new System.Drawing.Size(253, 34);
            this.mnuTrangChu.Text = "Trang chủ";
            this.mnuTrangChu.Click += new System.EventHandler(this.mnuTrangChu_Click);
            // 
            // mnuTaiKhoan
            // 
            this.mnuTaiKhoan.Name = "mnuTaiKhoan";
            this.mnuTaiKhoan.Size = new System.Drawing.Size(253, 34);
            this.mnuTaiKhoan.Text = "Quản lý tài khoản";
            this.mnuTaiKhoan.Click += new System.EventHandler(this.mnuTaiKhoan_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(250, 6);
            // 
            // mnuDangXuat
            // 
            this.mnuDangXuat.Name = "mnuDangXuat";
            this.mnuDangXuat.Size = new System.Drawing.Size(253, 34);
            this.mnuDangXuat.Text = "Đăng xuất";
            // 
            // mnuDanhMuc
            // 
            this.mnuDanhMuc.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuSanPham,
            this.mnuKhachHang,
            this.mnuNhaCungCap});
            this.mnuDanhMuc.Name = "mnuDanhMuc";
            this.mnuDanhMuc.Size = new System.Drawing.Size(109, 29);
            this.mnuDanhMuc.Text = "Danh mục";
            // 
            // mnuSanPham
            // 
            this.mnuSanPham.Name = "mnuSanPham";
            this.mnuSanPham.Size = new System.Drawing.Size(252, 34);
            this.mnuSanPham.Text = "Văn phòng phẩm";
            this.mnuSanPham.Click += new System.EventHandler(this.mnuSanPham_Click);
            // 
            // mnuKhachHang
            // 
            this.mnuKhachHang.Name = "mnuKhachHang";
            this.mnuKhachHang.Size = new System.Drawing.Size(252, 34);
            this.mnuKhachHang.Text = "Khách hàng";
            this.mnuKhachHang.Click += new System.EventHandler(this.mnuKhachHang_Click);
            // 
            // mnuNhaCungCap
            // 
            this.mnuNhaCungCap.Name = "mnuNhaCungCap";
            this.mnuNhaCungCap.Size = new System.Drawing.Size(252, 34);
            this.mnuNhaCungCap.Text = "Nhà cung cấp";
            this.mnuNhaCungCap.Click += new System.EventHandler(this.mnuNhaCungCap_Click);
            // 
            // mnuNghiepVu
            // 
            this.mnuNghiepVu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuBaoGia,
            this.mnuDonBan,
            this.mnuPhieuNhap,
            this.mnuTraHang});
            this.mnuNghiepVu.Name = "mnuNghiepVu";
            this.mnuNghiepVu.Size = new System.Drawing.Size(110, 29);
            this.mnuNghiepVu.Text = "Nghiệp vụ";
            // 
            // mnuBaoGia
            // 
            this.mnuBaoGia.Name = "mnuBaoGia";
            this.mnuBaoGia.Size = new System.Drawing.Size(272, 34);
            this.mnuBaoGia.Text = "Báo giá và đặt hàng";
            this.mnuBaoGia.Click += new System.EventHandler(this.mnuBaoGia_Click);
            // 
            // mnuDonBan
            // 
            this.mnuDonBan.Name = "mnuDonBan";
            this.mnuDonBan.Size = new System.Drawing.Size(272, 34);
            this.mnuDonBan.Text = "Đơn bán";
            this.mnuDonBan.Click += new System.EventHandler(this.mnuDonBan_Click);
            // 
            // mnuPhieuNhap
            // 
            this.mnuPhieuNhap.Name = "mnuPhieuNhap";
            this.mnuPhieuNhap.Size = new System.Drawing.Size(272, 34);
            this.mnuPhieuNhap.Text = "Phiếu nhập kho";
            this.mnuPhieuNhap.Click += new System.EventHandler(this.mnuPhieuNhap_Click);
            // 
            // mnuTraHang
            // 
            this.mnuTraHang.Name = "mnuTraHang";
            this.mnuTraHang.Size = new System.Drawing.Size(272, 34);
            this.mnuTraHang.Text = "Phiếu trả hàng";
            this.mnuTraHang.Click += new System.EventHandler(this.mnuTraHang_Click);
            // 
            // mnuBaoCao
            // 
            this.mnuBaoCao.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuBCDoanhThu,
            this.mnuBCTonKho});
            this.mnuBaoCao.Name = "mnuBaoCao";
            this.mnuBaoCao.Size = new System.Drawing.Size(91, 29);
            this.mnuBaoCao.Text = "Báo cáo";
            // 
            // mnuBCDoanhThu
            // 
            this.mnuBCDoanhThu.Name = "mnuBCDoanhThu";
            this.mnuBCDoanhThu.Size = new System.Drawing.Size(378, 34);
            this.mnuBCDoanhThu.Text = "Doanh thu và mặt hàng bán chạy";
            this.mnuBCDoanhThu.Click += new System.EventHandler(this.mnuBCDoanhThu_Click);
            // 
            // mnuBCTonKho
            // 
            this.mnuBCTonKho.Name = "mnuBCTonKho";
            this.mnuBCTonKho.Size = new System.Drawing.Size(378, 34);
            this.mnuBCTonKho.Text = "Trạng thái đơn và hàng tồn";
            this.mnuBCTonKho.Click += new System.EventHandler(this.mnuBCTonKho_Click);
            // 
            // mnuThoat
            // 
            this.mnuThoat.Name = "mnuThoat";
            this.mnuThoat.Size = new System.Drawing.Size(73, 29);
            this.mnuThoat.Text = "Thoát";
            this.mnuThoat.Click += new System.EventHandler(this.mnuThoat_Click);
            // 
            // lblTaiKhoan
            // 
            this.lblTaiKhoan.AutoSize = true;
            this.lblTaiKhoan.ForeColor = System.Drawing.Color.MidnightBlue;
            this.lblTaiKhoan.Location = new System.Drawing.Point(4, 0);
            this.lblTaiKhoan.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblTaiKhoan.Name = "lblTaiKhoan";
            this.lblTaiKhoan.Size = new System.Drawing.Size(82, 20);
            this.lblTaiKhoan.TabIndex = 1;
            this.lblTaiKhoan.Text = "Tài khoản:";
            // 
            // flpPhien
            // 
            this.flpPhien.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.flpPhien.AutoSize = true;
            this.flpPhien.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.flpPhien.Controls.Add(this.lblTaiKhoan);
            this.flpPhien.Controls.Add(this.lblNguoiDung);
            this.flpPhien.Controls.Add(this.label2);
            this.flpPhien.Controls.Add(this.lnkDangXuat);
            this.flpPhien.Location = new System.Drawing.Point(904, 9);
            this.flpPhien.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.flpPhien.Name = "flpPhien";
            this.flpPhien.Size = new System.Drawing.Size(278, 20);
            this.flpPhien.TabIndex = 3;
            this.flpPhien.WrapContents = false;
            // 
            // lblNguoiDung
            // 
            this.lblNguoiDung.AutoSize = true;
            this.lblNguoiDung.Location = new System.Drawing.Point(94, 0);
            this.lblNguoiDung.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblNguoiDung.Name = "lblNguoiDung";
            this.lblNguoiDung.Size = new System.Drawing.Size(68, 20);
            this.lblNguoiDung.TabIndex = 7;
            this.lblNguoiDung.Text = "bỏ trống";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(170, 0);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(14, 20);
            this.label2.TabIndex = 6;
            this.label2.Text = "|";
            // 
            // lnkDangXuat
            // 
            this.lnkDangXuat.AutoSize = true;
            this.lnkDangXuat.LinkBehavior = System.Windows.Forms.LinkBehavior.NeverUnderline;
            this.lnkDangXuat.Location = new System.Drawing.Point(192, 0);
            this.lnkDangXuat.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lnkDangXuat.Name = "lnkDangXuat";
            this.lnkDangXuat.Size = new System.Drawing.Size(82, 20);
            this.lnkDangXuat.TabIndex = 4;
            this.lnkDangXuat.TabStop = true;
            this.lnkDangXuat.Text = "Đăng xuất";
            // 
            // frmTrangChu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1200, 692);
            this.Controls.Add(this.flpPhien);
            this.Controls.Add(this.mnuChinh);
            this.MainMenuStrip = this.mnuChinh;
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "frmTrangChu";
            this.Text = "frmTrangChu";
            this.Load += new System.EventHandler(this.frmTrangChu_Load);
            this.mnuChinh.ResumeLayout(false);
            this.mnuChinh.PerformLayout();
            this.flpPhien.ResumeLayout(false);
            this.flpPhien.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip mnuChinh;
        private System.Windows.Forms.ToolStripMenuItem mnuHeThong;
        private System.Windows.Forms.ToolStripMenuItem mnuTrangChu;
        private System.Windows.Forms.ToolStripMenuItem mnuTaiKhoan;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem mnuDangXuat;
        private System.Windows.Forms.ToolStripMenuItem mnuDanhMuc;
        private System.Windows.Forms.ToolStripMenuItem mnuSanPham;
        private System.Windows.Forms.ToolStripMenuItem mnuKhachHang;
        private System.Windows.Forms.ToolStripMenuItem mnuNhaCungCap;
        private System.Windows.Forms.ToolStripMenuItem mnuNghiepVu;
        private System.Windows.Forms.ToolStripMenuItem mnuBaoGia;
        private System.Windows.Forms.ToolStripMenuItem mnuDonBan;
        private System.Windows.Forms.ToolStripMenuItem mnuPhieuNhap;
        private System.Windows.Forms.ToolStripMenuItem mnuTraHang;
        private System.Windows.Forms.ToolStripMenuItem mnuBaoCao;
        private System.Windows.Forms.ToolStripMenuItem mnuBCDoanhThu;
        private System.Windows.Forms.ToolStripMenuItem mnuBCTonKho;
        private System.Windows.Forms.ToolStripMenuItem mnuThoat;
        private System.Windows.Forms.Label lblTaiKhoan;
        private System.Windows.Forms.FlowLayoutPanel flpPhien;
        private System.Windows.Forms.LinkLabel lnkDangXuat;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblNguoiDung;
    }
}