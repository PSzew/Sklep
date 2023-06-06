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
using Sklep.Entity;

namespace Sklep.Pages.adminPages
{
    /// <summary>
    /// Logika interakcji dla klasy OrdersManagment.xaml
    /// </summary>
    public partial class OrdersManagment : Page
    {
        OrdersRepository or = new OrdersRepository();
        public OrdersManagment()
        {
            InitializeComponent();
            dGrid.ItemsSource = or.GetOrders();
        }

        private void moreinfo(object sender, RoutedEventArgs e)
        {
            if (dGrid.SelectedItem != null)
            {
                Orders o = (Orders)dGrid.SelectedItem as Orders;
                if (o != null)
                {
                    mainFrame.Content = new OrderMoreInfo(o);
                }
            }
        }

        private void delete(object sender, RoutedEventArgs e)
        {
            if (dGrid.SelectedItem != null)
            {
                OrdersRepository or = new OrdersRepository();
                OrderedItemsRepository oir = new OrderedItemsRepository();
                Orders o = (Orders)dGrid.SelectedItem as Orders;
                if (o != null) 
                {
                    oir.DeleteById(o.ID);
                    or.DeleteById(o.ID);
                    dGrid.ItemsSource = or.GetOrders();
                }
            }
        }
    }
}
