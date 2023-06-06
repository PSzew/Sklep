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
using static System.Collections.Specialized.BitVector32;
using Sklep.Pages.adminPages;

namespace Sklep.Pages
{
    /// <summary>
    /// Logika interakcji dla klasy AdminPage.xaml
    /// </summary>
    public partial class AdminPage : Page
    {
        Sesion sesion = new Sesion();
        public AdminPage()
        {
            InitializeComponent();
            stackPanel.DataContext = Sesion.sesion;
        }

        private void logout(object sender, RoutedEventArgs e)
        {
            sesion.EndSession();
            mainFrame.Content = new MainPage();
        }

        private void addProduct(object sender, RoutedEventArgs e)
        {
            mainFrame.Content = new AddProduct();
        }

        private void edit(object sender, RoutedEventArgs e)
        {
            mainFrame.Content = new ProductManagment();
        }

        private void Zamówienia(object sender, RoutedEventArgs e)
        {
            mainFrame.Content = new OrdersManagment();
        }
    }
}
