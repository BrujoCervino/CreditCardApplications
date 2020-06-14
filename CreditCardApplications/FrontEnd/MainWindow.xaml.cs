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
                ApplicantData.Title = (Titles)comboBox.SelectedIndex; 
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

            if(null != textBox)
            {
                ApplicantData.Email = textBox.Text;
            }
        }

        private void SaveAndContinueButton_Click(object sender, RoutedEventArgs e)
        {
            // Connect to database
        }
    }
}
