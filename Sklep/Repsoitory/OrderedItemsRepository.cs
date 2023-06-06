using Microsoft.Data.SqlClient;
using Sklep.entity;
using Sklep.Entity;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sklep.Repsoitory
{
    public class OrderedItemsRepository
    {

        SqlConnection connection =
            new SqlConnection(DbInfo.path);
        public int Insert(OrderedItem o)
        {
            SqlCommand insert_command =
                new SqlCommand("INSERT INTO ordereditems VALUES (@order,@product,@quantity)", connection);

            insert_command.CommandType = CommandType.Text;
            insert_command.Parameters.AddWithValue("@order", o.Order_id);
            insert_command.Parameters.AddWithValue("@product", o.Product_id);
            insert_command.Parameters.AddWithValue("@quantity", o.Quantity);
            connection.Open();
            int result = insert_command.ExecuteNonQuery();
            connection.Close();
            return result;
        }
        public ObservableCollection<OrderedItemsPlus> GetOrderedItems(Orders o)
        {
            SqlCommand select = new SqlCommand("SELECT * FROM ordereditems INNER JOIN products ON (ordereditems.product_ID = products.product_ID) WHERE order_id = @id", connection);
            select.CommandType = CommandType.Text;
            select.Parameters.AddWithValue("@id", o.ID);
            ObservableCollection<OrderedItemsPlus> orderedItems= new ObservableCollection<OrderedItemsPlus>();
            connection.Open();
            using(SqlDataReader reader = select.ExecuteReader())
            {
                while(reader.Read())
                {
                    int id = (int)reader["ordereditem_id"];
                    int orderid = (int)reader["order_id"];
                    int product = (int)reader["product_id"];
                    int Quantity = (int)reader["quantity"];
                    int? ID = (int)reader["product_id"];
                    string? title = reader["title"].ToString()?.TrimEnd();
                    decimal? price = (decimal)reader["price"];
                    int? quantity = (int)reader["quantity"];
                    string? description = reader["description"].ToString()?.TrimEnd();
                    int? Category = (int)reader["category"];
                    string? img = reader["image"].ToString()?.TrimEnd();
                    OrderedItemsPlus order = new OrderedItemsPlus(id, orderid, new Product(ID, title, price, quantity, description, Category, img), Quantity);
                    orderedItems.Add(order);
                }
            }
            return orderedItems;
        }
        public int DeleteById(int id)
        {
            SqlCommand delete = new SqlCommand("DELETE from ordereditems WHERE order_id = @id", connection);
            delete.Parameters.AddWithValue("@id", id);
            connection.Open();
            int result = delete.ExecuteNonQuery();
            connection.Close();
            return result;
        }
    }
}
