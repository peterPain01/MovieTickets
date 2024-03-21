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

namespace NetFlix.View.Admin
{
    public partial class AdminDashBoard : UserControl
    {
        public AdminDashBoard()
        {
            InitializeComponent();
        }

        private Button lastClickedButton;
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button clickedButton)
            {
                if (lastClickedButton != null)
                {
                    lastClickedButton.Background = new SolidColorBrush(Colors.Transparent);
                    lastClickedButton.Foreground = new SolidColorBrush(Colors.Black);
                }
                clickedButton.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#5B60C4"));
                clickedButton.Foreground = new SolidColorBrush(Colors.White);

                lastClickedButton = clickedButton;
            }
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            lastClickedButton = (Button)SpnTopMenu?.Children[0];
        }

       
    }
}
