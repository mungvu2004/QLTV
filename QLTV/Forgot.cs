using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using BCrypt.Net;

namespace QLTV
{
    public partial class Forgot : Form
    {
        public Forgot()
        {
            InitializeComponent();
        }

        Handel handel = new Handel();

        public bool checkPassword(string password)
        {
            return Regex.IsMatch(password, @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$");
        }
        private void btnForgot_Click(object sender, EventArgs e)
        {
            string email = txtEmail.Text;
            string password = txtForget.Text.Trim();
            string comfirm = txtForgetM.Text.Trim();

            // Kiểm tra email
            if (string.IsNullOrWhiteSpace(email))
            {
                MessageBox.Show(
                    "Bạn chưa nhập email. Vui lòng nhập email!",
                    "Thông báo",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
            else if (string.IsNullOrWhiteSpace(password) || string.IsNullOrWhiteSpace(comfirm))
            {
                // Kiểm tra mật khẩu và xác nhận mật khẩu
                MessageBox.Show(
                    "Bạn chưa nhập mật khẩu. Vui lòng nhập mật khẩu!",
                    "Thông báo",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
            else if (!checkPassword(password) || !checkPassword(comfirm))
            {
                // Kiểm tra độ phức tạp của mật khẩu và xác nhận mật khẩu
                MessageBox.Show(
                    "Mật khẩu phải có ít nhất 8 ký tự, bao gồm chữ hoa, chữ thường, chữ số và ký tự đặc biệt.",
                    "Thông báo",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
            else if (comfirm != password)
            {
                // Kiểm tra nếu mật khẩu và xác nhận mật khẩu không khớp
                MessageBox.Show(
                    "Bạn hãy kiểm tra lại mật khẩu!",
                    "Thông báo",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
            else
            {
                if (handel.CheckEmail(email))
                {
                    MessageBox.Show(
                        "Email bạn nhập không tồn tại!",
                        "Thông báo",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                    return;
                }
                string newPassword = BCrypt.Net.BCrypt.HashPassword(password);

                try
                {

                    using (SqlConnection sql = connection.Connection())
                    {
                        sql.Open();
                        string query = "UPDATE Users SET PasswordHash = @NewPassword WHERE Email = @Email";
                        using (SqlCommand cmd = new SqlCommand(query, sql))
                        {
                            cmd.Parameters.AddWithValue("@NewPassword", newPassword);
                            cmd.Parameters.AddWithValue("@Email", email);
                            cmd.ExecuteNonQuery();
                        }
                    }
                    
                    MessageBox.Show(
                        "Đã đổi mật khẩu thành công vui lòng đăng nhập lại!",
                        "Thông báo",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                    this.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi!!!" + ex.Message,"Lỗi");
                }
            }
            
        }
    }
}
