using System;
using System.Windows.Forms;

namespace QuanLyBanHang
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void btnDangnhap_Click(object sender, EventArgs e)
        {
            if (this.txtUser.Text == "teonv" && this.txtPass.Text == "123")
                this.Close();
            else
            {
                // [Fix] Lỗi giao diện chức năng đăng nhập
                //MessageBox.Show("Không đúng tên người dùng ? mật khẩu!!!", "Thông báo");
                // [Fixed]
                MessageBox.Show("Không đúng tên người dùng / mật khẩu!!!", "Thông báo");
                this.txtUser.Focus();
            }
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            DialogResult traloi = MessageBox.Show("Chắc không?", "Trả lời", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);

            if (traloi == DialogResult.OK)
                Application.Exit();
        }

        private void Form2_Load(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }



    }
}
