using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sklep.Entity;
using Sklep.entity;
using System.Data;
using System.Collections.ObjectModel;

namespace Sklep.Repsoitory
{
    public class OrdersRepository
    {
        SqlConnection connection =
            new SqlConnection(DbInfo.path);

        public int Insert(Orders o)
        {
            SqlCommand insert_command =
                new SqlCommand("INSERT INTO orders VALUES (@user,@price,@city,@zipcode,@phone,@adres)", connection);

            insert_command.CommandType = CommandType.Text;
            insert_command.Parameters.AddWithValue("@user", Sesion.sesion.ID);
            insert_command.Parameters.AddWithValue("@price", o.Price);
            insert_command.Parameters.AddWithValue("@city", o.City);
            insert_command.Parameters.AddWithValue("@zipcode", o.ZipCode);
            insert_command.Parameters.AddWithValue("@phone", o.Phone);
            insert_command.Parameters.AddWithValue("@adres", o.Adres);
            connection.Open();
            int result = insert_command.ExecuteNonQuery();
            connection.Close();
            return result;
        }
        public int GetId(User u)
        {
            SqlCommand select_Command = new SqlCommand("SELECT order_id FROM orders WHERE user_id = @user ORDER BY order_id DESC",connection);
            select_Command.Parameters.AddWithValue("@user", u.ID);
            int id = 0;
            connection.Open();
            using (SqlDataReader reader = select_Command.ExecuteReader())
            {
                while (reader.Read())
                {
                    id = (int)reader["order_id"];
                    break;
                }
            }
            connection.Close();
            return id;
        }
        public ObservableCollection<Orders> GetOrders()
        {
            SqlCommand select_Command = new SqlCommand("SELECT * FROM orders", connection);
            ObservableCollection<Orders> orders = new ObservableCollection<Orders>();
            connection.Open();
            using (SqlDataReader reader = select_Command.ExecuteReader())
            {
                while (reader.Read())
                {
                    int ID = (int)reader["order_id"]; 
                    int user_id = (int)reader["user_id"];
                    decimal price = (decimal)reader["price"];
                    string city = reader["city"].ToString().TrimEnd();
                    string zipcode = reader["zipcode"].ToString().TrimEnd();
                    int phone = (int)reader["phone"];
                    string adres = reader["adres"].ToString().TrimEnd();
                    Orders o = new Orders(ID, user_id, price, city, zipcode, phone, adres);
                    orders.Add(o);
                }
            }
            connection.Close();
            return orders;
        }
        public int DeleteById(int id)
        {
            SqlCommand delete = new SqlCommand("DELETE from orders WHERE order_id = @id", connection);
            delete.Parameters.AddWithValue("@id", id);
            connection.Open();
            int result = delete.ExecuteNonQuery();
            connection.Close();
            return result;
        }

    }
}
