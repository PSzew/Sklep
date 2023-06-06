using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
namespace Sklep.Pages.adminPages
{
    /// <summary>
    /// Logika interakcji dla klasy OrderMoreInfo.xaml
    /// </summary>
    public partial class OrderMoreInfo : Page
    {
        public OrderMoreInfo(Orders o)
        {
            InitializeComponent();
            OrderedItemsRepository ori = new OrderedItemsRepository();
            listView.ItemsSource = ori.GetOrderedItems(o);
        }
    }
}
