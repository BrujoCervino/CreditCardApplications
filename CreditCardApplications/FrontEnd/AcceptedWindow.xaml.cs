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
        public AcceptedWindow()
        {
            InitializeComponent();

            InitialiseImageSource(BackgroundImage, @"C:\Users\Fox_Mulder\Documents\Sparta\14-Week SDET Course\Projects\Project 2 - CRUD UI\CreditCardApplications\CreditCardApplications\FrontEnd\Graphics\AXP_World_Service.png");
#warning: bad practice to use such large image paths. Make these paths relative to shorten them
#warning: bad practice to use absolute image paths: will not work on another computer
            InitialiseImageSource(CardImage, @"C:\Users\Fox_Mulder\Documents\Sparta\14-Week SDET Course\Projects\Project 2 - CRUD UI\CreditCardApplications\CreditCardApplications\FrontEnd\Graphics\UK_AXP_Preferred_Rewards_Gold_Card.png");
            
        }

        public static void InitialiseImageSource(in Image image, in string uri)
        {
            if (null != image && null != uri)
            {
                ImageSource iS =
                    new BitmapImage( new Uri(uri) );
                image.Source = iS;
            }
        }
    }
}
