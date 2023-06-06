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
using Sklep.Entity;
using Sklep.Repsoitory;

namespace Sklep.Pages
{
    /// <summary>
    /// Logika interakcji dla klasy Cart.xaml
    /// </summary>
    public partial class Cart : Page
    {
        CartRepository cr = new CartRepository();
        ObservableCollection<Entity.Cart> cart;
        decimal price;
        public Cart()
        {
            InitializeComponent();       
            if (Sesion.sesion == null)
            {
                mainFrame.Content = new Login();
            }
            else
            {
                cart = cr.GetCart((int)Sesion.sesion.ID);
                ListView.ItemsSource = cart;
                price = 0;
                updatePrice();
            }
            
        }

        private void check(object sender, TextCompositionEventArgs e)
        {
            if (Int32.TryParse(e.Text, out int s))
            {
                e.Handled = false;
                var textbox = (TextBox)sender;
                Entity.Cart context = new Entity.Cart();
                context = (Entity.Cart)textbox.DataContext;
                if (int.Parse(textbox.Text+e.Text) > context.Product_id.Quantity)
                {
                    e.Handled = true;
                }
            }
            else
            {
                e.Handled = true;
            }
        }
        private void ifEmpty(object sender, RoutedEventArgs e)
        {
            var textbox = (TextBox)sender;
            if (string.IsNullOrEmpty(textbox.Text))
            {
                textbox.Text = "1";
                updatePrice();
            }
            else
            {
                Entity.Cart context = new Entity.Cart();
                context = (Entity.Cart)textbox.DataContext;
                cr.UpdateCart(context, int.Parse(textbox.Text));
                updatePrice();
            }
        }

        private void delete(object sender, RoutedEventArgs e)
        {
            var clicked = sender as Button;
            Entity.Cart cartItem = (Entity.Cart)clicked.CommandParameter;
            if (cartItem != null) 
            {
                cr.DeleteFromCart(cartItem);
                cart =  cr.GetCart((int)Sesion.sesion.ID);
                ListView.ItemsSource = cart;
                updatePrice();
            }
            updatePrice();
        }
        public void updatePrice()
        {
            price = 0;
            foreach (Entity.Cart c in cart)
            {
                price += (decimal)c.Quantity * (decimal)c.Product_id.Price;
            }
            priceFull.Content = price;
        }

        private void Order(object sender, RoutedEventArgs e)
        {
            if(cart.Count>0) 
            {
                mainFrame.Content = new Order();
            }
            else
            {
                MessageBox.Show("Najpierw dodaj cos do koszyka!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            
        }
    }
}
