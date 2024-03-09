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

namespace NetFlix.View
{
    public partial class Signup : Window
    {
        public Signup()
        {
            InitializeComponent();
            txtUser.Focus(); 
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DragMove();
            }
        }

        private void btnMinimize_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

     

        private void btnSignup_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BacktoLoginButton(object sender, RoutedEventArgs e)
        {
            var loginView = new LoginView();
            this.Close(); 
            loginView.Show(); 
        }
    }
}
