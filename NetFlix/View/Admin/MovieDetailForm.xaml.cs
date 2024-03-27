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
using System.Windows.Shapes;

namespace NetFlix.View.Admin
{
    public partial class MovieDetailForm : Window
    {
        public MovieDetailForm()
        {
            InitializeComponent();
        }

        //  Register Dependency
        public static readonly DependencyProperty FormHeadingProperty =
            DependencyProperty.Register("FormHeadingMovieDetail", typeof(String), typeof(MovieDetailForm));

        public String FormHeadingMovieDetail
        {
            get { return (String)GetValue(FormHeadingProperty); }
            set { SetValue(FormHeadingProperty, value); }
        }


        public static readonly DependencyProperty CommandProperty =
          DependencyProperty.Register("CommandMovieDetail", typeof(ICommand), typeof(MovieDetailForm));

        public ICommand CommandMovieDetail
        {
            get { return (ICommand)GetValue(CommandProperty); }
            set { SetValue(CommandProperty, value); }
        }

        private void Grid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                this.DragMove();
            }
        }

        private void Grid_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ClickCount == 2)
            {
                this.WindowState = WindowState.Normal;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void AdminForm_Unloaded(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
