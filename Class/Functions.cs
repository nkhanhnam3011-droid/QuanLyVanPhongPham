using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace QuanLyVanPhongPham.Class
{
    public static class Functions
    {
        public static SqlConnection Conn { get; private set; }

        // 1. Mở kết nối đến cơ sở dữ liệu
        public static void Connect()
        {
            if (Conn == null)
            {
                string chuoiKetNoi =
                    ConfigurationManager.ConnectionStrings["QLVPP"].ConnectionString;

                Conn = new SqlConnection(chuoiKetNoi);
            }

            if (Conn.State == ConnectionState.Broken)
                Conn.Close();

            if (Conn.State == ConnectionState.Closed)
                Conn.Open();
        }

        // 2. Đóng kết nối khi thoát chương trình
        public static void Disconnect()
        {
            if (Conn != null)
            {
                Conn.Close();
                Conn.Dispose();
                Conn = null;
            }
        }

        // 3. Lấy kết quả SELECT và trả về một bảng dữ liệu
        public static DataTable GetDataToTable(string sql, params SqlParameter[] thamSo)
        {
            Connect();

            DataTable bang = new DataTable();

            using (SqlDataAdapter adapter = new SqlDataAdapter(sql, Conn))
            {
                if (thamSo != null)
                    adapter.SelectCommand.Parameters.AddRange(thamSo);

                adapter.Fill(bang);
            }

            return bang;
        }

        // 4. Nạp dữ liệu vào ComboBox
        public static void FillCombo(string sql, ComboBox cbo, string ma, string ten,
                                     params SqlParameter[] thamSo)
        {
            DataTable bang = GetDataToTable(sql, thamSo);

            cbo.ValueMember = ma;
            cbo.DisplayMember = ten;
            cbo.DataSource = bang;
            cbo.SelectedIndex = -1;
        }

        // 5. Kiểm tra mã đã tồn tại chưa
        // Câu SELECT cần trả về bản ghi có mã cần tìm.
        public static bool CheckKey(string sql, params SqlParameter[] thamSo)
        {
            DataTable bang = GetDataToTable(sql, thamSo);
            return bang.Rows.Count > 0;
        }

        // 6. Lấy một giá trị, chẳng hạn tên hàng theo mã hàng
        public static string GetFieldValues(string sql, params SqlParameter[] thamSo)
        {
            Connect();

            using (SqlCommand lenh = new SqlCommand(sql, Conn))
            {
                if (thamSo != null)
                    lenh.Parameters.AddRange(thamSo);

                using (SqlDataReader reader = lenh.ExecuteReader())
                {
                    if (reader.Read())
                        return Convert.ToString(reader[0]);
                }
            }

            return string.Empty;
        }

        // 7. Thực hiện lệnh INSERT, UPDATE hoặc DELETE
        public static int RunSql(string sql, params SqlParameter[] thamSo)
        {
            Connect();

            using (SqlCommand lenh = new SqlCommand(sql, Conn))
            {
                if (thamSo != null)
                    lenh.Parameters.AddRange(thamSo);

                return lenh.ExecuteNonQuery();
            }
        }

        // 8. Xóa bản ghi, giữ tên hàm quen thuộc trong giáo trình
        public static int RunSqlDel(string sql, params SqlParameter[] thamSo)
        {
            return RunSql(sql, thamSo);
        }
    }
}