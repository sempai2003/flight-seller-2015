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
    /// Interaction logic for RegistrationPage.xaml
    /// </summary>
    /// 
    public partial class RegistrationPage : UserControl
    {
        private UserControl m_prevPage;

        public RegistrationPage()
        {
            InitializeComponent();
        }
        
        private bool isEmailValid(TextBox email)
        {
            bool isValid = true;
            if (email.Text.IndexOf('@') == -1 || email.Text.IndexOf('.') == -1)
            {
                email.Text = "Неверный адрес почты!";
                isValid = false;
                email.BorderBrush = Brushes.Red;
            }
            else
            {
                email.BorderBrush = Brushes.Azure;
            }
            return isValid;
        }

        private bool isPasswordValid(string pass1, string pass2)
        {
            bool isValid = true;
            if (!pass1.Equals(pass2) || pass1 == "")
                isValid = false;
            return isValid;
        }

        private bool checkEmpty(TextBox textBox) {
            bool empty = false;
            const string emptyMsg = "Поле не должно быть пустым!";
            if (textBox.Text == "" || textBox.Text == emptyMsg)
            {
                textBox.Text = emptyMsg;
                textBox.BorderBrush = Brushes.Red;
                empty = true;
            }
            else
            {
                textBox.BorderBrush = Brushes.Azure;
            }
            return empty;
        }

        private bool isPassportNoValid(TextBox passportNo)
        {
            bool isValid = true;
            Int64 num = 0;
            isValid = Int64.TryParse(passportNo.Text, out num);

            if (!isValid)
            {
                passportNo.BorderBrush = Brushes.Red;
            }
            else
            {
                passportNo.BorderBrush = Brushes.Azure;
            }
            
            return isValid;
        }
        

        private void RegistrationButtonClick(object sender, RoutedEventArgs e)
        {
            //=============================== check for all fields validity -> to another method ===============
            bool allFieldsAreValid = true;
            string newUserEmail = emailField.Text;
            if (!isEmailValid(emailField))
            {
                allFieldsAreValid = false;
            }

            string pass = passwordField.Password;
            string repeatPass = repeatPassportField.Password;
            if (!isPasswordValid(pass, repeatPass))
            {
                allFieldsAreValid = false;
                passwordField.BorderBrush = Brushes.Red;
                repeatPassportField.BorderBrush = Brushes.Red;
            }
            else
            {
                passwordField.BorderBrush = Brushes.Azure;
                repeatPassportField.BorderBrush = Brushes.Azure;
            }

            string newUserName = null;
            string newUserSurname = null;
            if (checkEmpty(nameField))
                allFieldsAreValid = false;
            else
                newUserName = nameField.Text;

            if (checkEmpty(surnameField))
                allFieldsAreValid = false;
            else
                newUserSurname = surnameField.Text;

            DateTime newUserBirthDate = new DateTime();
            if (birthDatePicker.SelectedDate.HasValue)
            {
                birthDatePicker.BorderBrush = Brushes.Azure;
                newUserBirthDate = birthDatePicker.SelectedDate.Value;
            }
            else
            {
                birthDatePicker.BorderBrush = Brushes.Red;
                allFieldsAreValid = false;
            }

            if (checkEmpty(citizenField))
            {
                allFieldsAreValid = false;
            }
            string newUserCitizen = citizenField.Text;

            if (checkEmpty(phoneField))
            {
                allFieldsAreValid = false;
            }
            string newUserPhone = phoneField.Text;

            DateTime newUserValidityDate = new DateTime();
            if (validityDatePicker.SelectedDate.HasValue && (birthDatePicker.DisplayDate < DateTime.Today))
            {
                validityDatePicker.BorderBrush = Brushes.Azure;
                newUserValidityDate = validityDatePicker.SelectedDate.Value;
            }
            else
            {
                validityDatePicker.BorderBrush = Brushes.Red;
                allFieldsAreValid = false;
            }

            if (!isPassportNoValid(passportNoField))
            {
                allFieldsAreValid = false;
            }
            string newUserPassportNo = passportNoField.Text;

            Gender newUserGender = Gender.Female;
            if (maleCheckBox.IsChecked.Value)
                newUserGender = Gender.Male;
            //=============================== check for all fields validity -> to another method ===============
            if (!allFieldsAreValid)
                return;

            string userXMLDocPath = @"..\..\Users.xml";         

            XmlDocument usersDocument = new XmlDocument();
            usersDocument.Load(userXMLDocPath);

            XmlNode rootNode = usersDocument.DocumentElement.SelectSingleNode("/users");
            XmlNode newUserNode = usersDocument.CreateNode(XmlNodeType.Element, "user", null);
            XmlNode newPassportNode = usersDocument.CreateNode(XmlNodeType.Element, "passport", null);
            XmlNode newTicketsNode = usersDocument.CreateNode(XmlNodeType.Element, "tickets", null);

            XmlNode email = usersDocument.CreateElement("email");
            XmlNode password = usersDocument.CreateElement("password");
            XmlNode phone = usersDocument.CreateElement("phone");
            XmlNode citizenship = usersDocument.CreateElement("citizenship");           
            XmlNode name = usersDocument.CreateElement("name");
            XmlNode surname = usersDocument.CreateElement("surname");
            XmlNode gender = usersDocument.CreateElement("gender");
            XmlNode birthDate = usersDocument.CreateElement("birthdate");
            

            XmlNode passportNo = usersDocument.CreateElement("passportNo");
            XmlNode passportValidityDate = usersDocument.CreateElement("validityDate");

            email.InnerText = newUserEmail;
            name.InnerText = newUserName;
            surname.InnerText = newUserSurname;
            password.InnerText = pass;
            phone.InnerText = newUserPhone;
            birthDate.InnerText = newUserBirthDate.ToShortDateString();
            citizenship.InnerText = newUserCitizen;
            switch (newUserGender)
            {
                case Gender.Male:
                    gender.InnerText = "Male";
                    break;
                case Gender.Female:
                    gender.InnerText = "Female";
                    break;
            }

            passportValidityDate.InnerText = newUserValidityDate.ToShortDateString();
            passportNo.InnerText = newUserPassportNo;           
            usersDocument.DocumentElement.AppendChild(newUserNode);
            newUserNode.AppendChild(email);
            newUserNode.AppendChild(password);
            newUserNode.AppendChild(phone);
            newUserNode.AppendChild(name);
            newUserNode.AppendChild(surname);
            newUserNode.AppendChild(gender);
            newUserNode.AppendChild(birthDate);
            newUserNode.AppendChild(citizenship);

            newUserNode.AppendChild(newPassportNode);
            newPassportNode.AppendChild(passportNo);
            newPassportNode.AppendChild(passportValidityDate);

            newUserNode.AppendChild(newTicketsNode);

            usersDocument.Save(userXMLDocPath);
            
            
            MessageBox.Show("Вы успешно зарегистрированы!", "Успех!");
            goBack();
            
        }

        private void goBackLabelMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            goBack();
        }

        private void goBack()
        {
            Visibility = System.Windows.Visibility.Hidden;
            m_prevPage.Visibility = System.Windows.Visibility.Visible;
        }

        private void registrationPageIsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if ((bool)e.NewValue)
            {
                var mainWindow = App.Current.MainWindow as MainWindow;
                m_prevPage = mainWindow.m_curPage;
                mainWindow.buyPage.Visibility = System.Windows.Visibility.Hidden;
                mainWindow.mainPage.Visibility = System.Windows.Visibility.Hidden;
                mainWindow.personalAreaPage.Visibility = System.Windows.Visibility.Hidden;
                mainWindow.searchPage.Visibility = System.Windows.Visibility.Hidden;

                emailField.Text = "";
                passwordField.Password = "";
                repeatPassportField.Password = "";
                nameField.Text = "";
                surnameField.Text = "";
                birthDatePicker.SelectedDate = new DateTime(1990, 1, 1);
                citizenField.Text = "";
                femaleCheckBox.IsChecked = false;
                maleCheckBox.IsChecked = true;
                phoneField.Text = "";
                passportNoField.Text = "";
                validityDatePicker.SelectedDate = DateTime.Today;
            }
        }
    }
}
