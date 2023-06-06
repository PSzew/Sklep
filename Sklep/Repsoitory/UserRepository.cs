using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sklep.entity;
using Microsoft.SqlServer.Server;
using Microsoft.Data.SqlClient;
using System.Data;
using System.Collections.ObjectModel;
using Sklep.Entity;
using System.Diagnostics;

namespace Sklep.Repsoitory
{
    class UserRepository
    {
        SqlConnection connection =
            new SqlConnection(DbInfo.path);
        public User LoginUser(User u)
        {
            SqlCommand Login = new SqlCommand(@"SELECT * FROM users WHERE Login = '"+u.Login+ "' AND Password COLLATE Latin1_General_CS_AS = '" + u.Password+"'", connection);
            Login.CommandType = CommandType.Text;
            User user = new User();
            bool isAdmin2;
            connection.Open();
            using (SqlDataReader reader = Login.ExecuteReader())
            {
                while (reader.Read())
                {
                    int? ID = (int)reader["ID"];
                    string? Name = reader["Name"].ToString()?.TrimEnd();
                    string? Surname = reader["Surname"].ToString()?.TrimEnd();
                    string? login = reader["Login"].ToString()?.TrimEnd();
                    string? email = reader["Email"].ToString()?.TrimEnd();
                    string? password = reader["Password"].ToString()?.TrimEnd();
                    int? isAdmin = (int)reader["isAdmin"];
                    if (isAdmin == 1)
                    {
                        isAdmin2 = true;
                    }
                    else
                        isAdmin2 = false;
                    user = new User(ID, Name, Surname, login, email,password,isAdmin2);
                }
            }
            if (string.IsNullOrEmpty(user.Name))
                user = null;
            connection.Close();
            return user;
        }
    }
}
