namespace QuanLyVanPhongPham.Forms
{
    partial class frmPhieuNhap
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
            this.lblTieuDe = new System.Windows.Forms.Label();
            this.grpDanhSach = new System.Windows.Forms.GroupBox();
            this.dgvDanhSachPhieuNhap = new System.Windows.Forms.DataGridView();
            this.btnLamMoi = new System.Windows.Forms.Button();
            this.btnLoc = new System.Windows.Forms.Button();
            this.dtpLocDenNgay = new System.Windows.Forms.DateTimePicker();
            this.lblDenNgay = new System.Windows.Forms.Label();
            this.dtpLocTuNgay = new System.Windows.Forms.DateTimePicker();
            this.chkLocNgay = new System.Windows.Forms.CheckBox();
            this.cboLocTrangThai = new System.Windows.Forms.ComboBox();
            this.lblLocTrangThai = new System.Windows.Forms.Label();
            this.txtTuKhoa = new System.Windows.Forms.TextBox();
            this.lblTuKhoa = new System.Windows.Forms.Label();
            this.grpThongTinChung = new System.Windows.Forms.GroupBox();
            this.txtLyDoHuy = new System.Windows.Forms.TextBox();
            this.lblLyDoHuy = new System.Windows.Forms.Label();
            this.txtNguoiXacNhan = new System.Windows.Forms.TextBox();
            this.lblNguoiXacNhan = new System.Windows.Forms.Label();
            this.txtNguoiLap = new System.Windows.Forms.TextBox();
            this.lblNguoiLap = new System.Windows.Forms.Label();
            this.txtTrangThai = new System.Windows.Forms.TextBox();
            this.lblTrangThai = new System.Windows.Forms.Label();
            this.cboNhaCungCap = new System.Windows.Forms.ComboBox();
            this.lblNhaCungCap = new System.Windows.Forms.Label();
            this.dtpNgayLap = new System.Windows.Forms.DateTimePicker();
            this.lblNgayLap = new System.Windows.Forms.Label();
            this.txtMaPN = new System.Windows.Forms.TextBox();
            this.lblMaPN = new System.Windows.Forms.Label();
            this.grpChiTiet = new System.Windows.Forms.GroupBox();
            this.txtTongTien = new System.Windows.Forms.TextBox();
            this.lblTongTien = new System.Windows.Forms.Label();
            this.dgvChiTietNhap = new System.Windows.Forms.DataGridView();
            this.btnXoaDong = new System.Windows.Forms.Button();
            this.btnThemDong = new System.Windows.Forms.Button();
            this.txtDonGiaNhap = new System.Windows.Forms.TextBox();
            this.lblDonGiaNhap = new System.Windows.Forms.Label();
            this.txtSoLuong = new System.Windows.Forms.TextBox();
            this.lblSoLuong = new System.Windows.Forms.Label();
            this.cboHangHoa = new System.Windows.Forms.ComboBox();
            this.lblHangHoa = new System.Windows.Forms.Label();
            this.btnLapMoi = new System.Windows.Forms.Button();
            this.btnLuu = new System.Windows.Forms.Button();
            this.btnBoQua = new System.Windows.Forms.Button();
            this.btnXacNhan = new System.Windows.Forms.Button();
            this.btnHuy = new System.Windows.Forms.Button();
            this.btnDong = new System.Windows.Forms.Button();
            this.grpDanhSach.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDanhSachPhieuNhap)).BeginInit();
            this.grpThongTinChung.SuspendLayout();
            this.grpChiTiet.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvChiTietNhap)).BeginInit();
            this.SuspendLayout();
            // 
            // lblTieuDe
            // 
            this.lblTieuDe.AutoSize = true;
            this.lblTieuDe.Font = new System.Drawing.Font("Segoe UI", 13F, System.Drawing.FontStyle.Bold);
            this.lblTieuDe.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(54)))), ((int)(((byte)(83)))));
            this.lblTieuDe.Location = new System.Drawing.Point(14, 8);
            this.lblTieuDe.Name = "lblTieuDe";
            this.lblTieuDe.Size = new System.Drawing.Size(358, 36);
            this.lblTieuDe.TabIndex = 0;
            this.lblTieuDe.Text = "QUẢN LÝ PHIẾU NHẬP KHO";
            // 
            // grpDanhSach
            // 
            this.grpDanhSach.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grpDanhSach.Controls.Add(this.dgvDanhSachPhieuNhap);
            this.grpDanhSach.Controls.Add(this.btnLamMoi);
            this.grpDanhSach.Controls.Add(this.btnLoc);
            this.grpDanhSach.Controls.Add(this.dtpLocDenNgay);
            this.grpDanhSach.Controls.Add(this.lblDenNgay);
            this.grpDanhSach.Controls.Add(this.dtpLocTuNgay);
            this.grpDanhSach.Controls.Add(this.chkLocNgay);
            this.grpDanhSach.Controls.Add(this.cboLocTrangThai);
            this.grpDanhSach.Controls.Add(this.lblLocTrangThai);
            this.grpDanhSach.Controls.Add(this.txtTuKhoa);
            this.grpDanhSach.Controls.Add(this.lblTuKhoa);
            this.grpDanhSach.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold);
            this.grpDanhSach.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(78)))), ((int)(((byte)(140)))));
            this.grpDanhSach.Location = new System.Drawing.Point(14, 38);
            this.grpDanhSach.Name = "grpDanhSach";
            this.grpDanhSach.Size = new System.Drawing.Size(1060, 192);
            this.grpDanhSach.TabIndex = 1;
            this.grpDanhSach.TabStop = false;
            this.grpDanhSach.Text = "1. Tìm kiếm, Lọc và Danh sách phiếu nhập";
            // 
            // dgvDanhSachPhieuNhap
            // 
            this.dgvDanhSachPhieuNhap.AllowUserToAddRows = false;
            this.dgvDanhSachPhieuNhap.AllowUserToDeleteRows = false;
            this.dgvDanhSachPhieuNhap.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvDanhSachPhieuNhap.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvDanhSachPhieuNhap.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDanhSachPhieuNhap.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.dgvDanhSachPhieuNhap.Location = new System.Drawing.Point(14, 88);
            this.dgvDanhSachPhieuNhap.MultiSelect = false;
            this.dgvDanhSachPhieuNhap.Name = "dgvDanhSachPhieuNhap";
            this.dgvDanhSachPhieuNhap.ReadOnly = true;
            this.dgvDanhSachPhieuNhap.RowHeadersWidth = 62;
            this.dgvDanhSachPhieuNhap.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvDanhSachPhieuNhap.Size = new System.Drawing.Size(1032, 94);
            this.dgvDanhSachPhieuNhap.TabIndex = 10;
            this.dgvDanhSachPhieuNhap.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvDanhSachPhieuNhap_CellClick);
            // 
            // btnLamMoi
            // 
            this.btnLamMoi.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.btnLamMoi.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnLamMoi.Location = new System.Drawing.Point(870, 50);
            this.btnLamMoi.Name = "btnLamMoi";
            this.btnLamMoi.Size = new System.Drawing.Size(115, 32);
            this.btnLamMoi.TabIndex = 9;
            this.btnLamMoi.Text = "Làm mới";
            this.btnLamMoi.UseVisualStyleBackColor = true;
            this.btnLamMoi.Click += new System.EventHandler(this.btnLamMoi_Click);
            // 
            // btnLoc
            // 
            this.btnLoc.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.btnLoc.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnLoc.Location = new System.Drawing.Point(765, 50);
            this.btnLoc.Name = "btnLoc";
            this.btnLoc.Size = new System.Drawing.Size(95, 32);
            this.btnLoc.TabIndex = 8;
            this.btnLoc.Text = "Lọc";
            this.btnLoc.UseVisualStyleBackColor = true;
            this.btnLoc.Click += new System.EventHandler(this.btnLoc_Click);
            // 
            // dtpLocDenNgay
            // 
            this.dtpLocDenNgay.CustomFormat = "dd/MM/yyyy";
            this.dtpLocDenNgay.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.dtpLocDenNgay.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpLocDenNgay.Location = new System.Drawing.Point(596, 52);
            this.dtpLocDenNgay.Name = "dtpLocDenNgay";
            this.dtpLocDenNgay.Size = new System.Drawing.Size(155, 33);
            this.dtpLocDenNgay.TabIndex = 7;
            // 
            // lblDenNgay
            // 
            this.lblDenNgay.AutoSize = true;
            this.lblDenNgay.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.lblDenNgay.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblDenNgay.Location = new System.Drawing.Point(596, 28);
            this.lblDenNgay.Name = "lblDenNgay";
            this.lblDenNgay.Size = new System.Drawing.Size(99, 28);
            this.lblDenNgay.TabIndex = 6;
            this.lblDenNgay.Text = "Đến ngày:";
            // 
            // dtpLocTuNgay
            // 
            this.dtpLocTuNgay.CustomFormat = "dd/MM/yyyy";
            this.dtpLocTuNgay.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.dtpLocTuNgay.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpLocTuNgay.Location = new System.Drawing.Point(427, 52);
            this.dtpLocTuNgay.Name = "dtpLocTuNgay";
            this.dtpLocTuNgay.Size = new System.Drawing.Size(155, 33);
            this.dtpLocTuNgay.TabIndex = 5;
            // 
            // chkLocNgay
            // 
            this.chkLocNgay.AutoSize = true;
            this.chkLocNgay.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.chkLocNgay.ForeColor = System.Drawing.SystemColors.ControlText;
            this.chkLocNgay.Location = new System.Drawing.Point(427, 26);
            this.chkLocNgay.Name = "chkLocNgay";
            this.chkLocNgay.Size = new System.Drawing.Size(159, 32);
            this.chkLocNgay.TabIndex = 4;
            this.chkLocNgay.Text = "Từ ngày (Lọc):";
            this.chkLocNgay.UseVisualStyleBackColor = true;
            this.chkLocNgay.CheckedChanged += new System.EventHandler(this.chkLocNgay_CheckedChanged);
            // 
            // cboLocTrangThai
            // 
            this.cboLocTrangThai.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboLocTrangThai.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.cboLocTrangThai.ForeColor = System.Drawing.SystemColors.ControlText;
            this.cboLocTrangThai.FormattingEnabled = true;
            this.cboLocTrangThai.Location = new System.Drawing.Point(258, 52);
            this.cboLocTrangThai.Name = "cboLocTrangThai";
            this.cboLocTrangThai.Size = new System.Drawing.Size(155, 36);
            this.cboLocTrangThai.TabIndex = 3;
            // 
            // lblLocTrangThai
            // 
            this.lblLocTrangThai.AutoSize = true;
            this.lblLocTrangThai.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.lblLocTrangThai.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblLocTrangThai.Location = new System.Drawing.Point(258, 28);
            this.lblLocTrangThai.Name = "lblLocTrangThai";
            this.lblLocTrangThai.Size = new System.Drawing.Size(102, 28);
            this.lblLocTrangThai.TabIndex = 2;
            this.lblLocTrangThai.Text = "Trạng thái:";
            // 
            // txtTuKhoa
            // 
            this.txtTuKhoa.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.txtTuKhoa.ForeColor = System.Drawing.SystemColors.ControlText;
            this.txtTuKhoa.Location = new System.Drawing.Point(14, 52);
            this.txtTuKhoa.Name = "txtTuKhoa";
            this.txtTuKhoa.Size = new System.Drawing.Size(230, 33);
            this.txtTuKhoa.TabIndex = 1;
            // 
            // lblTuKhoa
            // 
            this.lblTuKhoa.AutoSize = true;
            this.lblTuKhoa.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.lblTuKhoa.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblTuKhoa.Location = new System.Drawing.Point(14, 28);
            this.lblTuKhoa.Name = "lblTuKhoa";
            this.lblTuKhoa.Size = new System.Drawing.Size(187, 28);
            this.lblTuKhoa.TabIndex = 0;
            this.lblTuKhoa.Text = "Từ khóa (Mã / NCC):";
            // 
            // grpThongTinChung
            // 
            this.grpThongTinChung.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grpThongTinChung.Controls.Add(this.txtLyDoHuy);
            this.grpThongTinChung.Controls.Add(this.lblLyDoHuy);
            this.grpThongTinChung.Controls.Add(this.txtNguoiXacNhan);
            this.grpThongTinChung.Controls.Add(this.lblNguoiXacNhan);
            this.grpThongTinChung.Controls.Add(this.txtNguoiLap);
            this.grpThongTinChung.Controls.Add(this.lblNguoiLap);
            this.grpThongTinChung.Controls.Add(this.txtTrangThai);
            this.grpThongTinChung.Controls.Add(this.lblTrangThai);
            this.grpThongTinChung.Controls.Add(this.cboNhaCungCap);
            this.grpThongTinChung.Controls.Add(this.lblNhaCungCap);
            this.grpThongTinChung.Controls.Add(this.dtpNgayLap);
            this.grpThongTinChung.Controls.Add(this.lblNgayLap);
            this.grpThongTinChung.Controls.Add(this.txtMaPN);
            this.grpThongTinChung.Controls.Add(this.lblMaPN);
            this.grpThongTinChung.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold);
            this.grpThongTinChung.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(78)))), ((int)(((byte)(140)))));
            this.grpThongTinChung.Location = new System.Drawing.Point(14, 236);
            this.grpThongTinChung.Name = "grpThongTinChung";
            this.grpThongTinChung.Size = new System.Drawing.Size(1060, 152);
            this.grpThongTinChung.TabIndex = 2;
            this.grpThongTinChung.TabStop = false;
            this.grpThongTinChung.Text = "2. Thông tin chung phiếu nhập";
            // 
            // txtLyDoHuy
            // 
            this.txtLyDoHuy.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtLyDoHuy.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.txtLyDoHuy.ForeColor = System.Drawing.SystemColors.ControlText;
            this.txtLyDoHuy.Location = new System.Drawing.Point(554, 112);
            this.txtLyDoHuy.Name = "txtLyDoHuy";
            this.txtLyDoHuy.ReadOnly = true;
            this.txtLyDoHuy.Size = new System.Drawing.Size(492, 33);
            this.txtLyDoHuy.TabIndex = 13;
            // 
            // lblLyDoHuy
            // 
            this.lblLyDoHuy.AutoSize = true;
            this.lblLyDoHuy.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.lblLyDoHuy.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblLyDoHuy.Location = new System.Drawing.Point(554, 88);
            this.lblLyDoHuy.Name = "lblLyDoHuy";
            this.lblLyDoHuy.Size = new System.Drawing.Size(100, 28);
            this.lblLyDoHuy.TabIndex = 12;
            this.lblLyDoHuy.Text = "Lý do hủy:";
            // 
            // txtNguoiXacNhan
            // 
            this.txtNguoiXacNhan.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.txtNguoiXacNhan.ForeColor = System.Drawing.SystemColors.ControlText;
            this.txtNguoiXacNhan.Location = new System.Drawing.Point(284, 112);
            this.txtNguoiXacNhan.Name = "txtNguoiXacNhan";
            this.txtNguoiXacNhan.ReadOnly = true;
            this.txtNguoiXacNhan.Size = new System.Drawing.Size(255, 33);
            this.txtNguoiXacNhan.TabIndex = 11;
            this.txtNguoiXacNhan.TextChanged += new System.EventHandler(this.txtNguoiXacNhan_TextChanged);
            // 
            // lblNguoiXacNhan
            // 
            this.lblNguoiXacNhan.AutoSize = true;
            this.lblNguoiXacNhan.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.lblNguoiXacNhan.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblNguoiXacNhan.Location = new System.Drawing.Point(284, 88);
            this.lblNguoiXacNhan.Name = "lblNguoiXacNhan";
            this.lblNguoiXacNhan.Size = new System.Drawing.Size(153, 28);
            this.lblNguoiXacNhan.TabIndex = 10;
            this.lblNguoiXacNhan.Text = "Người xác nhận:";
            // 
            // txtNguoiLap
            // 
            this.txtNguoiLap.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.txtNguoiLap.ForeColor = System.Drawing.SystemColors.ControlText;
            this.txtNguoiLap.Location = new System.Drawing.Point(14, 112);
            this.txtNguoiLap.Name = "txtNguoiLap";
            this.txtNguoiLap.ReadOnly = true;
            this.txtNguoiLap.Size = new System.Drawing.Size(255, 33);
            this.txtNguoiLap.TabIndex = 9;
            // 
            // lblNguoiLap
            // 
            this.lblNguoiLap.AutoSize = true;
            this.lblNguoiLap.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.lblNguoiLap.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblNguoiLap.Location = new System.Drawing.Point(14, 88);
            this.lblNguoiLap.Name = "lblNguoiLap";
            this.lblNguoiLap.Size = new System.Drawing.Size(104, 28);
            this.lblNguoiLap.TabIndex = 8;
            this.lblNguoiLap.Text = "Người lập:";
            // 
            // txtTrangThai
            // 
            this.txtTrangThai.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtTrangThai.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold);
            this.txtTrangThai.ForeColor = System.Drawing.SystemColors.ControlText;
            this.txtTrangThai.Location = new System.Drawing.Point(819, 52);
            this.txtTrangThai.Name = "txtTrangThai";
            this.txtTrangThai.ReadOnly = true;
            this.txtTrangThai.Size = new System.Drawing.Size(227, 33);
            this.txtTrangThai.TabIndex = 7;
            // 
            // lblTrangThai
            // 
            this.lblTrangThai.AutoSize = true;
            this.lblTrangThai.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.lblTrangThai.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblTrangThai.Location = new System.Drawing.Point(819, 28);
            this.lblTrangThai.Name = "lblTrangThai";
            this.lblTrangThai.Size = new System.Drawing.Size(102, 28);
            this.lblTrangThai.TabIndex = 6;
            this.lblTrangThai.Text = "Trạng thái:";
            // 
            // cboNhaCungCap
            // 
            this.cboNhaCungCap.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboNhaCungCap.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.cboNhaCungCap.ForeColor = System.Drawing.SystemColors.ControlText;
            this.cboNhaCungCap.FormattingEnabled = true;
            this.cboNhaCungCap.Location = new System.Drawing.Point(384, 52);
            this.cboNhaCungCap.Name = "cboNhaCungCap";
            this.cboNhaCungCap.Size = new System.Drawing.Size(420, 36);
            this.cboNhaCungCap.TabIndex = 5;
            this.cboNhaCungCap.SelectedIndexChanged += new System.EventHandler(this.cboNhaCungCap_SelectedIndexChanged);
            // 
            // lblNhaCungCap
            // 
            this.lblNhaCungCap.AutoSize = true;
            this.lblNhaCungCap.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.lblNhaCungCap.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblNhaCungCap.Location = new System.Drawing.Point(384, 28);
            this.lblNhaCungCap.Name = "lblNhaCungCap";
            this.lblNhaCungCap.Size = new System.Drawing.Size(136, 28);
            this.lblNhaCungCap.TabIndex = 4;
            this.lblNhaCungCap.Text = "Nhà cung cấp:";
            // 
            // dtpNgayLap
            // 
            this.dtpNgayLap.CustomFormat = "dd/MM/yyyy";
            this.dtpNgayLap.Enabled = false;
            this.dtpNgayLap.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.dtpNgayLap.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpNgayLap.Location = new System.Drawing.Point(214, 52);
            this.dtpNgayLap.Name = "dtpNgayLap";
            this.dtpNgayLap.Size = new System.Drawing.Size(155, 33);
            this.dtpNgayLap.TabIndex = 3;
            // 
            // lblNgayLap
            // 
            this.lblNgayLap.AutoSize = true;
            this.lblNgayLap.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.lblNgayLap.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblNgayLap.Location = new System.Drawing.Point(214, 28);
            this.lblNgayLap.Name = "lblNgayLap";
            this.lblNgayLap.Size = new System.Drawing.Size(95, 28);
            this.lblNgayLap.TabIndex = 2;
            this.lblNgayLap.Text = "Ngày lập:";
            // 
            // txtMaPN
            // 
            this.txtMaPN.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold);
            this.txtMaPN.ForeColor = System.Drawing.SystemColors.ControlText;
            this.txtMaPN.Location = new System.Drawing.Point(14, 52);
            this.txtMaPN.Name = "txtMaPN";
            this.txtMaPN.ReadOnly = true;
            this.txtMaPN.Size = new System.Drawing.Size(185, 33);
            this.txtMaPN.TabIndex = 1;
            // 
            // lblMaPN
            // 
            this.lblMaPN.AutoSize = true;
            this.lblMaPN.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.lblMaPN.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblMaPN.Location = new System.Drawing.Point(14, 28);
            this.lblMaPN.Name = "lblMaPN";
            this.lblMaPN.Size = new System.Drawing.Size(98, 28);
            this.lblMaPN.TabIndex = 0;
            this.lblMaPN.Text = "Mã phiếu:";
            // 
            // grpChiTiet
            // 
            this.grpChiTiet.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grpChiTiet.Controls.Add(this.txtTongTien);
            this.grpChiTiet.Controls.Add(this.lblTongTien);
            this.grpChiTiet.Controls.Add(this.dgvChiTietNhap);
            this.grpChiTiet.Controls.Add(this.btnXoaDong);
            this.grpChiTiet.Controls.Add(this.btnThemDong);
            this.grpChiTiet.Controls.Add(this.txtDonGiaNhap);
            this.grpChiTiet.Controls.Add(this.lblDonGiaNhap);
            this.grpChiTiet.Controls.Add(this.txtSoLuong);
            this.grpChiTiet.Controls.Add(this.lblSoLuong);
            this.grpChiTiet.Controls.Add(this.cboHangHoa);
            this.grpChiTiet.Controls.Add(this.lblHangHoa);
            this.grpChiTiet.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold);
            this.grpChiTiet.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(78)))), ((int)(((byte)(140)))));
            this.grpChiTiet.Location = new System.Drawing.Point(14, 394);
            this.grpChiTiet.Name = "grpChiTiet";
            this.grpChiTiet.Size = new System.Drawing.Size(1060, 248);
            this.grpChiTiet.TabIndex = 3;
            this.grpChiTiet.TabStop = false;
            this.grpChiTiet.Text = "3. Chi tiết mặt hàng nhập kho (Nhấp đúp dòng hoặc bấm Xóa dòng để xóa)";
            // 
            // txtTongTien
            // 
            this.txtTongTien.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.txtTongTien.Font = new System.Drawing.Font("Segoe UI", 10.5F, System.Drawing.FontStyle.Bold);
            this.txtTongTien.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(196)))), ((int)(((byte)(43)))), ((int)(((byte)(28)))));
            this.txtTongTien.Location = new System.Drawing.Point(816, 210);
            this.txtTongTien.Name = "txtTongTien";
            this.txtTongTien.ReadOnly = true;
            this.txtTongTien.Size = new System.Drawing.Size(230, 35);
            this.txtTongTien.TabIndex = 10;
            this.txtTongTien.Text = "0 đ";
            this.txtTongTien.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // lblTongTien
            // 
            this.lblTongTien.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lblTongTien.AutoSize = true;
            this.lblTongTien.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblTongTien.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblTongTien.Location = new System.Drawing.Point(715, 214);
            this.lblTongTien.Name = "lblTongTien";
            this.lblTongTien.Size = new System.Drawing.Size(108, 28);
            this.lblTongTien.TabIndex = 9;
            this.lblTongTien.Text = "Tổng tiền:";
            // 
            // dgvChiTietNhap
            // 
            this.dgvChiTietNhap.AllowUserToAddRows = false;
            this.dgvChiTietNhap.AllowUserToDeleteRows = false;
            this.dgvChiTietNhap.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvChiTietNhap.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvChiTietNhap.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvChiTietNhap.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.dgvChiTietNhap.Location = new System.Drawing.Point(14, 88);
            this.dgvChiTietNhap.MultiSelect = false;
            this.dgvChiTietNhap.Name = "dgvChiTietNhap";
            this.dgvChiTietNhap.ReadOnly = true;
            this.dgvChiTietNhap.RowHeadersWidth = 62;
            this.dgvChiTietNhap.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvChiTietNhap.Size = new System.Drawing.Size(1032, 114);
            this.dgvChiTietNhap.TabIndex = 8;
            this.dgvChiTietNhap.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvChiTietNhap_CellClick);
            this.dgvChiTietNhap.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvChiTietNhap_CellDoubleClick);
            // 
            // btnXoaDong
            // 
            this.btnXoaDong.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.btnXoaDong.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnXoaDong.Location = new System.Drawing.Point(900, 50);
            this.btnXoaDong.Name = "btnXoaDong";
            this.btnXoaDong.Size = new System.Drawing.Size(130, 32);
            this.btnXoaDong.TabIndex = 7;
            this.btnXoaDong.Text = "Xóa dòng";
            this.btnXoaDong.UseVisualStyleBackColor = true;
            this.btnXoaDong.Click += new System.EventHandler(this.btnXoaDong_Click);
            // 
            // btnThemDong
            // 
            this.btnThemDong.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.btnThemDong.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnThemDong.Location = new System.Drawing.Point(755, 50);
            this.btnThemDong.Name = "btnThemDong";
            this.btnThemDong.Size = new System.Drawing.Size(135, 32);
            this.btnThemDong.TabIndex = 6;
            this.btnThemDong.Text = "Thêm dòng";
            this.btnThemDong.UseVisualStyleBackColor = true;
            this.btnThemDong.Click += new System.EventHandler(this.btnThemDong_Click);
            // 
            // txtDonGiaNhap
            // 
            this.txtDonGiaNhap.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.txtDonGiaNhap.ForeColor = System.Drawing.SystemColors.ControlText;
            this.txtDonGiaNhap.Location = new System.Drawing.Point(552, 52);
            this.txtDonGiaNhap.Name = "txtDonGiaNhap";
            this.txtDonGiaNhap.Size = new System.Drawing.Size(185, 33);
            this.txtDonGiaNhap.TabIndex = 5;
            this.txtDonGiaNhap.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtDonGiaNhap_KeyPress);
            // 
            // lblDonGiaNhap
            // 
            this.lblDonGiaNhap.AutoSize = true;
            this.lblDonGiaNhap.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.lblDonGiaNhap.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblDonGiaNhap.Location = new System.Drawing.Point(552, 28);
            this.lblDonGiaNhap.Name = "lblDonGiaNhap";
            this.lblDonGiaNhap.Size = new System.Drawing.Size(134, 28);
            this.lblDonGiaNhap.TabIndex = 4;
            this.lblDonGiaNhap.Text = "Đơn giá nhập:";
            // 
            // txtSoLuong
            // 
            this.txtSoLuong.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.txtSoLuong.ForeColor = System.Drawing.SystemColors.ControlText;
            this.txtSoLuong.Location = new System.Drawing.Point(418, 52);
            this.txtSoLuong.Name = "txtSoLuong";
            this.txtSoLuong.Size = new System.Drawing.Size(120, 33);
            this.txtSoLuong.TabIndex = 3;
            this.txtSoLuong.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtSoLuong_KeyPress);
            // 
            // lblSoLuong
            // 
            this.lblSoLuong.AutoSize = true;
            this.lblSoLuong.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.lblSoLuong.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblSoLuong.Location = new System.Drawing.Point(418, 28);
            this.lblSoLuong.Name = "lblSoLuong";
            this.lblSoLuong.Size = new System.Drawing.Size(96, 28);
            this.lblSoLuong.TabIndex = 2;
            this.lblSoLuong.Text = "Số lượng:";
            // 
            // cboHangHoa
            // 
            this.cboHangHoa.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboHangHoa.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.cboHangHoa.ForeColor = System.Drawing.SystemColors.ControlText;
            this.cboHangHoa.FormattingEnabled = true;
            this.cboHangHoa.Location = new System.Drawing.Point(14, 52);
            this.cboHangHoa.Name = "cboHangHoa";
            this.cboHangHoa.Size = new System.Drawing.Size(390, 36);
            this.cboHangHoa.TabIndex = 1;
            // 
            // lblHangHoa
            // 
            this.lblHangHoa.AutoSize = true;
            this.lblHangHoa.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.lblHangHoa.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblHangHoa.Location = new System.Drawing.Point(14, 28);
            this.lblHangHoa.Name = "lblHangHoa";
            this.lblHangHoa.Size = new System.Drawing.Size(100, 28);
            this.lblHangHoa.TabIndex = 0;
            this.lblHangHoa.Text = "Mặt hàng:";
            // 
            // btnLapMoi
            // 
            this.btnLapMoi.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnLapMoi.Location = new System.Drawing.Point(14, 650);
            this.btnLapMoi.Name = "btnLapMoi";
            this.btnLapMoi.Size = new System.Drawing.Size(110, 35);
            this.btnLapMoi.TabIndex = 4;
            this.btnLapMoi.Text = "Lập mới";
            this.btnLapMoi.UseVisualStyleBackColor = true;
            this.btnLapMoi.Click += new System.EventHandler(this.btnLapMoi_Click);
            // 
            // btnLuu
            // 
            this.btnLuu.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnLuu.Location = new System.Drawing.Point(134, 650);
            this.btnLuu.Name = "btnLuu";
            this.btnLuu.Size = new System.Drawing.Size(95, 35);
            this.btnLuu.TabIndex = 5;
            this.btnLuu.Text = "Lưu";
            this.btnLuu.UseVisualStyleBackColor = true;
            this.btnLuu.Click += new System.EventHandler(this.btnLuu_Click);
            // 
            // btnBoQua
            // 
            this.btnBoQua.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnBoQua.Location = new System.Drawing.Point(239, 650);
            this.btnBoQua.Name = "btnBoQua";
            this.btnBoQua.Size = new System.Drawing.Size(105, 35);
            this.btnBoQua.TabIndex = 6;
            this.btnBoQua.Text = "Bỏ qua";
            this.btnBoQua.UseVisualStyleBackColor = true;
            this.btnBoQua.Click += new System.EventHandler(this.btnBoQua_Click);
            // 
            // btnXacNhan
            // 
            this.btnXacNhan.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnXacNhan.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold);
            this.btnXacNhan.Location = new System.Drawing.Point(364, 650);
            this.btnXacNhan.Name = "btnXacNhan";
            this.btnXacNhan.Size = new System.Drawing.Size(200, 35);
            this.btnXacNhan.TabIndex = 7;
            this.btnXacNhan.Text = "Xác nhận nhập kho";
            this.btnXacNhan.UseVisualStyleBackColor = true;
            this.btnXacNhan.Click += new System.EventHandler(this.btnXacNhan_Click);
            // 
            // btnHuy
            // 
            this.btnHuy.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnHuy.Location = new System.Drawing.Point(574, 650);
            this.btnHuy.Name = "btnHuy";
            this.btnHuy.Size = new System.Drawing.Size(125, 35);
            this.btnHuy.TabIndex = 8;
            this.btnHuy.Text = "Hủy phiếu";
            this.btnHuy.UseVisualStyleBackColor = true;
            this.btnHuy.Click += new System.EventHandler(this.btnHuy_Click);
            // 
            // btnDong
            // 
            this.btnDong.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDong.Location = new System.Drawing.Point(974, 650);
            this.btnDong.Name = "btnDong";
            this.btnDong.Size = new System.Drawing.Size(100, 35);
            this.btnDong.TabIndex = 9;
            this.btnDong.Text = "Đóng";
            this.btnDong.UseVisualStyleBackColor = true;
            this.btnDong.Click += new System.EventHandler(this.btnDong_Click);
            // 
            // frmPhieuNhap
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(1088, 695);
            this.Controls.Add(this.btnDong);
            this.Controls.Add(this.btnHuy);
            this.Controls.Add(this.btnXacNhan);
            this.Controls.Add(this.btnBoQua);
            this.Controls.Add(this.btnLuu);
            this.Controls.Add(this.btnLapMoi);
            this.Controls.Add(this.grpChiTiet);
            this.Controls.Add(this.grpThongTinChung);
            this.Controls.Add(this.grpDanhSach);
            this.Controls.Add(this.lblTieuDe);
            this.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.MinimumSize = new System.Drawing.Size(1088, 695);
            this.Name = "frmPhieuNhap";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Quản lý Phiếu nhập kho";
            this.Load += new System.EventHandler(this.frmPhieuNhap_Load);
            this.grpDanhSach.ResumeLayout(false);
            this.grpDanhSach.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDanhSachPhieuNhap)).EndInit();
            this.grpThongTinChung.ResumeLayout(false);
            this.grpThongTinChung.PerformLayout();
            this.grpChiTiet.ResumeLayout(false);
            this.grpChiTiet.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvChiTietNhap)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblTieuDe;
        private System.Windows.Forms.GroupBox grpDanhSach;
        private System.Windows.Forms.Label lblTuKhoa;
        private System.Windows.Forms.TextBox txtTuKhoa;
        private System.Windows.Forms.Label lblLocTrangThai;
        private System.Windows.Forms.ComboBox cboLocTrangThai;
        private System.Windows.Forms.CheckBox chkLocNgay;
        private System.Windows.Forms.DateTimePicker dtpLocTuNgay;
        private System.Windows.Forms.Label lblDenNgay;
        private System.Windows.Forms.DateTimePicker dtpLocDenNgay;
        private System.Windows.Forms.Button btnLoc;
        private System.Windows.Forms.Button btnLamMoi;
        private System.Windows.Forms.DataGridView dgvDanhSachPhieuNhap;
        private System.Windows.Forms.GroupBox grpThongTinChung;
        private System.Windows.Forms.Label lblMaPN;
        private System.Windows.Forms.TextBox txtMaPN;
        private System.Windows.Forms.Label lblNgayLap;
        private System.Windows.Forms.DateTimePicker dtpNgayLap;
        private System.Windows.Forms.Label lblNhaCungCap;
        private System.Windows.Forms.ComboBox cboNhaCungCap;
        private System.Windows.Forms.Label lblTrangThai;
        private System.Windows.Forms.TextBox txtTrangThai;
        private System.Windows.Forms.Label lblNguoiLap;
        private System.Windows.Forms.TextBox txtNguoiLap;
        private System.Windows.Forms.Label lblNguoiXacNhan;
        private System.Windows.Forms.TextBox txtNguoiXacNhan;
        private System.Windows.Forms.Label lblLyDoHuy;
        private System.Windows.Forms.TextBox txtLyDoHuy;
        private System.Windows.Forms.GroupBox grpChiTiet;
        private System.Windows.Forms.Label lblHangHoa;
        private System.Windows.Forms.ComboBox cboHangHoa;
        private System.Windows.Forms.Label lblSoLuong;
        private System.Windows.Forms.TextBox txtSoLuong;
        private System.Windows.Forms.Label lblDonGiaNhap;
        private System.Windows.Forms.TextBox txtDonGiaNhap;
        private System.Windows.Forms.Button btnThemDong;
        private System.Windows.Forms.Button btnXoaDong;
        private System.Windows.Forms.DataGridView dgvChiTietNhap;
        private System.Windows.Forms.Label lblTongTien;
        private System.Windows.Forms.TextBox txtTongTien;
        private System.Windows.Forms.Button btnLapMoi;
        private System.Windows.Forms.Button btnLuu;
        private System.Windows.Forms.Button btnBoQua;
        private System.Windows.Forms.Button btnXacNhan;
        private System.Windows.Forms.Button btnHuy;
        private System.Windows.Forms.Button btnDong;
    }
}
