using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using Sunny.UI;
using System.Data.SqlClient;
using BCrypt.Net;

namespace QLTV
{
    public partial class Resigin : Form
    {
        public Resigin()
        {
            InitializeComponent();
        }

        private void linkLogin_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            fLogin fLogin = new fLogin();
            fLogin.ShowDialog();
        }
        public bool checkUsername(string username)
        {
            return Regex.IsMatch(username, @"^[a-zA-Z]|\d.{6,24}$");
        }
        public bool checkPassword(string password)
        {
            return Regex.IsMatch(password, @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$");
        }
        public bool checkEmail(string email)
        {
            return Regex.IsMatch(email, @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$");
        }

        SqlCommand SqlCommand;
        SqlDataAdapter SqlDataAdapter;
        Handel handel = new Handel();
        Users Users = new Users();
        private void btnResigin_Click(object sender, EventArgs e)
        {
            string userName = txtName.Text.Trim();
            string pass = txtPass.Text.Trim();
            string email = txtEmail.Text.Trim();
            string fullname = txtFullName.Text.Trim();
            if(userName == "" || pass == "" || email == "")
            {
                MessageBox.Show("Không được để trống thông tin đăng kí!", 
                    "Thông báo", 
                    MessageBoxButtons.OK, 
                    MessageBoxIcon.Stop);
            }
            else
            {
                if (!checkUsername(userName))
                {
                    MessageBox.Show(
                        "Tên đăng ký không được để trống hoặc có các ký tự đặc biệt",
                        "Cảnh báo",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Stop);
                }
                else if (!checkPassword(pass))
                {
                    MessageBox.Show(
                        "Mật khẩu phải có ít nhất 8 ký tự, bao gồm chữ hoa, chữ thường, chữ số và ký tự đặc biệt.",
                        "Cảnh báo",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Stop);
                }
                else if (!checkEmail(email))
                {
                    MessageBox.Show(
                        "Email không hợp lệ. Vui lòng nhập email đúng định dạng.",
                        "Cảnh báo",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Stop);
                }
                
            }
            if(handel.User("select * from Users where Username = '"+userName+"' or Email = '"+email+"'").Count != 0)
            {
                MessageBox.Show(
                        "Tên tài khoản hoặc Email đã tồn tại.",
                        "Cảnh báo",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Question);
            }
            string passwordHash = BCrypt.Net.BCrypt.HashPassword(pass);

            try
            {
                using (SqlConnection sql = connection.Connection())
                {
                    sql.Open();
                    string query = "INSERT INTO Users (Username, PasswordHash, FullName, Email) VALUES (@Username, @Password, @FullName, @Email)";
                    using (SqlCommand cmd = new SqlCommand(query, sql))
                    {
                        cmd.Parameters.AddWithValue("@Username", userName);
                        cmd.Parameters.AddWithValue("@Password", passwordHash);
                        cmd.Parameters.AddWithValue("@FullName", fullname);
                        cmd.Parameters.AddWithValue("@Email", email);

                        cmd.ExecuteNonQuery();
                    }
                }
                MessageBox.Show("Đăng ký thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi trong quá trình đăng ký: " + ex.Message, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Resigin_Load(object sender, EventArgs e)
        {

        }

        private void txtName_TextChanged(object sender, EventArgs e)
        {

        }

        private void uiTextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtPass_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtEmail_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
