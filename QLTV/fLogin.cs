using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BCrypt.Net;

namespace QLTV
{
    public partial class fLogin : Form
    {
        public fLogin()
        {
            InitializeComponent();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Forgot forgot = new Forgot();
            forgot.ShowDialog();
        }

        private void fLogin_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn thoát?", "Thông báo",  MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                e.Cancel = false;
            }
            else
            {
                e.Cancel = true;
            }
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Resigin resigin = new Resigin();
            resigin.ShowDialog();
        }
        Handel handel = new Handel();
        private void btnLogin_Click(object sender, EventArgs e)
        {
            string userName = txtName.Text.Trim();
            string pass = txtPass.Text.Trim();
           
            if(userName == "" || pass == "")
            {
                MessageBox.Show(
                    "Bạn phải nhập tên đăng nhập và mật khẩu.",
                    "Thông báo",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
            else
            {
                string query = $"select * from Users where Username = '{userName}'";
                var user = handel.User(query);
                if(user.Count != 0)
                {
                    string passwordHash = user[0].Password;
                    if(BCrypt.Net.BCrypt.Verify(pass, passwordHash))
                    {
                        MessageBox.Show("Đăng nhập thành công");
                    }
                    else
                    {
                        MessageBox.Show("Mật khẩu không chính xác");
                    }
                }
                else
                {
                    MessageBox.Show("Tên đăng nhập hoặc mật khẩu khopong chính xác!");
                }
            }
        }
    }
}
