using Sklep.entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sklep.Entity
{
    public class OrderedItemsPlus
    {
        public int ID { get; set; }
        public int Order_id { get; set; }
        public Product Product_id { get; set; }
        public int Quantity { get; set; }

        public OrderedItemsPlus(int iD, int order_id, Product product_id, int quantity)
        {
            ID = iD;
            Order_id = order_id;
            Product_id = product_id;
            Quantity = quantity;
        }
        public OrderedItemsPlus(int order_id, Product product_id, int quantity)
        {
            Order_id = order_id;
            Product_id = product_id;
            Quantity = quantity;
        }
    }
}
