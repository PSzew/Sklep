using Sklep.Repsoitory;
using System;
using System.Collections.Generic;
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

namespace Sklep.Pages.adminPages
{
    /// <summary>
    /// Logika interakcji dla klasy ProductManagment.xaml
    /// </summary>
    public partial class ProductManagment : Page
    {
        ProductRepository pr = new ProductRepository();
        public ProductManagment()
        {
            InitializeComponent();
            ListView.ItemsSource = pr.Get();
        }

        private void delete(object sender, RoutedEventArgs e)
        {
            var clicked = sender as Button;
            Product p = (Product)clicked.CommandParameter;
            if (p != null) 
            {
                pr.deleteProduct(p);
                ListView.ItemsSource = pr.Get();
            }
        }

        private void edit(object sender, MouseButtonEventArgs e)
        {
            Product p = (Product)ListView.SelectedItem;
            if (p != null) 
            {
                mainFrame.Content = new AddProduct(p);
            }
        }
    }
}
