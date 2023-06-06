using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace Sklep.entity
{
    public class Product
    {
        public int? ID { get; set; }
        public string? Title { get; set; }
        public decimal? Price { get; set; }
        public int? Quantity { get; set; }
        public string? Description { get; set; }
        public BitmapImage? Image { get; set; }
        public int? Category { get; set; }


        public Product(int? iD, string? title, decimal? price, int? quantity, string? description, int? category, string? image)
        {
            ID = iD;
            Title = title;
            Price = price;
            Quantity = quantity;
            Description = description;
            Category = category;
            string projectPath = System.IO.Path.GetDirectoryName(System.AppDomain.CurrentDomain.BaseDirectory);
            string finalpath = System.IO.Path.GetDirectoryName(System.IO.Path.GetDirectoryName(System.IO.Path.GetDirectoryName(projectPath)));
            string imageFinal = finalpath + image;
            Image = new BitmapImage(new Uri(imageFinal));
        }
        public Product(string? title, decimal? price, int? quantity, string? description, int? category, string? image)
        {
            Title = title;
            Price = price;
            Quantity = quantity;
            Description = description;
            Category = category;
            string projectPath = System.IO.Path.GetDirectoryName(System.AppDomain.CurrentDomain.BaseDirectory);
            string finalpath = System.IO.Path.GetDirectoryName(System.IO.Path.GetDirectoryName(System.IO.Path.GetDirectoryName(projectPath)));
            string imageFinal = finalpath + image;
            Image = new BitmapImage(new Uri(imageFinal));
        }
    }
}
