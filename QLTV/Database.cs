using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLTV
{
    internal class Database
    {
        private string _name;
        private string _email;
        private string _role;
        private DateTime _ngaytao;

        public Database(string name, string email, string role, DateTime ngaytao)
        {
            _name = name;
            _email = email;
            _role = role;
            _ngaytao = ngaytao;
        }
        
        public string Name { get => _name; set => _name = value; }
        public string Email { get => _email; set => _email = value; }
        public string Role { get => _role; set => _role = value; }
        public DateTime Ngaytao { get => _ngaytao; set => _ngaytao = value; }
    }
    class Users
    {
        private string _username;
        private string _password;
        
        public Users() { }

        public Users(string username, string password)
        {
            _username = username;
            _password = password;
            
        }

        public string Username { get => _username; set => _username = value; }
        public string Password { get => _password; set => _password = value; }
    }
    class Email
    {
        private string _email;
        public Email() { }
        public Email(string email)
        {
            _email = email;
        }
        public string email{ get => _email; set => email = value; }

    }
}
