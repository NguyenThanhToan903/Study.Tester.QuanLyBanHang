﻿using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace QuanLyBanHang
{
    public partial class frmKhachHangTheoThanhPho : Form
    {

        //Chuỗi kết nối
        //string strConnectionString = @"Server=.\SQLEXPRESS;Database=QuanLyBanHang;Integrated Security=True";
        //or
        string strConnectionString = @"Data Source=.\SQLEXPRESS;Initial Catalog=QuanLyBanHang;Integrated Security=SSPI";

        //Đối tượng kết nối
        SqlConnection conn = null;
        //Đối tượng đưa dữ liệu vào DataTable dtKhachHang = null;
        SqlDataAdapter daKhachHang = null;
        //Đối tượng hiển thị dữ liệu lên Form
        DataTable dtKhachHang = null;

        //Thêm cho ví dụ 10.5
        //Đối tượng đưa dữ liệu vào DataTable dtThanhPho = null;
        SqlDataAdapter daThanhPho = null;
        //Đối tượng hiển thị dữ liệu lên Form
        DataTable dtThanhPho = null;

        public frmKhachHangTheoThanhPho()
        {
            InitializeComponent();
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        void LoadData()
        {
            try
            {
                //Khởi động kết nối
                conn = new SqlConnection(strConnectionString);

                //Vận chuyển dữ liệu lên DataTable dtThanhPho
                daThanhPho = new SqlDataAdapter("SELECT * FROM ThanhPho", conn);
                dtThanhPho = new DataTable();
                dtThanhPho.Clear();
                daThanhPho.Fill(dtThanhPho);

                //Xóa các đối tượng trong Panel
                //Đưa dữ liệu lên ComboBox
                this.cbThanhPho.DataSource = dtThanhPho;
                this.cbThanhPho.DisplayMember = "TenThanhPho";
                this.cbThanhPho.ValueMember = "ThanhPho";

                #region [Fix] Tieu de ma khach hang tieng Viet
                //Vận chuyển dữ liệu lên DataTable dtKhachHang
                //daKhachHang = new SqlDataAdapter("SELECT  * FROM Khachhang", conn);
                daKhachHang = new SqlDataAdapter("SELECT  MaKH AS [Mã KH], TenCty AS [Tên Công Ty], DiaChi AS [Địa Chỉ], ThanhPho AS [Thành Phố], DienThoai AS [Điện Thoại] FROM Khachhang", conn);
                #endregion
                dtKhachHang = new DataTable();
                dtKhachHang.Clear();
                daKhachHang.Fill(dtKhachHang);
                //Đưa dữ liệu lên DataGridView
                this.dgvKhachHang.DataSource = dtKhachHang;
                //Thay đổi độ rộng cột
                dgvKhachHang.AutoResizeColumns();

                //Đếm số dòng trong datatable dtKhachHang
                #region [Fix] Tong so thong ke khong dung o chuc nang Quan ly khanh hang theo thanh pho
                //int soKH = Convert.ToInt32(dtKhachHang.Compute("COUNT(MAKH)", string.Empty)) + 1;
                int soKH = dtKhachHang.Rows.Count;
                #endregion
                //MessageBox.Show(soKH.ToString(), "Số dòng");
                this.txtTongSoKH.Text = soKH.ToString();

            }
            catch (SqlException)
            {
                MessageBox.Show("Không lấy được nội dung trong table Khachhang. Lỗi rồi!!!");
            }
        }


        private void LoadDataByCity()
        {
            try
            {
                //Khởi động kết nối
                conn = new SqlConnection(strConnectionString);

                //Vận chuyển dữ liệu lên DataTable dtKhachHang
                daKhachHang = new SqlDataAdapter("SELECT * FROM Khachhang WHERE ThanhPho = '" + this.cbThanhPho.SelectedValue.ToString() + "'", conn);
                dtKhachHang = new DataTable();
                dtKhachHang.Clear();
                daKhachHang.Fill(dtKhachHang);
                //Đưa dữ liệu lên DataGridView
                this.dgvKhachHang.DataSource = dtKhachHang;
                //Thay đổi độ rộng cột
                dgvKhachHang.AutoResizeColumns();

                //Đếm số dòng trong datatable dtKhachHang
                int soKH = Convert.ToInt32(dtKhachHang.Compute("COUNT(MAKH)", string.Empty)) + 1;
                this.txtTongSoKH.Text = soKH.ToString();

            }
            catch (SqlException)
            {
                MessageBox.Show("Không lấy được nội dung trong table Khachhang. Lỗi rồi!!!");
            }
        }

        private void frmKhachHangTheoThanhPho_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private void frmKhachHangTheoThanhPho_FormClosing(object sender, FormClosingEventArgs e)
        {
            //Giải phóng tài nguyên
            dtKhachHang.Dispose();
            dtKhachHang = null;
            //hủy kết nối
            conn = null;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            LoadDataByCity();
        }


    }
}
