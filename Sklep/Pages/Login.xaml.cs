using System;
using System.Collections.Generic;
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
using Sklep.Entity;
using Sklep.Repsoitory;
namespace Sklep.Pages
{
    /// <summary>
    /// Logika interakcji dla klasy Login.xaml
    /// </summary>
    public partial class Login : Page
    {
        Sesion sesion = new Sesion();
        public Login()
        {            
            InitializeComponent();
            if (Sesion.sesion != null)
            {
                if((bool)Sesion.sesion.isAdmin) 
                {
                    mainFrame.Content = new AdminPage();
                }
                else
                    mainFrame.Content = new UserPage();
            }
        }

        private void Loginbtndown(object sender, RoutedEventArgs e)
        {
            tryLogin();
        }
        private void tryLogin()
        {
            Regex password = new Regex("^(?=.*\\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[a-zA-Z]).{8,}$");
            if (!string.IsNullOrEmpty(login.Text) && login.Text.Length >= 4)
            {
                if (!string.IsNullOrEmpty(pass.Password) && password.IsMatch(pass.Password))
                {
                    UserRepository ur = new UserRepository();
                    User TryToLogin = new User(login.Text, pass.Password);
                    if (ur.LoginUser(TryToLogin) != null)
                    {
                        sesion.StartSession(ur.LoginUser(TryToLogin));
                        if ((bool)Sesion.sesion.isAdmin)
                        {
                            mainFrame.Content = new AdminPage();
                        }
                        else
                            mainFrame.Content = new UserPage();
                    }
                    else
                        MessageBox.Show("Podano nie prawidłowy login lub hasło!", "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else
                    MessageBox.Show("Hasło musi zawierać przynajmniej 8 znaków w tym jeden duży oraz literę!", "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);

            }
            else
                MessageBox.Show("Login musi zawierać przynajmniej 4 znaki!", "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        private void entercheck(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return || e.Key == Key.Enter)
            {
                tryLogin();
            }
        }

        private void JumpToPassowrd(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return || e.Key == Key.Enter)
            {
                pass.Focus();
            }
        }
    }
}
