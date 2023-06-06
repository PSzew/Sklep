using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sklep.Entity
{
    public class User
    {
        public int? ID { get; set; }
        public string? Name { get; set; }
        public string? Surname { get; set; }
        public string? Login { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public bool? isAdmin { get; set; }

        public User(int? iD, string? name, string? surname, string? login, string? email, string? password, bool? isAdmin)
        {
            ID = iD;
            Name = name;
            Surname = surname;
            Login = login;
            Email = email;
            Password = password;
            this.isAdmin = isAdmin;
        }
        public User(string? name, string? surname, string? login, string? email, string? password, bool? isAdmin)
        {
            Name = name;
            Surname = surname;
            Login = login;
            Email = email;
            Password = password;
            this.isAdmin = isAdmin;
        }

        public User(string? login,string? password)
        {
            Login = login;
            Password = password;
        }
        public User()
        {

        }
    }
}
