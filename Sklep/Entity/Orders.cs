using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sklep.Entity
{
    public class Orders
    {
        public int ID { get; set; }
        public int UserID { get; set; }
        public decimal Price { get; set; }
        public string City { get; set; }
        public string ZipCode { get; set; }
        public int Phone { get; set; }
        public string Adres { get; set; }

        public Orders(int iD, int userID, decimal price, string city, string zipCode, int phone, string adres  )
        {
            ID = iD;
            UserID = userID;
            Price = price;
            City = city;
            ZipCode = zipCode;
            Phone = phone;
            Adres = adres;
        }

        public Orders(int userID, decimal price, string city, string zipCode, int phone, string adres)
        {
            UserID = userID;
            Price = price;
            City = city;
            ZipCode = zipCode;
            Phone = phone;
            Adres = adres;
        }
    }
}
