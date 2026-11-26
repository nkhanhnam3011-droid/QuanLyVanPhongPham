using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLyVanPhongPham.Forms
{
    public partial class frmTrangChu : Form
    {
        public frmTrangChu()
        {
            InitializeComponent();
        }
        public frmTrangChu(string tenDangNhap) : this()
        {
            lblNguoiDung.Text = tenDangNhap;
        }
        private void mnuTrangChu_Click(object sender, EventArgs e)
        {
            
        }
        private void mnuSanPham_Click(object sender, EventArgs e)
        {
            using (frmSanPham frm = new frmSanPham())
            {
                frm.ShowDialog(this);
            }
        }

        private void mnuTaiKhoan_Click(object sender, EventArgs e)
        {
            using (frmQuanLyTaiKhoan frm = new frmQuanLyTaiKhoan()) 
            { 
                frm.ShowDialog(this); 
            }
        }

        private void mnuKhachHang_Click(object sender, EventArgs e)
        {
            using (frmKhachHang frm = new frmKhachHang())
            {
                frm.ShowDialog(this);
            }
        }

        private void mnuNhaCungCap_Click(object sender, EventArgs e)
        {
            using (frmNhaCungCap frm = new frmNhaCungCap())
            {
                frm.ShowDialog(this);
            }
        }

        private void mnuBaoGia_Click(object sender, EventArgs e)
        {
            using (frmBaoGia frm = new frmBaoGia())
            {
                frm.ShowDialog(this);
            }
        }

        private void mnuDonBan_Click(object sender, EventArgs e)
        {
            using (frmDonBan frm = new frmDonBan())
            {
                frm.ShowDialog(this);
            }
        }

        private void mnuPhieuNhap_Click(object sender, EventArgs e)
        {
            using (frmPhieuNhap frm = new frmPhieuNhap())
            {
                frm.ShowDialog(this);
            }
        }

        private void mnuTraHang_Click(object sender, EventArgs e)
        {
            using (frmTraHang frm = new frmTraHang())
            {
                frm.ShowDialog(this);
            }
        }

        private void mnuBCDoanhThu_Click(object sender, EventArgs e)
        {
            using (frmBaoCaoDoanhThu frm = new frmBaoCaoDoanhThu())
            {
                frm.ShowDialog(this);
            }
        }

        private void mnuBCTonKho_Click(object sender, EventArgs e)
        {
            using (frmBaoCaoTonKho frm = new frmBaoCaoTonKho())
            {
                frm.ShowDialog(this);
            }
        }

        private void mnuThoat_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn có muốn thoát phần mềm?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
            //Class.Functions.Disconnect();
            Application.Exit();
        }
    }
}
