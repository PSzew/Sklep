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

namespace Sklep.Repsoitory
{
    internal class CategoryRepository
    {
        SqlConnection connection =
            new SqlConnection(DbInfo.path);

        public int InsertCategory(string? title)
        {
            SqlCommand insert_command =
                new SqlCommand("INSERT INTO " +
                               "category VALUES (@title)", connection);

            insert_command.CommandType = CommandType.Text;
            insert_command.Parameters.AddWithValue("@title", title);
            connection.Open();
            int insert_id = insert_command.ExecuteNonQuery();
            connection.Close();
            return insert_id;
        }
        public ObservableCollection<category> GetAllCategory()
        {
            ObservableCollection<category> Categories = new ObservableCollection<category>();
            SqlCommand select_command = new SqlCommand("SELECT * FROM category", connection);
            select_command.CommandType = CommandType.Text;
            connection.Open();
            using (SqlDataReader reader = select_command.ExecuteReader())
            {
                while (reader.Read())
                {
                    string? title = reader["title"].ToString()?.TrimEnd();
                    int? ID = (int)reader["category_id"];
                    Categories.Add(new category(ID, title));
                }
            }
            connection.Close();
            return Categories;
        }
    }
}
