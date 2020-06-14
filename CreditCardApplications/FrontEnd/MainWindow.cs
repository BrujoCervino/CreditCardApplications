using System;
using System.Collections.Generic;
using System.Linq;
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

    public enum Months
    {
        January = 1,
        February,
        March,
        April,
        May,
        June,
        July,
        August,
        September,
        October,
        November,
        December
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
        public readonly CreditCardApplicantData ApplicantData = new CreditCardApplicantData();

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

            // Populate the birthyear dropdown as either "YYYY" or between 1880 and the current year inclusive
            int earliestApplicantBirthyear = 1880;
            int range = 1 + Math.Abs(earliestApplicantBirthyear - DateTime.Now.Year);
            YearComboBox.Items.Add("YYYY");
            foreach (int yearToAdd in Enumerable.Range(earliestApplicantBirthyear, range))
            {
                YearComboBox.Items.Add(yearToAdd);
            }
            // YearComboBox defaultly reads "YYYY"
            YearComboBox.Text = YearComboBox.Items[0].ToString();

            // Populate the birthmonth dropdown as either "MM" or between 01 and 12 inclusive
            MonthComboBox.Items.Add("MM");
            StringBuilder stringBuilder = new StringBuilder();
            for(int i = 1; i <= (int)Months.December; ++i)
            {
                stringBuilder.Clear();
                if (i < 10)
                {
                    stringBuilder.Append('0');
                }
                stringBuilder.Append(i.ToString());
                MonthComboBox.Items.Add(stringBuilder.ToString());
            }
           // MonthComboBox defaultly reads "MM"
           MonthComboBox.Text = MonthComboBox.Items[0].ToString();




            // Populate the birthday dropdown as either "DD" or between 01 and 31 inclusive
            DayComboBox.Items.Add("DD");
            for (int i = 1; i <= 31; ++i)
            {
                stringBuilder.Clear();
                if (i < 10)
                {
                    stringBuilder.Append('0');
                }
                stringBuilder.Append(i.ToString());
                DayComboBox.Items.Add(stringBuilder.ToString());
            }

            // MonthComboBox defaultly reads "DD"
            DayComboBox.Text = DayComboBox.Items[0].ToString();
        }
    }
}
