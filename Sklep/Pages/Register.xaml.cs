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
using Sklep.Entity;
using Sklep.Repsoitory;

namespace Sklep.Pages
{
    /// <summary>
    /// Logika interakcji dla klasy Register.xaml
    /// </summary>
    public partial class Register : Page
    {
        public Register()
        {
            InitializeComponent();
        }

        private void register(object sender, RoutedEventArgs e)
        {
            Regex emailValidation = new Regex("^[\\w-\\.]+@([\\w-]+\\.)+[\\w-]{2,10}$");
            Regex passwordValidation = new Regex("^(?=.*\\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[a-zA-Z]).{8,}$");
            if(!string.IsNullOrEmpty(name.Text) && !string.IsNullOrEmpty(surname.Text) && !string.IsNullOrEmpty(login.Text) && !string.IsNullOrEmpty(email.Text)
                && !string.IsNullOrEmpty(pass1.Text) && !string.IsNullOrEmpty(pass2.Text))
            {
                if(name.Text.Length >= 4 && surname.Text.Length >=4 && login.Text.Length>=4)
                {
                    if (emailValidation.IsMatch(email.Text))
                    {
                        if(pass1.Text == pass2.Text)
                        {
                            if(passwordValidation.IsMatch(pass1.Text))
                            {
                                bool isValid = true;
                                UserRepository ur = new UserRepository();
                                ObservableCollection<User> users = new ObservableCollection<User>();
                                users = ur.TryUsers();
                                foreach(User u in users)
                                {
                                    if(u.Email == email.Text || u.Login == login.Text)
                                    {
                                        isValid = false; 
                                        break;
                                    }
                                }
                                if (isValid)
                                {
                                    User user = new User(name.Text, surname.Text, login.Text, email.Text, pass1.Text, false);
                                    ur.AddUser(user);
                                    mainFrame.Content = new Login();

                                }
                                else
                                    MessageBox.Show("Konto z podanym Login'em lub E-mail'em juz istnieje", "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
                            }
                            else
                                MessageBox.Show("Hasło musi zawierać przynajmniej 8 znaków w tym jeden duży oraz literę!", "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);

                        }
                        else
                            MessageBox.Show("Podane hasła nie są takie same!", "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);

                    }
                    else
                        MessageBox.Show("Podano nie prawidłowy E-mail", "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);

                }
                else
                    MessageBox.Show("Imie,Nazwisko i Login muszą zawierać przynajmniej 4 znaki!", "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
                MessageBox.Show("Pola nie mogą być puste!","Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }
}
