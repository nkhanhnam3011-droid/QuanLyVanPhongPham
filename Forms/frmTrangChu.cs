using System;
using System.Windows.Forms;

namespace QuanLyVanPhongPham.Forms
{
    public partial class frmTrangChu : Form
    {

        public frmTrangChu(string tenDangNhap) : this()
        {
            lblNguoiDung.Text = tenDangNhap;
        }

        public frmTrangChu()
        {
            InitializeComponent();
        }

        private void frmTrangChu_Load(object sender, EventArgs e)
        {
        }

        private ToolStripItem mucDuocChon;

        private void MoForm(Form formCon, ToolStripMenuItem mucHienTai)
        {
            mucDuocChon = null;
            if (HienForm(formCon, this, mucHienTai) != DialogResult.OK)
                return;

            ToolStripItem dich = mucDuocChon;
            mucDuocChon = null;
            if (dich != null && dich != mnuTrangChu)
                BeginInvoke(new Action(dich.PerformClick));
        }

        private DialogResult HienForm(Form form, Form chuSoHuu, ToolStripMenuItem mucHienTai)
        {
            using (form)
            {
                MenuStrip menu = form.MainMenuStrip;
                if (menu == null)
                {
                    MessageBox.Show("Hãy đặt thuộc tính MainMenuStrip cho " + form.Name + ".");
                    return DialogResult.Cancel;
                }

                GanMenu(form, menu.Items, mucHienTai);
                return form.ShowDialog(chuSoHuu);
            }
        }

        private void GanMenu(Form form, ToolStripItemCollection cacMuc, ToolStripMenuItem mucHienTai)
        {
            foreach (ToolStripItem muc in cacMuc)
            {
                if (muc.Name == "mnuPhien")
                {
                    muc.Text = "Phiên: " + lblNguoiDung.Text;
                    continue;
                }

                ToolStripMenuItem mucCon = muc as ToolStripMenuItem;
                if (mucCon == null)
                    continue;

                ToolStripItem[] timThay = mnuChinh.Items.Find(muc.Name, true);
                ToolStripItem mucGoc = timThay.Length == 0 ? null : timThay[0];
                if (mucGoc != null)
                {
                    mucCon.Available = mucCon.Available && mucGoc.Available;
                    mucCon.Enabled = mucCon.Enabled && mucGoc.Enabled && mucGoc != mucHienTai;
                }

                if (mucCon.HasDropDownItems)
                    GanMenu(form, mucCon.DropDownItems, mucHienTai);
                else if (mucGoc != null)
                    mucCon.Click += (sender, e) =>
                    {
                        if (!mucGoc.Enabled || !mucGoc.Available)
                            return;
                        if (mucGoc == mnuThoat)
                            mnuThoat.PerformClick();
                        else
                        {
                            mucDuocChon = mucGoc;
                            form.DialogResult = DialogResult.OK;
                        }
                    };
            }
        }

        private void mnuTrangChu_Click(object sender, EventArgs e)
        {

        }
        private void mnuSanPham_Click(object sender, EventArgs e)
        {
            MoForm(new frmSanPham(), mnuSanPham);
        }

        private void mnuTaiKhoan_Click(object sender, EventArgs e)
        {
            MoForm(new frmQuanLyTaiKhoan(), mnuTaiKhoan);
        }

        private void mnuKhachHang_Click(object sender, EventArgs e)
        {
            MoForm(new frmKhachHang(), mnuKhachHang);
        }

        private void mnuNhaCungCap_Click(object sender, EventArgs e)
        {
            MoForm(new frmNhaCungCap(), mnuNhaCungCap);
        }

        private void mnuBaoGia_Click(object sender, EventArgs e)
        {
            MoForm(new frmBaoGia(), mnuBaoGia);
        }

        private void mnuDonBan_Click(object sender, EventArgs e)
        {
            MoForm(new frmDonBan(), mnuDonBan);
        }

        private void mnuPhieuNhap_Click(object sender, EventArgs e)
        {
            MoForm(new frmPhieuNhap(), mnuPhieuNhap);
        }

        private void mnuTraHang_Click(object sender, EventArgs e)
        {
            MoForm(new frmTraHang(), mnuTraHang);
        }

        private void mnuBCDoanhThu_Click(object sender, EventArgs e)
        {
            MoForm(new frmBaoCaoDoanhThu(), mnuBCDoanhThu);
        }

        private void mnuBCTonKho_Click(object sender, EventArgs e)
        {
            MoForm(new frmBaoCaoTonKho(), mnuBCTonKho);
        }

        private void mnuThoat_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn có muốn thoát phần mềm?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
                Application.Exit();
        }
    }
}
