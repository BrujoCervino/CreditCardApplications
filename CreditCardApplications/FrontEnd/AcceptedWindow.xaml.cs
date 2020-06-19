using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace FrontEnd
{
    /// <summary>
    /// Interaction logic for AcceptedWindow.xaml
    /// </summary>
    public partial class AcceptedWindow : Window
    {
        public AcceptedWindow(in bool approved = true)
        {
            InitializeComponent();
            if(!approved)
            {
                ApprovalHeading.Text = "We're sorry: your application was unsuccessful";
                ApprovalSubheading.Text = "You can apply again 3 months from now";
            }
        }
    }
}
