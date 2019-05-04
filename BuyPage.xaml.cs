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
using System.Xml;

namespace FlightSeller
{
    /// <summary>
    /// Interaction logic for BuyPage.xaml
    /// </summary>
    public partial class BuyPage : UserControl
    {
        public List<Ticket> m_tickets { get; private set; }

        public BuyPage()
        {
            InitializeComponent();
            m_tickets = new List<Ticket>();
        }

        public void addTicket(Ticket ticket)
        {
            m_tickets.Add(ticket);
        }

        private void confirmButtonClick(object sender, RoutedEventArgs e)
        {
            XmlDocument usersDocument = new XmlDocument();
            usersDocument.Load(@"..\..\Users.xml");
            XmlNodeList users = usersDocument.DocumentElement.SelectNodes("user");
            foreach (XmlNode user in users)
            {
                XmlNode email = user.SelectSingleNode("email");
                var mainWindow = App.Current.MainWindow as MainWindow;
                if (email.InnerText == mainWindow.m_user.m_email)
                {
                    foreach (Ticket ticket in m_tickets)
                    {
                        XmlNode tickets = user.SelectSingleNode("tickets");
                        XmlNode newTicket = usersDocument.CreateNode(XmlNodeType.Element, "ticket", null);
                        XmlNode departureCity = usersDocument.CreateElement("departureCity");
                        XmlNode arrivalCity = usersDocument.CreateElement("arrivalCity");
                        XmlNode departureTime = usersDocument.CreateElement("departureTime");
                        XmlNode arrivalTime = usersDocument.CreateElement("arrivalTime");
                        XmlNode seatType = usersDocument.CreateElement("seatType");
                        XmlNode price = usersDocument.CreateElement("price");

                        departureCity.InnerText = ticket.m_departureCity;
                        arrivalCity.InnerText = ticket.m_arrivalCity;
                        departureTime.InnerText = ticket.m_departureTime.ToString();
                        arrivalTime.InnerText = ticket.m_arrivalTime.ToString();
                        if (ticket.m_isBusiness)
                            seatType.InnerText = "Бизнес класс";
                        else
                            seatType.InnerText = "Эконом класс";
                        price.InnerText = ticket.m_price.ToString();

                        newTicket.AppendChild(departureCity);
                        newTicket.AppendChild(arrivalCity);
                        newTicket.AppendChild(departureTime);
                        newTicket.AppendChild(arrivalTime);
                        newTicket.AppendChild(seatType);
                        newTicket.AppendChild(price);
                        tickets.AppendChild(newTicket);
                        usersDocument.Save(@"..\..\Users.xml");
                    }
                    m_tickets.Clear();
                    MessageBox.Show("Билет успешно забронирован!", "Успех!"); 
                    mainWindow.mainPage.Visibility = System.Windows.Visibility.Visible;

                    mainWindow.buyPage.Visibility = System.Windows.Visibility.Hidden;
                    mainWindow.registrationPage.Visibility = System.Windows.Visibility.Hidden;
                    mainWindow.searchPage.Visibility = System.Windows.Visibility.Hidden;
                    mainWindow.personalAreaPage.Visibility = System.Windows.Visibility.Hidden;
                    break;
                }
            }
        }

        private void buyPageIsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if ((bool)e.NewValue)
            {
                var mainWindow = App.Current.MainWindow as MainWindow;
                mainWindow.m_curPage = mainWindow.buyPage;
                mainWindow.mainLogInLabel.Cursor = Cursors.Arrow;
                mainWindow.mainLogInLabel.Foreground = Brushes.Gray;
                mainWindow.mainPage.Visibility = System.Windows.Visibility.Hidden;
                mainWindow.personalAreaPage.Visibility = System.Windows.Visibility.Hidden;
                mainWindow.registrationPage.Visibility = System.Windows.Visibility.Hidden;
                mainWindow.searchPage.Visibility = System.Windows.Visibility.Hidden;
                firstFlightInfo.Content = m_tickets[0].m_departureCity + "->" + m_tickets[0].m_arrivalCity + ": " + m_tickets[0].m_departureTime;
                if (m_tickets.Count == 2)
                {
                    secondFlightInfo.Content = m_tickets[1].m_departureCity + "->" + m_tickets[1].m_arrivalCity + ": " + m_tickets[1].m_departureTime;
                }
                else
                    secondFlightInfo.Content = "";

                double price = 0.0;
                foreach (Ticket ticket in m_tickets)
                {
                    price += ticket.m_price;
                }

                summaryInfo.Content = price.ToString() + " рублей";

                nameInfo.Content = "Пассажир: " + mainWindow.m_user.m_name + "   " + mainWindow.m_user.m_surname;
                Int64 passportNo = mainWindow.m_user.m_passportNo;
                String valDate = mainWindow.m_user.m_validityDate.ToShortDateString();
                passportInfo.Content = "Номер паспорта: " + Convert.ToString(passportNo) + ".  Действителен до: " + valDate;
            } else
            {
                var mainWindow = App.Current.MainWindow as MainWindow;
                mainWindow.mainLogInLabel.Cursor = Cursors.Hand;
                mainWindow.mainLogInLabel.Foreground = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Color.FromArgb(0xFF, 0x41, 0x1B, 0xEC));
            }
        }
    }
}
