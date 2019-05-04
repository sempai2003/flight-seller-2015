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

namespace FlightSeller
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            m_user = null;
            InitializeComponent(); 
            Loaded += delegate
            {
                MinWidth = ActualWidth;
                MinHeight = ActualHeight;
            };
        }

        private void mainLogInLabelMouseLeftButtonDown(object sender, MouseEventArgs e)
        {
            if (mainLogInLabel.Foreground == Brushes.Gray)
                return;
            if (mainLogInLabel.Content.Equals("Войти"))
            {
                Login.ShowLogInWindow();
                
            }
            else
            {
                userNameLabel.Content = "";
                logOutUser();
            }
        }

        public User m_user { get; private set; }
        public UserControl m_curPage { get; set; }

        public void logInUser(User user)
        {
            m_user = user;
            userNameLabel.Cursor = Cursors.Hand;
            userNameLabel.Content = m_user.m_name + " " + m_user.m_surname;
            mainLogInLabel.Content = "Выход";
        }

        public void logOutUser()
        {
            m_user = null;
            userNameLabel.Cursor = Cursors.Arrow;
            mainLogInLabel.Content = "Войти";
        }

        public bool isUserLogIn()
        {
            return m_user != null;
        }

        private void userNameLabelMouseDown(object sender, MouseButtonEventArgs e)
        {
            var mainWindow = App.Current.MainWindow as MainWindow;
            if (mainWindow.m_user == null)
                return;
            personalAreaPage.Visibility = System.Windows.Visibility.Visible;
        }

        private void goToMainPageMouseDown(object sender, MouseButtonEventArgs e)
        {
            var mainWindow = App.Current.MainWindow as MainWindow;
            mainWindow.mainPage.Visibility = System.Windows.Visibility.Visible;

            mainWindow.buyPage.Visibility = System.Windows.Visibility.Hidden;
            mainWindow.registrationPage.Visibility = System.Windows.Visibility.Hidden;
            mainWindow.searchPage.Visibility = System.Windows.Visibility.Hidden;
            mainWindow.personalAreaPage.Visibility = System.Windows.Visibility.Hidden;
            mainWindow.searchPage.borderInfoFromTo.Visibility = Visibility.Hidden;
            mainWindow.searchPage.borderInfoToFrom.Visibility = Visibility.Hidden; 

        }

        private void mainWindowLoaded(object sender, RoutedEventArgs e)
        {
            mainPage.Visibility = System.Windows.Visibility.Visible;
        }
    }
}

