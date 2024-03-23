using NetFlix.CustomControls;
using NetFlix.ViewModel;
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

namespace NetFlix.View
{
    /// <summary>
    /// Interaction logic for VoucherForm.xaml
    /// </summary>
    public partial class VoucherForm : Window
    {
        public VoucherForm()
        {
            InitializeComponent();
        }

        public void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove(); 
        }

       
    }
}
