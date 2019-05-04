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
using System.Windows.Shapes;
using System.Xml;

namespace FlightSeller
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        private Login()
        {
            InitializeComponent();
        }

        public static Login ShowLogInWindow()
        {
            if (instance == null)
            {
                instance = new Login();  
            }
            instance.Show();
            return instance;
        }

        private static Login instance;

        private void WindowClosing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = true;
            Visibility = System.Windows.Visibility.Hidden;
        }

        private void LogInButtonClick(object sender, RoutedEventArgs e)
        {
            XmlDocument flightsDoc = new XmlDocument();
            flightsDoc.Load(@"..\..\Users.xml");

            XmlNodeList users = flightsDoc.DocumentElement.SelectNodes("/users/user");

            foreach (XmlNode user in users)
            {
                string userEmail = user.SelectSingleNode("email").InnerText;
                string userPassword = user.SelectSingleNode("password").InnerText;

                if (userEmail.Equals(loginField.Text) && userPassword.Equals(passwordField.Password))
                {
                    statusLabel.Content = "";
                    string userName = user.SelectSingleNode("name").InnerText;
                    string userSurname = user.SelectSingleNode("surname").InnerText;
                    var mainWindow = App.Current.MainWindow as MainWindow;

                    Int64 passportNo = Convert.ToInt64(user.SelectSingleNode("passport").SelectSingleNode("passportNo").InnerText);
                    DateTime valDate = Convert.ToDateTime(user.SelectSingleNode("passport").SelectSingleNode("validityDate").InnerText);

                    
                    mainWindow.logInUser(new User(userEmail, userName, userSurname, passportNo, valDate));
                    Visibility = System.Windows.Visibility.Hidden;
                    return;
                }
                
            }

            const string status = "Неверный логин или пароль!";
            statusLabel.Content = status;
        }

        private void registrationLabelMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var mainWindow = App.Current.MainWindow as MainWindow;
            mainWindow.searchPage.Visibility = System.Windows.Visibility.Hidden;
            Visibility = System.Windows.Visibility.Hidden;
            mainWindow.registrationPage.Visibility = System.Windows.Visibility.Visible;
        }

        private void LoginBoxGotFocus(object sender, RoutedEventArgs e)
        {
            TextBox tb = (sender as TextBox);
            if (tb == null)
                return;

            tb.Text = "";
        }

        private void PasswordBoxGotFocus(object sender, RoutedEventArgs e)
        {
            PasswordBox tb = (sender as PasswordBox);
            if (tb == null)
                return;

            tb.Password = "";
        }

        private void loginWindowIsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            loginField.Text = "e-mail";
            passwordField.Password = "пароль";
        }
    }
}
