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
using Sklep.Pages;
using Sklep.Repsoitory;

namespace Sklep
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            mainFrame.NavigationUIVisibility = NavigationUIVisibility.Hidden;
            mainFrame.Content = new MainPage();
        }

        private void Drag(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
                this.DragMove();
        }

        private void Minimalize(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void Close(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Category(object sender, RoutedEventArgs e)
        {
            mainFrame.Content = new Category();
        }

        private void Login(object sender, RoutedEventArgs e)
        {
            mainFrame.Content = new Login();
        }

        private void mainpage(object sender, RoutedEventArgs e)
        {
            mainFrame.Content = new MainPage();
        }

        private void cart(object sender, RoutedEventArgs e)
        {
            mainFrame.Content = new Cart();
        }
    }
}
//PALETA @https://coolors.co/0a2239-53a2be-1d84b5-132e32-176087
