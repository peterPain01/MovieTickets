using Microsoft.Win32;
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
    public partial class VoucherForm : Window
    {
        //  Register Dependency
        public static readonly DependencyProperty FormHeadingProperty =
            DependencyProperty.Register("FormHeadingVoucher", typeof(String), typeof(VoucherForm));

        public String FormHeadingVoucher
        {
            get { return (String)GetValue(FormHeadingProperty); }
            set { SetValue(FormHeadingProperty, value); }
        }

        public static readonly DependencyProperty CommandProperty =
          DependencyProperty.Register("CommandVoucher", typeof(ICommand), typeof(VoucherForm));

        public ICommand CommandVoucher
        {
            get { return (ICommand)GetValue(CommandProperty); }
            set { SetValue(CommandProperty, value); }
        }

        public VoucherForm()
        {
            InitializeComponent();
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
    }
}
