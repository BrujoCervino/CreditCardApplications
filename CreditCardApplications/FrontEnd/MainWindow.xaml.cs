using CardLib;
using CreditCardApplicantData.Enums;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
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

namespace FrontEnd
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private void TitleComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Store the sender as a combo box, by which the user sets their title.
            ComboBox comboBox = sender as ComboBox;

            if (null != TitleComboBox)
            {

            }
        }

        private void BirthdateComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Store the sender as a combo box, by which the user sets their title.
            ComboBox comboBox = sender as ComboBox;

            if (null != comboBox)
            {
                var a = comboBox.Tag;
                switch (a)
                {
                    case "MonthChanged":
                        MonthComboBox.Items.Remove("29");
                        MonthComboBox.Items.Remove("30");
                        MonthComboBox.Items.Remove("31");
                        break;
                }
            }
        }

        private void EmailEntry_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox textBox = sender as TextBox;

            if (null != textBox)
            {

            }
        }

        private DateTime ParseBirthdate(string month, string day, string year)
        {
#warning firstDayOrMonth is used in many places and should be a constant.
            const string firstDayOrMonth = "01";
#warning firstYear is used in many places and should be a constant.
            const string firstYear = "1880";
            // Format day, month and year:
            //  If null or default, let them equal their earliest possible value.
            //  ToDo: make the above a function and implement it here
            month = ((month != "MM") ? month : firstDayOrMonth)??firstDayOrMonth;
            day = ((day != "DD") ? day : firstDayOrMonth)??firstDayOrMonth;
            year = ((year != "YYYY") ? year : firstDayOrMonth)?? firstYear;

            // Parse the birthdate
            string stringifiedBirthdate = $"{month}/{day}/{year}";
            return DateTime.Parse(stringifiedBirthdate);
        }

        private void SaveAndContinueButton_Click(object sender, RoutedEventArgs e)
        {
#warning Crud update unfinished

            DateTime applicantBirthDate
                = ParseBirthdate(MonthComboBox.Text, DayComboBox.Text, YearComboBox.Text);



            // CrudManager.Update
            // (
            //     (Titles)TitleComboBox.SelectedIndex,
            //     FirstNameTextEntry.Text,
            //     MiddleNameTextEntry.Text,
            //     applicantBirthDate,
            //
            // );
        }
    }
}
