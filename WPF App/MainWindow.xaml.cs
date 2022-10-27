using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace WPF_App
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private static int selectedTabIndex = 0;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void HandleSendRequest(object sender, RoutedEventArgs e)
        {
            var soapClient = new ServiceReferenceSoap.WebServiceSoapClient();

            switch (selectedTabIndex)
            {
                case 0:
                    HelloResultTB.Text = soapClient.HelloWorld();
                    break;
                case 1:
                    int a, b;
                    int.TryParse(ATB.Text, out a);
                    int.TryParse(BTB.Text, out b);

                    AddResultTB.Text = soapClient.Add(a, b).ToString();
                    break;
                case 2:
                    double tempFahrenheit;
                    double.TryParse(FahrenheitTB.Text, out tempFahrenheit);

                    FTCResultTB.Text = soapClient.FahrenheitToCelsius(tempFahrenheit).ToString();
                    break;
                case 3:
                    double tempCelsius;
                    double.TryParse(CelsiusTB.Text, out tempCelsius);

                    CTFResultTB.Text = soapClient.CelsiusToFahrenheit(tempCelsius).ToString();
                    break;
                case 4:
                    var responseData = soapClient.ReadUserData();

                    IEnumerable<string> formatedProperties =
                         responseData.GetType()
                            .GetProperties()
                            .Skip(1)
                            .Select(p => p.Name + ": " + p.GetValue(responseData).ToString());

                    ReadUserDataTB.Text = string.Join(Environment.NewLine, formatedProperties);
                    break;
                case 5:
                    var userData = new ServiceReferenceSoap.UserData()
                    {
                        FirstName = CUFirstNameTB.Text,
                        LastName = CULastNameTB.Text,
                        Age = int.Parse(CUAgeTB.Text),
                        Gender = CUGenderComboBox.Text,
                        Email = CUEmailTB.Text,
                        IsRetired = CUIsRetiredCheckBox.IsChecked.Value
                    };

                    var responseData2 = soapClient.CreateUserData(userData);

                    IEnumerable<string> formatedProperties2 =
                         responseData2.GetType()
                            .GetProperties()
                            .Skip(1)
                            .Select(p => p.Name + ": " + p.GetValue(responseData2).ToString());

                    CreateUserDataTB.Text = string.Join(Environment.NewLine, formatedProperties2);
                    break;
                case 6:
                    var updatedData = new ServiceReferenceSoap.UserData()
                    {
                        FirstName = UUFirstNameTB.Text,
                        LastName = UULastNameTB.Text,
                        Age = int.Parse(UUAgeTB.Text),
                        Gender = UUGenderComboBox.Text,
                        Email = UUEmailTB.Text,
                        IsRetired = UUIsRetiredCheckBox.IsChecked.Value
                    };

                    var responseData3 = soapClient.UpdateUserData(updatedData);

                    IEnumerable<string> formatedProperties3 =
                         responseData3.GetType()
                            .GetProperties()
                            .Skip(1)
                            .Select(p => p.Name + ": " + p.GetValue(responseData3).ToString());

                    UpdateUserDataTB.Text = string.Join(Environment.NewLine, formatedProperties3);
                    break;
            }

            soapClient.Close();
        }

        private void HandleSelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            TabControl tabControl = sender as TabControl;
            selectedTabIndex = tabControl.SelectedIndex;

            if(selectedTabIndex == 6)
            {
                var soapClient = new ServiceReferenceSoap.WebServiceSoapClient();
                var data = soapClient.ReadUserData();

                UUFirstNameTB.Text = data.FirstName;
                UULastNameTB.Text = data.LastName;
                UUGenderComboBox.Text = data.Gender;
                UUAgeTB.Text = data.Age.ToString();
                UUIsRetiredCheckBox.IsChecked = data.IsRetired;

                soapClient.Close();
            }
        }
    }
}
