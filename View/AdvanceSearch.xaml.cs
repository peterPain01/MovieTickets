using NetFlix.ViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
    public partial class AdvanceSearch : UserControl
    {
        public AdvanceSearch()
        {
            InitializeComponent();
        }

        private void txtMovieName_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.Enter)
            {
                ((NetFlix.ViewModel.AdvanceSearch)DataContext).SearchingCommand.Execute(txtMovieName.Text); 
            }
        }
        private void GridViewColumnHeader_Click(object sender, RoutedEventArgs e)
        {
            GridViewColumnHeader headerClicked = e.OriginalSource as GridViewColumnHeader;
            if (headerClicked != null)
            {
                string sortBy = headerClicked.Content.ToString();
                ICollectionView dataView = CollectionViewSource.GetDefaultView(lvResults.ItemsSource);

                if (dataView.SortDescriptions.Count > 0 && dataView.SortDescriptions[0].PropertyName == sortBy)
                {
                    if (dataView.SortDescriptions[0].Direction == ListSortDirection.Ascending)
                    {
                        // Set sorting direction to Descending
                        dataView.SortDescriptions.Clear();
                        dataView.SortDescriptions.Add(new SortDescription(sortBy, ListSortDirection.Descending));
                    }
                    else
                    {
                        dataView.SortDescriptions.Clear();
                    }
                }
                else
                {
                    dataView.SortDescriptions.Clear();
                    dataView.SortDescriptions.Add(new SortDescription(sortBy, ListSortDirection.Ascending));
                }
            }
        }

    }
}
