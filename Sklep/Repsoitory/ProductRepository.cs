using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using Sklep.entity;
using Sklep.Entity;

namespace Sklep.Repsoitory
{
    internal class ProductRepository
    {
        SqlConnection connection =
            new SqlConnection(DbInfo.path);
        public int Insert(Product product) 
        {
            SqlCommand insert_command =
                new SqlCommand("INSERT INTO " + 
                               "products VALUES (@title,@price,@quantity,@description,@category,@img)",connection);

            insert_command.CommandType = CommandType.Text;
            insert_command.Parameters.AddWithValue("@title", product.Title);
            insert_command.Parameters.AddWithValue("@price", product.Price);
            insert_command.Parameters.AddWithValue("@quantity", product.Quantity);
            insert_command.Parameters.AddWithValue("@description", product.Description);
            insert_command.Parameters.AddWithValue("@category", product.Category);
            string fileName = System.IO.Path.GetFileName(product.Image.UriSource.LocalPath);
            string finalImage = @"\Images\ProductImages\" + fileName;
            insert_command.Parameters.AddWithValue("@img", finalImage);
            connection.Open();
            int insert_id = insert_command.ExecuteNonQuery();
            connection.Close();
            return insert_id;
        }
        public ObservableCollection<Product> GetByCategory(category cat)
        {
            ObservableCollection <Product> products = new ObservableCollection<Product>();
            SqlCommand select_command = new SqlCommand("SELECT * FROM products WHERE category = @category", connection);  
            select_command.Parameters.AddWithValue("@category",cat.ID);
            connection.Open();
            using (SqlDataReader reader = select_command.ExecuteReader())
            {
                while(reader.Read())
                {
                    int? ID = (int)reader["product_id"];
                    string? title = reader["title"].ToString()?.TrimEnd();
                    decimal? price = (decimal)reader["price"];
                    int? quantity = (int)reader["quantity"];
                    string? description = reader["description"].ToString()?.TrimEnd();
                    int? Category = (int)reader["category"];
                    string? img = reader["image"].ToString()?.TrimEnd();
                    products.Add(new Product(ID, title, price, quantity, description, Category,img));
                }
            }
            connection.Close();

            return products;
        }
        public ObservableCollection<Product> Get()
        {
            ObservableCollection<Product> products = new ObservableCollection<Product>();
            SqlCommand select_command = new SqlCommand("SELECT * FROM products", connection);
            connection.Open();
            using (SqlDataReader reader = select_command.ExecuteReader())
            {
                while (reader.Read())
                {
                    int? ID = (int)reader["product_id"];
                    string? title = reader["title"].ToString()?.TrimEnd();
                    decimal? price = (decimal)reader["price"];
                    int? quantity = (int)reader["quantity"];
                    string? description = reader["description"].ToString()?.TrimEnd();
                    int? Category = (int)reader["category"];
                    string? img = reader["image"].ToString()?.TrimEnd();
                    products.Add(new Product(ID, title, price, quantity, description, Category, img));
                }
            }
            connection.Close();

            return products;
        }
        public int deleteProduct(Product p)
        {
            SqlCommand delete_command = new SqlCommand("DELETE FROM products WHERE product_id = @id", connection);
            delete_command.Parameters.AddWithValue("@id", p.ID);
            connection.Open();
            int result = delete_command.ExecuteNonQuery();
            connection.Close();
            return result;
        }
        public int Update(Product old,Product product)
        {
            SqlCommand update_command =
                new SqlCommand("UPDATE products SET title = @title,price = @price,quantity=@quantity,description=@description,category = @category, image = @img WHERE product_id = @id", connection);

            update_command.CommandType = CommandType.Text;
            update_command.Parameters.AddWithValue("@title", product.Title);
            update_command.Parameters.AddWithValue("@price", product.Price);
            update_command.Parameters.AddWithValue("@quantity", product.Quantity);
            update_command.Parameters.AddWithValue("@description", product.Description);
            update_command.Parameters.AddWithValue("@category", product.Category);
            update_command.Parameters.AddWithValue("@id", old.ID);
            string fileName = System.IO.Path.GetFileName(product.Image.UriSource.LocalPath);
            string finalImage = @"\Images\ProductImages\" + fileName;
            update_command.Parameters.AddWithValue("@img", finalImage);
            connection.Open();
            int insert_id = update_command.ExecuteNonQuery();
            connection.Close();
            return insert_id;
        }
        public int Decrement(Product p ,int quantity)
        {
            SqlCommand update_command = new SqlCommand("UPDATE products SET quantity = @quan WHERE product_id = @id", connection);
            update_command.Parameters.AddWithValue("@quan", p.Quantity - quantity);
            update_command.Parameters.AddWithValue("@id", p.ID);
            connection.Open();
            int result = update_command.ExecuteNonQuery();
            connection.Close();
            return result;
        }
    }
}
