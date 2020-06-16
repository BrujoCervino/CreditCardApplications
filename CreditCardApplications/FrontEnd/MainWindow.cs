using Globals.Enums;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace FrontEnd
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            var titles = Enum.GetNames(typeof(Titles)).ToImmutableArray<object>();
            InitComboBox(TitleComboBox, null, titles);

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

        // Populates ComboBox cb with firstToAdd (e.g. "MM" before a list of months) then with objectsToAdd.
        public void PopulateComboBox(in ComboBox cb, in object firstToAdd, in ImmutableArray<object> objectsToAdd)
        {
            if(null != cb)
            {
                if(null != firstToAdd)
                {
                    cb.Items.Add(firstToAdd);
                }
                if(null != objectsToAdd)
                {
                    foreach (object obj in objectsToAdd)
                    {
                        cb.Items.Add(obj); 
                    }
                }
            }
        }

        // Sets a ComboBox's text to its items' first element, stringified. 
        public void SetDefaultInComboBox(in ComboBox cb)
        {
            if (null != cb && null != cb.Items)
            {
                cb.Text = cb.Items[0].ToString();
            }
        }

        // Sets up a combo box: populates its items and sets the default text/input as the first item in the list.
        // Example: populate the months ComboBox and set its default as "MM".
        public void InitComboBox(in ComboBox cb, in object firstToAdd, in ImmutableArray<object> objectsToAdd)
        {
            PopulateComboBox(cb, firstToAdd, objectsToAdd);
            SetDefaultInComboBox(cb);
        }
    }
}
