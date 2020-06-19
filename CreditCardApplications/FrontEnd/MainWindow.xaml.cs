using CrudOperations;
using Globals.Enums;
using DatabaseBackEnd;
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

            bool approved = null != comboBox && TitleComboBox.Items.Contains(comboBox.SelectedItem);
            //DisplayApprovalIcon(approved);
        }

        private void DisplayApprovalIcon(bool approved)
        {
            throw new NotImplementedException();
        }

        private void EmailEntry_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox textBox = sender as TextBox;

            if (null != textBox)
            {

            }
        }

        private void SaveAndContinueButton_Click(object sender, RoutedEventArgs e)
        {
            // Don't allow the user to submit their application until these are valid:
            // Title, First Name, Surname, Email, Year, Month
            if (!TitleBoxIsValid()
                || !TextEntryIsValid(FirstNameTextEntry)
                || !TextEntryIsValid(SurnameTextEntry)
                || !EmailIsValid(EmailEntry)
                || !DateDropDownIsValid(YearComboBox, YearsList, YearsTitle)
                || !DateDropDownIsValid(MonthComboBox, MonthsList, MonthsTitle))
            {
                return;
            }

            // Parse the birthdate and title
            DateTime applicantBirthDate
            = ParseAndFormatBirthdate(DayComboBox.Text, MonthComboBox.Text, YearComboBox.Text);

            Titles title = (Titles)TitleComboBox.SelectedIndex;

            // Process the applicant's application
            CrudManager.CreateApplication(title, FirstNameTextEntry.Text, MiddleNameTextEntry.Text, SurnameTextEntry.Text, applicantBirthDate, EmailEntry.Text, "07722 222 222", "07722 222 222", 25_000, 1_000);

            CreditChecker creditChecker = new InternalCreditChecker();
            var approval = creditChecker.PerformCreditCheck(null);
            bool accepted = approval.Approved;

            var acceptedWindow = new AcceptedWindow(accepted); // Make new window
            acceptedWindow.Show();
            acceptedWindow.Focus();
            Close(); // Close current window
        }

        // Returns true only if text entry is not null and its text is not null or whitespace or empty
        protected bool TextEntryIsValid(in TextBox textEntry)
        {
            return textEntry != null && !string.IsNullOrWhiteSpace(textEntry.Text);
        }

        // Returns true only if TextEntryIsValid(emailEntry) and the email contains character '@'.
        protected bool EmailIsValid(in TextBox emailEntry)
        {
#warning Unit Test this. It should never throw a null exception because of the short circuit
            return TextEntryIsValid(emailEntry) && emailEntry.Text.Contains('@');
        }

        // Returns true only if the title combo box is not null and its text is not null and its text contains an element of the titles enum.
        protected bool TitleBoxIsValid()
        {
            return TitleComboBox != null
                && TitleComboBox.Text != null
                && Enum.GetNames(typeof(Titles)).Contains(TitleComboBox.Text);
        }

        protected bool DateDropDownIsValid(in ComboBox cb, in ImmutableList<object> range, in string undesired)
        {
            return
                // Combo box
                cb != null
                && cb.SelectedItem != null
                // Range
                && range != null
                && range.Count > 0
                // Undesired
                && !string.IsNullOrWhiteSpace(undesired)
                && cb.SelectedItem.ToString() != undesired
                // Containing
                && range.Contains(cb.SelectedItem);
        }

        protected void BirthdateComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox comboBox = (ComboBox)sender;

#warning Change this function so that the number of days shown in the day box depends on the months box and whether the year is a leap year
            //switch (comboBox.Uid) // Todo: change this to an if statement instead
            //{
            //    //Commented out because it throws an exception. Fix soon:
            //    case "Year": break;
            //    case "Month": 
            //        var month = (comboBox.SelectedItem != MonthsTitle) 
            //            ? comboBox.SelectedIndex
            //            : (int)Months.January; // January has 31 days
            //        
            //        int year = (YearComboBox.SelectedItem.ToString() != YearsTitle)
            //            ? (int)YearComboBox.SelectedItem
            //            : DateTime.Now.Year;
            //    
            //        if (DayComboBox != null && DayComboBox.Items != null && DayComboBox.Items.Count > 0)
            //        {
            //            int numDays = GetNumDaysInMonth(month, year);
            //            DayComboBox.Items.Clear();
            //            for(int i = 0; i < DaysList.Count; ++i)
            //            {
            //                DayComboBox.Items.Add(DaysList[i]);
            //            }
            //        }
            //        break;
            //    case "Day": break;
            //    default: break;
            //}
        }

        protected int GetNumDaysInMonth(in int month, in int year)
        {
            Months eMonth = (Months)month;

            int numDays = 31;
            switch (eMonth)
            {
                case Months.February:
                    numDays = (DateTime.IsLeapYear(year)) ? 29 : 28;
                    break;
                case Months.April:
                case Months.June:
                case Months.September:
                case Months.November:
                    numDays = 30;
                    break;
            }

            return numDays;
        }
    }
}
