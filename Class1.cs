using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Login_system
{
    public class User
    {
        public string Username { get; set; }
        public string Password { get; set; }

        public User(string Username, string Password) {
            this.Username = Username;
            this.Password = Password;
        
        }
    }
}
