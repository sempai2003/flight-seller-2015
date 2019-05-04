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
    /// Interaction logic for PersonalAreaPage.xaml
    /// </summary>
    public partial class PersonalAreaPage : UserControl
    {
        private string m_userMail;
        private int m_countOfTickets;

        public PersonalAreaPage()
        {
            InitializeComponent();
        }

        private void personalAreaPageIsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if ((bool)e.NewValue)
            {
                var mainWindow = App.Current.MainWindow as MainWindow;
                mainWindow.m_curPage = mainWindow.personalAreaPage;
                mainWindow.mainPage.Visibility = System.Windows.Visibility.Hidden;
                mainWindow.registrationPage.Visibility = System.Windows.Visibility.Hidden;
                mainWindow.searchPage.Visibility = System.Windows.Visibility.Hidden;
                mainWindow.buyPage.Visibility = System.Windows.Visibility.Hidden;

                m_userMail = mainWindow.m_user.m_email;
                updateCountOfTickets();
                showTickets(0, Math.Min(15, m_countOfTickets));
            }
        }

        private void updateCountOfTickets()
        {
            XmlNode userNode = findUserNode();
            if (userNode != null)
                m_countOfTickets = userNode.SelectSingleNode("tickets").SelectNodes("ticket").Count;
            else
                m_countOfTickets = 0;
        }

        private void showTickets(int firstIndex, int count)
        {
            XmlNode userNode = findUserNode();
            if (userNode != null)
            {
                ticketList.Items.Clear();
                XmlNodeList ticketNodes = userNode.SelectSingleNode("tickets").SelectNodes("ticket");
                int ind = 0;
                foreach (XmlNode ticketNode in ticketNodes)
                {
                    if (ind >= firstIndex && ind < firstIndex + count)
                    {
                        string departureCity = ticketNode.SelectSingleNode("departureCity").InnerText;
                        string arrivalCity = ticketNode.SelectSingleNode("arrivalCity").InnerText;
                        DateTime departureTime = Convert.ToDateTime(ticketNode.SelectSingleNode("departureTime").InnerText);
                        double price = Convert.ToDouble(ticketNode.SelectSingleNode("price").InnerText);
                        string seatType = ticketNode.SelectSingleNode("seatType").InnerText;

                        ticketList.Items.Add(new ListViewRow()
                        {
                            m_departureCity = departureCity,
                            m_arrivalCity = arrivalCity,
                            m_departureTime = departureTime,
                            m_price = price,
                            m_seatType = seatType
                        });
                    }
                    else
                        if (ind >= firstIndex + count)
                            break;
                    ind++;
                }
            }
        }

        private XmlNode findUserNode()
        {
            XmlDocument usersDocument = new XmlDocument();
            usersDocument.Load(@"..\..\Users.xml");
            XmlNodeList userNodes = usersDocument.DocumentElement.SelectNodes("user");
            foreach (XmlNode userNode in userNodes)
            {
                if (userNode.SelectSingleNode("email").InnerText == m_userMail)
                {
                    return userNode;
                }
            }
            return null;
        }

        private class ListViewRow
        {
            public string m_departureCity { get; set; }
            public string m_arrivalCity { get; set; }
            public DateTime m_departureTime { get; set; }
            public double m_price { get; set; }
            public string m_seatType { get; set; }

        }
    }
}
