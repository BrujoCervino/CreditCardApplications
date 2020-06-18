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

        private void BirthdateComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
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
            // Don't allow the user to submit their application until the title box contains a valid value
            if (!TitleBoxIsValid())
            {
                return;
            }

            // Parse the birthdate and title
            DateTime applicantBirthDate
            = ParseAndFormatBirthdate(MonthComboBox.Text, DayComboBox.Text, YearComboBox.Text);

            Titles title = (Titles)TitleComboBox.SelectedIndex;

            // Process the applicant's application
            CrudManager.CreateEntry(title, FirstNameTextEntry.Text, MiddleNameTextEntry.Text, SurnameTextEntry.Text, applicantBirthDate, EmailEntry.Text, "07722 222 222", "07722 222 222", 25_000, 1_000);

            var acceptedWindow = new AcceptedWindow(); // Make new window
            acceptedWindow.Show();
            acceptedWindow.Focus();
            Close(); // Close current window
        }

        private bool FirstNameTextEntryIsValid()
        {
            return FirstNameTextEntry != null && !string.IsNullOrWhiteSpace(FirstNameTextEntry.Text);
        }

        // Returns true only if the title combo box is not null and its text is not null and its text contains an element of the titles enum.
        protected bool TitleBoxIsValid()
        {
            return TitleComboBox != null
                && TitleComboBox.Text != null
                && Enum.GetNames(typeof(Titles)).Contains(TitleComboBox.Text);
        }
    }
}
