using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using Sklep.entity;
using Sklep.Entity;
using Sklep.Pages;

namespace Sklep.Repsoitory
{
    public class CartRepository
    {
        SqlConnection connection =
            new SqlConnection(DbInfo.path);
        public ObservableCollection<Entity.Cart> GetCart(int id)
        {
            ObservableCollection<Entity.Cart> Cart = new ObservableCollection<Entity.Cart>();

            SqlCommand select_command = new SqlCommand("SELECT * FROM cart INNER JOIN products ON (cart.product_ID = products.product_ID) WHERE user_ID = @id", connection);
            select_command.Parameters.AddWithValue("@id", id);
            connection.Open();
            using (SqlDataReader reader = select_command.ExecuteReader())
            {
                while (reader.Read())
                {
                    int? cartID = (int)reader["cart_id"];
                    int? cartquantity = (int)reader["inCartQuantity"];
                    int? ID = (int)reader["product_id"];
                    string? title = reader["title"].ToString()?.TrimEnd();
                    decimal? price = (decimal)reader["price"];
                    int? quantity = (int)reader["quantity"];
                    string? description = reader["description"].ToString()?.TrimEnd();
                    int? cat = (int)reader["category"];                    
                    string? img = reader["image"].ToString()?.TrimEnd();
                    int? user = (int)reader["user_ID"];
                    Cart.Add(new Entity.Cart(cartID, new Product(ID,title, price, quantity, description, cat, img),cartquantity,user));
                }
            }
            connection.Close();      
            return Cart;
        }
        public int AddToCart(User u,Product p)
        {
            SqlCommand insert_command = new SqlCommand("INSERT INTO " +
                               "cart VALUES (@product,1,@user)", connection);
            insert_command.Parameters.AddWithValue("@product", p.ID);
            insert_command.Parameters.AddWithValue("@user", u.ID);
            connection.Open();
            int result = insert_command.ExecuteNonQuery();
            connection.Close();
            return result;
        }
        public int UpdateCart(Entity.Cart cartold,int quantity)
        {
            SqlCommand update_command = new SqlCommand("Update cart SET InCartQuantity = @quantity WHERE cart_ID = @id", connection);
            update_command.Parameters.AddWithValue("@quantity", quantity);
            update_command.Parameters.AddWithValue("@id", cartold.Id);
            connection.Open();
            int result =update_command.ExecuteNonQuery();
            connection.Close();
            return result;
        }
        public int DeleteFromCart(Entity.Cart cart)
        {
            SqlCommand update_command = new SqlCommand("DELETE FROM cart WHERE cart_ID = @id", connection);
            update_command.Parameters.AddWithValue("@id", cart.Id);
            connection.Open();
            int result = update_command.ExecuteNonQuery();
            connection.Close();
            return result;
        }
        public int DeleteCart(User u)
        {
            SqlCommand update_command = new SqlCommand("DELETE FROM cart WHERE user_ID = @id", connection);
            update_command.Parameters.AddWithValue("@id", u.ID);
            connection.Open();
            int result = update_command.ExecuteNonQuery();
            connection.Close();
            return result;
        }
    }
}
