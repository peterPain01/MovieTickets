using System;
using System.Collections.Generic;
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
using XAct;

namespace NetFlix.View
{
    public partial class AdminDashBoard : UserControl
    {
        public AdminDashBoard()
        {
            InitializeComponent();
            lastButton = menuBtnDashboard; 
            lastButton.Style = (Style)FindResource("menuButtonActive");
        }

        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                //this.DragMove();
            }
        }   

        Button lastButton; 
        private void menuBtnMovie_Click(object sender, RoutedEventArgs e)
        {
            var button = (Button)sender;
            if(lastButton != null && lastButton != button)
            {
                lastButton.Style = (Style)FindResource("menuButton"); 
                button.Style = (Style)FindResource("menuButtonActive");
                lastButton = button; 
            }
        }
    }
}
