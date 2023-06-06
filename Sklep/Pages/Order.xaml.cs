using Sklep.Entity;
using Sklep.Repsoitory;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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

namespace Sklep.Pages
{
    /// <summary>
    /// Logika interakcji dla klasy Order.xaml
    /// </summary>
    public partial class Order : Page
    {
        public Order()
        {
            InitializeComponent();
        }

        private void order(object sender, RoutedEventArgs e)
        {
            if(!string.IsNullOrEmpty(city.Text) && !string.IsNullOrEmpty(adres.Text) && !string.IsNullOrEmpty(zipcode.Text) && !string.IsNullOrEmpty(tel.Text)) 
            {
                Regex zipcodetest = new Regex("((([0-9]{2})|([0-9]_)|(__)|(_[0-9]))-(([0-9]{3})|(_[0-9]{2})|(__[0-9])|([0-9]_[0-9])|([0-9]__)|(_[0-9]_))$)|(^(([0-9]{2})|([0-9]_)|(_[0-9]))-(___))");
                Regex phonetest = new Regex("\\(?([0-9]{3})\\)?([ .-]?)([0-9]{3})\\2([0-9]{3})");
                bool zipcodematch = false;
                bool phonematch = false;
                if (zipcodetest.IsMatch(zipcode.Text))
                    zipcodematch = true;
                else
                    MessageBox.Show("Podano nie prawidłowy kod pocztowy!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                if(phonetest.IsMatch(tel.Text))
                    phonematch = true;
                else
                    MessageBox.Show("Podano nie prawidłowy numer telefonu!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                if(phonematch && zipcodematch) 
                {
                    CartRepository cr = new CartRepository();
                    OrdersRepository or = new OrdersRepository();
                    OrderedItemsRepository oir = new OrderedItemsRepository();
                    ProductRepository pr = new ProductRepository();

                    ObservableCollection<Entity.Cart> cart = cr.GetCart((int)Sesion.sesion.ID);
                    decimal price = 0;
                    foreach (Entity.Cart cartitem in cart) 
                    {
                        price += (decimal)cartitem.Quantity * (decimal)cartitem.Product_id.Price;
                        pr.Decrement(cartitem.Product_id, (int)cartitem.Quantity);
                    }
                    Orders o = new Orders((int)Sesion.sesion.ID,price,city.Text,zipcode.Text,int.Parse(tel.Text),adres.Text);
                    or.Insert(o);
                    int orderID = or.GetId(Sesion.sesion);
                    foreach(Entity.Cart cartitem in cart)
                    {
                        OrderedItem oi = new OrderedItem(orderID, (int)cartitem.Product_id.ID, (int)cartitem.Quantity);
                        oir.Insert(oi);
                    }
                    cr.DeleteCart(Sesion.sesion);
                    MessageBox.Show("Zamówienie przebiegło pomyślnie!","Sklep",MessageBoxButton.OK, MessageBoxImage.Information);
                    mainFrame.Content = new MainPage();
                }
            }
            else
                MessageBox.Show("Prosze podać prawidłowe dane!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        private void goBack(object sender, RoutedEventArgs e)
        {
            mainFrame.Content = new Cart();
        }
    }
}
