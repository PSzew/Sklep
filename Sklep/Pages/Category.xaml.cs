using System;
using System.Collections.Generic;
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
    /// Logika interakcji dla klasy Category.xaml
    /// </summary>
    public partial class Category : Page
    {
        public Category()
        {
            InitializeComponent();
            CategoryRepository cat = new CategoryRepository();
            ListView.ItemsSource = cat.GetAllCategory(); 

        }

        private void GoToProductList(object sender, MouseButtonEventArgs e)
        {
            #pragma warning disable CS8602 // Wyłuskanie odwołania, które może mieć wartość null.
            var SelectedCateogry = (sender as ListView).SelectedItem;
            #pragma warning restore CS8602 // Wyłuskanie odwołania, które może mieć wartość null.
            if (SelectedCateogry != null) 
            {
                category cat = (category)SelectedCateogry;
                mainFrame.Content = new ProductList(cat);
            }
        }

        private void mainFrame_Navigated(object sender, NavigationEventArgs e)
        {

        }
    }
}
