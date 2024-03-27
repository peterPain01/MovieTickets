using Netflix.Utils;
using Netflix.ViewModel;
using NetFlix.View;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace NetFlix.CustomControls
{
    public partial class Header : UserControl
    {

        public Header()
        {
            InitializeComponent();
            var dataContext = new HeaderViewModel();
            this.DataContext = dataContext;
        }

        private void txtMovieName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                string searchText = txtMovieName.Text;
                ((HeaderViewModel)DataContext).SearchCommand.Execute(searchText);
            }
        }

        private void TextBlock_MouseDown(object sender, MouseButtonEventArgs e)
        {
            ((HeaderViewModel)DataContext).NavigateToLanding.Execute(null);
        }

        private void Image_MouseDown(object sender, MouseButtonEventArgs e)
        {
            ((HeaderViewModel)DataContext).NavigateToLanding.Execute(null);
        }

        private void Account_MouseDown(object sender, MouseButtonEventArgs e)
        {
            ((HeaderViewModel)DataContext).NavigateToUserProfile.Execute(null);
        }

        private void NowShowingClick(object sender, MouseButtonEventArgs e)
        {
            ((HeaderViewModel)DataContext).NavigateToNowShowing.Execute(null);
        }

        private void AdvanceSearch_MouseDown(object sender, MouseButtonEventArgs e)
        {
            ((HeaderViewModel)DataContext).NavigateToAdvanceSearch.Execute(null);
        }

        private void Logout_MouseDown(object sender, MouseButtonEventArgs e)
        {
            ((HeaderViewModel)DataContext).NavigateToLogin.Execute(null);
        }
    }
}
