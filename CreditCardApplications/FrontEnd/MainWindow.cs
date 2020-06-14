using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;

namespace FrontEnd
{
    // Can't figure out how to make this 
    public enum Titles
    {
        Title = 0,
        Mr,
        Mrs,
        Miss,
        Ms
    }

    // ToDo: Move this to another project within the solution
    // Contains data for an applicant: income, etc.
#warning CreditCardApplicantData must to be moved into another layer of the program.
    public class CreditCardApplicantData
    {
        // ~~ Personal Details ~~
        public Titles Title { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string Surname { get; set; }
        public DateTime BirthDate { get; set; }

        // ~~ Contact Details ~~
        public string Email { get; set; }
        public int MobileNumber { get; set; }
        public int HomePhoneNumber { get; set; } 
        public string HouseNumber { get; set; }
        public string PostalCode { get; set; }

        // ~~ Financial Details ~~
        public decimal AnnualPersonalIncome { get; set; }
        public decimal OtherHouseholdIncome { get; set; }
    }


    public partial class MainWindow : Window
    {
        public CreditCardApplicantData ApplicantData { get; set; } = new CreditCardApplicantData();
        
        public MainWindow()
        {
            InitializeComponent();

            // Populate TitleComboBox
            foreach (string title in Enum.GetNames(typeof(Titles)))
            {
                TitleComboBox.Items.Add(title);
            }

            // TitleComboBox defaultly reads "Title"
            TitleComboBox.Text = TitleComboBox.Items[0].ToString();
        }
    }
}
