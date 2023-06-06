using Sklep.entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sklep.Entity
{
    public class Cart
    {
        public int? Id { get; set; }
        public Product? Product_id { get; set; }
        public int? Quantity { get; set; }
        public int? user_ID { get; set; }

        public Cart(int? id, Product? product_id, int? quantity, int? user)
        {
            Id = id;
            Product_id = product_id;
            Quantity = quantity;
            user_ID = user;
        }
        public Cart(Product? product_id, int? quantity ,int? user)
        {
            Product_id = product_id;
            Quantity = quantity;
            this.user_ID = user;
        }
        public Cart()
        {

        }

    }
}
