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

namespace Sklep.Pages
{
    /// <summary>
    /// Logika interakcji dla klasy UserPage.xaml
    /// </summary>
    public partial class UserPage : Page
    {
        Sesion sesion = new Sesion();
        public UserPage()
        {
            InitializeComponent();
            stackPanel.DataContext = Sesion.sesion;
        }

        private void logout(object sender, RoutedEventArgs e)
        {
            sesion.EndSession();
            mainFrame.Content = new MainPage();
        }
    }
}
