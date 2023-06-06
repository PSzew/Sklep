using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Sklep.entity;
using Sklep.Entity;
using Sklep.Repsoitory;

namespace Sklep.Pages
{
    /// <summary>
    /// Logika interakcji dla klasy ProductList.xaml
    /// </summary>
    public partial class ProductList : Page
    {
        ProductRepository p = new ProductRepository();
        category? Cat;
        ObservableCollection<Product> products;
        public ProductList()
        {
            InitializeComponent();
            Cat= null;
            
        }
        public ProductList(category cat)
        {
            InitializeComponent();
            Cat = cat;
            products = p.GetByCategory(cat);
            ListView.ItemsSource =products;
            
        }

        private void addToCart(object sender, RoutedEventArgs e)
        {
            if (Sesion.sesion == null)
            {
                mainFrame.Content = new Login();
            }
            else
            {
                var cliced = sender as Button;
                Product? product = cliced.CommandParameter as Product;
                bool updated = false;
                if (product != null)
                {
                    CartRepository cr = new CartRepository();
                    ObservableCollection<Entity.Cart> cart = cr.GetCart((int)Sesion.sesion.ID);
                    foreach(Entity.Cart c in cart)
                    {
                        if (c.Product_id.ID == product.ID)
                        {
                            cr.UpdateCart(c, (int)c.Quantity+1);
                            updated = true;
                        }
                    }
                    if (!updated)
                    {
                        cr.AddToCart(Sesion.sesion, product);
                    }
                        
                }
            }          
        }
    }
}
