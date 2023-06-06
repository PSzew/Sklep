using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sklep.Entity
{
    public class OrderedItem
    {
        public int ID { get; set; }
        public int Order_id { get; set; }
        public int Product_id { get; set; }
        public int Quantity { get; set; }
        public OrderedItem(int iD, int prder_id, int product_id, int quantity)
        {
            ID = iD;
            Order_id = prder_id;
            Product_id = product_id;
            Quantity = quantity;
        }
        public OrderedItem(int prder_id, int product_id, int quantity)
        {
            Order_id = prder_id;
            Product_id = product_id;
            Quantity = quantity;
        }
    }
}
