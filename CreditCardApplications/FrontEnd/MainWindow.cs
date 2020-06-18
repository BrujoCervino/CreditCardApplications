using Globals.Enums;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace FrontEnd
{
    public partial class MainWindow : Window
    {
        // ComboBox defaults
        public const string YearsTitle = "YYYY";
        public const string MonthsTitle = "MM";
        public const string DaysTitle = "DD";
        public const string TitleTitle = "Title";

        public static readonly int HighestFutureproofAge = 120;
        public const string FirstDayOrMonth = "01";

        public readonly ImmutableList<object> Years;

        public MainWindow()
        {
            InitializeComponent();

            // Initialise the title combo box
            var titles = Enum.GetNames(typeof(Titles)).ToImmutableList<object>();
            InitComboBox(TitleComboBox, TitleTitle, titles);

            // Populate the birthyear dropdown as either "YYYY" 
            //  or between (this year and HighestFutureproofAge years ago) inclusive
            object[] years = new object[HighestFutureproofAge];
            for(int i = 0; i < HighestFutureproofAge; ++i)
            {
                years[i] = DateTime.Now.Year - i;
            }
            Years = years.ToImmutableList();
            InitComboBox(YearComboBox, YearsTitle, years.ToImmutableList());

            // Populate the birthmonth dropdown as either "MM" or between 01 and 12 inclusive
            MonthComboBox.Items.Add(MonthsTitle);
            StringBuilder stringBuilder = new StringBuilder();
            for (int i = 1; i <= (int)Months.December; ++i)
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
            SetDefaultInComboBox(MonthComboBox);

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
            SetDefaultInComboBox(DayComboBox);
        }

        // Populates ComboBox cb with firstToAdd (e.g. "MM" before a list of months) then with objectsToAdd.
        private void PopulateComboBox(in ComboBox cb, in object firstToAdd, in ImmutableList<object> objectsToAdd)
        {
            if(null != cb)
            {
                if(null != firstToAdd)
                {
                    cb.Items.Add(firstToAdd);
                }
                if(null != objectsToAdd && objectsToAdd.Count > 0)
                {
                    foreach (object obj in objectsToAdd)
                    {
                        cb.Items.Add(obj); 
                    }
                }
            }
        }

        // Sets a ComboBox's text to its items' first element, stringified. 
        private void SetDefaultInComboBox(in ComboBox cb)
        {
            if (null != cb && null != cb.Items && cb.Items.Count > 0)
            {
                cb.Text = cb.Items[0].ToString();
            }
        }

        // Sets up a combo box: populates its items and sets the default text/input as the first item in the list.
        // Example: populate the months ComboBox and set its default as "MM".
        protected void InitComboBox(in ComboBox cb, in object firstToAdd, in ImmutableList<object> objectsToAdd)
        {
            PopulateComboBox(cb, firstToAdd, objectsToAdd);
            SetDefaultInComboBox(cb);
        }

        // Parses the given birthyear, replacing any nulls or defaults with the earliest valid element in its list
        private DateTime ParseAndFormatBirthdate(string month, string day, string year)
        {
            string firstYear = (DateTime.Now.Year - HighestFutureproofAge).ToString();
            // Format day, month and year:
            //  If null or default, let them equal their earliest possible value.
            //  ToDo: make the above a function and implement it here
            month = ((month != MonthsTitle) ? month : FirstDayOrMonth) ?? FirstDayOrMonth;
            day   = ((day != DaysTitle)     ? day   : FirstDayOrMonth) ?? FirstDayOrMonth;
            year  = ((year != YearsTitle)   ? year  : FirstDayOrMonth) ?? firstYear;

            // Parse the birthdate
            string stringifiedBirthdate = $"{month}/{day}/{year}";
            return DateTime.Parse(stringifiedBirthdate);
        }
    }
}
